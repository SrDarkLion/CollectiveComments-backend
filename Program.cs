using FluentValidation;
using CollectiveComments.DTO;
using CollectiveComments.Models;
using CollectiveComments;
using CollectiveComments.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

builder.Services.AddValidatorsFromAssemblyContaining<CompanyDTOValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/companies", async (
    AppDbContext dbCotext,
    [FromBody] CreateCompanyDTO companyDTO,
    IValidator<CreateCompanyDTO> validator
    ) =>
{
    var validationResult = await validator.ValidateAsync(companyDTO);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
    }

    if (await dbCotext.Companies.AnyAsync(c => c.Name == companyDTO.Name))
    {
        return Results.Conflict($"There is already a company with the name {companyDTO.Name}.");
    }

    var newCompany = new Company
    {
        Id = Guid.NewGuid(),
        Name = companyDTO.Name,
        Password = BCrypt.Net.BCrypt.HashPassword(companyDTO.Password),
        CreatedAt = DateTime.UtcNow
    };

    newCompany.GenerateCode();

    dbCotext.Companies.Add(newCompany);

    await dbCotext.SaveChangesAsync();

    return Results.Created($"/companies/{newCompany.Code}", newCompany);
});

app.MapPost("/feedbacks/{code}", async (AppDbContext dbContext, string code, CreateFeedbackDTO feedbackDTO) =>
{

    var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Code == code);
    if (company == null)
    {
        return Results.NotFound("Company Not Found.");
    }

    var newFeedback = new Feedback
    {
        Id = Guid.NewGuid(),
        CompanyCode = code,
        Message = feedbackDTO.Message,
        CreatedAt = DateTime.UtcNow
    };

    dbContext.Feedbacks.Add(newFeedback);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/feedback/{code}{feedbackDTO}", feedbackDTO);
});

app.MapGet("/feedbacks/{code}", async (AppDbContext dbContext, string code) =>
{

    var feedback = await dbContext.Feedbacks.FirstOrDefaultAsync(c => c.CompanyCode == code);
    if (feedback == null)
    {
        return Results.NotFound("Company Not Found.");
    }

    var feedbacks = await dbContext.Feedbacks
        .Where(f => f.CompanyCode == feedback.CompanyCode)
        .OrderByDescending(f => f.CreatedAt)
        .ToListAsync();

    return Results.Ok(feedbacks);
});


app.Run();

//http://localhost:5238/swagger/index.html
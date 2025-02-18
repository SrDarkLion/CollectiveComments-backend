using FluentValidation;
using FluentValidation.Results;
using CollectiveComments.DTO;
using CollectiveComments.Models;
using CollectiveComments;
using CollectiveComments.Validators;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

builder.Services.AddValidatorsFromAssemblyContaining<CompanyDTOValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/companies", async (AppDbContext dbCotext, CreateCompanyDTO companyDTO, IValidator<CreateCompanyDTO> validator) =>
{
    // Validação do DTO usando FluentValidation
    ValidationResult validationResult = await validator.ValidateAsync(companyDTO);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
    }

    if (await dbCotext.companies.AnyAsync(c => c.Name == companyDTO.Name))
    {
        return Results.Conflict($"Já existe uma empresa com o nome '{companyDTO.Name}'.");
    }

    var newCompany = new Company
    {
        Id = Guid.NewGuid(),
        Name = companyDTO.Name,
        Password = BCrypt.Net.BCrypt.HashPassword(companyDTO.Password),
        CreatedAt = DateTime.UtcNow
    };

    newCompany.GenerateCode();

    dbCotext.companies.Add(newCompany);

    await dbCotext.SaveChangesAsync();

    return Results.Created($"/companies/{newCompany.Code}", newCompany);
});

app.MapPost("/companies/{code}/feedbacks", async (AppDbContext dbContext, string code, Feedback feedback) =>
{

    var company = await dbContext.companies.FirstOrDefaultAsync(c => c.Code == code);
    if (company == null)
    {
        return Results.NotFound("Empresa não encontrada.");
    }

    feedback.CompanyId = company.Id;

    dbContext.feedbacks.Add(feedback);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/companies/{code}/feedbacks/{feedback.Company.Code}", feedback);
});

// Rota para listar os feedbacks de uma empresa
app.MapGet("/companies/{code}/feedbacks", async (AppDbContext dbContext, string code) =>
{

    var company = await dbContext.companies.FirstOrDefaultAsync(c => c.Code == code);
    if (company == null)
    {
        return Results.NotFound("Empresa não encontrada.");
    }

    var feedbacks = await dbContext.feedbacks
        .Where(f => f.CompanyId == company.Id)
        .OrderByDescending(f => f.CreatedAt)
        .ToListAsync();

    return Results.Ok(feedbacks);
});


app.Run();

//http://localhost:5238/swagger/index.html

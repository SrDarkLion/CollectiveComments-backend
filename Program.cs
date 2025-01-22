using CollectiveComments;
using CollectiveComments.Migrations;
using CollectiveComments.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbCotext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/companies", async (AppDbCotext dbCotext, Company newCompany) => {
    if (string.IsNullOrWhiteSpace(newCompany.Name) ||
        string.IsNullOrWhiteSpace(newCompany.Password))
    {
        return Results.BadRequest("Nome e senha são obrigatórios.");
    }
    newCompany.Id = Guid.NewGuid(); // Gera o ID antes
    var idPrefix = newCompany.Id.ToString().Split('-')[0]; // Pega os primeiros 8 caracteres
    newCompany.Code = $"{newCompany.Name}-{idPrefix}".ToLower().Replace(" ", "-");

    newCompany.Password = BCrypt.Net.BCrypt.HashPassword(newCompany.Password);

    var exitingCompany = await dbCotext.companies.FirstOrDefaultAsync(c => c.Code == newCompany.Code);

    if (exitingCompany != null) {
        return Results.Conflict($"Já existe uma empresa com o código '{newCompany.Code}'.");
    }

    dbCotext.companies.Add(newCompany);
    await dbCotext.SaveChangesAsync();

    return Results.Created($"/companies/{newCompany.Id}", newCompany);
});

// Rota para enviar um feedback
app.MapPost("/companies/{companyId}/feedbacks", async (AppDbCotext dbContext, Guid companyId, Feedback feedback) => {
    // Verifica se a empresa existe
    var company = await dbContext.companies.FirstOrDefaultAsync(c => c.Id == companyId);
    if (company == null)
    {
        return Results.NotFound("Empresa não encontrada.");
    }

    // Configura o feedback com o ID da empresa
    feedback.CompanyId = companyId;

    dbContext.feedbacks.Add(feedback);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/companies/{companyId}/feedbacks/{feedback.Id}", feedback);
});

// Rota para listar os feedbacks de uma empresa
app.MapGet("/companies/{companyId}/feedbacks", async (AppDbCotext dbContext, Guid companyId) => {
    // Verifica se a empresa existe
    var company = await dbContext.companies.FirstOrDefaultAsync(c => c.Id == companyId);
    if (company == null)
    {
        return Results.NotFound("Empresa não encontrada.");
    }

    // Retorna os feedbacks associados à empresa
    var feedbacks = await dbContext.feedbacks
        .Where(f => f.CompanyId == companyId)
        .OrderByDescending(f => f.CreatedAt)
        .ToListAsync();

    return Results.Ok(feedbacks);
});


app.Run();

//http://localhost:5238/swagger/index.html

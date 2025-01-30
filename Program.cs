using CollectiveComments;
using CollectiveComments.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/companies", async (AppDbContext dbCotext, Company newCompany) => {
    if (string.IsNullOrWhiteSpace(newCompany.Name) ||
        string.IsNullOrWhiteSpace(newCompany.Password))
    {
        return Results.BadRequest("Nome e senha são obrigatórios.");
    }

    newCompany.Id = Guid.NewGuid(); // Gera o ID antes
 
    newCompany.GenerateCode();

    newCompany.Password = BCrypt.Net.BCrypt.HashPassword(newCompany.Password);

    dbCotext.companies.Add(newCompany);

    await dbCotext.SaveChangesAsync();

    return Results.Created($"/companies/{newCompany.Id}", newCompany);
});

app.MapPost("/companies/{code}/feedbacks", async (AppDbContext dbContext, string code, Feedback feedback) =>
{
    // Verifica se a empresa existe
    var company = await dbContext.companies.FirstOrDefaultAsync(c => c.Code == code);
    if (company == null)
    {
        return Results.NotFound("Empresa não encontrada.");
    }

    // Configura o feedback com o ID da empresa
    feedback.CompanyId = company.Id; // Corrigido para associar ao ID correto da empresa

    dbContext.feedbacks.Add(feedback);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/companies/{code}/feedbacks/{feedback.Company.Code}", feedback);
});

// Rota para listar os feedbacks de uma empresa
app.MapGet("/companies/{code}/feedbacks", async (AppDbContext dbContext, string code) =>
{
    // Verifica se a empresa existe
    var company = await dbContext.companies.FirstOrDefaultAsync(c => c.Code == code);
    if (company == null)
    {
        return Results.NotFound("Empresa não encontrada.");
    }

    // Retorna os feedbacks associados à empresa
    var feedbacks = await dbContext.feedbacks
        .Where(f => f.CompanyId == company.Id) // Corrigido para acessar o ID correto da empresa
        .OrderByDescending(f => f.CreatedAt)
        .ToListAsync();

    return Results.Ok(feedbacks);
});


app.Run();

//http://localhost:5238/swagger/index.html

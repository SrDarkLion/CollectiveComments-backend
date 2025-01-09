using CollectiveComments;
using CollectiveComments.Migrations;
using CollectiveComments.Models;
using Microsoft.EntityFrameworkCore;

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
        string.IsNullOrWhiteSpace(newCompany.Password) ||
        string.IsNullOrWhiteSpace(newCompany.Code))
    {
        return Results.BadRequest("Nome, senha e código são obrigatórios.");
    }

    var exitingCompany = await dbCotext.companies.FirstOrDefaultAsync(c => c.Code == newCompany.Code);

    if (exitingCompany != null) {
        return Results.Conflict($"Já existe uma empresa com o código '{newCompany.Code}'.");
    }

    dbCotext.companies.Add(newCompany);
    await dbCotext.SaveChangesAsync();

    return Results.Created($"/companies/{newCompany.Id}", newCompany);
});

app.Run();

//http://localhost:5238/swagger/index.html
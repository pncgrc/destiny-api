using DestinyAPI.Models;
using DestinyAPI.Repositories;
using DestinyAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies
var useInMemory = false;

if (useInMemory)
{
    builder.Services.AddSingleton<IFactionRepository, InMemoryFactionRepository>();
    builder.Services.AddSingleton<IWeaponRepository, InMemoryWeaponRepository>();
    builder.Services.AddSingleton<ICharacterRepository, InMemoryCharacterRepository>();
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<DestinyDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

    builder.Services.AddScoped<IFactionRepository, EfFactionRepository>();
    builder.Services.AddScoped<ICharacterRepository, EfCharacterRepository>();
    builder.Services.AddScoped<IWeaponRepository, EfWeaponRepository>();
}

var app = builder.Build();

// Enable middleware to serve generated Swagger as JSON endpoint
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
// Specify the Swagger JSON endpoint explicitly (optional but recommended)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Destiny API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at app root (http://localhost:port/)
});

/* CHARACTERS */

app.MapGet("/api/characters", async (ICharacterRepository repository) =>
{
    var characters = await repository.GetAllAsync();
    return Results.Ok(characters);
});

app.MapGet("/api/characters/{id:int}", async (int id, ICharacterRepository repository) =>
{
    var character = await repository.GetByIdAsync(id);
    return character is not null ? Results.Ok(character) : Results.NotFound();
});

app.MapPost("/api/characters", async ([FromBody] Character newCharacter, ICharacterRepository repository) =>
{
    var created = await repository.AddAsync(newCharacter);
    return Results.Created($"/api/characters/{created.Id}", created);
});

app.MapPut("/api/characters/{id:int}", async (int id, [FromBody] Character updatedCharacter, ICharacterRepository repository) =>
{
    if (id != updatedCharacter.Id) return Results.BadRequest();
    await repository.UpdateAsync(updatedCharacter);
    return Results.NoContent();
});

app.MapDelete("/api/characters/{id:int}", async (int id, ICharacterRepository repository) =>
{
    await repository.DeleteAsync(id);
    return Results.NoContent();
});

/* FACTIONS */

app.MapGet("/api/factions", async (IFactionRepository repository) =>
{
    var factions = await repository.GetAllAsync();
    return Results.Ok(factions);
});

app.MapGet("/api/factions/{id:int}", async (int id, IFactionRepository repository) =>
{
    var faction = await repository.GetByIdAsync(id);
    return faction is not null ? Results.Ok(faction) : Results.NotFound();
});

app.MapPost("/api/factions", async ([FromBody] Faction newFaction, IFactionRepository repository) =>
{
    var created = await repository.AddAsync(newFaction);
    return Results.Created($"/api/factions/{created.Id}", created);
});

app.MapPut("/api/factions/{id:int}", async (int id, [FromBody] Faction updatedFaction, IFactionRepository repository) =>
{
    if (id != updatedFaction.Id) return Results.BadRequest("ID mismatch.");
    await repository.UpdateAsync(updatedFaction);
    return Results.NoContent();
});

app.MapDelete("/api/factions/{id:int}", async (int id, IFactionRepository repository) =>
{
    await repository.DeleteAsync(id);
    return Results.NoContent();
});

/* WEAPONS */

app.MapGet("/api/weapons", async (IWeaponRepository repository) =>
{
    var weapons = await repository.GetAllAsync();
    return Results.Ok(weapons);
});

app.MapGet("/api/weapons/{id:int}", async (int id, IWeaponRepository repository) =>
{
    var weapon = await repository.GetByIdAsync(id);
    return weapon is not null ? Results.Ok(weapon) : Results.NotFound();
});

app.MapPost("/api/weapons", async ([FromBody] Weapon newWeapon, IWeaponRepository repository) =>
{
    var created = await repository.AddAsync(newWeapon);
    return Results.Created($"/api/weapons/{created.Id}", created);
});

app.MapPut("/api/weapons/{id:int}", async (int id, [FromBody] Weapon weapon, IWeaponRepository repository) =>
{
    if (id != weapon.Id) return Results.BadRequest("ID in URL and body must match.");
    var success = await repository.UpdateAsync(weapon);
    return success ? Results.NoContent() : Results.NotFound();
});

app.MapDelete("/api/weapons/{id:int}", async (int id, IWeaponRepository repository) =>
{
    var success = await repository.DeleteAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
});

app.Run();

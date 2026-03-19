using AstroBookings.Rockets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IRepositorioCohete, RepositorioCoheteEnMemoria>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    currentTime = DateTimeOffset.Now
}));

app.MapearEndpointsCohete();

app.Run();

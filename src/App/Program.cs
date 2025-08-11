using Presentation.Registrations;
using Application.Registrations;
using Infrastructure.Registrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterApplicationLayer()
    .RegisterInfrastructureLayer(builder.Configuration)
    .RegisterPresentationLayer();

var app = builder.Build();

app.UseApplicationLayer();
app.UsePresentationLayer();

app.Run();
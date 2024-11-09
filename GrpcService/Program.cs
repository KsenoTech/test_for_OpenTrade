using GrpcService.Services;
using TranslationServiceCore;
using TranslationServiceLibrary;


var builder = WebApplication.CreateBuilder(args);


// Регистрация зависимостей
builder.Services.AddSingleton<TranslationCache>();
builder.Services.AddSingleton<ITranslationService, TranslationService>();
builder.Services.AddSingleton<ITranslationService, GrpcTranslationClient>(provider =>
{
    var grpcAddress = "https://localhost:5222"; // Замените на ваш адрес gRPC-сервера
    return new GrpcTranslationClient(grpcAddress);
});

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<TranslationServiceGrpc>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

using TranslationAPI.Models;
using TranslationAPI.Services;
using Microsoft.Extensions.Options;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load the env variables from .env.local
Env.Load("../.env.local");

// Google Cloud Creds
// var googleCredentialsPath = builder.Configuration["GoogleCloud:CredentialPath"];
// Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", googleCredentialsPath);

var googleCredentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
Console.WriteLine("Google Credentials Path: " + googleCredentialsPath); 
if (!string.IsNullOrEmpty(googleCredentialsPath))
{
    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", googleCredentialsPath);
}
else
{
    Console.WriteLine("Error: GOOGLE_APPLICATION_CREDENTIALS not set properly.");
}


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// API Key 
builder.Services.Configure<OpenAiOptions>(builder.Configuration.GetSection("OpenAI"));

// register OpenAIService
builder.Services.AddSingleton<OpenAIService>();
// register TextToSpeechService
builder.Services.AddSingleton<TextToSpeechService>();


// Auth
builder.Services.AddAuthentication(); 
builder.Services.AddAuthorization();  

var app = builder.Build();

app.UseRouting();         
app.UseAuthorization();  

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

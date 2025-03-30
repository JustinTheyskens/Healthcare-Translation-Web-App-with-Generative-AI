using TranslationAPI.Models;
using TranslationAPI.Services;
using Microsoft.Extensions.Options;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load the env variables from .env.local
Env.Load("../.env.local");

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

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// CORS
app.UseCors("AllowAll"); 

app.UseRouting();         
app.UseAuthorization();  

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

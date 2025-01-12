using Microsoft.AspNetCore.Http.Features;
using vopperAcademyBackEnd.Data;
using vopperAcademyBackEnd.Repository.Chapters;
using vopperAcademyBackEnd.Repository.Courses;
using vopperAcademyBackEnd.Repository.Platforms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MongoConnection")
);

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<IPlatformsRepository, PlatformsRepository>();
builder.Services.AddScoped<IChaptersRepository, ChaptersRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin() // Permitir solicitudes desde cualquier origen
            .AllowAnyMethod() // Permitir cualquier método (GET, POST, etc.)
            .AllowAnyHeader()); // Permitir cualquier encabezado;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();


app.Run();
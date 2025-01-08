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

builder.Services.Configure<FormOptions>(options =>
{
    // Establece el límite para el tamaño de los archivos
    options.MultipartBodyLengthLimit = 10L * 1024L * 1024L * 1024L; // 10 GB, ajusta según sea necesario
});

builder.WebHost.ConfigureKestrel(options =>
{
    // Establece el límite para el tamaño del cuerpo de la solicitud
    options.Limits.MaxRequestBodySize = 10L * 1024L * 1024L * 1024L; // 10 GB
});

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
});

app.Run();
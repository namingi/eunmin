using backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(
"http://0.0.0.0:8080"
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
options =>
options.UseNpgsql(
builder.Configuration.GetConnectionString(
"Default"
))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "react",
        policy =>
        policy
            .WithOrigins(
                "https://eunmin.vercel.app"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("react");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();
}

app.Run();


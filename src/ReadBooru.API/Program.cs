using ReadBooru.API;
using ReadBooru.API.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//adding authentication field to swagger
builder.Services.AddSwaggerGen( opt => 
{
    opt.SwaggerDoc("v1", new OpenApiInfo{Title="Readbooru", Version="v1"});
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        In=ParameterLocation.Header,
        Description="Please enter a token",
        Name = "Auth",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme 
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IImageRepo, ImageRepo>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddControllers();


//db connection
var ConnectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<AppDBContext>(options => 
    options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString) )
);

//json web token(JWT) configuration
var JwtOptionsSection = builder.Configuration.GetRequiredSection("Jwt");
builder.Services.Configure<JwtOptions>(JwtOptionsSection);

//adding authentication 
builder.Services.AddAuthentication(options=>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>{
    var key = Encoding.UTF8.GetBytes(  JwtOptionsSection["Key"] );

    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = JwtOptionsSection["Issuer"],
        ValidAudience = JwtOptionsSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseRouting();

//map controllers
app.MapControllerRoute(
    name: "default",
    pattern:"{controller=post}"
);
app.MapControllerRoute(
    name: "image",
    pattern:"{controller=image}"
);
app.MapControllerRoute(
    name: "account",
    pattern:"{controller=account}"
);
// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

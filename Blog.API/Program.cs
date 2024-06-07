using Blog.Common.Models.JwtOptions;
using Blog.Data.Context;
using Blog.Data.Repositories;
using Blog.Services.Api;
using Blog.Services.Api.Extensions;
using Blog.Services.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer" , new OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }  
    });
});

builder.Services.AddServices(builder);

void ConfigureServices(IServiceCollection services)
{
   services.AddAuthorization(options =>
    {
        options.AddPolicy("OnlyAdmin", policy =>
        {
            policy.RequireRole("Admin");
        });
    });
    services.AddMvcCore();
}
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

void  Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseAuthentication(); // Authentication middleware
    app.UseAuthorization(); // Authorization middleware
    app.UseMvc();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
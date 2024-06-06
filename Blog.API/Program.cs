using Blog.Common.Models.JwtOptions;
using Blog.Data.Context;
using Blog.Data.Repositories;
using Blog.Services.Api;
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

builder.Services.AddDbContext<BlogDbContext>();

/*options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(name:"BlogDbContext"));
});*/

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserHelper>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwt = builder.Configuration.GetSection(nameof(JwtOption)).Get<JwtOption>();
    var signinKey = System.Text.Encoding.UTF32.GetBytes(jwt!.signinKey);
    
 options.TokenValidationParameters = new TokenValidationParameters()
 {
     ValidIssuer = jwt!.Issuer,
     ValidAudience = jwt.Audience,
     ValidateIssuer = true,
     ValidateAudience = true,
     IssuerSigningKey = new SymmetricSecurityKey(signinKey),
     ValidateIssuerSigningKey =  true,
     ValidateLifetime = true,
     ClockSkew = TimeSpan.Zero,
     
 };
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyCours");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
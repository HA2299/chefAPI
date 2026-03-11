using CodeFirst.Models;
using Repository.Entities;
using Repository.interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Service.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiProjectChef
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.  
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "securityLessonWebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter your bearer token",
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
                        new string[] {}
                    }
                });
            });

            //שימוש בטוקן כדי לאמת
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(option =>
                  option.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = builder.Configuration["Jwt:Issuer"],
                      ValidAudience = builder.Configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                  });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5173")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });


            // Dependency Injection
            builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
            builder.Services.AddScoped<IService<Category>, CategoryService>();
            builder.Services.AddScoped<IRepository<Chef>, ChefRepository>();
            builder.Services.AddScoped<IService<ChefDto>, ChefService>();
            builder.Services.AddScoped<IChef, ChefService>();
            builder.Services.AddScoped<IRepository<Favorite>, FavoriteRepository>();
            builder.Services.AddScoped<IService<Favorite>, FavoriteService>();
            builder.Services.AddScoped<IRepository<Ingredient>, IngredientRepository>();
            builder.Services.AddScoped<IService<Ingredient>, IngredientService>();
            builder.Services.AddScoped<IRepository<Recipe>, RecipeRepository>();
            builder.Services.AddScoped<IService<RecipeDto>, RecipeService>();
            builder.Services.AddScoped<IRecipe, RecipeService>();
            builder.Services.AddScoped<IRepository<RecipeIngredient>, RecipeIngredientRepository>();
            builder.Services.AddScoped<IService<RecipeIngredient>, RecipeIngredientService>();
            builder.Services.AddScoped<IRecipeIngredient, RecipeIngredientService>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IService<User>, UserService>();
            builder.Services.AddScoped<IUser, UserRepository>();
            builder.Services.AddScoped<ILogin, UserLoginService>();
            builder.Services.AddScoped<IContext, ChefDB>();

            // הוסף את AutoMapper
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<MyRecipesMapper>();
                cfg.AddProfile<MyChefsMapper>();
            });

            // הוסף את HttpClient
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.  
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Enable CORS
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

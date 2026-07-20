using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using OrderManagement.Api.Middlewares;
using OrderManagement.Application;
using OrderManagement.Infrastructure;
using OrderManagement.Infrastructure.Persistence;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();


try
{
    Log.Information("Iniciando aplicação");


    var builder = WebApplication.CreateBuilder(args);


    builder.Host.UseSerilog();

    builder.Services.AddControllers();

    builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings =
            builder.Configuration
                .GetSection("Jwt");


        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,


                ValidIssuer =
                    jwtSettings["Issuer"],


                ValidAudience =
                    jwtSettings["Audience"],


                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            jwtSettings["Key"]!))
            };
    });


    builder.Services.AddAuthorization();


    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition(
     "Bearer",
     new OpenApiSecurityScheme
     {
         Name = "Authorization",

         Type = SecuritySchemeType.Http,

         Scheme = "bearer",

         BearerFormat = "JWT",

         In = ParameterLocation.Header,

         Description =
             "Informe o token JWT no formato: token"
     });


        options.AddSecurityRequirement(document =>
        {
            return new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference(
                "Bearer",
                document),
            []
        }
    };
        });

        options.UseInlineDefinitionsForEnums();

        options.SupportNonNullableReferenceTypes();

        options.CustomSchemaIds(type => type.FullName);

        var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";

        var xmlPath = Path.Combine(
            AppContext.BaseDirectory,
            xmlFile);

        options.IncludeXmlComments(xmlPath, true);
    });

    builder.Services.AddApplication();

    builder.Services.AddInfrastructure(
        builder.Configuration);



    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.MigrateAsync();

            Log.Information("Migrations aplicadas com sucesso.");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Erro ao aplicar as migrations.");

            throw;
        }
    }

        app.UseSwagger();

        app.UseSwaggerUI();


    app.UseSerilogRequestLogging();

    app.UseMiddleware<ExceptionMiddleware>();


    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.UseAuthentication();

    app.UseAuthorization();


    app.MapControllers();



    app.Run();

}
catch (Exception ex)
{

    Log.Fatal(
        ex,
        "Aplicação finalizada inesperadamente");

}
finally
{

    Log.CloseAndFlush();

}
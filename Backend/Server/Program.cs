using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BackendNetwork.Data;
using BackendNetwork.Models;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendNetwork.Services;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("NextJs", policy =>
    {
        policy
            .WithOrigins("http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});
builder.Services.AddScoped<AuthService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HealthCare API",
        Description = "An ASP.NET Core Web API for Users to log in and book and manage their appointments!"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
});

options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
    }
});
});

var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(jwtSettings.SecretKey)
        ),
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();

// Seed Role on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();

    
   
    
    if (!db.Roles.Any(r => r.Name == "Patient"))
    {
        db.Roles.Add(new Role { Name = "Patient" });
        
    }
    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new Category { Name = "Check up"},
            new Category { Name = "Follow up"},
            new Category { Name = "Consultation"}
        );
    }
    db.SaveChanges();
    
    var checkup = db.Categories.First(s => s.Name == "Check up");
    var Followup = db.Categories.First(s => s.Name == "Follow up");
    var Consultation = db.Categories.First(s => s.Name == "Consultation");

    if (!db.Specialities.Any())
    {
        db.Specialities.AddRange(
            new Speciality { Name = "Cardiologist"},
            new Speciality { Name = "Dematologist"},
            new Speciality { Name = "Neurology"}
        );
    }
    db.SaveChanges();

    var neurology = db.Specialities.First(s => s.Name == "Neurology");
    var dematologist = db.Specialities.First(s => s.Name == "Dematologist");
    var cardiologist = db.Specialities.First(s => s.Name == "Cardiologist");


    if(!db.Clinics.Any())
    {
        db.Clinics.AddRange(
            new Clinic { Name = "Mercy Clinic"},
            new Clinic { Name = "Hope Clinic"},
            new Clinic { Name = "Recovery Clinic"}
        );
        db.SaveChanges();
    }
    var Hopeclinic = db.Clinics.First(c => c.Name == "Hope Clinic");
    var Mercyclinic = db.Clinics.First(c => c.Name == "Mercy Clinic");
    var Recoveryclinic = db.Clinics.First(c => c.Name == "Recovery Clinic");

    if (!db.Doctors.Any())
    {
        db.Doctors.AddRange(
            new Doctor
            {
                Firstname = "john",
                Lastname = "sparten",
                SpecialityId = neurology.Id,
                ClinicId = Mercyclinic.Id
            },
            new Doctor
            {
                Firstname = "Freddie",
                Lastname = "Mclovin",
                SpecialityId = dematologist.Id,
                ClinicId = Hopeclinic.Id
            },
            new Doctor
            {
                Firstname = "Henry",
                Lastname = "Oliver",
                SpecialityId = neurology.Id,
                ClinicId = Hopeclinic.Id
            }
        );
        db.SaveChanges();
    }


    
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "doc";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthCare API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("NextJs");

app.MapControllers();

app.Run();

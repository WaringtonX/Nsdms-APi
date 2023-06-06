using AutoMapper;
using mersetaWebAPI.CustomModels.UserModels;
using mersetaWebAPI.Data;
using mersetaWebAPI.Extentions;
using mersetaWebAPI.Implementation;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
//private IConfiguration Configuration = new IConfiguration();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Inject DBContext 
var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<mersetaContext>(options => options.EnableSensitiveDataLogging(true).UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring), mySqlOptionsAction: sqloptions =>
{
    sqloptions.EnableRetryOnFailure(maxRetryCount: 10,
           maxRetryDelay: TimeSpan.FromSeconds(30),
           errorNumbersToAdd: null);
}));    

// configure DI for application services
builder.Services.AddScoped<ITowns, TownService>();
builder.Services.AddScoped<ICompany, CompanyService>();
builder.Services.AddScoped<ISaqua, SaqaQualificationService>();
builder.Services.AddScoped<ICompanyLeaners, CompanyLearnerService>();
builder.Services.AddScoped<ISingleUser, SingleUserService>();
builder.Services.AddScoped<ICompLearner, CompLearnerService>();
builder.Services.AddScoped<IIntervention, InterventionService>();
builder.Services.AddScoped<IFunding, FundingService>();
builder.Services.AddScoped<ITrainingProvider, TrainingProviderService>();
builder.Services.AddScoped<IUsers, UserService>();
builder.Services.AddScoped<ILookups, LookupService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IJbpmqueries, JbpmqueriesService>();
builder.Services.AddScoped<IRejectionReason, RejectionReasonsList>();
builder.Services.AddScoped<ILearnerIndicator, LearnerIndicatorService>();
builder.Services.AddScoped<ILearnerApplicationLifecycle, LearnerApplicationLifecycleService>();
builder.Services.AddScoped<IAssessment, AssessmentService>();
builder.Services.AddScoped<ITasks, TaskService>();
builder.Services.AddScoped<ISite, SiteService>();
//Mapper.CreateMap<User, UserUpdate>();

#region Swagger Configuration
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 6.0 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
#endregion

#region Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
           
options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
    };
});
#endregion
/*builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });

}); */


/*var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200",
                                              "https://localhost:7253/api/User");
                      });
}); */

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger(//options =>
   // {
       // options.SerializeAsV2 = true;
   // }
   );
    app.UseSwaggerUI(//options =>
    //{
    //   // options.SwaggerEndpoint("/applicationName/swagger/v1/swagger.json", "v1");
    //   // options.RoutePrefix = "info/swagger";
    //}
    );
//}

//app.UseCors("default");
//app.UseCors(MyAllowSpecificOrigins);
app.UseCorsExtension();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

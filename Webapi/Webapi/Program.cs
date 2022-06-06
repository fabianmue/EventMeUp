using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Webapi.DatabaseContext;
using Webapi.Models.Identity;
using Webapi.Repositories.Events;
using Webapi.ServiceProviderExtensions;
using Webapi.Services.Events;

var allowCorsLocalhost = "allowCorsLocalhost";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<JsonOptions>(options =>
  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddDbContext<WebapiContext>(options =>
{
  string temp = builder.Configuration.GetValue<string>("ENVIRONMENT");
  string connectionString = builder.Configuration.GetValue<string>("WEBAPI_CONNECTIONSTRING");
  options.UseNpgsql(connectionString);
});
builder.Services
  .AddIdentity<WebapiUser, IdentityRole>(options =>
  {
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
  })
  .AddEntityFrameworkStores<WebapiContext>()
  .AddDefaultTokenProviders();
builder.Services
  .AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidAudience = builder.Configuration["JWT:ValidAudience"],
      ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["JWT_SECRET"]))
    };
  });
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configuration =>
{
  configuration.CustomOperationIds(apiDescription =>
    $"{apiDescription.ActionDescriptor.RouteValues["controller"]}_{((ControllerActionDescriptor)apiDescription.ActionDescriptor).ActionName}");
  configuration.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "EventMeUp Webapi",
    Version = "v1",
    Contact = new OpenApiContact
    {
      Name = "fabianmue"
    }
  });
  var securityScheme = new OpenApiSecurityScheme
  {
    Name = "Authorization",
    BearerFormat = "JWT",
    Scheme = "Bearer",
    Description = "Enter the token.",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Reference = new OpenApiReference
    {
      Type = ReferenceType.SecurityScheme,
      Id = "Bearer"
    }
  };
  configuration.AddSecurityDefinition("Bearer", securityScheme);
  configuration.AddSecurityRequirement(
    new OpenApiSecurityRequirement { { securityScheme, Array.Empty<string>() } });
  configuration.IncludeXmlComments(Path.Combine(
    AppContext.BaseDirectory,
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
  ));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: allowCorsLocalhost, builder =>
    builder
      .WithOrigins("http://localhost:4200")
      .AllowAnyMethod()
      .AllowAnyHeader());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  if (app.Environment.IsEnvironment("Local"))
  {
    scope.ServiceProvider.DeleteDatabase();
  }

  scope.ServiceProvider.MigrateDatabase();

  if (app.Environment.IsEnvironment("Local"))
  {
    scope.ServiceProvider.Seed();
  }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

if (app.Environment.IsEnvironment("Local"))
{
  app.UseCors(allowCorsLocalhost);
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Services; 
using Microsoft.EntityFrameworkCore;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using ApiPortal_DataLake.Application.Filters;
using ApiPortal_DataLake.Domain.Shared.Constants;
using Api_Dc.Domain.Contracts;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Get AppSettings
var configuration = builder.Configuration;
// IWebHostEnvironment environment = builder.Environment;
var allowedOrigins = configuration["AllowedOrigins"];

// Set DB Conexion
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(configuration["ConnectionString:DefaultConnection"]));

// Add services to the container.
builder.Services.AddTransient<IJwt, Jwt>();  
 
 
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IListasService, ListasService>(); 
builder.Services.AddScoped<ValidateTokenAttribute>();
 



//BD
builder.Services.AddTransient<IOrdenProduccion, OrdenProduccionService>();
builder.Services.AddTransient<IProyecto, ProyectoService>();
builder.Services.AddTransient<IAmbiente, AmbienteService>();
builder.Services.AddTransient<IProducto, ProductoService>();
builder.Services.AddTransient<IListasSisgecoService, ListasSisgecoService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IDetalleOrdenProduccionService, DetalleOrdenProduccionService>();
builder.Services.AddTransient<ILayoutGrupo, LayoutService>();
builder.Services.AddTransient<IOperacionesContruccion, OperacionesConstruccionService>();

builder.Services.AddTransient<IDetalleOpGrupo, DetalleOpGrupoService>();
builder.Services.AddTransient<IMantenimientoOp, MantenimientoOpService>();

builder.Services.AddTransient<IRol, RolService>();
builder.Services.AddTransient<IPerfilService, PerfilService>();
builder.Services.AddTransient<IComponentes, ComponentesService>();
builder.Services.AddTransient<IMonitoreo, MonitoreoService>();
builder.Services.AddTransient<ISupervision, SupervisionOpService>();


builder.Services.AddTransient<ILineaProd, LineaProdService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = HeaderRequestConstant.Authorization,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
        {
            builder
          .WithOrigins(allowedOrigins.Split(";"))
            .AllowAnyOrigin()
            .AllowAnyHeader()
            
            .AllowAnyMethod();
            
        });
});



builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

XmlConfigurator.Configure(new FileInfo("log4net.config"));

builder.Logging.AddLog4Net();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api DC Blinds"));
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.Run();

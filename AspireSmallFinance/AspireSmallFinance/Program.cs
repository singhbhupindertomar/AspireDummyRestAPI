using AspireSmallFinance.Models.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using AspireSmallFinance.Handler;
using AspireSmallFinance.Services;
using AspireSmallFinance.Authentication;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddDbContext<IApplicationDBContext,ApplicationDBContext>(contextOptions =>
{
    contextOptions.UseSqlServer(builder.Configuration.GetConnectionString("aspireDBConn"));
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(BasicAuthenticationDefaults.AuthenticationScheme,
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
        {
            Name = "Authorizaton",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = BasicAuthenticationDefaults.AuthenticationScheme,
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Basic Auth Header"
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = BasicAuthenticationDefaults.AuthenticationScheme
            }
        },
        new string[]{ "Basic "}
        }
    });
});



builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication",null);



//builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseAuthorization();
    app.UseSwaggerUI().UseAuthorization();
}
app.UseRouting();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<UserAuthorizationMiddleware>("LoginAuth");

app.MapControllers();

app.Run();

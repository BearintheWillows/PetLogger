using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetLoggerAPI.Data;
using PetLoggerAPI.Data.Handlers;
using PetLoggerAPI.Data.Models;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add ApplicationDbContext and SQL server support
builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) )
);

//Add Identity support
builder.Services.AddIdentity<AppUser, IdentityRole>( options => {
		options.SignIn.RequireConfirmedAccount = true;
		options.Password.RequireDigit = true;
		options.Password.RequireLowercase = true;
		options.Password.RequireUppercase = true;
		options.Password.RequireNonAlphanumeric = true;
		options.Password.RequiredLength = 6;
	}
).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication( opt => {
		opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	}
).AddJwtBearer( opt => {
		opt.TokenValidationParameters = new TokenValidationParameters {
			RequireExpirationTime = true,
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration[ "Jwt:Issuer" ],
			ValidAudience = builder.Configuration[ "Jwt:Audience" ],
			IssuerSigningKey =
				new SymmetricSecurityKey(
					System.Text.Encoding.UTF8.GetBytes( builder.Configuration[ "Jwt:SecurityKey" ] )
				)
		};
	}
);

builder.Services.AddScoped<JwtHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() ) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
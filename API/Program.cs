using EducacionContinua.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var proveedor = builder.Services.BuildServiceProvider();
var configuration = proveedor.GetRequiredService<IConfiguration>();

builder.Services.AddCors(opciones =>
{
    var frontendAdmin = configuration.GetValue<string>("frontendAdmin");
    var frontendWeb = configuration.GetValue<string>("frontendWeb");
    opciones.AddDefaultPolicy(builder => {
        builder.WithOrigins(frontendAdmin).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        builder.WithOrigins(frontendWeb).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

builder.Services.AddStartupSetup(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();


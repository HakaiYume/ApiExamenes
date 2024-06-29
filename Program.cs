using ApiExamne.Data;
using ApiExamne.Services;

var builder = WebApplication.CreateBuilder(args);

//Cors
var MyOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyOrigins, policy => {
        policy.WithOrigins("*");
        policy.AllowAnyHeader();
        policy.WithMethods("GET", "POST", "PUT", "DELETE");
        });
});

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DBcontext
builder.Services.AddSqlServer<ApiExamenesContext>(builder.Configuration.GetConnectionString("BDConection"));

//Services
builder.Services.AddScoped<IExamenService, ExamenService>();
builder.Services.AddScoped<IPreguntaService, PreguntaService>();
builder.Services.AddScoped<IRespuestaService, RespuestaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//cords
app.UseCors(MyOrigins);

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;//used for mysql usemysql function
using WebApicrud.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//ms sql server
//below 2 lines added manually for sqlserver connection string for ms sql server
//string st = "server=.;database=dbstudent;trusted_connection=true";
//builder.Services.AddDbContext<ApiDbContext>(o =>o.UseSqlServer(st));


//mysql    below 2 lines
string st = "server=localhost;port=3306;database=dbstudent;user=root;password=1234";
builder.Services.AddDbContext<ApiDbContext>(o => o.UseMySql(st, ServerVersion.AutoDetect(st)));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//enable cors policy
builder.Services.AddCors(p => p.AddPolicy("corspolicy1", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
})); 

//you can add "*" for any front end uri cors permission


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//add this next 1 line for cors
app.UseCors("corspolicy1");


app.UseHttpsRedirection();

app.UseAuthorization();

//2 lines  Serve static files (including index.html) from the wwwroot folder
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();

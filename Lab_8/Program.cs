using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Map("/", () => "Страница маршрута 1");
app.Map("/route1", () => "Страница маршрута 2");
app.Map("/route2", () => "Страница маршрута 3");

app.Map("/users", () => new Person("Lena", 37));

app.Map("/users/{id?}", (string? id) => $"User Id: {id ?? "Undefined"}"); // необязательный параметр

app.Map(
    "{controller=Home}/{action=Index}/{id?}",
    (string controller, string action, string? id) =>
        $"Controller: {controller} \nAction: {action} \nId: {id}" // значение параметров по умолчанию
);

app.Map("users/{**info}", (string info) => $"User Info: {info}"); // непроизвольное кол-во параметров

app.Map(
    "/user/{name:alpha:minlength(2)}/{age:int:range(1, 110)}",  // ограничения на алфавитные символы, минимальную длину слова и возраст
    (string name, int age) => $"User Age: {age} \nUser Name:{name}"
);

app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
        string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

record class Person(string Name, int Age);
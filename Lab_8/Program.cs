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

app.Map("/", () => "�������� �������� 1");
app.Map("/route1", () => "�������� �������� 2");
app.Map("/route2", () => "�������� �������� 3");

app.Map("/users", () => new Person("Lena", 37));

app.Map("/users/{id?}", (string? id) => $"User Id: {id ?? "Undefined"}"); // �������������� ��������

app.Map(
    "{controller=Home}/{action=Index}/{id?}",
    (string controller, string action, string? id) =>
        $"Controller: {controller} \nAction: {action} \nId: {id}" // �������� ���������� �� ���������
);

app.Map("users/{**info}", (string info) => $"User Info: {info}"); // �������������� ���-�� ����������

app.Map(
    "/user/{name:alpha:minlength(2)}/{age:int:range(1, 110)}",  // ����������� �� ���������� �������, ����������� ����� ����� � �������
    (string name, int age) => $"User Age: {age} \nUser Name:{name}"
);

app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
        string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

record class Person(string Name, int Age);
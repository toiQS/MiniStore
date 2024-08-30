using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Services.customer;
using MiniStore.Services.invoice;
using MiniStore.Services.invoiceDetail;
using MiniStore.Services.item;
using MiniStore.Services.styleItem;
using MiniStore.Services.supplier;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();

builder.Services.AddScoped<ISupplierServices, SupplierServices>();
builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
builder.Services.AddScoped<IItemServices, ItemServices>();
builder.Services.AddScoped<IStyleItemService, StyleItemService>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IInvoiceServices, InvoiceServices>();
builder.Services.AddScoped<IInvoiceDetailService, InvoiceDetailServices>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Kioxk.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("DefaultSQLiteConnection");
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlite(connectionString));
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

LectureBd();

app.Run();

static void LectureBd()
{
    _ = Task.Run(() =>
    {
        var cont = new Kioxk.Server.Controllers.CommandeController(new AppDBContext(new DbContextOptions<AppDBContext>()));

        while (true)
        {
            Console.WriteLine(" ...");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" myDataBase");
            Console.ResetColor();
            var order = Console.ReadLine();
            if (order == "")
            {
                cont.AfficheLivret();
                cont.AfficheCommandes();
                Console.WriteLine("");
                Console.WriteLine(" END");
            }
            else if (order!.StartsWith("val "))
            {
                string value = order[4..];
                if (!String.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        cont.Valided(int.Parse(value));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    Console.WriteLine("Done!");
                }
            }
            else if (order.StartsWith("rm "))
            {
                string value = order[3..];
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Are you sure ? Pin:");
                    if (Console.ReadLine() == "1234")
                    {
                        try
                        {
                            cont.Remove(int.Parse(value));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        Console.WriteLine("Remove ok!");
                    }
                    else { Console.WriteLine("fail!"); }
                }
            }
            else if (order == "clear all")
            {
                Console.WriteLine("Are you sure? All data will be clear! Pin:");
                if (Console.ReadLine() == "666")
                {
                    cont.ClearAll();
                    Console.WriteLine("Clear ok!");
                }
                else { Console.WriteLine("fail!"); }
            }
            else if (order == "clear livret")
            {
                Console.WriteLine("Are you sure? All data in Livret will be clear! Pin:");
                if (Console.ReadLine() == "l666")
                {
                    cont.ClearLivret();
                    Console.WriteLine("Clear Livret ok!");
                }
                else { Console.WriteLine("fail!"); }
            }
            else if (order == "clear coms")
            {
                Console.WriteLine("Are you sure? All data in Coms will be clear! Pin:");
                if (Console.ReadLine() == "c666")
                {
                    cont.ClearComs();
                    Console.WriteLine("Clear Coms ok!");
                }
                else { Console.WriteLine("fail!"); }
            }
        }
    });
}












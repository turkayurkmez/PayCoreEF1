// See https://aka.ms/new-console-template for more information
using firstApp;

Console.WriteLine("Hello, World!");
/* POCO: Plain Old C# Object
 *       Bildiğin C# Nesnesi
 * POJO: Plain Old Java Object      
 */
Console.WriteLine("Veritabanı kontrol ediliyor....");
Console.WriteLine(Commands.CreateDbAndSeedData(true) ? "Veritabanı oluşturuldu ve veri eklendi" : "veri tabanı var...");
Commands.ListAll();
Console.WriteLine("-------------------------");
Commands.ChangeWebUrl();

using NorthwindApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

var db = new NorthwindContext();
var categoriesList = db.Categories.ToList();

foreach (var category in categoriesList)
{
    Console.WriteLine(category.CategoryName);
}

Console.WriteLine("************************************");

var descCategoriesList = categoriesList.OrderByDescending(c => c.CategoryName);

foreach (var category in descCategoriesList)
{
    Console.WriteLine(category.CategoryName);
}

//Console.WriteLine(categoriesList.AsQueryable().ToList()[0]);

//db.Regions.Add(new Region() { RegionId = 5, RegionDescription = "SouthWestern" });
//db.SaveChanges();

Console.WriteLine("***************Regions*********************");
var regions = db.Regions.ToList();
regions.ForEach(region => Console.WriteLine(region.RegionDescription));

Console.WriteLine("***********************************");

var sheilta = from Employee 
              in db.Employees 
              select Employee; 
sheilta.ToList().ForEach(item => 
Console.WriteLine($"{item.FirstName} {item.LastName}"));

//db.Regions.Remove(db.Regions.ToList()[4]);
//db.SaveChanges();

using Microsoft.EntityFrameworkCore;
using NorthwindConsoleApp.Models;
using System.Linq;

//Northwind EF HW

//EX1
var db = new NorthwindContext();
var regionsList = db.Regions.ToList();

//EX2
Console.WriteLine("****************Regions****************");
regionsList.ForEach(region => Console.WriteLine(region.RegionDescription));

//EX3-4
var territories = db.Territories;
Console.WriteLine("**************Territories**************");
foreach (var territory in territories)
{
    Console.WriteLine(territory.TerritoryDescription);
}

//EX5-a
Console.WriteLine("**********Regions-Territories**********");
var regions = db.Regions;
foreach (var region in regions)
{
    Console.WriteLine(region.RegionDescription);
    foreach(var territory in region.Territories)
    {
        Console.WriteLine("  -"+territory.TerritoryDescription);
    }
}

//EX5-b
var regionsWithTeritories = regions.Include("Territories");
foreach (var region in regionsWithTeritories)
{
    Console.WriteLine(region.RegionDescription);
    foreach( var territory in region.Territories)
    {
        Console.WriteLine("  -"+ territory.TerritoryDescription);
    }
}

//EX6
var newOrder = new Order() { CustomerId = "FRANK", EmployeeId = 6, OrderDate = new DateTime(2022,08,02), ShipAddress = "7 Piccadilly Rd." , ShipCity = "New York", ShipCountry = "New York" };

//db.Orders.Add(newOrder);
db.SaveChanges();


var newOrderDetails1 = new OrderDetail { ProductId = 11, UnitPrice = 95, Quantity = 3 , OrderId = newOrder.OrderId };
var newOrderDetails2 = new OrderDetail { ProductId = 56, UnitPrice = 47, Quantity = 6 , OrderId = newOrder.OrderId };
var newOrderDetails3 = new OrderDetail { ProductId = 74, UnitPrice = 120, Quantity = 5 , OrderId = newOrder.OrderId };

//newOrder.OrderDetails.Add(newOrderDetails1);
//newOrder.OrderDetails.Add(newOrderDetails2);
//newOrder.OrderDetails.Add(newOrderDetails3);
db.SaveChanges();

//EX7
var ordersReport = (from order in db.Orders
                  join details in db.OrderDetails
                  on order.OrderId equals details.OrderId
                  join product in db.Products
                  on details.ProductId equals product.ProductId
                  join employee in db.Employees
                  on order.EmployeeId equals employee.EmployeeId
                  select new { order.OrderId, product.ProductName, employee.FirstName }).ToList();

//EX8
var ordersList = db.Orders.ToList();
ordersList[ordersList.Count() - 1].EmployeeId = 5;

db.SaveChanges();

//EX9
#region Method Syntax
var idToRemove = ordersList[ordersList.Count() - 1].OrderId;
var orderDetailsToRemove = db.OrderDetails.FirstOrDefault(od => od.OrderId == idToRemove);

db.OrderDetails.Remove(orderDetailsToRemove);
db.SaveChanges();
#endregion

#region Query Syntax
//var orderDetails = (from od in db.OrderDetails
//                    orderby od.OrderId descending
//                    select od);
//var orderDetailsToRemove = orderDetails.FirstOrDefault();

//db.OrderDetails.Remove(orderDetailsToRemove);
//db.SaveChanges();
#endregion

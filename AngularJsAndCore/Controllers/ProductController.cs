using AngularJsAndCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace AngularJsAndCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDBContext _db;
        public ProductController(AppDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
       public JsonResult AllProducts()
        {
           var data= _db.Products.ToList();
            return Json(data, new JsonSerializerOptions());
        }
        public JsonResult AddProduct([FromBody] Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            var data = _db.Products.ToList();
            return Json(data, new JsonSerializerOptions());
        }
        public JsonResult UpdateProduct([FromBody] Product product)
        {
            var upProduct = _db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            upProduct.ProductName = product.ProductName;
            upProduct.Price = product.Price;
            _db.Entry(upProduct).State = EntityState.Modified;
            _db.SaveChanges();
            var data = _db.Products.ToList();
            return Json(data, new JsonSerializerOptions());          
        }
        public string DeleteProduct(int ProductId)
        {
            var data = _db.Products.Where(e => e.ProductId == ProductId).Select(p => p).FirstOrDefault();
            if (data != null)
            {
                _db.Products.Remove(data);
            }
            _db.SaveChanges();
            return "Data deleted successfully";
        }
    }
}

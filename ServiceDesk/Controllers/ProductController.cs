using ServiceDesk.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceDesk.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult GetAllProducts()
        {
            ProductRepository ProductRepo = new ProductRepository();
            return View(ProductRepo.GetAllProducts());
        }
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductRepository ProductRepo = new ProductRepository();

                    if (ProductRepo.AddProduct(product))
                    {
                        ViewBag.Message = "Product details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                ProductRepository ProductRepo = new ProductRepository();
                if (ProductRepo.DeleteProduct(id))
                {
                    ViewBag.AlertMsg = "Product details deleted successfully";
                }
                return RedirectToAction("GetAllProducts");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditProduct(int id)
        {
            ProductRepository ProductRepo = new ProductRepository();

            return View(ProductRepo.GetAllProducts().Find(Product => Product.ProductCode == id));

        }

        [HttpPost]
        public ActionResult EditProduct(Product obj)
        {
            try
            {
                ProductRepository ProductRepo = new ProductRepository();

                ProductRepo.UpdateProduct(obj);
                return RedirectToAction("GetAllProducts");
            }
            catch
            {
                return View();
            }
        }
    }
}
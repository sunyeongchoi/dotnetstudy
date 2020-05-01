using System.Linq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SavaProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //데이터 값들 중에 뭔가 문제가 존재하는 경우
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public ActionResult Delete(int productId)
        {
            Product deleteProduct = repository.DeleteProduct(productId);
            if(deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deleteProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
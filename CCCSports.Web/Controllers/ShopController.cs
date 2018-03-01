using CCCSports.Data;
using CCCSports.Web.Extensions;
using CCCSports.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCCSports.Web.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Store
        /// <summary>
        /// Returns ShopHome view with a list of all products. Initialises ProductViewModel
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopHome()
        {

            var products = db.Products.OrderBy(e => e.ProductCost)
                .Select(e => new ProductViewModel()
                {
                    ProductId = e.ProductId,
                    ProductName = e.ProductName,
                    ProductDesc = e.ProductDesc,
                    ProductCost = e.ProductCost,
                    ProductStock = e.ProductStock,
                    ProductRating = e.ProductRating
                });


            return View(new ProductViewModel()
            {
                Products = products
            });
        }

        /// <summary>
        /// Returns shopping cart view
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCart()
        {
            ViewBag.Message = "Shopping Cart";

            return View();
        }

        // GET: Product
        /// <summary>
        /// Retunrs the create product view
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateProduct()
        {
            return View();
        }


        /// <summary>
        /// Allows a product to be created and saved to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ProductInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                ViewBag.Message = "Create Product Listing";

                var e = new Product()
                {
                    ProductName = model.InputProductName,
                    ProductDesc = model.InputProductDesc,
                    ProductCost = model.InputProductCost,
                    ProductStock = model.InputProductStock
                };

                db.Products.Add(e);
                db.SaveChanges();

                //Display notification message "Product Created"
                this.AddNotification("Product Listing Added", NotificationType.INFO);

                return RedirectToAction("ShopHome", "Shop");

            }

            return View(model);
        }



        /// <summary>
        /// Deletes the selected product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteProduct(int id, ProductInputModel model)
        {
            var itemToDelete = LoadItem(id);

            if (itemToDelete == null)
            {
                this.AddNotification("Cannot delete Item #" + id + " it does not exist.", NotificationType.ERROR);
                return RedirectToAction("ShopHome", "Shop");
            }

            db.Products.Remove(itemToDelete);
            db.SaveChanges();
            this.AddNotification("Product #" + id + " Deleted.", NotificationType.INFO);
            return RedirectToAction("ShopHome", "Shop");
        }



        /// <summary>
        /// GET edit item and some validation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var itemToEdit = LoadItem(id);

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot Edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("ShopHome", "Shop");
            }

            var model = ProductInputModel.CreateFromProduct(itemToEdit);
            return View("~/Views/Shop/EditProduct.cshtml", model);
        }

        /// <summary>
        /// Allows the slected item to be edited
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditProduct(int id, ProductInputModel model)
        {

            var itemToEdit = LoadItem(id);

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("ShopHome", "Shop");
            }

            if (model != null && ModelState.IsValid)
            {
                itemToEdit.ProductName = model.InputProductName;
                itemToEdit.ProductDesc = model.InputProductDesc;
                itemToEdit.ProductCost = model.InputProductCost;
                itemToEdit.ProductStock = model.InputProductStock;


                db.SaveChanges();
                this.AddNotification("Changes saved.", NotificationType.INFO);
                return RedirectToAction("ShopHome", "Shop");
            }

            return View("~/Views/Shop/EditProduct.cshtml", model);
        }


        /// <summary>
        /// Get's the product to be used in other methods
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Target Item</returns>
        private Product LoadItem(int id)
        {
            var itemToEdit = db.Products.Where(e => e.ProductId == id).FirstOrDefault();

            return itemToEdit;
        }



    }
}
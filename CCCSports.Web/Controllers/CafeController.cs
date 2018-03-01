using CCCSports.Data.FacilitiesClasses;
using CCCSports.Web.Extensions;
using CCCSports.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace CCCSports.Web.Controllers
{
    public class CafeController : BaseController
    {

        /// <summary>
        /// Returns cafe home view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CafeHome()
        {

            var cafeItems = db.CafeStock.OrderBy(e => e.ItemStock)
                .Select(e => new CafeItemViewModel()
            {
                ItemId = e.ItemId,
                ItemName = e.ItemName,
                ItemCost = e.ItemCost,
                ItemStock = e.ItemStock
                
            });


            return View(new CafeItemViewModel()
            {
                CafeItems = cafeItems
            });

        }

        /// <summary>
        /// Returns add item view
        /// </summary>
        /// <returns></returns>
        public ActionResult AddItem()
        {
            return View();
        }

        /// <summary>
        /// Allows a product to be created
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(CafeItemInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                ViewBag.Message = "Create Item Listing";

                var e = new CafeItem()
                {
                    ItemName = model.InputItemName,
                    ItemCost = model.InputItemCost,
                    ItemStock = model.InputItemStock
                };

                db.CafeStock.Add(e);
                db.SaveChanges();

                //Display notification message "Item Created"
                this.AddNotification("Item Added", NotificationType.INFO);

                return RedirectToAction("CafeHome", "Cafe");

            }

            return View(model);
        }

        /// <summary>
        /// Deletes the selected item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteItem(int id, CafeItemInputModel model)
        {
            var itemToDelete = LoadItem(id);

            if (itemToDelete == null)
            {
                this.AddNotification("Cannot delete Item #" + id + " it does not exist.", NotificationType.ERROR);
                return RedirectToAction("CafeHome", "Cafe");
            }

            db.CafeStock.Remove(itemToDelete);
            db.SaveChanges();
            this.AddNotification("Item #" + id + " Deleted.", NotificationType.INFO);
            return RedirectToAction("CafeHome", "Cafe");

        }


        /// <summary>
        /// GET edit item and some validation, initialises the CafeItemInputModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditItem(int id)
        {
            var itemToEdit = LoadItem(id);

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot Edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("CafeHome", "Cafe");
            }

            var model = CafeItemInputModel.CreateFromProduct(itemToEdit);
            return View(model);
        }

        /// <summary>
        /// Allows the slected item to be edited
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(int id, CafeItemInputModel model)
        {

            var itemToEdit = LoadItem(id);

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("CafeHome", "Cafe");
            }

            if (model != null && ModelState.IsValid)
            {
                itemToEdit.ItemName = model.InputItemName;
                itemToEdit.ItemCost = model.InputItemCost;
                itemToEdit.ItemStock = model.InputItemStock;

                db.SaveChanges();
                this.AddNotification("Changes saved.", NotificationType.INFO);
                return RedirectToAction("CafeHome", "Cafe");
            }

            return View(model);
        }




        /// <summary>
        /// Get's the item to be used in other methods
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>Target Item</returns>
        private CafeItem LoadItem(int id)
        {
            var itemToEdit = db.CafeStock.Where(e => e.ItemId == id).FirstOrDefault();

            return itemToEdit;
        }

    }
}
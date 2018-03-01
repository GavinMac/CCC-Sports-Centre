using CCCSports.Data;
using CCCSports.Web.Extensions;
using CCCSports.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCCSports.Web.Controllers
{
    public class EquipmentController : BaseController
    {
        // GET: Equipment
        /// <summary>
        /// Returns equipment homepage view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EquipmentHome()
        {

            var equipmentList = db.Equipment.OrderBy(e => e.EquipmentCost)
                .Select(e => new EquipmentViewModel()
                {
                    EquipmentId = e.EquipmentId,
                    EquipmentName = e.EquipmentName,
                    EquipmentCost = e.EquipmentCost,
                    EquipmentCondition = e.EquipmentCondition,
                    AmountInStock = e.AmountInStock
                });


            return View(new EquipmentViewModel()
            {
                EquipmentList = equipmentList
            });

        }

        /// <summary>
        /// Returns add equipment view
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEquipment()
        {
            return View();
        }

        /// <summary>
        /// Allows equipment to be created
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEquipment(EquipmentInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                ViewBag.Message = "Create Item Listing";

                var e = new Equipment()
                {
                    EquipmentName = model.EquipmentName,
                    EquipmentCondition = model.EquipmentCondition,
                    EquipmentCost = model.EquipmentCost,
                    AmountInStock = model.AmountInStock
                };

                db.Equipment.Add(e);
                db.SaveChanges();

                //Display notification message "Item Created"
                this.AddNotification("Equipment Added", NotificationType.INFO);

                return RedirectToAction("EquipmentHome", "Equipment");

            }

            return View(model);
        }

        /// <summary>
        /// Deletes the selected equipment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteEquipment(int id, EquipmentInputModel model)
        {
            var itemToDelete = LoadItem(id);

            if (itemToDelete == null)
            {
                this.AddNotification("Cannot delete Item #" + id + " it does not exist.", NotificationType.ERROR);
                return RedirectToAction("EquipmentHome", "Equipment");
            }

            db.Equipment.Remove(itemToDelete);
            db.SaveChanges();
            this.AddNotification("Equipment #" + id + " Deleted.", NotificationType.INFO);
            return RedirectToAction("EquipmentHome", "Equipment");
        }



        /// <summary>
        /// GET edit item and some validation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditEquipment(int id)
        {
            var itemToEdit = this.LoadItem(id);

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot Edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("EquipmentHome", "Equipment");
            }

            var model = EquipmentInputModel.CreateFromEquipment(itemToEdit);
            return View(model);
        }

        /// <summary>
        /// Allows the slected post to be edited
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditEquipment(int id, EquipmentInputModel model)
        {

            var itemToEdit = LoadItem(id);

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("EquipmentHome", "Equipment");
            }

            if (model != null && ModelState.IsValid)
            {
                itemToEdit.EquipmentName = model.EquipmentName;
                itemToEdit.EquipmentCost = model.EquipmentCost;
                itemToEdit.AmountInStock = model.AmountInStock;
                itemToEdit.EquipmentCondition = model.EquipmentCondition;

                db.SaveChanges();
                this.AddNotification("Changes saved.", NotificationType.INFO);
                return RedirectToAction("EquipmentHome", "Equipment");
            }

            return View(model);
        }



        //public ActionResult AddToHire(int id, EquipmentViewModel model)
        //{
        //    //Get current user
        //    var userId = User.Identity.GetUserId();
        //    var currentUser = db.Users.Find(userId);

        //    //Finds the item to be added to the cart
        //    var EquipmentToAdd = LoadItem(id);

        //    //Creaes a new cart
        //    var newCart = new EquipmentHireCart();

        //    //Sets current user to new cart
        //    newCart.CartCustomer = currentUser;

        //    //Adds selected equipment to new cart
        //    newCart.Items.Add(EquipmentToAdd);

        //    //Adds new cart to carts in database
        //    db.HireCarts.Add(newCart);

        //    return View();
        //}


        /// <summary>
        /// Get's the equipment to be used in other methods
        /// </summary>
        /// <param name="id">Equipment ID</param>
        /// <returns>Target Item</returns>
        private Equipment LoadItem(int id)
        {
            var itemToEdit = db.Equipment.Where(e => e.EquipmentId == id).FirstOrDefault();

            return itemToEdit;
        }

    }
}
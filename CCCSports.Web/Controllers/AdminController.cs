using CCCSports.Data;
using CCCSports.Web.Extensions;
using CCCSports.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCCSports.Web.Controllers
{
    public class AdminController : BaseController
    {

        /// <summary>
        /// Returns admin view
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminSettings()
        {
            return View();
        }

        // GET: Admin

        /// <summary>
        /// Gets and displays all users and their roles
        /// </summary>
        /// <returns></returns>
        public ActionResult DisplayUsers()
        {
            var userRoles = new List <UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore); //Initalizers userManager to deal with users and roles

            //Get all the usernames
            foreach (var user in userStore.Users)
            {
                var r = new UserDetailsViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    InputUsername = user.InputUserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    HouseNumber = user.HouseNumber,
                    Street = user.Street,
                    Town = user.Town,
                    PostCode = user.PostCode,            
                    
                };
                userRoles.Add(r);
            }
            //Get all the Roles for our users
            foreach (var user in userRoles)
            {
                user.Roles = userManager.GetRoles(userStore.Users.First(s => s.UserName == user.Username).Id);
            }

            return View(userRoles);
        }


        ///////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets and displays all staff users and roles
        /// </summary>
        /// <returns></returns>
        public ActionResult DisplayStaff()
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore); //Initalizers userManager to deal with users and roles

            //Get all the usernames
            foreach (var user in userStore.Users)
            {
                var r = new UserDetailsViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    InputUsername = user.InputUserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    HouseNumber = user.HouseNumber,
                    Street = user.Street,
                    Town = user.Town,
                    PostCode = user.PostCode,
                };
                userRoles.Add(r);
            }
            //Get all the Roles for our users
            foreach (var user in userRoles)
            {
                user.Roles = userManager.GetRoles(userStore.Users.First(s => s.UserName == user.Username).Id);
            }

            return View(userRoles);
        }


        ///////////////////////////////////////////////////////////////////////////////////////



        /// <summary>
        /// Changes a users role to admin
        /// </summary>
        /// <param name="id">users Id</param>
        /// <param name="Roles">Users roles</param>
        /// <returns>returns the users display view</returns>
        public async System.Threading.Tasks.Task<ActionResult> ChangeRoleAdmin(string id, IEnumerable<string> Roles)
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            IList<string> roles = await userManager.GetRolesAsync(id);
            if (id != null)
            {
                foreach (var currentRoleName in roles)
                {
                    IdentityResult deletionResult = await userManager.RemoveFromRoleAsync(id, currentRoleName); // removes a user from all there current roles
                }

                await userManager.AddToRoleAsync(id, "Admin"); //adds them to the admin role
            }
            db.SaveChanges();
            this.AddNotification("Account has been made administrator.", NotificationType.SUCCESS);
            return RedirectToAction("DisplayUsers", "Admin");
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Changes a users role to manager
        /// </summary>
        /// <param name="id">users Id</param>
        /// <param name="Roles">Users roles</param>
        /// <returns>returns the users display view</returns>      
        public async System.Threading.Tasks.Task<ActionResult> ChangeRoleManager(string id, IEnumerable<string> Roles)
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            IList<string> roles = await userManager.GetRolesAsync(id);
            if (id != null)
            {
                foreach (var currentRoleName in roles)
                {
                    IdentityResult deletionResult = await userManager.RemoveFromRoleAsync(id, currentRoleName); // removes a user from all there current roles
                }

                await userManager.AddToRoleAsync(id, "Manager"); //adds them to the manager role
            }
            db.SaveChanges();
            this.AddNotification("Account has been made a manager.", NotificationType.SUCCESS);
            return RedirectToAction("DisplayUsers", "Admin");
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Changes a users role to bookings clerk
        /// </summary>
        /// <param name="id">users Id</param>
        /// <param name="Roles">Users roles</param>
        /// <returns>returns the users display view</returns>
        public async System.Threading.Tasks.Task<ActionResult> ChangeRoleClerk(string id, IEnumerable<string> Roles)
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            IList<string> roles = await userManager.GetRolesAsync(id);
            if (id != null)
            {
                foreach (var currentRoleName in roles)
                {
                    IdentityResult deletionResult = await userManager.RemoveFromRoleAsync(id, currentRoleName); // removes a user from all there current roles
                }

                await userManager.AddToRoleAsync(id, "BookingsClerk"); //adds them to the clerk role
            }
            db.SaveChanges();
            this.AddNotification("Account has been made a bookings clerk.", NotificationType.SUCCESS);
            return RedirectToAction("DisplayUsers", "Admin");
        }


        ///////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Changes a users role to membership clerk
        /// </summary>
        /// <param name="id">users Id</param>
        /// <param name="Roles">Users roles</param>
        /// <returns>returns the users display view</returns>
        public async System.Threading.Tasks.Task<ActionResult> ChangeRoleMClerk(string id, IEnumerable<string> Roles)
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            IList<string> roles = await userManager.GetRolesAsync(id);
            if (id != null)
            {
                foreach (var currentRoleName in roles)
                {
                    IdentityResult deletionResult = await userManager.RemoveFromRoleAsync(id, currentRoleName); // removes a user from all there current roles
                }

                await userManager.AddToRoleAsync(id, "MembershipClerk"); //adds them to the membership clerk role
            }
            db.SaveChanges();
            this.AddNotification("Account has been made a membership clerk.", NotificationType.SUCCESS);
            return RedirectToAction("DisplayUsers", "Admin");
        }


        ///////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Changes a users role to staff
        /// </summary>
        /// <param name="id">users Id</param>
        /// <param name="Roles">Users roles</param>
        /// <returns>returns the users display view</returns>
        public async System.Threading.Tasks.Task<ActionResult> ChangeRoleStaff(string id, IEnumerable<string> Roles)
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            IList<string> roles = await userManager.GetRolesAsync(id);
            if (id != null)
            {
                foreach (var currentRoleName in roles)
                {
                    IdentityResult deletionResult = await userManager.RemoveFromRoleAsync(id, currentRoleName); // removes a user from all there current roles
                }

                await userManager.AddToRoleAsync(id, "Staff"); //adds them to the staff role
            }
            db.SaveChanges();
            this.AddNotification("Account has been made a staff member.", NotificationType.SUCCESS);
            return RedirectToAction("DisplayUsers", "Admin");
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Changes a users role to a member
        /// </summary>
        /// <param name="id">users Id</param>
        /// <param name="Roles">Users roles</param>
        /// <returns>returns the users display view</returns>
        public async System.Threading.Tasks.Task<ActionResult> ChangeRoleMember(string id, IEnumerable<string> Roles)
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            IList<string> roles = await userManager.GetRolesAsync(id);
            if (id != null)
            {
                foreach (var currentRoleName in roles)
                {
                    IdentityResult deletionResult = await userManager.RemoveFromRoleAsync(id, currentRoleName); // removes a user from all there current roles
                }

                await userManager.AddToRoleAsync(id, "Member"); //adds them to the admin role
                
            }

            context.Users.Find(id).IsMember = true;
            db.SaveChanges();
            this.AddNotification("Account has been made a member.", NotificationType.SUCCESS);
            return RedirectToAction("DisplayUsers", "Admin");
        }

        ///////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Changes a users role to user
        /// </summary>
        /// <param name="id">users Id</param>
        /// <param name="Roles">Users roles</param>
        /// <returns>returns the users display view</returns>
        public async System.Threading.Tasks.Task<ActionResult> ChangeRoleUser(string id, IEnumerable<string> Roles)
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            IList<string> roles = await userManager.GetRolesAsync(id);
            if (id != null)
            {
                foreach (var currentRoleName in roles)
                {
                    IdentityResult deletionResult = await userManager.RemoveFromRoleAsync(id, currentRoleName); // removes a user from all there current roles
                }

                await userManager.AddToRoleAsync(id, "User"); //adds them to the admin role
            }

            context.Users.Find(id).IsMember = false;
            db.SaveChanges();
            this.AddNotification("Account has been made a user.", NotificationType.SUCCESS);
            return RedirectToAction("DisplayUsers", "Admin");
        }



        ///////////////////////////////// CRUD Methods /////////////////////////////////



        /// <summary>
        /// Deletes the selected user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteItem(string id, ApplicationUser model)
        {
            var userToDelete = LoadUser(id);

            if (userToDelete == null)
            {
                this.AddNotification("Cannot delete user" + id + " user does not exist.", NotificationType.ERROR);
                return RedirectToAction("AdminSettings", "Admin");
            }

            db.Users.Remove(userToDelete);
            db.SaveChanges();
            this.AddNotification("User " + id + " Deleted.", NotificationType.INFO);
            return RedirectToAction("AdminSettings", "Admin");

        }



        /// <summary>
        /// Get's the user to be used in other methods
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>Target Item</returns>
        private ApplicationUser LoadUser(string id)
        {
            var userToLoad = db.Users.Where(e => e.Id == id).FirstOrDefault();

            return userToLoad;
        }



    }
}
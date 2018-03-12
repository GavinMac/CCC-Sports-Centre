using Braintree;
using CCCSports.Data;
using CCCSports.Web.Extensions;
using CCCSports.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CCCSports.Web.Controllers
{
    public class MembershipController : BaseController
    {

        /// <summary>
        /// Returns membership homepage
        /// </summary>
        /// <returns></returns>
        public ActionResult MembersHome()
        {
            return View();
        }

        //
        // GET: /Account/Login
        /// <summary>
        /// Returns membership signup homepage and created braintree client token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MembershipSignUp()
        {

            var model = new MembershipInputModel()
            {
                IsFamily = false
            };

            var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "",
                PublicKey = "",
                PrivateKey = ""
            };

            var clientToken = gateway.ClientToken.generate();
            ViewBag.ClientToken = clientToken;


            return View(model);

        }

        /// <summary>
        /// Changes a users role to a member and starts membership
        /// </summary>
        /// <param name="id">users Id</param>
        /// <param name="Roles">Users roles</param>
        /// <returns>returns the users display view</returns>
        public ActionResult AddToMember()
        {
            var userRoles = new List<UserDetailsViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            //Get current user
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);


            //Sets their membership type based on age
            if(currentUser.Age <= 16)
            {
                currentUser.MemberType = "Juvenile";
            }
            else if (currentUser.Age > 16)
            {
                currentUser.MemberType = "Adult";
            }

            IList<string> roles = userManager.GetRoles(userId);
            if (userId != null)
            {
                foreach (var currentRoleName in roles)
                {
                    IdentityResult deletionResult = userManager.RemoveFromRole(userId, currentRoleName); // removes a user from all there current roles
                }

                userManager.AddToRole(userId, "Member"); //adds them to the admin role          
            }

            //Sets the user as a member.
            currentUser.IsMember = true;

            db.SaveChanges();


            //Redirect home
            return RedirectToAction("Index", "Home");
        }




        /// <summary>
        /// Starts membership subscription using braintree
        /// </summary>
        /// <param name="currentUser"></param>
        [HttpPost]
        public ActionResult MembershipSignUp(FormCollection collection)
        {

            //Get current user
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);

            string nonce = collection["payment_method_nonce"];
                

            var model = new MembershipInputModel()
            {
                IsFamily = false
            };

            var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "",
                PublicKey = "",
                PrivateKey = ""
            };


            //Create new customer
            var CustomerRequest = new CustomerRequest
            {
                CustomerId = currentUser.Id,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Email = currentUser.Email,
                CreditCard = new CreditCardRequest
                {
                    PaymentMethodNonce = collection["payment_method_nonce"],
                    Options = new CreditCardOptionsRequest
                    { VerifyCard = true }
                }

        };
            Result<Customer> CustomerResult = gateway.Customer.Create(CustomerRequest);

            string cardToken = null;

            if (CustomerResult.IsSuccess())
            {
                cardToken = CustomerResult.Target.PaymentMethods[0].Token;
                AddToMember();
            }

            bool success = CustomerResult.IsSuccess();
            // true



            ///////


            if (currentUser.MemberType == "Adult")
            {
                var request = new SubscriptionRequest
                {
                    PaymentMethodToken = cardToken,
                    PlanId = "cccadult"
                };

                Result<Subscription> result = gateway.Subscription.Create(request);

                //Success validation
                if (result.IsSuccess())
                {              
                    this.AddNotification("Success! Membership created. You will receive an email shortly", NotificationType.SUCCESS);
                    var newMembership = new CentreMembership()
                    {
                        MemberId = currentUser.Id,
                        MemberName = currentUser.FirstName + " " + currentUser.LastName,
                        MemberType = "Adult",
                        MembershipUser = currentUser
                    };             
                    SendMemberConfirmation(currentUser);
                    db.Memberships.Add(newMembership);
                    db.SaveChanges();
                }
                else
                {
                    this.AddNotification("Failed. Your membership has not been successful", NotificationType.ERROR);
                }

            }

            else if (currentUser.MemberType == "Juvenile")
            {
                var request = new SubscriptionRequest
                {
                    PaymentMethodToken = cardToken,
                    PlanId = "cccjuv"
                };

                Result<Subscription> result = gateway.Subscription.Create(request);

                //Success validation
                if (result.IsSuccess())
                {
                    this.AddNotification("Success! Membership created. You will receive an email shortly", NotificationType.SUCCESS);
                    var newMembership = new CentreMembership()
                    {
                        MemberId = currentUser.Id,
                        MemberName = currentUser.FirstName + " " + currentUser.LastName,
                        MemberType = "Juvenile",
                        MembershipUser = currentUser
                    };
                    SendMemberConfirmation(currentUser);

                    db.Memberships.Add(newMembership);

                    db.SaveChanges();
                }
                else
                {
                    this.AddNotification("Failed. Your membership has not been successful", NotificationType.ERROR);
                }

            }
            else if (model.IsFamily == true)
            {
                var request = new SubscriptionRequest
                {
                    PaymentMethodToken = cardToken,
                    PlanId = "cccfam"
                };

                Result<Subscription> result = gateway.Subscription.Create(request);

                //Success validation
                if (result.IsSuccess())
                {
                    this.AddNotification("Success! Membership created. You will receive an email shortly", NotificationType.SUCCESS);
                    var newMembership = new CentreMembership()
                    {
                        MemberId = currentUser.Id,
                        MemberName = currentUser.FirstName + " " + currentUser.LastName,
                        MemberType = "Family",
                        MembershipUser = currentUser
                    };          
                    SendMemberConfirmation(currentUser);
                    db.Memberships.Add(newMembership);
                    db.SaveChanges();
                }
                else
                {
                    this.AddNotification("Failed. Your membership has not been successful", NotificationType.ERROR);
                }
            }
            else
            {
                this.AddNotification("Error. Try again.", NotificationType.ERROR);
            }

            return View(model);

        }



        /// <summary>
        /// Creates and sends confirmation email with SendGrid
        /// </summary>
        static void SendMemberConfirmation(ApplicationUser currentUser)
        {
            try
            {
                //var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
                string apiKey = "";
                var client = new SendGridClient(apiKey);

                var from = new EmailAddress("membership@cccsports.com");
                var subject = "CCC Sports - Membership Confirmation";

                var to = new EmailAddress(currentUser.Email);

                var htmlContent = "<h1>CCC Sports Membership Confirmation</h1><br/>";
                var plainTextContent = "Thank you " + currentUser.FirstName + "! " + "Your membership has been created! you will be billed anually depending on your membership type.";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);

                string costDisplay;

                if (currentUser.MemberType == "Adult")
                {
                    costDisplay = "You're accout type is: Adult - £30";
                }
                else if (currentUser.MemberType == "Juvenile")
                {
                    costDisplay = "You're accout type is: Juvenile - £15";
                }
                else if (currentUser.MemberType == "Family")
                {
                    costDisplay = "You're accout type is: Family - £70";
                }
                else
                {
                    costDisplay = "";
                }

                msg.AddContent(MimeType.Text, costDisplay);

                var response = client.SendEmailAsync(msg);
            }
            catch { }
           
            

        }
    }
}

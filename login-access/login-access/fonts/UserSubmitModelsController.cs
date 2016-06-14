using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using login_access.Models;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;

namespace login_access.Controllers
{
    public class UserSubmitModelsController : Controller
    {
        // This is the URL the application will authenticate at.
        const string authString = "https://login.microsoftonline.com/0dca9baa-2f8d-4304-b8b4-b30cbbb29faf";
        // These are the credentials the application will present during authentication
        // and were retrieved from the Azure Management Portal.
        const string clientID = "5d2765c4-ca5e-4ea9-8ed7-051ca0cbfa45";
        const string clientSecret = "jZDjwIk9Pavgt4QcFdOTUpPwr3R0cfLPGirivqWFSpI=";
        // The Azure AD Graph API is the "resource" we're going to request access to.
        const string resAzureGraphAPI = "https://graph.windows.net";
        // The Azure AD Graph API for my directory is available at this URL.
        const string serviceRootURL = "https://graph.windows.net/0dca9baa-2f8d-4304-b8b4-b30cbbb29faf";

        private UserSubmitDBContext db = new UserSubmitDBContext();

        // GET: UserSubmitModels
        public ActionResult Index()
        {
            return View(db.UserSubmits.ToList());
        }

        // GET: UserSubmitModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmitModel userSubmitModel = db.UserSubmits.Find(id);
            if (userSubmitModel == null)
            {
                return HttpNotFound();
            }
            return View(userSubmitModel);
        }

        // GET: UserSubmitModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserSubmitModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Email,Comment,CoolDB,AwesomeDB")] UserSubmitModel userSubmitModel)
        {
            if (ModelState.IsValid)
            {
                db.UserSubmits.Add(userSubmitModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userSubmitModel);
        }

        // GET: UserSubmitModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmitModel userSubmitModel = db.UserSubmits.Find(id);
            if (userSubmitModel == null)
            {
                return HttpNotFound();
            }
            return View(userSubmitModel);
        }

        // POST: UserSubmitModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Email,Comment,CoolDB,AwesomeDB")] UserSubmitModel userSubmitModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSubmitModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userSubmitModel);
        }

        // GET: UserSubmitModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmitModel userSubmitModel = db.UserSubmits.Find(id);
            if (userSubmitModel == null)
            {
                return HttpNotFound();
            }
            return View(userSubmitModel);
        }

        // POST: UserSubmitModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSubmitModel userSubmitModel = db.UserSubmits.Find(id);
            db.UserSubmits.Remove(userSubmitModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: UserSubmitModels/Approve/5
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmitModel userSubmitModel = db.UserSubmits.Find(id);
            if (userSubmitModel == null)
            {
                return HttpNotFound();
            }
            return View(userSubmitModel);
        }

        // POST: UserSubmitModels/Delete/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> ApproveConfirmed(int id)
        {
            UserSubmitModel userSubmitModel = db.UserSubmits.Find(id);


       
            Uri serviceRoot = new Uri(serviceRootURL);
            ActiveDirectoryClient adClient = new ActiveDirectoryClient(
                serviceRoot,
                async () => await GetAppTokenAsync());




            // Look up a user in the directory by their UPN.
            /*var name = "Erik";
            var userLookupTask = adClient.Users.Where(
                user => user.DisplayName.Equals(
                    name, StringComparison.CurrentCultureIgnoreCase)).ExecuteSingleAsync();

            var userJohnDoe = (User)await userLookupTask;*/

            string AzureADDomainName = "ucrcohortaddev.onmicrosoft.com";

            string trimmed = userSubmitModel.Name.Trim(' ');

            IUser userToBeAdded = new User() {
                DisplayName = userSubmitModel.Name,
                UserPrincipalName = userSubmitModel.Name.ToLower().Replace(" ", "") + "@" + AzureADDomainName,
                MailNickname = userSubmitModel.Name.ToLower().Replace(" ", ""),
                AccountEnabled = true,
                PasswordProfile = new PasswordProfile
                {

                    Password = "TempP@ssw0rd!",
                    ForceChangePasswordNextLogin = true
                }
             };

            await adClient.Users.AddUserAsync(userToBeAdded);


            //Task<IPagedCollection<IUser>> getGraphObjectsTask = adClient.Users.ExecuteAsync();

            //IPagedCollection<IUser> graphObjects = await getGraphObjectsTask;

            //var userLookupTask = adClient.Users.Where;

            // Show John Doe's Name
            //Console.WriteLine(graphObjects.CurrentPage.Where(user => user.DisplayName == "Erik"));
            //var user2 = graphObjects.CurrentPage.Where(user => user.DisplayName == "Erik");


            db.UserSubmits.Remove(userSubmitModel);
            db.SaveChanges();
            return View(@"ApproveConfirmed", userSubmitModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private static async Task<string> GetAppTokenAsync()
        {
            // Instantiate an AuthenticationContext for my directory (see authString above).
            AuthenticationContext authenticationContext = new AuthenticationContext(authString, false);

            // Create a ClientCredential that will be used for authentication.
            // This is where the Client ID and Key/Secret from the Azure Management Portal is used.
            ClientCredential clientCred = new ClientCredential(clientID, clientSecret);

            // Acquire an access token from Azure AD to access the Azure AD Graph (the resource)
            // using the Client ID and Key/Secret as credentials.
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(resAzureGraphAPI, clientCred);

            // Return the access token.
            return authenticationResult.AccessToken;
        }
    }
}

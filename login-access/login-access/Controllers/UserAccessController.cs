using login_access.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System;

namespace login_access.Controllers
{
    public class UserAccessController : Controller
    {


        private int id;

        public UserAccessController()
        {

            id = 0;
        }

        // GET: UserAccess
        /*public ActionResult Index()
        {
            return View();
        }*/

        //GET: /HelloWorld/ 
        public ActionResult SubmitRequest()
        {
            return View();
            //return "This is my <b>default</b> action...";

        }

        [HttpPost]
        public ActionResult SubmitRequest(UserSubmitModel model)
        {
            if (model.Name == null)
            {
                model.ReturnText = "Name Field Empty";
                return View(model);
            }
            else if (model.FreeText == null)
            {
                model.ReturnText = "Description Field Empty";
                return View(model);
            }


            using (var db = new UserSubmitDBContext())
            {

                if (ModelState.IsValid)
                {
                    db.UserSubmits.Add(model);
                    db.SaveChanges();
                }
            }
 
            return View(@"Submitted");
        }


    }
}
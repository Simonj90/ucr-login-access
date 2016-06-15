using login_access.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System;

namespace login_access.Controllers
{
    public class UserAccessController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("SubmitRequest")]
        public ActionResult Index(UserSubmitModel model)
        {
            using (var db = new UserSubmitDBContext())
            {
                if (ModelState.IsValid)
                {
                    db.UserSubmits.Add(model);
                    db.SaveChanges();
                    return View(@"Submitted");
                }
            }
            return View(@"Index");
        }
    }
}
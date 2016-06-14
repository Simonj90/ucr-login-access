using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using login_access.Models;

namespace login_access.Controllers
{
    public class UserSubmitModelsController : Controller
    {
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
        public ActionResult ApproveConfirmed(int id)
        {
            UserSubmitModel userSubmitModel = db.UserSubmits.Find(id);
            
            // Access to Azure AD, send email?!?

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
    }
}

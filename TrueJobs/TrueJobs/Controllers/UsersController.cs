using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrueJobs;

namespace TrueJobs.Controllers
{
    public class UsersController : Controller
    {
        private JobsEntities2 db = new JobsEntities2();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file2, CV cv, IEnumerable<HttpPostedFileBase> files, User user)
        {

            if (file2 != null && file2.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileNameWithoutExtension(file2.FileName) + " _ " + DateTime.Now.ToString("ddHmmss") + Path.GetExtension(file2.FileName);
               
                var path = Path.Combine(Server.MapPath("~/App_Data/imgpoze"), fileName);
                user.Photo = fileName;

                file2.SaveAs(path);
               


            }

            if (ModelState.IsValid)
            {
                user.Email = User.Identity.Name;
                db.Users.Add(user);
                db.SaveChanges();

                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName_att = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName_att);
                        cv.CV_path = fileName_att;
                        cv.User_ID = user.User_ID;
                        file.SaveAs(path);


                        db.CVs.Add(cv);
                        db.SaveChanges();

                    }
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "User_ID,FirstName,LastName,Email,Phone,Photo,Location,Experience")] */User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.CVs.RemoveRange(db.CVs.Where(c => c.User_ID == id));
            db.SaveChanges();
            return RedirectToAction("Index");
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

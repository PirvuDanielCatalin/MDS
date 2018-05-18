using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrueJobs;
using PagedList;

namespace TrueJobs.Controllers
{
    public class JobsController : Controller
    {
        private JobsEntities2 db = new JobsEntities2();

        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Company).Include(j => j.Interest);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.Company_ID = new SelectList(db.Companies, "Company_ID", "Name");
            ViewBag.Interest_ID = new SelectList(db.Interests, "Interest_ID", "Name");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job)
        {

         

            if (ModelState.IsValid)
            {
                var email = User.Identity.Name;
                Company company = db.Companies.SingleOrDefault(d => d.Email == email);
                job.Company_ID = company.Company_ID;

                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Details", "Companies", new { email = User.Identity.Name.TrimEnd()});
            }


          

            
            ViewBag.Company_ID = new SelectList(db.Companies, "Company_ID", "Name", job.Company_ID);
            ViewBag.Interest_ID = new SelectList(db.Interests, "Interest_ID", "Name", job.Interest_ID);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company_ID = new SelectList(db.Companies, "Company_ID", "Name", job.Company_ID);
            ViewBag.Interest_ID = new SelectList(db.Interests, "Interest_ID", "Name", job.Interest_ID);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Job_ID,Name,Location,Description,Requirements,Type,Interest,Experience,Company_ID,Interest_ID,Attributes")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Company_ID = new SelectList(db.Companies, "Company_ID", "Name", job.Company_ID);
            ViewBag.Interest_ID = new SelectList(db.Interests, "Interest_ID", "Name", job.Interest_ID);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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


        public ActionResult CompanyJobs(int id, string sortOrder, string searchString,
            string currentFilter, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "RecordName" ? "date_desc" : "RecordName";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;



            var jobs = from s in db.Jobs
                            select s;

            jobs = jobs.Where(s => s.Company_ID == id);

            switch (sortOrder)
            {
                case "name_desc":
                    //words = words.OrderByDescending(s => s.Title);
                    jobs = jobs.OrderByDescending(s => s.Name);
                    // model = model.OrderByDescending(s => s.Tag_name);

                    break;
                default:  // Name ascending 		
                          // words = words.OrderBy(s => s.Word);
                    jobs = jobs.OrderBy(s => s.Name);
                    // model = model.OrderBy(s => s.Tag_name);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(jobs.ToPagedList(pageNumber, pageSize));



        }



    }
}

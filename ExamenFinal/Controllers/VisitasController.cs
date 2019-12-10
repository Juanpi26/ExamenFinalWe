using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamenFinal.Models;

namespace ExamenFinal.Controllers
{
    public class VisitasController : Controller
    {
        private Examen_finalEntities db = new Examen_finalEntities();

        // GET: Visitas
        public ActionResult Index()
        {
            var visitas = db.Visitas.Include(v => v.Contacto);
            return View(visitas.ToList());
        }

        // GET: Visitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // GET: Visitas/Create
        public ActionResult Create()
        {
            ViewBag.Contacto_que_lo_recibio = new SelectList(db.Contacto, "ID", "Nombre");
            return View();
        }

        // POST: Visitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Motivo_visita,Fecha_visita,Hora_entrada,Hora_salida,Nombre_completo,Contacto_que_lo_recibio")] Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                db.Visitas.Add(visitas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Contacto_que_lo_recibio = new SelectList(db.Contacto, "ID", "Nombre", visitas.Contacto_que_lo_recibio);
            return View(visitas);
        }

        // GET: Visitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Contacto_que_lo_recibio = new SelectList(db.Contacto, "ID", "Nombre", visitas.Contacto_que_lo_recibio);
            return View(visitas);
        }

        // POST: Visitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Motivo_visita,Fecha_visita,Hora_entrada,Hora_salida,Nombre_completo,Contacto_que_lo_recibio")] Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Contacto_que_lo_recibio = new SelectList(db.Contacto, "ID", "Nombre", visitas.Contacto_que_lo_recibio);
            return View(visitas);
        }

        // GET: Visitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitas visitas = db.Visitas.Find(id);
            db.Visitas.Remove(visitas);
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

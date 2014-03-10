using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CamadaDados;
using CamadaDados.DbContextClass;
using CamadaDados.Cliente;
using System.Text.RegularExpressions;

namespace BeautySalon.Controllers
{
    public class ClienteController : Controller
    {
        private BeautySalonDbContext db = new BeautySalonDbContext();
     
        // GET: /Cliente/
        public ActionResult Index()
        {
            return View(db.Clientes.ToList());
        }

        // GET: /Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteModel clientemodel = db.Clientes.Find(id);
            if (clientemodel == null)
            {
                return HttpNotFound();
            }
            return View(clientemodel);
        }

        // GET: /Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCliente,Nome,DataNascimento,Email,CPF")] ClienteModel clientemodel)
        {
            using (var objCliente = new Cliente())
            {
                if (ModelState.IsValid)
                {

                    var cpf = LimparMascara(clientemodel.CPF);
                    clientemodel.CPF = cpf;
                    objCliente.InsertOrUpdate(clientemodel);
                    //objCliente.Create(clientemodel);
                    return RedirectToAction("Index");
                }
                return View(clientemodel);
            }
        }

        public static string LimparMascara(string texto)
        {
            texto = texto.Replace("&nbsp;", string.Empty);
            texto = texto.Replace("_", string.Empty);
            texto = texto.Replace("-", string.Empty);
            texto = texto.Replace(",", string.Empty);
            texto = texto.Replace("/", string.Empty);
            texto = texto.Replace(".", string.Empty);
            texto = texto.Replace(":", string.Empty);
            return texto;
        }

       
        // GET: /Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteModel clientemodel = db.Clientes.Find(id);
            if (clientemodel == null)
            {
                return HttpNotFound();
            }
            return View(clientemodel);
        }

        // POST: /Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCliente,Nome,DataNascimento,Email,CPF")] ClienteModel clientemodel)
        {
            using (var objCliente = new Cliente())
            {
                if (ModelState.IsValid)
                {
                    objCliente.InsertOrUpdate(clientemodel);
                    //db.Entry(clientemodel).State = EntityState.Modified;
                    //db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(clientemodel);
            }
        }

        // GET: /Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteModel clientemodel = db.Clientes.Find(id);
            if (clientemodel == null)
            {
                return HttpNotFound();
            }
            return View(clientemodel);
        }

        // POST: /Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClienteModel clientemodel = db.Clientes.Find(id);
            db.Clientes.Remove(clientemodel);
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

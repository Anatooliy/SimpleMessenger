using Messenger.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Messenger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationContext db = new ApplicationContext();

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            return View(db.Messages.OrderByDescending(e => e.Id).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {           
            return PartialView();
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMessage(Message message)
        {
            ViewBag.UserName = User.Identity.Name;

            message.Date = DateTime.Now;
            message.UserId = UserManager.FindByName(User.Identity.Name).Id;

            db.Messages.Add(message);
            
            db.SaveChanges();
            var mes = db.Messages.OrderByDescending(e => e.Id);
            
            return PartialView(message);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }      
            
            return PartialView(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditMessage(Message message)
        {
            Message newMessage = db.Messages.Find(message.Id);
            if (newMessage.UserId != UserManager.FindByName(User.Identity.Name).Id)
            {
                return Json("false", JsonRequestBehavior.DenyGet);
            }
            newMessage.MessagesText = message.MessagesText;

            db.Entry(newMessage).State = EntityState.Modified;
            db.SaveChanges();

            return Json(newMessage.MessagesText, JsonRequestBehavior.DenyGet);
        }



        [HttpPost]        
        public JsonResult Delete(int id)
        {            
            Message b = db.Messages.Find(id);
           
            if (b == null || b.UserId != UserManager.FindByName(User.Identity.Name).Id)
            {               
                return Json("false", JsonRequestBehavior.DenyGet);
            }
            
            db.Messages.Remove(b);
            db.SaveChanges();              

            return Json("true", JsonRequestBehavior.DenyGet);
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
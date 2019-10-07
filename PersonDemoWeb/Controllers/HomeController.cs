using PersonDemoWeb.EF;
using PersonDemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonDemoWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetPeople());
        }

        public ActionResult Add(PersonViewModel model)
        {

            string cnString = GetCnString();
            using (var db = new persondemoEntities(cnString))
            {
                db.People.Add(new Person()
                {
                    LastName = model.NewPerson.LastName,
                    FirstName = model.NewPerson.FirstName,
                    Sport = model.NewPerson.Sport
                });
                db.SaveChanges();
            }
            ModelState.Clear();
            return View("Index", GetPeople());
        }

        public ActionResult Delete(int? id)
        {
            if (id!=null)
            {
                using (var db = new persondemoEntities(GetCnString()))
                {
                    var person = new Person() { ID = id.Value };
                    db.People.Attach(person);                                                            
                    db.People.Remove(person);
                    db.SaveChanges();
                }

            }            
            return View("Index", GetPeople());
        }

        private PersonViewModel GetPeople()
        {
            var model = new PersonViewModel();
            string cnString = GetCnString();
            using (var db = new persondemoEntities(cnString))
            {
                model.People = new List<PersonModel>();
                foreach (Person p in db.People)
                {
                    model.People.Add(new PersonModel()
                    {
                        LastName = p.LastName,
                        FirstName = p.FirstName,
                        Sport = p.Sport,
                        ID = p.ID
                    });
                }

                model.ServerName = db.Database.SqlQuery<string>("SELECT @@SERVERNAME").First();
                model.NewPerson = new PersonModel();
            }
            return model;
        }


        private string GetCnString()
        {
            string cnstr = (string)Session["cnString"];            

            return cnstr;
        }
    }
}
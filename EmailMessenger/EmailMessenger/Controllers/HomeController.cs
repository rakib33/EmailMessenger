using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailMessenger.Controllers
{
    public class HomeController : Controller
    {
        //diferent type polymorphisom 
        public int IsPolymorpsim() {
            return 0;
        }
        public int IsPolymorpsim(int a){ return 0;}//it is polymorphisom
        //public int IsPolymorpsim(){ return 0;}//it is not polymorphisom
        //public float IsPolymorpsim(){ return 0;}//it is not polymorphisom, only return type change not polymorphism 
        /// <summary>
        /// float IsPolymorpsim(int a,int b) and 
        /// float IsPolymorpsim(float a, int b) and 
        /// float IsPolymorpsim(float a, int b,int c)
        /// is polumorphism
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public float IsPolymorpsim(int a,int b)
        {
            return 0;
        }

        //public float Polymorpsim(int a, int b){ return 0; }
        //public int IsPolymorpsim(int a, int b) {  return 0; }
        //public float IsPolymorpsim(int a, int b) { return 0; }

        public float IsPolymorpsim(float a, int b)//it is polymorphism
        {
            return 0;
        }
        public float IsPolymorpsim(float a, int b,int c)//it is polymorphism
        {
            return 0;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult JavaScriptDependency() {
            return View();
        }
    }
}
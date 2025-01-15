using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP.NET_HoangCongSon.Context;
using System.Web.Mvc;

namespace ASP.NET_HoangCongSon.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();

        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}
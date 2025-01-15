using ASP.NET_HoangCongSon.Context;
using ASP.NET_HoangCongSon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_HoangCongSon.Controllers
{
    public class PaymentController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                // lấy thông tin giỏ hàng từ biến session  
                var lstCart = (List<CartModel>)Session["cart"];
                // gán dữ liệu cho Order  
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.Id = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWebsiteBanHangEntities.Orders.Add(objOrder);
                //lưu thông tin dữ liệu vào bảng order
                objWebsiteBanHangEntities.SaveChanges();

                // Lấy OrderId vừa tạo để lưu vào bảng OrderDetail.  
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objWebsiteBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
                objWebsiteBanHangEntities.SaveChanges();
                Session["cart"] = null;
                Session["count"] = null;
            }
                return View();
        }
    }
}
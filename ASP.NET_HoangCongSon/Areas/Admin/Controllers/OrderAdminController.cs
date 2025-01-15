﻿using ASP.NET_HoangCongSon.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_HoangCongSon.Areas.Admin.Controllers
{
    public class OrderAdminController : Controller
    {
        WebsiteBanHangEntities dbObj = new WebsiteBanHangEntities();

        public ActionResult Index(string searchTerm, int? page)
        {
            // Get all products as IQueryable
            var lstOrder = dbObj.Orders.AsQueryable();



            return View(lstOrder);  // Return IPagedList<Product> to the view
        }
        public ActionResult Details(int Id)
        {
            var objOrder = dbObj.Orders.Where(n => n.Id == Id).FirstOrDefault();
            return View(objOrder);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objOrder = dbObj.Orders.Where(n => n.Id == Id).FirstOrDefault();
            return View(objOrder);
        }

        [HttpPost]
        public ActionResult Delete(Order objOrde)
        {
            var objOrder = dbObj.Orders.Where(n => n.Id == objOrde.Id).FirstOrDefault();

            dbObj.Orders.Remove(objOrder);
            dbObj.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Order objOrder)
        {
            try
            {
                // Thêm người dùng vào cơ sở dữ liệu
                dbObj.Orders.Add(objOrder);
                dbObj.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Product
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return HttpNotFound();

            var order = dbObj.Orders.Find(id);
            if (order == null) return HttpNotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order objOrder)
        {
            try
            {
                var existingOrder = dbObj.Orders.Find(objOrder.Id);
                if (existingOrder == null)
                {
                    return HttpNotFound();
                }

                // Cập nhật các trường khác
                existingOrder.Name = objOrder.Name;
                existingOrder.Id = objOrder.Id;
                existingOrder.Status = objOrder.Status;
                existingOrder.CreatedOnUtc = objOrder.CreatedOnUtc;

                // Đánh dấu thực thể là đã chỉnh sửa
                dbObj.Entry(existingOrder).State = EntityState.Modified;

                // Lưu thay đổi vào cơ sở dữ liệu
                dbObj.SaveChanges();

                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết (sử dụng thư viện log như NLog hoặc Serilog)
                TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình cập nhật sản phẩm. Vui lòng thử lại.";
                return RedirectToAction("Edit", new { id = objOrder.Id });
            }
        }
    }
}
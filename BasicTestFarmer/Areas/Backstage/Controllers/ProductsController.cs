using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicTestFarmer.Models;

namespace BasicTestFarmer.Areas.Backstage.Controllers
{
    public class ProductsController : Controller
    {
         FarmerEntities db = new FarmerEntities();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        //產品資訊頁面
        public ActionResult Product()
        {
            var proList = db.Product.OrderBy(p => p.ProductID);
            return View(proList);
        }

        //修改產品
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var editProList = db.Product.Where(p => p.ProductID == id).FirstOrDefault();
            return View(editProList);
        }

        //修改產品資訊
        [HttpPost]
        public ActionResult EditProduct(Product _product,int ProductID,string Shelves, HttpPostedFileBase ProdouctImage, SubProductCategories subProductCategory, string categoryM)
        {
            var editProList = db.Product.Where(p => p.ProductID == ProductID).FirstOrDefault();
            var db_cate = db.SubProductCategories.Where(p => p.ProductID == _product.ProductID);


            if (Request.Form.Count > 0)
            {
                if (Shelves == "1")
                {
                    editProList.ShelvesStateID = 1;
                }
                else if (Shelves == "2")
                {
                    editProList.ShelvesStateID = 2;
                }
                else
                {
                    editProList.ShelvesStateID = 3;
                }
                if (ProdouctImage != null)
                {
                    //把圖片轉成2進制
                    var imgSize = ProdouctImage.ContentLength;
                    byte[] imgByte = new byte[imgSize];
                    ProdouctImage.InputStream.Read(imgByte, 0, imgSize);
                    editProList.ProducltImage = imgByte;
                }
                editProList.ProductName = _product.ProductName;
                editProList.Quantity = _product.Quantity;
                editProList.Price = _product.Price;
                editProList.Description = _product.Description;
                //editProList.PublishDate = _product.PublishDate;
                //editProList.supplierID = Convert.ToInt32(Request.Cookies["UserID"].Value);

                //修改多類別的中分類
                db_cate.First().ProductCategoriesMID = Convert.ToInt32(categoryM);
                db_cate.First().ProductID = _product.ProductID;
                db.SaveChanges();
            }

            return RedirectToAction("Product");
        }

        //刪除產品
        public ActionResult DelProduct(int id)
        {
            var del = db.SubProductCategories.Where(p => p.ProductID == id).FirstOrDefault();
            db.SubProductCategories.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Product");
        }

        //取得圖片
        public ActionResult GetImageByte(int id = 1)
        {
            Product products = db.Product.Find(id);
            byte[] img = products.ProducltImage;
            return File(img, "image/jpg");
        }

        //產品上下架_新增產品GET
        [HttpGet]
        public ActionResult CreateProduct()
        {
            var categories = db.ProductCategoriesM.Select(c => new
            {
                c.ProductCategoriesMID,
                c.CProductCategoriesMName
            });
            ViewBag.category = new SelectList(categories, "ProductCategoriesMID", "CProductCategoriesMName");
            return View();
        }


        //產品上下架_新增產品POST
        //根據Shelves狀態來修改編排
        [HttpPost]
        public ActionResult CreateProduct(Product _product, string Shelves, HttpPostedFileBase ProductImage, SubProductCategories subProductCategory, string categoryM)
        {
            if (Request.Form.Count > 0)
            {
                if (Shelves == "1")
                {
                    _product.ShelvesStateID = 1;
                }
                else if (Shelves == "2")
                {
                    _product.ShelvesStateID = 2;
                }
                else
                {
                    _product.ShelvesStateID = 3;
                }
                if (ModelState.IsValid)
                {
                    if (ProductImage != null)
                    {
                        //把圖片轉成2進制
                        var imgSize = ProductImage.ContentLength;
                        byte[] imgByte = new byte[imgSize];
                        ProductImage.InputStream.Read(imgByte, 0, imgSize);
                        _product.ProducltImage = imgByte;
                    }
                    else
                    {
                        ViewBag.message = "請選擇檔案!!!";
                    }
                }
                db.Product.Add(_product);
                if (categoryM != null)
                {
                    subProductCategory.ProductCategoriesMID = Convert.ToInt32(categoryM);
                    subProductCategory.ProductID = _product.ProductID;
                    db.SubProductCategories.Add(subProductCategory);
                    db.SaveChanges();
                }
                return RedirectToAction("Product");
            }
            return View();
        }
    }
}
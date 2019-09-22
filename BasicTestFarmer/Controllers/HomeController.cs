using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicTestFarmer.Models;
using BasicTestFarmer.Viewmodel;

namespace BasicTestFarmer.Controllers
{
    public class HomeController : Controller
    {
        FarmerEntities db = new FarmerEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //大類別資訊
        public ActionResult ProductsJson(int id = 1, string sortExpr = "asc")
        {
            var products = db.Product.Where(p => p.SubProductCategories.FirstOrDefault()
              .ProductCategoriesM.ProductCategoriesID == id).GroupJoin(db.ProductSaleslist, c => c.ProductID, s => s.ProductID, (c, s) => new
              {
                  ProdcutID = c.ProductID,
                  ProductName = c.ProductName,
                  Price = c.Price,
                  Description = c.Description,
                  SalesPrice = s.Select(x => x.SalesPrice).FirstOrDefault()
              }).Select(p => new
              {
                  p.ProdcutID,
                  p.ProductName,
                  p.Price,
                  p.Description,
                  p.SalesPrice
              });
            //}).AsQueryable();
            if (sortExpr == "asc")
            {
                products = products.OrderBy(o => o.Price);
            }
            else
            {
                products = products.OrderByDescending(o => o.Price);
            }
            return Json(products.ToList(), JsonRequestBehavior.AllowGet);
        }

        //中類別資訊
        public ActionResult ProductsJsonM(int id = 1, string sortExpr = "asc")
        {
            var products = db.Product.Where(p => p.SubProductCategories.FirstOrDefault()
              .ProductCategoriesMID == id).Select(p => new
              {
                  p.ProductID,
                  p.ProductName,
                  p.Price,
                  p.Description
              });
            if (sortExpr == "asc")
            {
                products = products.OrderByDescending(o => o.Price);
            }
            else
            {
                products = products.OrderByDescending(o => o.Price);
            }
            return Json(products.ToList(), JsonRequestBehavior.AllowGet);
        }


        //大類別搜尋PartialView
        public ActionResult CategoriesPartial()
        {
            var Category = db.ProductCategory.Select(p => new catepro{ ProductCategoryID= p.ProductCategoryID
                , CProductCategoryName= p.CProductCategoryName }).ToList();
            var CategroyM = db.ProductCategoriesM.Select(p => new cateproM{
               ProductCategoriesMID= p.ProductCategoriesMID,
                CProductCategoriesMName= p.CProductCategoriesMName,
                ProductCategoriesID=p.ProductCategoriesID
            }).ToList();
            var cate = new cateList { Category= Category, CategroyM= CategroyM };
            return PartialView(cate);
        }



        //大類別搜尋Json
        public ActionResult CategoriesJson()
        {
            var Category = db.ProductCategory.Select(p => new { p.ProductCategoryID, p.CProductCategoryName });
            var CategroyM = db.ProductCategoriesM.Select(p => new {
                p.ProductCategoriesMID,
                p.CProductCategoriesMName,
                p.ProductCategoriesID
            });
            var cate = new { Category, CategroyM };
            return Json(cate, JsonRequestBehavior.AllowGet);
        }
        //中類別
        public ActionResult CategoriesJsonM(int id = 2)
        {
            var CategroyM = db.ProductCategoriesM.Select(p => new
            {
                p.ProductCategoriesMID,
                p.CProductCategoriesMName,
                p.SubProductCategories.FirstOrDefault().Product.Price,
                sales = p.SubProductCategories.FirstOrDefault().Product.ProductSaleslist.FirstOrDefault().SalesPrice.ToString()
            });
            return Json(CategroyM, JsonRequestBehavior.AllowGet);
        }

        //使用Ajax
        //顯示大類別產品
        public ActionResult Getproduct(string keyword,int cateid=1)
        {
            var getProduct = db.Product.Select(p => new Det { ProductName = p.ProductName,
                    Price = p.Price, Description = p.Description, ProductID = p.ProductID }).ToList();
            //ViewBag.ProName = getProduct.ToList();
            return View(getProduct);
        }



        public ActionResult GetProductsJson(int Category =1,int CategoryM=0)
        {
            
            return View();
        }

        //過濾產品
        public ActionResult Filterproduct(int? pricemin, int? pricemax, int[] categories)
        {
            bool cate = false;
            if (categories[0] == 0)
            {
                cate = true;
            }
            var products = db.Product.Where(p => (categories.Contains((int)p.SubProductCategories.FirstOrDefault()
                  .ProductCategoriesM.ProductCategoriesID) || cate) &&
                  (!pricemin.HasValue || p.Price >= pricemin) && (!pricemax.HasValue || p.Price <= pricemax));
            return View();
        }

        //關鍵字查詢產品
        public ActionResult Searchproductview(string keyword)
        {
            return View(db.Product.Where(p => p.ProductName.Contains(keyword) || p.Description.Contains(keyword)).ToList());
        }

        //價錢拉條查詢
        public JsonResult FetchProductPriceRange()
        {
            var productPrices = db.Product.OrderBy(o => o.Price).Select(o => o.Price.Value).ToList();
            int min = productPrices[0];
            int max = productPrices[productPrices.Count - 1];
            return Json(new { Max = max, Min = min }, JsonRequestBehavior.AllowGet);
        }

        //產品敘述
        public ActionResult ProductRendition(int id = 1)
        {
            var products = db.Product.Where(p => p.ProductID == id).Select(p => new ProductRendition
            {
                productID = id,
                ProductName = p.ProductName,
                Price = p.Price,
                saleprice = (p.ProductSaleslist.FirstOrDefault().StartDate
                < DateTime.Now && p
                 .ProductSaleslist.FirstOrDefault().EndDate > DateTime.Now)
                 ? p.ProductSaleslist.FirstOrDefault().SalesPrice : 0,
                UserName = p.Member.UserName,
                Description = p.Description,
                Quantity = (int)p.Quantity,
                Supplier = p.Member.verify.FirstOrDefault().VerifyDetial.FirstOrDefault().FarmName
                
            }).ToList();
            List<SelectListItem> quantitySelection = new List<SelectListItem>();
            for (int i = 1; i <= products.First().Quantity; i++)
            {
                if (i % 21 == 0)
                    break;
                quantitySelection.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            TempData["QuantitySelection"] = quantitySelection;
            return View(products.ToList());
        }

        //取得圖片
        public ActionResult GetImageByte(int id = 1)
        {
            Product products = db.Product.Find(id);
            byte[] img = products.ProducltImage;
            return File(img, "image/jpg");
        }

        //購物車介面
        public ActionResult ShoppingCar(int id=0)
        {
                var shopping = db.Shopping.Select(s => new shoplist
                {
                    ShoppingID=s.ShoppingID,
                   ProductName=s.Product.ProductName,
                    ProductID = s.ProductID,
                    Price = (s.Product.ActivitIDetail.FirstOrDefault()
                                  .StateDate < DateTime.Now && s.Product.ActivitIDetail
                                  .FirstOrDefault().EndDate > DateTime.Now) ? (int)s.Product.ActivitIDetail
                                  .FirstOrDefault().SalePrice : (int)s.Product.Price,
                    TrueQuantity = (int)s.Product.Quantity,
                    Quantity = s.Quantity,
                    total = (s.Product.ActivitIDetail
                                  .FirstOrDefault().StateDate < DateTime.Now && 
                                  s.Product.ActivitIDetail.FirstOrDefault().EndDate > DateTime.Now)
                                  ? s.Quantity * s.Product.ActivitIDetail.FirstOrDefault()
                                  .SalePrice : s.Quantity * s.Product.Price
                });
            return View(shopping.ToList());
        }

        //加入購物車
        public ActionResult ShoppingCarAdd(int pid=1,int Quantity=1)
        {
            var Addcar = db.Product.Where(p => p.ProductID == pid).Select(p => new carList
            {
                productID = p.ProductID,
                productName = p.ProductName,
                quantity = p.Quantity,
                price = p.Price
            }).First();
            
           var Addcarlist= db.Shopping.Add( new Shopping() {
                ProductID=Addcar.productID,
                Quantity=Quantity
            });
            db.Shopping.Add(Addcarlist);
            db.SaveChanges();
            return RedirectToAction("ShoppingCar","Home");
        }

        //刪除購物車
        public ActionResult ShoppingCarDelete(int id)
        {
            db.Shopping.Remove(db.Shopping.Find(id));
            db.SaveChanges();
            return View("ShoppingCar");
        }

    }
}
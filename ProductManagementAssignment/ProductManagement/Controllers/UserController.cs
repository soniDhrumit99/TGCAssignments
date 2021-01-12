using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProductManagement.Context;
using ProductManagement.Models;

/*--------------------------------   MVC Controller for User Interaction   -------------------------------------------*/

namespace ProductManagement.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ProductDbContext db = new ProductDbContext();
        private static readonly Logger logger = LogManager.GetLogger("MVCLogger");

        [HttpGet]
        [AllowAnonymous]
        //GET: /User/Login
        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //POST: /User/Login
        public ActionResult Login(Accounts account)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("loginError", "Incorrect Username or Password");
                return View();
            }
            else
            {
                try
                {
                    var result = db.Accounts.Any(x => x.Username.Equals(account.Username, StringComparison.OrdinalIgnoreCase) && x.Password == account.Password);
                    if(result)
                    {
                        var user = db.Accounts.Single(x => x.Username.Equals(account.Username, StringComparison.OrdinalIgnoreCase) && x.Password == account.Password); ;
                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        logger.Info($"User {user.Username} logged in at {DateTime.UtcNow:yyyy-MM-dd:hh-mm-ss.fff}");
                        if (!TempData.ContainsKey("activeUser"))
                            TempData.Add("activeUser", user);           // Adding active user to tempdata for referencing in User/dashboard
                        else
                            TempData["activeUser"] = user;
                        TempData.Keep("activeUser");
                        return RedirectToAction("dashboard", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("loginError", "Incorrect Username or Password");
                        return View();
                    }
                }
                catch(Exception e)
                {
                    ViewBag.Error = e;
                    logger.Error(e, "Error in database");
                    return View("Error");
                }
            }
        }

        [HttpGet]
        //GET: /User/Dashboard
        public ActionResult Dashboard()
        {
            ViewBag.Title = "Dashboard";
            try
            {
                if (TempData.ContainsKey("activeUser"))
                {
                    var user = ((Accounts)TempData.Peek("activeUser"));
                    ViewBag.username = user.Username;   //Passing username to the View.
                    return View();
                }
                else
                {
                    throw new HttpException(500, "Internal Server Error. Please try after sometime or contact the developer.");
                }
            }
            catch(Exception e)
            {
                ViewBag.Error = e;
                logger.Error(e, "Error in TempData");
                return View("Error");
            }
            
        }

        [HttpGet]
        //GET: /User/Products
        public async Task<ActionResult> Products()
        {
            ViewBag.Title = "Products";
            try
            {
                Products productModel = new Products();
                List<Categories> categories = db.Categories.ToList<Categories>();
                productModel.Categories = categories;           // Populating productModel with list of categories for showing in table.
                if(!TempData.ContainsKey("emptyProductModel"))
                    TempData.Add("emptyProductModel", productModel); // For future reference.
                TempData.Keep("emptyProductModel");
                var resp = await GlobalVariables.client.GetAsync("Products");
                if (resp.IsSuccessStatusCode)
                {
                    IEnumerable<Products> products = resp.Content.ReadAsAsync<IEnumerable<Products>>().Result;
                    foreach(var x in products)
                    {
                        x.ImagePath = GlobalVariables.imagePath.Replace('~', ' ') + x.ImagePath;
                        x.Categories = categories;
                    }
                    return View(products);
                }
                else
                {
                    logger.Error($"Error while getting product list. Response Status Code {resp.StatusCode} with message {resp.ReasonPhrase}");
                    throw new HttpException(Convert.ToInt32(resp.StatusCode), resp.ReasonPhrase.ToString());
                }
            }
            catch(Exception e)
            {
                logger.Error(e, "Error with database or Api");
                ViewBag.Error = e;
                return View("Error");
            }
        }

        [HttpGet]
        //GET: /User/AddProduct
        public async Task<ActionResult> AddProduct()
        {
            ViewBag.Title = "AddProduct";
            try
            {
                Products productModel = new Products();
                // From here -------------
                if (TempData.ContainsKey("emptyProductModel"))
                {
                    productModel = (Products)TempData.Peek("emptyProductModel");
                }
                else
                {
                    HttpResponseMessage resp = await GlobalVariables.client.GetAsync("Categories");
                    if (resp.IsSuccessStatusCode)
                    {
                        productModel.Categories = resp.Content.ReadAsAsync<List<Categories>>().Result;
                        if (!TempData.ContainsKey("emptyProductModel"))
                            TempData.Add("emptyProductModel", productModel);
                        TempData.Keep("emptyProductModel");
                    }
                    else
                    {
                        logger.Error($"Error while fetching category list. Response Status code {resp.StatusCode} with message {resp.ReasonPhrase}");
                        throw new HttpException(Convert.ToInt32(resp.StatusCode), resp.ReasonPhrase.ToString());
                    }
                }
                // To here ----------- Populating the product model with categories for the dropdown list.
                if (TempData.ContainsKey("editing"))        // Setting the mode to add new product
                {
                    TempData["editing"] = false;
                }
                else
                {
                    TempData.Add("editing", false);
                }
                TempData.Keep("editing");
                ViewBag.Title = "Add Product";
                return View("ProductDetails", productModel);
            }
            catch(Exception e)
            {
                logger.Error(e, "Error with database or Api");
                ViewBag.Error = e;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: /User/AddProduct
        public async Task<ActionResult> AddProduct(Products product)
        {
            if (TempData.ContainsKey("editing"))
            {
                TempData["editing"] = false;
            }
            else
            {
                TempData.Add("editing", false);
            }
            TempData.Keep("editing");
            Products productModel = (Products)TempData.Peek("emptyProductModel");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("modelError", "PLease enter valid details.");
                return View(productModel);
            }
            else
            {
                try
                {
                    string fileExt = Path.GetExtension(product.image.FileName);
                    if (GlobalVariables.validExts.Contains(fileExt))
                    {
                        // From here -----------------
                        string trimmedProductName = "";
                        string[] productName = product.Name.Trim().Split(' ');
                        foreach(string x in productName)
                        {
                            trimmedProductName = String.Concat(trimmedProductName, x.Trim());
                        }
                        string imageFileName = trimmedProductName + "_" + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss-fff") + fileExt;
                        // To here ----------- Making the image name server safe.

                        product.ImagePath = imageFileName;
                        product.image.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.imagePath), imageFileName));
                        product.image = null;
                        HttpResponseMessage resp = await GlobalVariables.client.PostAsJsonAsync("Products", product);
                        if (resp.IsSuccessStatusCode)
                        {
                            logger.Info($"User {((Accounts)TempData.Peek("activeUser")).Username} added a new product with name {product.Name} at {DateTime.UtcNow:yyyy-MM-dd:hh-mm-ss.fff}");
                            if (!TempData.ContainsKey("productAdded"))
                                TempData.Add("productAdded", true);
                            else
                                TempData["productAdded"] = true;
                            return RedirectToAction("Products", "User");
                        }
                        else
                        {
                            logger.Error($"Error while posting the new product. Response status code {resp.StatusCode} with message {resp.ReasonPhrase}");
                            throw new HttpException(Convert.ToInt32(resp.StatusCode), resp.ReasonPhrase.ToString());
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("modelError", "Please upload a valid file.");
                        return View(productModel);
                    }
                }
                catch(Exception e)
                {
                    logger.Error(e, "Error with database or Api");
                    ViewBag.Error = e;
                    return View("Error");
                }
            }
        }


        [HttpGet]
        //GET: /User/EditProduct/{id?}
        public ActionResult EditProduct(int id)
        {
            if (TempData.ContainsKey("editing"))        //Setting the mode to edit product
            {
                TempData["editing"] = true;
            }
            else
            {
                TempData.Add("editing", true);
            }
            TempData.Keep("editing");
            try
            {
                HttpResponseMessage resp = GlobalVariables.client.GetAsync("Products/" + id.ToString()).Result;
                if (resp.IsSuccessStatusCode)
                {
                    Products product = resp.Content.ReadAsAsync<Products>().Result;
                    product.ImagePath = GlobalVariables.imagePath.Replace('~', ' ') + product.ImagePath;   //Changing the image path so that it show in the view
                    product.Categories = ((Products)TempData.Peek("emptyProductModel")).Categories;             //For populating the dropdown list.
                    ViewBag.Title = "Edit Product";
                    if(!TempData.ContainsKey("activeProduct"))                                             //Saving the product that is being edited for future reference in post action.
                        TempData.Add("activeProduct", product);
                    return View("ProductDetails", product);
                }
                else
                {
                    logger.Error($"Error while fethcing details of a  product. Response status code {resp.StatusCode} with message {resp.ReasonPhrase}");
                    throw new HttpException(Convert.ToInt32(resp.StatusCode), resp.ReasonPhrase.ToString());
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Error with database or Api");
                ViewBag.Error = e;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //PUT: /User/EditProduct
        public async Task<ActionResult> EditProduct(Products product)
        {
            if (TempData.ContainsKey("editing"))
            {
                TempData["editing"] = true;
            }
            else
            {
                TempData.Add("editing", true);
            }
            TempData.Keep("editing");
            Products productModel = (Products)TempData.Peek("activeProduct");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("modelError", "Please enter valid details.");
                return View("ProductDetails", productModel);
            }
            else
            {
                try
                {
                    if (product.image == null)          // If image is not changed, just simply save the product to Database.
                    {
                        product.ImagePath = productModel.ImagePath.Replace("/Images/", "").Trim();
                        HttpResponseMessage resp = await GlobalVariables.client.PutAsJsonAsync("Products/" + product.Id, product);
                        if (resp.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            logger.Info($"User {((Accounts)TempData.Peek("activeUser")).Username} edited the product {product.Name} at {DateTime.UtcNow:yyyy-MM-dd:hh-mm-ss.fff}");
                            if (!TempData.ContainsKey("productEdited"))
                                TempData.Add("productEdited", true);
                            else
                                TempData["productEdited"] = true;
                            return RedirectToAction("Products", "User");
                        }
                        else
                        {
                            logger.Error($"Error while updating the product {product.Name}. Response status code {resp.StatusCode} with message {resp.ReasonPhrase}");
                            throw new HttpException(Convert.ToInt32(resp.StatusCode), resp.ReasonPhrase.ToString());
                        }
                    }
                    else                                // Else first save the image first and then save data into Database.
                    {
                        string fileExt = Path.GetExtension(product.image.FileName);
                        if (GlobalVariables.validExts.Contains(fileExt))
                        {
                            string trimmedProductName = "";
                            string[] productName = product.Name.Trim().Split(' ');
                            foreach (string x in productName)
                            {
                                trimmedProductName = String.Concat(trimmedProductName, x.Trim());
                            }
                            string imageFileName = trimmedProductName + "_" + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss-fff") + fileExt;
                            product.ImagePath = imageFileName;
                            product.image.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.imagePath), imageFileName));
                            product.image = null;
                            HttpResponseMessage resp = await GlobalVariables.client.PutAsJsonAsync("Products/" + product.Id, product);
                            if (resp.IsSuccessStatusCode)
                            {
                                logger.Info($"User {((Accounts)TempData.Peek("activeUser")).Username} edited the product {product.Name} at {DateTime.UtcNow:yyyy-MM-dd:hh-mm-ss.fff}");
                                TempData.Add("productEdited", true);
                                return RedirectToAction("Products", "User");
                            }
                            else
                            {
                                logger.Error($"Error while updating the product {product.Name}. Response status code {resp.StatusCode} with message {resp.ReasonPhrase}");
                                throw new HttpException(Convert.ToInt32(resp.StatusCode), resp.ReasonPhrase.ToString());
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("modelError", "Please upload a valid file.");
                            return View(productModel);
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error with database or Api");
                    ViewBag.Error = e;
                    return View("Error");
                }
            }
        }

        //DELETE: /User/DeleteProduct/{id?}
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                HttpResponseMessage resp = await GlobalVariables.client.DeleteAsync("Products/" + id.ToString());
                if (resp.IsSuccessStatusCode)
                {
                    Products product = resp.Content.ReadAsAsync<Products>().Result;
                    logger.Info($"User {((Accounts)TempData.Peek("activeUser")).Username} deleted the product {product.Name} at {DateTime.UtcNow:yyyy-MM-dd:hh-mm-ss.fff}");
                    if (!TempData.ContainsKey("productDeleted"))
                        TempData.Add("productDeleted", true);
                    else
                        TempData["productDeleted"] = true;
                    return RedirectToAction("Products", "User");
                }
                else
                {
                    throw new HttpException(Convert.ToInt32(resp.StatusCode), resp.ReasonPhrase.ToString());
                }
            }
            catch(Exception e)
            {
                ViewBag.Error = e;
                return View("Error");
            }
        }

        [HttpGet]
        //GET: /User/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            if (!TempData.ContainsKey("freshLogout"))
                TempData.Add("freshLogout", true);
            else
                TempData["freshLogout"] = true;
            TempData.Keep("freshLogout");
            logger.Info($"User {((Accounts)TempData.Peek("user")).Username} logged out at {DateTime.UtcNow:yyyy-MM-dd:hh-mm-ss.fff}");
            return RedirectToAction("Login", "User");
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
using System.Web.Mvc;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagement;
using ProductManagement.Controllers;
using System.Threading.Tasks;
using ProductManagement.Models;

namespace ProductManagement.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Login()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewBag.Title);
        }

        [TestMethod]
        public void Dashboard()
        {
            UserController controller = new UserController();
            ViewResult result = controller.Dashboard() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Dashboard", result.ViewBag.Title);
        }

        [TestMethod]
        public async Task Products()
        {
            UserController controller = new UserController();
            ViewResult result = await controller.Products() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Products", result.ViewBag.Title);
        }

        [TestMethod]
        public async Task AddProduct()
        {
            UserController controller = new UserController();
            /*Products product = new Products();
            product.Name = "Test Product";
            product.ImagePath = "Test path";
            product.Price = 123.123M;
            product.Quantity = 5;*/
            ViewResult result = await controller.AddProduct() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Add Product", result.ViewBag.Title);
            Assert.AreEqual("ProductManagement.Models.Products", result.Model.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Testing_Assignment_2.Controllers;

namespace Testing_Assignment_2.Tests.Controllers
{
    public class DefaultControllerTest
    {
        private readonly DefaultController _defaultController;

        public DefaultControllerTest()
        {
            _defaultController = new DefaultController();
        }

        [Fact]
        public void Test_ToLowerCase()
        {
            var input = "SOME UPPER CASE STRING";
            var expectedOutput = "some upper case string";
            var response = _defaultController.ToLowerCase(input);
            Assert.Equal(expectedOutput, response.Content);
        }

        [Fact]
        public void Test_ToUpperCase()
        {
            var input = "some upper case string";
            var expectedOutput = "SOME UPPER CASE STRING";
            var response = _defaultController.ToUpperCase(input);
            Assert.Equal(expectedOutput, response.Content);
        }

        [Fact]
        public void Test_ToTitleCase()
        {
            var input = "some UPPER case string";
            var expectedOutput = "Some UPPER Case String";
            var response = _defaultController.ToTitleCase(input);
            Assert.Equal(expectedOutput, response.Content);
        }

        [Theory]
        [InlineData("some lower UPPER case string", false)]
        [InlineData("fully lowercase string", true)]
        public void Test_InLowerCase(string input, bool expectedOutput)
        {
            var response = _defaultController.InLowerCase(input);
            Assert.Equal(expectedOutput, response.Content);
        }

        [Theory]
        [InlineData("some lower UPPER case string", false)]
        [InlineData("FULLY UPPERCASE STRING", true)]
        public void Test_InUpperCase(string input, bool expectedOutput)
        {
            var response = _defaultController.InUpperCase(input);
            Assert.Equal(expectedOutput, response.Content);
        }

        [Theory]
        [InlineData("36fs", false)]
        [InlineData("657", true)]
        public void Test_IsInt(string input, bool expectedOutput)
        {
            var response = _defaultController.IsInt(input);
            Assert.Equal(expectedOutput, response.Content);
        }

        [Fact]
        public void Test_RemoveLastCharacter()
        {
            var input = "some lower UPPER case string";
            var expectedOutput = "some lower UPPER case strin";
            var response = _defaultController.RemoveLastCharacter(input);
            Assert.Equal(expectedOutput, response.Content);
        }

        [Fact]
        public void Test_WordCount()
        {
            var input = "some lower UPPER case string";
            var expectedOutput = 5;
            var response = _defaultController.WordCount(input);
            Assert.Equal(expectedOutput, response.Content);
        }

        [Theory]
        [InlineData("234", 234)]
        [InlineData("563", 563)]
        public void Test_ToInt(string input, int expectedOutput)
        {
            var response = _defaultController.ToInt(input);
            Assert.Equal(expectedOutput, response.Content);
        }
    }
}

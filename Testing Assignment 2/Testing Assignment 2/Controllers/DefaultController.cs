using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Testing_Assignment_2.Controllers
{
    public class DefaultController : ApiController
    {
        public JsonResult<string> ToLowerCase(string input)
        {
            return Json(input.ToLower());
        }

        public JsonResult<string> ToUpperCase(string input)
        {
            return Json(input.ToUpper());
        }

        public JsonResult<string> ToTitleCase(string input)
        {
            TextInfo text = new CultureInfo("en-us", false).TextInfo;
            return Json(text.ToTitleCase(input));
        }

        public JsonResult<bool> InLowerCase(string input)
        {
            var isLower = true;
            foreach(var letter in input)
            {
                var ascii = (int)letter;
                if(!(ascii >= 97 && ascii <= 122) && (ascii != 32))
                {
                    isLower = false;
                }
            }
            return Json(isLower);
        }

        public JsonResult<bool> InUpperCase(string input)
        {
            var isLower = true;
            foreach (var letter in input)
            {
                var ascii = (int)letter;
                if (!(ascii >= 65 && ascii <= 90) && (ascii != 32))
                {
                    isLower = false;
                }
            }
            return Json(isLower);
        }

        public JsonResult<bool> IsInt(string input)
        {
            try
            {
                var x = int.Parse(input);
                return Json(true);
            } catch(Exception)
            {
                return Json(false);
            }
        }

        public JsonResult<string> RemoveLastCharacter(string input)
        {
            return Json(input.Substring(0, input.Length - 1));
        }

        public JsonResult<int> WordCount(string input)
        {
            return Json(input.Split(' ').Length);
        }

        public JsonResult<int> ToInt(string input)
        {
            try
            {
                var x = int.Parse(input);
                return Json(x);
            }
            catch (Exception)
            {
                return Json(-19191);
            }
        }
    }
}

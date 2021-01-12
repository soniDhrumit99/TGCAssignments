using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ProductManagement.Models
{
    public class Products
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [DataType(DataType.Text, ErrorMessage = "Please enter a valid product name")]
        [StringLength(50, ErrorMessage = "Name length cannot be more than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is a required field.")]
        public int Category { get; set; }

        [NotMapped]
        public List<Categories> Categories { get; set; }


        [Required(ErrorMessage = "Price is a required field.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is a required field.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [DataType(DataType.MultilineText, ErrorMessage = "Please enter valid description.")]
        [StringLength(200, ErrorMessage = "200 characters limit.")]
        public string Description { get; set; }

        [DisplayName("Image")]
        public string ImagePath {get; set;}

        [NotMapped]
        public HttpPostedFileBase image { get; set; }
    }
}
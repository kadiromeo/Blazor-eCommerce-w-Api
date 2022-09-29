using System.ComponentModel.DataAnnotations;
using System;

namespace Blazor_eCommerce_Project.Models
{
    public class CourseDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Must be have fill...!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price Must be have fill...!")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Must be selected Is-Active...!")]
        public bool IsActive { get; set; }
        
        [Required(ErrorMessage = "Description Must be have fill...!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Title Must be have fill...!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Title Must be have fill...!")]

        public string SubTitle { get; set; }
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ImgUrl { get; set; }
        public int TotalCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNS.WebAPI.Models
{
    public class StudyBibleDaily
    {
        [Key]
        public int Date { get; set; }
        public string Content { get; set; }
    }
    public class StudyBibleMonthly
    {
        public int Id { get; set; }
        public Month Month { get; set; }
        public List<StudyBibleDaily> Daily { get; set; } 
    }


    public enum Month
    {
        [Display(Name = "Tháng Một")]
        Jan = 1,
        [Display(Name = "Tháng Hai")]
        Feb = 2,
        [Display(Name = "Tháng Ba")]
        Mar = 3,
        [Display(Name = "Tháng Tư")]
        Apr = 4,
        [Display(Name = "Tháng Năm")]
        May = 5,
        [Display(Name = "Tháng Sáu")]
        Jun = 6,
        [Display(Name = "Tháng Bảy")]
        Jul = 7,
        [Display(Name = "Tháng Tám")]
        Aug = 8,
        [Display(Name = "Tháng Chín")]
        Sep = 9,
        [Display(Name = "Tháng Mười")]
        Oct = 10,
        [Display(Name = "Tháng Mười Một")]
        Nov = 11,
        [Display(Name = "Tháng Mười Hai")]
        Dec = 12
    }
}
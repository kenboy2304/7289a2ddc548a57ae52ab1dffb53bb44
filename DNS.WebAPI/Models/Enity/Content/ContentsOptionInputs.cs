using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DNS.WebAPI.Models.Enity.Content
{
    public class ContentsOptionInputs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1), Key]
        public int Id { get; set; }
        [Column(Order = 2), ForeignKey("ContentType"), Key]
        public int ContentTypeId { get; set; }
        [Column(Order = 3), ForeignKey("TypeInput"), Key]
        public int TypeInputId { get; set; }
        public string Label { get; set; }
        [Display(Name = "Giá trị mặc định ")]
        public string DefaultValue { get; set; }
        [Display(Name = "Tùy chỉnh giao diện quản lý")]
        public string CustomManagerTemplate { get; set; }
        [Display(Name = "Tùy chỉnh giao diện hiển thị ")]
        public string CustomDisplayTemplate { get; set; }
        public ContentType ContentType { get; set; }
        public TypeInput TypeInput { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNS.WebAPI.Models.Enity.Settings
{
    public partial class BroadcastSchedule
    {
        public int Id { get; set; }
        public DayBroadcast Day { get; set; }
        public List<string> Broadcast { get; set; }

        
    }

    public partial class BroadcastSchedule
    {
 
    }

    public enum DayBroadcast
    {
        [Display(Name = "Chủ Nhật")]
        Su,
        [Display(Name = "Thứ Hai")]
        Mo,
        [Display(Name = "Thứ Ba")]
        Tu,
        [Display(Name = "Thứ Tư")]
        We,
        [Display(Name = "Thứ Năm")]
        Th,
        [Display(Name = "Thứ Sáu")]
        Fr,
        [Display(Name = "Thứ Bảy")]
        Sa
    }
}
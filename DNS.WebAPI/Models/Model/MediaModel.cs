using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DNS.WebAPI.Models
{
    public class MediaModel
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string MediaUrl { get; set; }
        public DateTime PulishDate { get; set; }
        public bool IsError { get; set; }

        public string GetUrlKeyword()
        {
            return ConvertToUnsign3(Name) + "-" + PulishDate.ToLocalTime().ToString("dMyyyy");
        }

        public static string ConvertToUnsign3(string str)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var temp = str.Normalize(NormalizationForm.FormD);
            temp = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            temp = Regex.Replace(temp, @"\W+", " ");
            temp = new Regex(@"\s+").Replace(temp, " ");
            temp = new Regex(@"^\s+|\s+$").Replace(temp, "");
            temp = Regex.Replace(temp, @"\s", "-");
            return temp.ToLower();
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;

namespace DNS.GetDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            var path = Directory.GetCurrentDirectory() + "\\data.txt";
            var file =new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                line = line.Remove(line.Length - 1);
                
                var obj = new JavaScriptSerializer().Deserialize<MediaModel>(line);
                Console.WriteLine(obj.PulishDate.ToLocalTime());
                using (var db = new DNSContext())
                {
                    Catalog catalog;
                    if (!db.Catalogs.Any(c => c.Name.Equals(obj.CategoryId)))
                    {
                        catalog = new Catalog
                        {
                            Name = obj.CategoryId,
                            UrlKeyWord = ConvertToUnsign3(obj.CategoryId),
                            Publish = true,
                            DateCreated = DateTime.Now,
                            Title = obj.CategoryId,
                            CatalogParentId = 2,
                            ShowInMainMenu = true,
                            AcceptDownload = true

                        };
                        db.Catalogs.Add(catalog);
                        db.SaveChanges();
                    }
                        //khong biet
                    else
                    {
                        catalog = db.Catalogs.Single(c => c.Name.Equals(obj.CategoryId));
                    }
                    var article = new Article
                    {
                        Name = obj.Name,

                        DateCreated = obj.PulishDate.ToLocalTime(),
                        PublishedDate = obj.PulishDate.ToLocalTime(),
                        UrlKeyword = ConvertToUnsign3(obj.Name) +"-"+ obj.PulishDate.ToLocalTime().ToString("dMyyyy"),
                        Mp3Url = obj.MediaUrl,
                        Mp3Error = obj.IsError,
                        Catalogs = new List<Catalog> { catalog },
                        Type = ArticleType.Article,
                        Title = obj.Name,
                        Publish = true
                    };
                    if (db.Articles.Any(a => a.Name == article.Name && a.PublishedDate == article.PublishedDate))
                        continue;
                    Console.WriteLine(article.PublishedDate);
                    db.Articles.Add(article);
                    db.SaveChanges();
                }

            }
            file.Close();
            Console.ReadLine();
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

        public class MediaModel
        {
            public string Name { get; set; }
            public string CategoryId { get; set; }
            public string MediaUrl { get; set; }
            public DateTime PulishDate { get; set; }
            public bool IsError { get; set; }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using DNS.WebAPI.Models.Enity.Settings;

namespace DNS.WebAPI
{
    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DNSContext>
    {
        protected override void Seed(DNSContext context)
        {
            //Content Type
            var typeInput = new List<TypeInput>
            {
                new TypeInput {Name = "Text"},
                new TypeInput {Name = "Text Area"},
                new TypeInput {Name = "Html"},
                new TypeInput {Name = "Image"},
                new TypeInput {Name = "Mp3"},
                new TypeInput {Name = "Youtube"},
            };
            context.TypeInputs.AddRange(typeInput);
            context.SaveChanges();

            context.Catalogs.AddRange(new List<Catalog>
            {
                new Catalog
                {
                    Name = "Trang chủ",
                    Url = "/",
                    ShowInMainMenu = true,
                    Publish = true,
                    DateCreated = DateTime.Now,
                    CatalogParentId = null,
                },
                new Catalog
                {
                    Name = "Chương trình",
                    Url = "#",
                    ShowInMainMenu = true,
                    Publish = true,
                    DateCreated = DateTime.Now,
                    CatalogParentId = null

                },
                new Catalog
                {
                    Name = "Tài liệu",
                    Url = "#",
                    ShowInMainMenu = true,
                    Publish = true,
                    DateCreated = DateTime.Now,
                    CatalogParentId = null
                },
                new Catalog
                {
                    Name = "Nguồn sống",
                    Url = "/",
                    ShowInMainMenu = true,
                    Publish = true,
                    DateCreated = DateTime.Now,
                    CatalogParentId = null
                },
            });

            context.SaveChanges();
            #region Catalog Default
            var cat = "Thờ Phượng Giảng Luận,Học Kinh Thánh,Niềm Tin Và Cuộc Sống,Ca Khúc Tâm Linh,Lời Hằng Sống,Niềm Hy Vọng,Giờ Phụ Nữ,Lẽ Thật Cho Đời Sống,Sống Đạo Gia Đình,Sống Đạo Tăng Trưởng,Sống Đạo Áp Dụng,Văn Phẩm Cơ Đốc,Niềm Tin Minh Họa,Làm Môn Đệ Chúa,Chương Trình Hôm Nay".Split(',');
            var catalogs = new List<string[]>();
            catalogs.Add(new[]
            {
                "Thờ Phượng Giảng Luận","Chương trình Thờ Phượng - Giảng Luận suy tôn, ca ngợi Chúa qua những bài thánh ca và trình bày về lẽ thật về Đức Chúa Trời qua phần đọc Kinh Thánh và giảng giải Lời Chúa."
            });
            catalogs.Add(new[]
            {
                "Học Kinh Thánh","Chương trình Học Kinh Thánh giúp quý thính giả hiểu được chân lý của Chúa để sống với chân lý đó và trở nên người truyền rao ơn cứu rỗi cho đồng loại."
            });
            catalogs.Add(new[]
            {
                "Niềm Tin Và Cuộc Sống","Chương trình Niềm Tin và Cuộc Sống chia sẻ những bài học Kinh Thánh về niềm tin Cơ Đốc và như thế nào niềm tin đó ảnh hưởng đến đời sống của chúng ta."
            });
            catalogs.Add(new[]
            {
                "Ca Khúc Tâm Linh","Chương trình Ca Khúc Tâm Linh trình bày những bài hát ca ngợi Chúa thể hiện đức tin, hy vọng và tình yêu với Chúa trong cuộc sống Cơ Đốc."
            });
            catalogs.Add(new[]
            {
                "Lời Hằng Sống","Chương trình Lời Hằng Sống giảng giải Kinh Thánh theo từng sách, đoạn nhằm mục đích đem Lời Hằng Sống của Đức Chúa Trời được mặc khải trong Kinh Thánh về Con Ngài là Chúa Cứu Thế Giê-xu và chương trình cứu rỗi đời đời của Ngài đối với mỗi chúng ta để chúng ta có thể biết Ngài, kính yêu Ngài, thờ phượng Ngài và phục vụ Ngài."
            });

            catalogs.Add(new[]
            {
                "Niềm Hy Vọng","Chương trình Niềm Hy Vọng nhằm mục đích đem đến cho quý thính giả hy vọng trong Chúa Cứu Thế Giê-xu là Con Đức Chúa Trời đã chịu chết để chuộc tội cho chúng ta, nhưng cũng đã sống lại để ban cho chúng ta sự sống tâm linh hôm nay và trong cả cõi vĩnh hằng."
            });
            catalogs.Add(new[]
            {
                "Giờ Phụ Nữ","Qua chương trình Giờ Phụ Nữ chúng ta sẽ học về những nguyên tắc Kinh Thánh dạy liên quan đến vai trò của người phụ nữ trong gia đình và Hội Thánh. Chúng tôi cũng sẽ chia sẻ những nghiên cứu, học hỏi đem lại hữu ích cho đời sống người phụ nữ trước những trách nhiệm trong đời sống bận rộn ngày hôm nay."
            });
            catalogs.Add(new[]
            {
                "Lẽ Thật Cho Đời Sống","Chương trình Lẽ Thật Cho Đời Sống đem lẽ thật của Đức Chúa Trời trong Kinh Thánh để áp dụng cho đời sống đức tin hằng ngày, hầu mở đường cho những ai chưa nhận biết Chúa Cứu Thế Giê-xu có thể đi đến chỗ tin nhận Chúa để được tha tội và được sự sống đời đời; và để gây dựng con dân Chúa được trưởng thành trong đức tin để sống hữu hiệu cho danh Chúa Cứu Thế Giê-xu phục vụ mục đích đời đời của Nước Đức Chúa Trời."
            });
            catalogs.Add(new[]
            {
                "Sống Đạo Áp Dụng","Chương trình Sống Đạo Áp Dụng trình bày những nguyên tắc Kinh Thánh để quý thính giả hiểu biết về luật lệ thánh của Chúa hầu giúp quý vị có thể áp dụng cho đời sống dức tin hằng ngày."
            });
            catalogs.Add(new[]
            {
                "Sống Đạo Gia Đình","Chương trình Sống Đạo Gia Đình cung cấp cho quý thính giả những bài học về hôn nhân , gia đình, dạy dỗ con cái trong ánh sáng của Lời Chúa hầu giúp cho đời sống gia đình được đẹp lòng Chúa và ích lợi cho công việc nhà Ngài."
            });
            catalogs.Add(new[]
            {
                "Sống Đạo Tăng Trưởng","Chương trình Sống Đạo Tăng Trưởng đem đến cho quý thính giả những bài học thực tiễn để quý vị có thể tăng trưởng trong sự nhận biết Chúa Cứu Thế và áp dụng các nguyên tắc Kinh Thánh vào trong nếp sống mỗi ngày."
            });
            catalogs.Add(new[]
            {
                "Văn Phẩm Cơ Đốc","Chương trình Văn Phẩm Cơ Đốc giới thiệu đến quý thính giả những quyển sách bồi linh, truyện Cơ Đốc nhằm cung cấp những kiến thức Kinh Thánh bổ ích trong đời sống theo Chúa."
            });
            catalogs.Add(new[]
            {
                "Niềm Tin Minh Họa","Chương trình Niềm Tin Minh Họa trình bày những mẩu truyện ngắn, lời chứng của người theo Chúa, minh họa quyền năng biến đổi của Tin Lành Chúa Cứu Thế Giê-xu."
            });
            catalogs.Add(new[]
            {
                "Làm Môn Đệ Chúa",""
            });

            //foreach (var c in catalogs)
            //{
            //    context.Catalogs.Add(new Catalog
            //    {
            //        Name = c[0],
            //        Desc = c[1],
            //        ShowInMainMenu = true,
            //        CatalogParentId = 2,
            //        DateCreated = DateTime.Now,
            //        Publish = true,
            //    });
            //    context.SaveChanges();
            //} 
            #endregion




            //Settings
            Setting setting;

            #region Setting BroadcastDay
            foreach (DayBroadcast day in Enum.GetValues(typeof(DayBroadcast)))
            {
                var bs = new BroadcastSchedule { Day = day };
                setting = new Setting { Name = typeof(BroadcastSchedule).Name };
                setting.SetValue(bs);
                context.Settings.Add(setting);
                context.SaveChanges();
            } 
            #endregion

            #region Settings Genaral
            //Settings Genaral
            setting = new Setting { Name = typeof(GeneralSettings).Name };
            setting.SetValue(new GeneralSettings
            {
                Name = "ĐÀI NGUỒN SỐNG",
                Address = "P.O. BOX 1 La Mirada, CA 90637-2509",
                CopyRight = "© 2011 FEBC-Vietnam Ministries. All rights reserved.",
                Email = "nguonsong2001@yahoo.com",
                Facebook = "https://www.facebook.com/dainguonsong",
                Title = "Đài nguồn sống",
                Desc = "Đài Nguồn Sống - Tiếng Nói của Tình Yêu, Chân Lý và Hy Vọng. Phát Thanh Hằng Ngày Trên Radio và Internet",
                KeyWord = "đài nguồn sống, dai nguon song, dainguonsong, tin lanh, phát thanh, phat thanh, tin lành, co doc nhan, cơ đốc nhân, kinh thanh, kinh thánh, Chúa, chua, duc chua troi, đức chúa trời, giê-xu, giexu, gie xu, duc thanh linh, đức thánh linh,  cầu nguyện, pray, worship, tôn vinh,  ân điển, cơ đốc, bồi linh, boi linh, bài giảng, bai giang, việt nam, vietnam, nhà thờ, nha tho, hội thánh, hoi thanh, tình yêu, tinh yeu, làm chứng, lam chung, mục sư, mucsu, chứng đạo, chung dao, tâm linh, tam linh, hon nhan, hôn nhân, gia dinh, gia đình, sứ đồ, thánh đồ, niềm tin, vietnamese christian, christian",
                Intro = new DefaultMedia
                {
                    Name = "Lời giới thiệu",
                    DefaultMp3 = "http://www.dainguonsong.com/media/mp3/introduction/introduction.mp3"
                },
                Endding = new DefaultMedia
                {
                    Name = "Lời kết thúc",
                    DefaultMp3 = "http://www.dainguonsong.com/media/mp3/introduction/ending.mp3"
                }
            });
            context.Settings.Add(setting);
            context.SaveChanges();
            
            #endregion

            #region Settings Broadcast Catalogs
            //Settings Broadcast Catalogs
            setting = new Setting { Name = typeof(BroadcastCatalog).Name };
            setting.SetValue(new BroadcastCatalog { Id = 0, CatalogList = "" });
            context.Settings.Add(setting);
            context.SaveChanges(); 
            #endregion

            //System Layout
            setting = new Setting { Name = typeof(Layout).Name };
            setting.SetValue(new BroadcastCatalog { Id = 0, CatalogList = "" });
            context.Settings.Add(setting);
            context.SaveChanges(); 



            base.Seed(context);
        }
    }
}
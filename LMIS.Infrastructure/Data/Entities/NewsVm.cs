using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class NewsVm
    {
        public decimal NewsID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public DateTime NewsDate { get; set; }
        public DateTime NewsExpiryDate { get; set; }
        public string NewsBannerPath { get; set; }
        public string NewsIconPath { get; set; }
        public string  NewsVideoPath { get; set; }
        public int NewsLangauage { get; set; }
        public string  PostUserID { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsInformal { get; set; }
        public bool IsAchievement { get; set; }

    }
}

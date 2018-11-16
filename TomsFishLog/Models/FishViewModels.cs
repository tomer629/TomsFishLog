using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TomsFishLog.Models
{
    public class ThumbnailPartialViewmodel
    {
        [Key]
        public string FishID { get; set; }
        public List<FishModels.FishImageUrl> FishImageList { get; set; }
    }
}
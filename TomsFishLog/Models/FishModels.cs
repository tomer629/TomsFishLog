using System;

namespace TomsFishLog.Models
{
    public class FishModels
    {
        public class Fish
        {
            public FishImage[] images { get; set; }
            public FishWeather weather { get; set; }

            public string strSpecies { get; set; }
            public string strLocationName { get; set; }

            public DateTime dteDateTimeCaught { get; set; }
            public DateTime dteDateTimeEntered { get; set; }

            public decimal? decLengthInches { get; set; }
            public decimal? decWeightLbs { get; set; }
            
            public decimal? decWaterLevel { get; set; }

            public decimal? decLatitude { get; set; }
            public decimal? decLongitude { get; set; }
        }
        
        public class RecentSpecies
        {
            public short? SpeciesID { get; set; }
            public string SpeciesName { get; set; }
        }

        public class FishLogOptions
        {
            public int? markerSize { get; set; }
            public decimal? markerOpacity { get; set; }
        }

        public class AmazonS3Url
        {
            public string objectKey { get; set; }
            public string url { get; set; }
            public DateTime expires { get; set; }
        }

        public class FishImage
        {
            public ExifData ExifData { get; set; }
            public AmazonS3Url thumb { get; set; }
            public AmazonS3Url fullSize { get; set; }

            //public string strAmazonOriginalImageUrl { get; set; }
            //public string strAmazonOriginalImageObjectKey { get; set; }
            //public DateTime? dteAmazonOriginalImageExpires { get; set; }

            //public string strAmazonThumbUrl { get; set; }
            //public string strAmazonThumbObjectKey { get; set; }
            //public DateTime? dteAmazonThumbExpires { get; set; }

        }

        public class ExifData
        {
            public DateTime dateTimeTaken;
            public int[] GPSAltitude;
            public int GPSDifferential;
            public int GPSImgDirection;
            public decimal GPSLatitude;
            public decimal GPSLongitude;
            public string LongitudeRef;
            public string LatitudeRef;
            public ushort Orientation;
            public int orientation;
        }

        public class FishWeather
        {
            public ForecastIO.Currently current { get; set; }
            public ForecastIO.Daily daily { get; set; }
            public ForecastIO.Hourly hourly { get; set; }
        }

    }
}

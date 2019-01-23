using System;
using System.ComponentModel.DataAnnotations;

namespace TomsFishLog.Models
{
    // TODO: pretty sure this outer wrapper "FishModels" doesnt need to be here.
    public class FishModels
    {
        public class Fish
        {
            public string FishID { get; set; }
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

        public class NewFishInfo
        {
            public string fishID { get; set; }
            public int LastSpecies { get; set; }
            public float LastLat { get; set; }
            public float LastLng { get; set; }

            //public SpeciesSliderVals[] FishSpecies { get; set; }
        }

        public class SpeciesSliderVals
        {
            public int SpeciesID { get; set; }
            public string SpeciesName { get; set; }

            public int LengthSliderMin { get; set; }
            public int LengthSliderMax { get; set; }
            public decimal LengthSliderStart { get; set; }
            public decimal LengthSliderStep { get; set; }
            
            public int WeightSliderMin { get; set; }
            public int WeightSliderMax { get; set; }
            public decimal WeightSliderStart { get; set; }
            public decimal WeightSliderStep { get; set; }
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

        public class FishImage
        {
            public ExifData ExifData { get; set; }
            public string FishID { get; set; }

            public AmazonS3Url thumb { get; set; }      // todo this can go, replaced with AmazonS3Image below
            public AmazonS3Url fullSize { get; set; }   // todo this can go, replaced with AmazonS3Image below
        }

        public class AmazonS3Url
        {
            [Key]
            public string objectKey { get; set; }
            public string url { get; set; }
            public DateTime expires { get; set; }
        }

        public class FishImageUrl
        {
            [Key]
            public string FishID { get; set; }
            public string thumbUrl { get; set; }
            public string fullSizeUrl { get; set; }
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

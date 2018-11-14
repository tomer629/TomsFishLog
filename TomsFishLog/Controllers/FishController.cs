using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ExifLib;
using ForecastIO;
using ImageResizer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TomsFishLog.Models;





namespace TomsFishLog.Controllers
{
    public class FishController : Controller
    {
        DatabaseClass dbClass = new DatabaseClass();

        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _awsBucketName = ConfigurationManager.AppSettings["AWSBucketname"];
        private static readonly string _forecastIO_API_Key = ConfigurationManager.AppSettings["ForecastIO_API_Key"];


        //imp! changed build config for debugging, change it back for publishing: Right click Project in solution explorer -> properties >         


        //todo do this with json async, remember to do as much processing and validation as posible client side (javascript) so you arent paying for as many cpu cycles
        public ActionResult submitFish(Models.FishModels.Fish fish)
        {
            dbClass.EnterFish(fish);

            //todo if they entered a new species, add it to the species table and set a flag for this user in userOptions signaling this user has custom species.
            //todo animate this with so it is clear it worked and then refresh the list of fish below and highlight the newly added fish.

            return RedirectToAction("EnterFish", "Fish");
        }

        public ActionResult EnterFish()
        {
            //imp !  Use a ViewModel for enter fish, it handles multiple errors really nicely. Look at register and login pages for examples of how to use.

            //load last 5 species caught into session
            // todo: change this to get all recent data like last location and... ?? other stuff maybe
            //   do it all in once DB call

            // https://github.com/jasonholloway/miniguid

            // fill TempData stuff needed for a new fish
            FishModels.NewFishInfo NewFishInfo =  dbClass.getNewFishInfo();   //todo implement this later
            Session["NewFishInfo"] = NewFishInfo;

            var recentSpecies = dbClass.getRecentSpeciesByAnglerID();
            Session["RecentSpecies"] = recentSpecies; 

            FishModels.Fish fish = new FishModels.Fish();   //new 11/11/18
            fish.FishID = NewFishInfo.fishID;               //new 11/11/18

            //return View("EnterFish");
            return View("EnterFish", fish);                       //new 11/11/18
        }

        public PartialViewResult _ThumbnailPartial(string fishID)
        {
            Models.ThumbnailPartialViewmodel vm = new ThumbnailPartialViewmodel();
            vm.FishID = fishID;

            // get all images of the fish
            //List<Models.FishModels.AmazonS3Url> images = dbClass.getImageUrlsForFish(fishID);

            // check if the session contains the samefishId


            return PartialView("_ThumbnailPartial", vm);
        }

        public PartialViewResult _EnterFishPartial(Models.FishModels.Fish fish)
        {
            //todo hook up to google maps somehow, let them place the location caught on a map if they are entering the fish on website. 
            //See if there is some way to get gps location from a phone browser.

            return PartialView("_EnterFishPartial");
        }

        public PartialViewResult _fishLogPartial()
        {
            List<Models.FishModels.Fish> fishList = dbClass.getFishByUsername(User.Identity.Name);

            //todo check for users with 0 fish entered and display message
            return PartialView("_FishLogPartial", fishList);
        }

        public ActionResult Log()
        {
            List<Models.FishModels.Fish> fishList = dbClass.getFishByUsername(User.Identity.Name);

            fishList = fishList.OrderBy(m => m.dteDateTimeEntered).ToList();
            ViewData["speciesList"] = getSpeciesList(fishList);

            Models.FishModels.FishLogOptions opt = new Models.FishModels.FishLogOptions();
            opt = dbClass.getFishLogOptions();
            Session["FishLogOptions"] = opt;

            return View(fishList);
        }

        public ActionResult Log2()
        {
            List<Models.FishModels.Fish> fishList = dbClass.getFishByUsername(User.Identity.Name);
            fishList = fishList.OrderBy(m => m.dteDateTimeEntered).ToList();

            Session["speciesList"] = getSpeciesList(fishList);

            Models.FishModels.FishLogOptions opt = new Models.FishModels.FishLogOptions();
            opt = dbClass.getFishLogOptions();
            Session["FishLogOptions"] = opt;

            return View(fishList);
        }

        public PartialViewResult _MapPartial()
        {
            List<Models.FishModels.Fish> fishList = dbClass.getFishByUsername(User.Identity.Name);
            fishList = fishList.OrderBy(m => m.dteDateTimeEntered).ToList();

            //ViewData["speciesList"] = getSpeciesList(fishList);

            return PartialView("_MapPartial", fishList);
        }

        public List<SelectListItem> getSpeciesList(List<Models.FishModels.Fish> fishList)
        {
            List<string> speciesList = new List<string>();
            speciesList = fishList.Select(m => m.strSpecies).Distinct().ToList();

            List<SelectListItem> speciesSelectList = new List<SelectListItem>();
            bool setSelected = true; //sets selected species to most recent entry
            foreach (var s in speciesList)
            {
                SelectListItem species = new SelectListItem() { Text = s };
                if (setSelected == true)
                {
                    species.Selected = true;
                    setSelected = false;
                }
                speciesSelectList.Add(species);
            }
            return speciesSelectList;
        }


        public JsonResult getFishLogOptions()
        {
            Models.FishModels.FishLogOptions opt = new Models.FishModels.FishLogOptions();
            opt = dbClass.getFishLogOptions();

            return Json(opt);
        }

        public JsonResult updateFishLogOptions(int markerSize, decimal markerOpacity)
        {
            //save to db
            bool saved = dbClass.updateFishLogOptions(markerSize, markerOpacity);

            //update the session
            Models.FishModels.FishLogOptions opt = new Models.FishModels.FishLogOptions();
            opt.markerSize = markerSize;
            opt.markerOpacity = markerOpacity;
            Session["FishLogOptions"] = opt;

            return Json(opt);
        }




        [HttpPost]
        public JsonResult getWeatherTest(string test)
        {
            //return Json("return response here");
            var result = new { text = "it worked" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string convertBearingToDirection(float fltHeading)
        {
            int heading = Convert.ToInt16(fltHeading);
            var directions = new string[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N" };

            var index = (heading + 23) / 45;
            return directions[index];
        }


        [HttpPost]
        public JsonResult getWeather(decimal latitude, decimal longitude, string dateCaught, string timeCaught)
        {
            //  https://developer.forecast.io/docs/v2  <-- Weather API Documentation

            DateTime imageDateTime = new DateTime();

            try
            {
                //convert date and time strings
                imageDateTime = Convert.ToDateTime(dateCaught + ' ' + timeCaught);
            }
            catch (Exception ex)
            {
                ErrorLoggingClass errorLog = new ErrorLoggingClass();
                errorLog.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "FishController", "getWeather", User.Identity.Name, "dateCaught:" + dateCaught + ", timeCaught:" + timeCaught);
                return Json(false);
            }





            float lat = (float)latitude;
            float lng = (float)longitude;

            ForecastIORequest request = new ForecastIORequest(_forecastIO_API_Key, lat, lng, imageDateTime, Unit.us, null);
            var response = request.Get();

            FishModels.FishWeather fw = new FishModels.FishWeather();
            fw.current = response.currently;
            fw.daily = response.daily;
            fw.hourly = response.hourly;



            //save response to session
            Session["FishWeather"] = fw;

            //save the


            var result = new {
                temp = fw.current.temperature,
                feelsLike = fw.current.apparentTemperature,
                icon = fw.current.icon,
                summary = fw.current.summary,
                windSpd = fw.current.windSpeed,
                windDir = convertBearingToDirection(fw.current.windBearing),
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public static DateTime UnixTSToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AsyncUpload(IEnumerable<HttpPostedFileBase> files)
        {
            int count = 0;
            string thumbNail = "";
            decimal imageLat = 0;
            decimal imageLong = 0;
            DateTime imageDateTime = new DateTime();

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0 && file.ContentLength / 1000 < 4096)
                    {
                        //resize
                        MemoryStream s1 = resizeImage(file, "thumb");
                        FishModels.AmazonS3Url thumbUrl = UploadToS3(s1, "thumb");
                        MemoryStream s2 = resizeImage(file, "1200");
                        FishModels.AmazonS3Url fullSizeUrl = UploadToS3(s2, "1200");

                        ////get Exif Data
                        Stream s = file.InputStream;
                        FishModels.ExifData e = GetExifData(s);

                        FishModels.FishImage image = new FishModels.FishImage();
                        FishModels.NewFishInfo newFishInfo = (FishModels.NewFishInfo) Session["NewFishInfo"];
                        image.FishID = newFishInfo.fishID;
                        image.ExifData = e;
                        image.thumb = thumbUrl;
                        image.fullSize = fullSizeUrl;

                        //get some stuff from first image only
                        if (count == 0)
                        {
                            thumbNail = thumbUrl.url;
                            imageLat = image.ExifData.GPSLatitude;
                            imageLong = image.ExifData.GPSLongitude;
                            imageDateTime = image.ExifData.dateTimeTaken;
                        }

                        //save image data to DB
                        dbClass.saveImage(image); 

                        // get weather data
                        //getWeatherData(imageLat, imageLong, image.ExifData.dateTimeTaken);

                        //consider forcing the user to resize, then they can get paid version to raise their upload size.
                        //i could also use the Javascript library from google to upload straight to S3 if I didnt have to process(resize) the images.

                        //report back that we are resizing here? How is it updating status bar?
                    }
                    //var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);



                    //set filename = userID(int) + timestamp or GUID 
                    //var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);



                    //upload to amazon S3
                    //save S3 key to DB

                    //get exif data
                    //save exif data to DB

                    //start API calls for weather, water level, etc...





                    //file.SaveAs(path);
                    count++;

                }
            }

            var result = new { url = thumbNail, latitude = imageLat, longitude = imageLong, imageDate = imageDateTime.ToShortDateString(), imageTime = imageDateTime.ToShortTimeString() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public MemoryStream resizeImage(HttpPostedFileBase file, string resizeOption)
        {
            var resizeSettings = new ResizeSettings();
            resizeSettings.Format = "jpg";

            switch (resizeOption)
            {
                case "thumb":
                    resizeSettings.MaxWidth = 250;
                    resizeSettings.Quality = 75;
                    break;

                case "1200":
                    resizeSettings.MaxWidth = 1000; //todo paid: allow full size image uploads
                    resizeSettings.Quality = 90;    //todo paid: allow higher quality uploads (ie use less compression)
                    break;
            }

            MemoryStream stream = new MemoryStream();
            ImageBuilder.Current.Build(file, stream, resizeSettings, true, true);

            return stream;
        }

        public FishModels.ExifData GetExifData(Stream s)
        {
            FishModels.ExifData e = new FishModels.ExifData();

            double[] latArray;
            double[] longArray;
            string latRef;
            string longRef;

            //// Instantiate the reader
            try
            {
                using (ExifReader reader = new ExifReader(s))
                {
                    DateTime dt = new DateTime();

                    reader.GetTagValue(ExifTags.DateTimeOriginal, out dt);
                    if (dt.Date < DateTime.Now.AddYears(-100)) //checking if date is *Loval 
                    { reader.GetTagValue(ExifTags.DateTimeDigitized, out dt); }
                    if (dt.Date < DateTime.Now.AddYears(-100)) //checking if date is *Loval 
                    { reader.GetTagValue(ExifTags.DateTime, out dt); }
                    e.dateTimeTaken = dt;

                    reader.GetTagValue(ExifTags.Orientation, out e.Orientation);

                    reader.GetTagValue(ExifTags.GPSAltitude, out e.GPSAltitude);
                    reader.GetTagValue(ExifTags.GPSDifferential, out e.GPSDifferential);
                    reader.GetTagValue(ExifTags.GPSImgDirection, out e.GPSImgDirection);
                    reader.GetTagValue(ExifTags.GPSLatitude, out latArray);
                    reader.GetTagValue(ExifTags.GPSLongitude, out longArray);
                    reader.GetTagValue(ExifTags.GPSLongitudeRef, out longRef);
                    reader.GetTagValue(ExifTags.GPSLatitudeRef, out latRef);

                    if (latArray != null && longArray != null)
                    {
                        e.GPSLatitude = ConvertDegreesToDecimalDegrees(latArray, latRef);
                        e.GPSLongitude = ConvertDegreesToDecimalDegrees(longArray, longRef);
                    }
                }
            }
            catch (Exception ex)
            { 
                //error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "saveImage", HttpContext.Current.User.Identity.Name, null);
                return e;
            }
            

            return e;
        }

        [HttpPost]
        public JsonResult DeleteImageFromS3(string objectKey)
        {
            try
            {
                AmazonS3Client s3Client = new AmazonS3Client();
                DeleteObjectRequest request = new DeleteObjectRequest();

                request.BucketName = _awsBucketName;
                request.Key = objectKey;

                s3Client.DeleteObject(request);
                s3Client.Dispose();

                return Json(true);
            }
            catch (Exception ex){
                ErrorLoggingClass errorLog = new ErrorLoggingClass();
                errorLog.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "FishController", "DeleteImageFromS3", User.Identity.Name, null);
                return Json(false);
            }            
        }

        //private static async Task DeleteObjectNonVersionedBucketAsync()
        //{
        //    try
        //    {
        //        var deleteObjectRequest = new DeleteObjectRequest
        //        {
        //            BucketName = bucketName,
        //            Key = keyName
        //        };

        //        Console.WriteLine("Deleting an object");
        //        await AmazonS3Client.DeleteObjectAsync(deleteObjectRequest);
        //    }
        //    catch (AmazonS3Exception e)
        //    {
        //        Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
        //    }
        //}

        static decimal ConvertDegreesToDecimalDegrees(double[] coordArray, string latLngRef)
        {
            if (coordArray.Length == 3)
            {
                double degrees = coordArray[0] +
                   (coordArray[1] / 60) +
                   (coordArray[2] / 3600);

                if (latLngRef == "S")
                {
                    // it is "south" therefore needs to be a negative number
                    degrees = 0 - degrees;
                }

                if (latLngRef == "W")
                {
                    // it is "West" therefore needs to be a negative number
                    degrees = 0 - degrees;
                }


                decimal returnValue = Convert.ToDecimal(degrees);

                return returnValue;
            }
            else
            {
                return 0;
                //throw new ArgumentException("The degrees array must contain 3 different values for Degrees, Minutes and Seconds");
            }
        }


        public Models.FishModels.AmazonS3Url UploadToS3(MemoryStream stream, string testName)
        {
            try
            {

                string userID = getUserID();
                int anglerID = dbClass.getAnglerID();

                Guid g = new Guid();
                g = Guid.NewGuid();

                string objectKey = string.Format("UPLOADS/{0}/{1}.jpg", anglerID.ToString(), g);       //todo put this back after testing ****888@@@@@@@@@@@@@@ 
                //string objectKey = string.Format("UPLOADS/{0}/{1}.jpg", anglerID.ToString(), g);  /// with this it will keep overwriting the same image in S3

                PutObjectResponse response = new PutObjectResponse();
                // Create S3 service client.             
                using (IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.USEast1))
                {
                    // Setup request for putting an object in S3.                 
                    PutObjectRequest request = new PutObjectRequest
                    {
                        BucketName = _awsBucketName,
                        Key = objectKey,
                        InputStream = stream      //SEND THE FILE STREAM 
                    };

                    //todo: change this expiration stuff to a few months. Not sure if changing it here is enough, forgot how the caching stuff worked.
                    request.Headers.Expires = new DateTime(2020, 1, 1);   //.. this shouldnt be a set date anyway...
                    request.Headers.CacheControl = "max-age=77760000";    //..this might be the one that sets time
                    //request.Headers["expires"] = "Thu, 01 Dec 1994 16:00:00 GMT";

                    // Make service call and get back the response.                 
                    response = s3Client.PutObject(request);

                    //string objectUrl = GeneratePreSignedURL(objectKey);
                }

                FishModels.AmazonS3Url s3UrlObject = new Models.FishModels.AmazonS3Url();

                s3UrlObject = dbClass.GeneratePreSignedURL(objectKey);


                return s3UrlObject;
            }
            catch (Exception ex)
            {
                ErrorLoggingClass errorLog = new ErrorLoggingClass();
                errorLog.logError(ex.Message, ex.Source, ex.StackTrace, "Upload Image", "FishController", "UploadToS3", User.Identity.Name, "");
                return null;
            }
        }

        public string getUserID()
        {
            string userID = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id;
            return userID;
        }

        




















        //******************************************************************************
        //*******     FILTER STUFF    ***********

        public JsonResult filterSpecies(string[] species)
        {


            return Json(true);
        }

    }
}


//imp  to fix published sight crashing on login - do the IP fix again but with the login DB, i think you just did it with the DB you created (TomsFishDB), you need to add asp.net DB



//todo ******************************** IDEAS ******************************** //

// todo add upload image to EnterFish. Get GPS coords from pic to place marker. Alert user that the image contained GPS info and ask if they want to use it to place a marker.
//   also make this an option in the options menu to always place marker if photo contains GPS coords. Save timestamp from image too and ask if they want that to be the timecaught.


// todo make a custom marker image that is transparent around the outside. then you will be able to click that transparent part of the image and make the icon easier to click even though
//  it will still look small.


//todo ******************************** Fish Log Screen ******************************** 
//just use dots for fish markers so they stay small. This will look much better with many fish on the screen. 
//todo  This would be really cool: options to color code fish by size on the map, or timecaught, or water level. This would make it much easier to visualize the data.
//    example: Bass - under 14": blue marker, 14' -17":green marker, 17"-20":red marker, etc...
// also Make it so mouseover in the list of fish highlights the fish you are moused over on the map, and display
//  full stats for that fish.
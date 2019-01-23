using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TomsFishLog.Models;
using MiniGuids;
using Amazon;

namespace TomsFishLog
{
    public class DatabaseClass
    {
        FishDBDataContext FishDB = new FishDBDataContext();
        ErrorLoggingClass error = new ErrorLoggingClass();

        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _awsBucketName = ConfigurationManager.AppSettings["AWSBucketname"];
        private static readonly Amazon.RegionEndpoint _awsRegionEndpoint = Amazon.RegionEndpoint.USEast1;

        //public bool EnterFishOld(string username, Models.FishModels.Fish fish)
        //{
        //    try
        //    {
        //        DateTime date = new DateTime();
        //        date = DateTime.Now;
        //        FishDB.EnterFish(999999, 
        //            username, fish.strSpecies, fish.strLocationName, fish.decLengthInches, fish.decWeightLbs, fish.decWaterLevel, fish.decLatitude, fish.decLongitude);
        //        //todo fix this username =999999...not even sure wtf this is. probably from before I added userId to the user DB. Will need to get MY UserID (the int), from DB and
        //        // save to Session for use here and other places I want userID
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "EnterFish", HttpContext.Current.User.Identity.Name, null);
        //        return false;
        //    }
        //}

        public bool EnterFish(Models.FishModels.Fish fish)
        {
            try
            {
                string Id = GetId();

                DateTime dtEntered = new DateTime();
                dtEntered = DateTime.Now;
                //DateTime dtCaught = //TODO: give user option to manually enter datetime caught
                //dateCaught  

                FishDB.spEnterFish(Id, fish.FishID, fish.strSpecies, dtEntered, dtEntered, fish.decLengthInches, fish.decWeightLbs);
                //todo fix this username =999999...not even sure wtf this is. probably from before I added userId to the user DB. Will need to get MY UserID (the int), from DB and
                // save to Session for use here and other places I want userID
                //todo: fix dateCaught Parameter. Allow user to change that when entering a fish. It shouldnt always be set to DateEntered

                // add images
                foreach (FishModels.FishImage image in fish.images)
                {
                    // when you write the images to DB the first time, save them to the session.
                    // then when you get here and you have a fishID, come back and update them with fishID. 

                    // you will need ot change spEnterFish so it returns the fishID
                }

                return true;
            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "EnterFish", HttpContext.Current.User.Identity.Name, null);
                return false;
            }
        }

        public bool SaveImage(FishModels.FishImage i)
        {
            try
            {
                string Id = GetId();
                FishDB.spInsertImage(Id, i.FishID, i.thumb.objectKey, i.thumb.url, i.thumb.expires, i.fullSize.objectKey, i.fullSize.url, i.fullSize.expires);

                return true;
            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "saveImage", HttpContext.Current.User.Identity.Name, null);
                return false;
            }
        }

        public bool DeleteImage(string fishID, int imageNum)
        {
            try
            {
                var keys = FishDB.spGetObjectKeysForImage(fishID, imageNum).First();

                DeleteS3Object(keys.fullSizeObjectKey);
                DeleteS3Object(keys.thumbObjectKey);    // We just assume it got deleted. You can write something to clean up keys that didnt get deleted.
                FishDB.spDeleteImage(fishID, imageNum); 

                return true;
            }
            catch(Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "deleteImage", HttpContext.Current.User.Identity.Name, null);
                return false;
            }
        }

        private void DeleteS3Object(string objectKey)
        {
            try
            {
                AmazonS3Client client = new AmazonS3Client(_awsRegionEndpoint);

                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = _awsBucketName,
                    Key = objectKey
                };

                client.DeleteObject(deleteObjectRequest);
            }
            catch (AmazonS3Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "DeleteS3Object", HttpContext.Current.User.Identity.Name, null);
            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "DeleteS3Object", HttpContext.Current.User.Identity.Name, null);
            }
        }
        

        public FishModels.NewFishInfo GetNewFishInfo() {
            try
            {
                FishModels.NewFishInfo NewFishInfo = new FishModels.NewFishInfo();

                NewFishInfo.fishID = MiniGuid.NewGuid();  //https://github.com/jasonholloway/miniguid
                NewFishInfo.LastSpecies = 9; //todo - fix

                //todo get the rest...
                //  gps cords
                //  species caught


                return NewFishInfo;
            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "getNewFishInfo", HttpContext.Current.User.Identity.Name, null);
                FishModels.NewFishInfo NewFishInfo = new FishModels.NewFishInfo();
                return NewFishInfo;
            }
        }

        public FishModels.SpeciesSliderVals GetSliderValuesForSpecies (int species)
        {
            try
            {
                var vals = FishDB.GetSliderValuesForSpecies(species).First();
                FishModels.SpeciesSliderVals s = new FishModels.SpeciesSliderVals();

                s.SpeciesID = vals.SpeciesID;
                s.SpeciesName = vals.SpeciesName;
                s.LengthSliderMin = (int)vals.LengthSliderMin;
                s.LengthSliderMax = (int)vals.LengthSliderMax;
                s.LengthSliderStart = (decimal)vals.LengthSliderStart;
                s.LengthSliderStep = (decimal)vals.LengthSliderStep;
                s.WeightSliderMin = (int)vals.WeightSliderMin;
                s.WeightSliderMax = (int)vals.WeightSliderMax;
                s.WeightSliderStart = (decimal)vals.WeightSliderStart;
                s.WeightSliderStep = (decimal)vals.WeightSliderStep;

                return s;
            }
            catch(Exception ex)
            {
                // todo set defauts for when it fails
                FishModels.SpeciesSliderVals s = new FishModels.SpeciesSliderVals();

                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "GetSliderValuesForSpecies", HttpContext.Current.User.Identity.Name, null);
                return s;
            }
            

        }

        public List<FishModels.FishImageUrl> GetImageUrlsForFish(string fishID) 
        {
            List<FishModels.FishImageUrl> imgs = new List<FishModels.FishImageUrl>();

            var lst = FishDB.spGetImagesByFishID(fishID).ToList();

            foreach (var a in lst) {
                FishModels.FishImageUrl img = new FishModels.FishImageUrl();
                img.FishID = a.FishID;
                img.thumbUrl = a.thumbUrl;
                img.fullSizeUrl = a.fullSizeUrl;

                imgs.Add(img);
            }
            return imgs;
        }


        public string GetId()
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());
            return user.Id;
        }

        //public int getUserID()
        //{
        //    try
        //    {
        //        int userID = FishDB.spGetUserID(HttpContext.Current.User.Identity.Name).First().UserID;
        //        return userID;
        //    }
        //    catch (Exception ex)
        //    {
        //        error.logError(ex.Message, ex.Source, ex.StackTrace, "getUserID", "DatabaseClass", "getUserID", HttpContext.Current.User.Identity.Name, null);
        //        return 0;
        //    }
            
        //}

        public int GetAnglerID()
        {
            try
            {
                string userID = HttpContext.Current.User.Identity.GetUserId();
                int AnglerID = FishDB.spGetAnglerID(userID).First().AnglerID;
                return AnglerID;

            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "getAnglerID", "DatabaseClass", "getAnglerID", HttpContext.Current.User.Identity.Name, null);
                return 0;
            }
        }

        public List<Models.FishModels.Fish> GetFishByUsername(string username)
        {
            try
            {
                List<Models.FishModels.Fish> fishlst = new List<Models.FishModels.Fish>();
                var lst = FishDB.spGetFishByUser(username).ToList();

                foreach(var a in lst)
                {
                    Models.FishModels.Fish f = new Models.FishModels.Fish();

                    f.strSpecies = a.strSpecies;
                    f.strLocationName = a.strLocationName;
                    f.dteDateTimeEntered = Convert.ToDateTime(a.dteTimeEntered);

                    f.decLengthInches = a.decLengthInches;
                    f.decWeightLbs = a.decWeightLbs;

                    f.decWaterLevel = a.decWaterLevel;
                    
                    f.decLatitude = a.decLatitude;
                    f.decLongitude = a.decLongitude;

                    fishlst.Add(f);
                }

                return fishlst;
            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "getFishByUsername", HttpContext.Current.User.Identity.Name, null);
                return null;
            }
        }

        public bool UpdateFishLogOptions(int markerSize, decimal markerOpacity)
        {
            try
            {
                var userID = FishDB.spGetUserID(HttpContext.Current.User.Identity.Name).FirstOrDefault().UserID;
                var a = FishDB.spUpdateUserOptions(userID, markerSize, markerOpacity);
                return true;
            }

            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Fish Log", this.GetType().FullName, GetCurrentMethod(), HttpContext.Current.User.Identity.Name, null);
                return false;
            }
        }


        public List<FishModels.RecentSpecies> getRecentSpeciesByAnglerID()
        {
            try
            {
                string Id = GetId();
                var l = FishDB.spGetLast5FishById(Id).ToList();

                List<FishModels.RecentSpecies> recentSpecies = new List<FishModels.RecentSpecies>();
                foreach (var x in l)
                {
                    FishModels.RecentSpecies r = new FishModels.RecentSpecies();
                    r.SpeciesID   = x.SpeciesID;
                    r.SpeciesName = x.SpeciesName;

                    recentSpecies.Add(r);
                }

                return recentSpecies;
            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Fish Log", this.GetType().FullName, GetCurrentMethod(), HttpContext.Current.User.Identity.Name, null);
                return null;
            }
        }


        public Models.FishModels.FishLogOptions getFishLogOptions()
        {
            try {
                int userID = FishDB.spGetUserID(HttpContext.Current.User.Identity.Name).FirstOrDefault().UserID;
                var a = FishDB.spGetUserOptions(userID).FirstOrDefault();

                Models.FishModels.FishLogOptions opt = new Models.FishModels.FishLogOptions();
                opt.markerSize = a.MarkerSize;
                opt.markerOpacity = a.MarkerOpacity;

                return opt;
            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Fish Log", this.GetType().FullName, GetCurrentMethod(), HttpContext.Current.User.Identity.Name, null);
                return null;
            }

        }

        public Models.FishModels.AmazonS3Url GeneratePreSignedURL(string objectKey)
        {
            //string urlString = "";

            Models.FishModels.AmazonS3Url AmazonS3Url = new Models.FishModels.AmazonS3Url();

            AmazonS3Url.expires = DateTime.Now.AddDays(1); //this is needed because for some reason you cant just say: Expires = AmazonS3Url.expires below...but you can do this... weird
            AmazonS3Url.objectKey = objectKey;

            try
            {
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = _awsBucketName,
                    Key = objectKey,
                    Expires = DateTime.Now.AddDays(1)
                };


                IAmazonS3 s3Client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.USEast1);
                AmazonS3Url.url = s3Client.GetPreSignedURL(request1);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    ErrorLoggingClass errorLog = new ErrorLoggingClass();
                    errorLog.logError(amazonS3Exception.Message, amazonS3Exception.Source, amazonS3Exception.StackTrace, "GeneratePreSignedURL", "DB Class", "", HttpContext.Current.User.Identity.Name, "AMAZON_S3_EXCEPTION: Check the provided AWS Credentials.");
                    return null;
                }
                else
                {
                    ErrorLoggingClass errorLog = new ErrorLoggingClass();
                    errorLog.logError(amazonS3Exception.Message, amazonS3Exception.Source, amazonS3Exception.StackTrace, "GeneratePreSignedURL", "DatabaseClass", "GeneratePreSignedURL", HttpContext.Current.User.Identity.Name, null);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLoggingClass errorLog = new ErrorLoggingClass();
                errorLog.logError(ex.Message, ex.Source, ex.StackTrace, "GeneratePreSignedURL", "FishController", "GeneratePreSignedURL", HttpContext.Current.User.Identity.Name, "");
                return null;
            }

            return AmazonS3Url;
        }






        public bool SaveFishWeather()
        {
            //get Weather Data from session
            FishModels.FishWeather fw = new FishModels.FishWeather();
            fw = (FishModels.FishWeather)HttpContext.Current.Session["FishWeather"];




            try
            {
                // FishDB.saveFishWeather(tons of variables go here...);
                return true;
            }
            catch (Exception ex)
            {
                error.logError(ex.Message, ex.Source, ex.StackTrace, "saveFishWeather", "DatabaseClass", "saveFishWeather", HttpContext.Current.User.Identity.Name, null);
                return false;
            }
        }




        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
    }


    //***********************************************************************************************************************
    //**  Dropdown Menus  *
    //*********************
    public class DropdownMenus
    {
        public static List<SelectListItem> ReferralType()
        {
            List<SelectListItem> l = new List<SelectListItem>();

            l.Add(new SelectListItem { Text = "Current Student", Value = "1" });
            l.Add(new SelectListItem { Text = "School Presentation", Value = "2" });
            l.Add(new SelectListItem { Text = "Magazine Ad", Value = "3" });
            l.Add(new SelectListItem { Text = "Radio Ad", Value = "4" });
            l.Add(new SelectListItem { Text = "TV Ad", Value = "5" });
            l.Add(new SelectListItem { Text = "Search Engine", Value = "6" });
            l.Add(new SelectListItem { Text = "Other", Value = "7" });

            return l;
        }

        public static List<SelectListItem> DefaultSpeciesList()
        {
            //todo on the enter fish page, make buttons for the top 3 species caught over the last 20 or so entries. Then have the droplist for the rest of species?

            try
            {
                FishDBDataContext FishDB = new FishDBDataContext();
                var id = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId()).Id;
                var anglerID = FishDB.spGetAnglerID(id).ToList().First().AnglerID;
                var speciesList = FishDB.spGetSpeciesList(anglerID).ToList();
                //var recentSpecies = FishDB.spGetLast5FishById().ToList();

                List<SelectListItem> l = new List<SelectListItem>();
                foreach (var i in speciesList)
                {
                    l.Add(new SelectListItem { Text = i.SpeciesName, Value = i.speciesID.ToString() });
                }

                return l;
            }
            catch (Exception ex)
            {
                ErrorLoggingClass error = new ErrorLoggingClass();
                error.logError(ex.Message, ex.Source, ex.StackTrace, "Enter Fish", "DatabaseClass", "DefaultSpeciesList", HttpContext.Current.User.Identity.Name, null);

                // create default list
                List<SelectListItem> l = new List<SelectListItem>();
                l.Add(new SelectListItem { Text = "Smallmouth Bass", Value = "1" });
                l.Add(new SelectListItem { Text = "Largemouth Bass", Value = "2" });
                l.Add(new SelectListItem { Text = "Northern Pike", Value = "3" });
                l.Add(new SelectListItem { Text = "Flathead Catfish", Value = "4" });
                l.Add(new SelectListItem { Text = "Channel Catfish", Value = "5" });
                l.Add(new SelectListItem { Text = "Walleye", Value = "6" });

                return l;
            }
        }
    }
}

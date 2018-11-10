using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Xml.Linq;

namespace TomsFishLog
{
    public class ErrorLoggingClass
    {
        public void logError(string ErrorMessage, string Source, string StackTrace, string PageName, string ClassName, string MethodName, string UserName, string Values)
        {
            bool newFile = false;
            XDocument xmlError;
            FileInfo ErrorFile = new FileInfo(HttpContext.Current.Server.MapPath(@"~/ErrorLog.xml"));
            if (File.Exists(ErrorFile.FullName))
            {
                xmlError = XDocument.Load(ErrorFile.FullName);
            }
            else
            {
                newFile = true;
                xmlError = new XDocument();
            }
            XElement error = new XElement("error");
            error.Add(new XAttribute("dteErrorOccured", DateTime.Now.ToShortDateString()));
            var errorIDs = (from element in xmlError.Descendants("error")
                            orderby element.Attribute("errorID").Value
                            select Convert.ToInt32(element.Attribute("errorID").Value)).ToList();
            errorIDs.Sort();
            int errorID = 0;
            if (errorIDs.Count > 0)
            {
                errorID = errorIDs.Last() + 1;
            }
            error.Add(new XAttribute("errorID", errorID));
            error.Add(new XElement("errorMessage", ErrorMessage));
            error.Add(new XElement("source", Source));
            error.Add(new XElement("stackTrace", StackTrace));
            error.Add(new XElement("pageName", PageName));
            error.Add(new XElement("className", ClassName));
            error.Add(new XElement("methodName", MethodName));
            error.Add(new XElement("userID", UserName));
            error.Add(new XElement("time", DateTime.Now));
            error.Add(new XElement("values", Values));
            if (xmlError.Root != null)
            {
                xmlError.Root.Add(error);
            }
            else
            {
                XElement root = new XElement("errors");
                root.Add(error);
                xmlError.Add(root);
            }

            try
            {
                //DatabaseClass dbClass = new DatabaseClass();
               // dbClass.InsertErrorLog(ErrorMessage, StackTrace, ContractInfo);
            }
            catch (Exception ex)
            {
                //sendEmail("TomsFishLog has encountered an error and it could not be saved to database." + error.ToString() + "\n\n Exception:\n" + ex.Message);
            }

            try
            {
                xmlError.Save(ErrorFile.FullName);
            }
            catch (Exception ex)
            {
                //sendEmail("TomsFishLog has occured an error and it could not be logged" + error.ToString() + "\n\n Exception:\n" + ex.Message);
            }
            finally
            {
                //sendEmail("TomsFishLog has encountered an error!" + "\n\n" + error.ToString());
            }
        }

        //private bool sendEmail(string body)
        //{
        //    bool sent = true;

        //    //SmtpClient smtpClient = new SmtpClient();
        //    ////smtpClient.Host = "172.24.11.70";
        //    //smtpClient.Host = ConfigurationManager.AppSettings["MailServer"].ToString();        //live ip
        //    //MailMessage objMail = new MailMessage();
        //    //MailAddress fromAddress = new MailAddress("bsalisbury@thebureau.com", "AIS-Art Evaluator");
        //    //objMail.From = fromAddress;

        //    SmtpClient mailClient = new SmtpClient("smtp.office365.com");

        //    //Your Port gets set to what you found in the "POP, IMAP, and SMTP access" section from Web Outlook

        //    mailClient.Port = 587;

        //    //Set EnableSsl to true so you can connect via TLS
        //    mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    //mailClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
        //    mailClient.EnableSsl = false;

        //    //Your network credentials are going to be the Office 365 email address and the password
        //    mailClient.UseDefaultCredentials = true;
        //    System.Net.NetworkCredential cred = new System.Net.NetworkCredential("outbound@artinstructionschools.edu", "AIS!2014");
        //    mailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
        //    mailClient.PickupDirectoryLocation = @"c:\inetpub\mailroot\Pickup";
        //    mailClient.Credentials = cred;

        //    MailMessage objMail = new MailMessage();
        //    objMail.IsBodyHtml = true;

        //    objMail.From = new MailAddress("outbound@artinstructionschools.edu", "AIS-Art Evaluator");

        //   // objMail.To.Add("bsalisbury@thebureau.com");
        //    objMail.To.Add("dev@artinstructionschools.edu");
        //    //objMail.To.Add("smurto@thebureau.com");
        //    //objMail.To.Add("rmulder@thebureau.com");
        //    objMail.IsBodyHtml = false;
        //    objMail.Subject = "The AIS-Art Evaluator has encountered a error";
        //    objMail.Body = "Hello\n\n" +
        //       body;
        //    try
        //    {
        //        mailClient.Send(objMail);
        //    }
        //    catch
        //    {
        //        sent = false;
        //    }
        //    return sent;
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using LoginRegistrationWebApp.Models;
using LoginRegistrationWebApp.ViewModel;

namespace LoginRegistrationWebApp.Repositories
{
    public class SiteUserRepository
    {
        private UserRegiatrationDBEntities db;
        public SiteUserRepository()
        {
            db = new UserRegiatrationDBEntities();
        }

        public bool AddUser(SiteUserViewModel objSiteUserViewModel)
        {
            objSiteUserViewModel.IsValid = false;

            SiteUser objSiteUser = new SiteUser();
            /*objSiteUser.ID = objSiteUserViewModel.ID;*/
            objSiteUser.Username = objSiteUserViewModel.Username;
            objSiteUser.Email = objSiteUserViewModel.Email;
            objSiteUser.Password = objSiteUserViewModel.Password;
            objSiteUser.IsValid = objSiteUserViewModel.IsValid;

            db.SiteUsers.Add(objSiteUser);
            db.SaveChanges();

            BuildEmailTemplate(objSiteUser.ID);

            return true;
        }

        public void BuildEmailTemplate(int regID)
        {

            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = db.SiteUsers.Where(x => x.ID == regID).FirstOrDefault();
            var url = "https://localhost:44371/" + "Register/Confirm?regId=" + regID;
            body = body.Replace("@ViewBag.CofirmationLink", url);
            body = body.ToString();

            BuildEmailTemplate("Your Account Is Successfully Created", body, regInfo.Email);
        }
        public bool ConfirmUser(int regId)
        {
            SiteUser Data = db.SiteUsers.Where(x => x.ID == regId).FirstOrDefault();
            Data.IsValid = true;
            db.SaveChanges();

            return true;
        }
        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "sahillocus@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;

            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if(!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendMail(mail);                           
        }

        public static void SendMail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("sahillocus@gmail.com", "9759648146");
            try
            {
                client.Send(mail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
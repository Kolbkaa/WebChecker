using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebChecker.ViewModel;

namespace WebChecker.Tool
{
    public class SendMail
    {

        public string SmtpServer { get; private set; }
        public int SmtpPort { get; private set; }
        public string SmtpUsername { get; private set; }
        public string SmtpPassword { get; private set; }
        public bool Ssl { get; private set; }
        public bool CorrectConfiguration { get; set; }

        private SmtpClient _smtp = new SmtpClient();

        public SendMail()
        {
            CorrectConfiguration = false;
            var serializableService = new SerializableService<ConfigurationViewModel>();

            var configuration = serializableService.Deserialize();
            if(configuration != null)
            {
                SmtpServer = configuration.SmtpServer;
                SmtpPort = configuration.SmtpPort;
                SmtpUsername = configuration.SmtpUsername;
                SmtpPassword = configuration.SmtpPassword;
                Ssl = configuration.Ssl;
                CorrectConfiguration = true;

            }

        }

        //public void ConfigureMail(string smtpServer, string smtpPort, string smtpUsername, string smtpPassword, bool ssl)
        //{

        //    SmtpServer = smtpServer;
        //    SmtpPort = Convert.ToInt32(smtpPort);
        //    SmtpUsername = smtpUsername;
        //    SmtpPassword = smtpPassword;
        //    Ssl = ssl;

        //}

        //public void SaveConfToSetting()
        //{
        //    Properties.Settings.Default.smtpCorrectConf = CorrectConfiguration;
        //    Properties.Settings.Default.smtpSerwer = SmtpServer;
        //    Properties.Settings.Default.smtpPort = SmtpPort;
        //    Properties.Settings.Default.smtpUsername = SmtpUsername;
        //    Properties.Settings.Default.smtpPassword = SmtpPassword;
        //    Properties.Settings.Default.ssl = Ssl;
        //    Properties.Settings.Default.Save();
        //}
        //public void LoadConfFromSetting()
        //{
        //    CorrectConfiguration = Properties.Settings.Default.smtpCorrectConf;
        //    SmtpServer = Properties.Settings.Default.smtpSerwer;
        //    SmtpPort = Properties.Settings.Default.smtpPort;
        //    SmtpUsername = Properties.Settings.Default.smtpUsername;
        //    SmtpPassword = Properties.Settings.Default.smtpPassword;
        //    Ssl = Properties.Settings.Default.ssl;
        //}
        public bool CheckConnect()
        {
            bool check = false;
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPort, Ssl);
                    check = client.IsConnected;

                    client.Send(TestMail());
                    client.Disconnect(true);
                }
                return check;
            }
            catch (Exception e)
            {
                Error.ShowError(e.Message);
                //Task.Run(() => LogToFile.SaveLogToFile("SendMail: StackTrace: " + e.StackTrace + "Massage: " + e.Message + "Response: "));
                return check;
            }

        }
        private MimeMessage TestMail()
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Test", SmtpUsername));
            mail.To.Add(new MailboxAddress("Test", SmtpUsername));
            mail.Subject = "Test";
            mail.Body = new TextPart("plain") { Text = "Test" };
            return mail;
        }
        //public bool CheckAuth()
        //{
        //    bool check = false;
        //    try
        //    {
        //        using (var client = new SmtpClient())
        //        {
        //            client.Connect(SmtpServer, SmtpPort);

        //            client.Authenticate(SmtpUsername, SmtpPassword);

        //            check = client.IsAuthenticated;

        //            client.Disconnect(true);
        //        }
        //        return check;
        //    }
        //    catch (Exception e)
        //    {
        //        //Task.Run(() => LogToFile.SaveLogToFile("SendMail: StackTrace: " + e.StackTrace + "Massage: " + e.Message + "Response: "));
        //        return check;
        //    }


        //}

        public void SendReport(string message, Stream filePath, string name, string url)
        {
            if (CorrectConfiguration == false)
            {
                return;
            }

            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Porównywarka", "biuro@arante.pl"));
            mail.To.Add(new MailboxAddress("Porównywarka", "biuro@arante.pl"));
            mail.Subject = $"[Porównywarka] Raport {url}";

            var builder = new BodyBuilder { TextBody = message };

            builder.Attachments.Add(name,filePath);

            mail.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(SmtpServer, SmtpPort, Ssl);
                client.Authenticate(SmtpUsername, SmtpPassword);
                client.Send(mail);
            }
            //mail.Body = new TextPart("plain") { Text = message };


            //// create an image attachment for the file located at path
            //var attachment = new MimePart("text", "csv")
            //{

            //    //Content = new MimeContent(File.OpenRead(filePath), ContentEncoding.Default),
            //    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            //    ContentTransferEncoding = ContentEncoding.Base64,
            //    FileName = Path.GetFileName(filePath)
            //};
        }
    }
}

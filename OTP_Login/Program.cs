using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace OTP_Login
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Welcome the system. \n\nPlease enter your e-mail: ");
            string email = Console.ReadLine();

            // check is valid email
            CheckEmail ep = new CheckEmail();
            bool kontrol = ep.IsValidEmail(email);

            Random rd = new Random();
            string otpcode = (rd.Next(100000, 999999)).ToString();

            if(kontrol == true)
            {
                try
                {

                    SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com", 587);

                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    System.Net.NetworkCredential basicAuthenticationInfo = new
                    System.Net.NetworkCredential("your@email.com", "emailpass");
                    mySmtpClient.Credentials = basicAuthenticationInfo;
                    mySmtpClient.EnableSsl = true;

                    // add from,to mailaddresses
                    MailAddress from = new MailAddress("your@email.com", "System");
                    MailAddress to = new MailAddress(email, "User");
                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                    // add ReplyTo
                    MailAddress replyTo = new MailAddress("your@email.com");
                    myMail.ReplyToList.Add(replyTo);

                    // set subject and encoding
                    myMail.Subject = "Your OTP Code";
                    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = "<b>Your OTP code for login: </b>" + otpcode;
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true;
                    

                    mySmtpClient.Send(myMail);
                    Console.Write("Your OTP code sent to your e-mail. Please write the OTP code: ");
                    string userotp = Console.ReadLine();

                    if(otpcode == userotp)
                    {
                        Console.WriteLine("Success Login!");
                        Console.ReadLine();
                    } else
                    {
                        Console.WriteLine("Wrong OTP code. Program is closing...");
                        System.Threading.Thread.Sleep(2000);
                    }
                    
                }

                catch (SmtpException ex)
                {
                    throw new ApplicationException
                      ("SmtpException has occured: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            } else if(kontrol == false)
            {
                Console.WriteLine("Error: Non-valid email. Program is closing...");
                System.Threading.Thread.Sleep(2000);
            } else
            {
                Console.WriteLine("Error: Email is not recognized.");
            }

            // check end
                     
        }
    }
}

using System.Net;
using System.Net.Mail;
using System.Windows;

namespace HotelSmartManagement.Common.Helpers
{
    public static class EmailHelper
    {
        private static readonly string _smtpServer = "smtp.gmail.com";
        private static readonly int _smtpPort = 587;
        private static readonly string _smtpEmail = "bookings.dotnethms@gmail.com";
        private static readonly string _smtpPassword = "tqwtgkxluwnlvuav"; // Yes, I know this is here. Normally this would be in a secure location, but as this is an app password for email sending, it's not critical.

        public static async Task SendVerificationEmailAsync(string toEmail, int verificationCode)
        {
            var subject = "Your Verification Code - Hotel Smart Management System";
            var body = $@"
            <html>
                <body style='font-family: Arial, sans-serif;'>
                    <div style='background-color: #f9f9f9; padding: 20px;'>
                        <h2 style='color: #3498db;'>Hotel Smart Management System</h2>
                        <p>Dear New Staff Member,</p>
                        <p>Thank you for using <strong>Hotel Smart Management System</strong>. Please use the following verification code to complete your sign up:</p>
                        <p style='font-size: 24px; font-weight: bold; color: #2c3e50;'>{verificationCode}</p>
                        <p>If you did not request this code, please ignore this email.</p>
                        <br/>
                        <p>Best regards,<br/>Hotel Smart Management System Team</p>
                    </div>
                </body>
            </html>";

            await SendEmailAsync(toEmail, subject, body.Replace("{{verificationCode}}", verificationCode.ToString()));
        }

        private static async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_smtpEmail),
                    Subject = subject,
                    Body = content,
                    IsBodyHtml = true
                };
                message.To.Add(toEmail);

                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(_smtpEmail, _smtpPassword);
                    await smtpClient.SendMailAsync(message);
                }
                Console.WriteLine($"Email sent to {toEmail} with subject '{subject}'");
            } 
            catch (Exception e)
            {
                // Log the exception and display a message to the user.
                Console.WriteLine($"An error occurred while sending an email: {e.Message}");
                MessageBox.Show("An error occurred while sending an email. Please try again later.", "Email Error");
            }
        }
    }
}

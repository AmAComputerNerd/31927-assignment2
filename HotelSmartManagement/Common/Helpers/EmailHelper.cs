using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;
using iText.Kernel.Pdf.Canvas.Parser.ClipperLib;
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

        public static async Task SendLeaveRequestCreationEmailAsync(string toEmail, LeaveRequest leaveRequest)
        {
            var isApproved = leaveRequest.IsApproved ? "Approved" : "Pending";

            var subject = "Your Leave Request Has Been Submitted - Hotel Smart Management System";
            var body = $@"
<html>
    <body style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px;'>
        <div style='background-color: #ffffff; border: 1px solid #ddd; border-radius: 8px; padding: 20px; max-width: 600px; margin: auto;'>
            <h2 style='color: #3498db; text-align: center;'>Hotel Smart Management System</h2>
            <p>Dear User,</p>
            <p>Your leave request has been successfully submitted in the <strong>Hotel Smart Management System</strong>.</p>
            <p>Here are the details of your request:</p>
            <ul style='list-style-type: none; padding: 0;'>
                <li><strong>Leave Request ID:</strong> {leaveRequest.UniqueId}</li>
                <li><strong>Start Time:</strong> {leaveRequest.StartAt}</li>
                <li><strong>End Time:</strong> {leaveRequest.EndAt}</li>
                <li><strong>Description:</strong> {leaveRequest.Description}</li>
                <li><strong>Current Approval Status:</strong> {isApproved}</li>
            </ul>
            <p>If you have any questions or need to make changes to your request, please contact your HR department or log in to your account on the <strong>Hotel Smart Management System</strong>.</p>
            <p>Thank you for using our system!</p>
            <br/>
            <p style='font-size: 12px; color: #777; text-align: center;'>Hotel Smart Management System - All rights reserved.</p>
        </div>
    </body>
</html>";
            await SendEmailAsync(toEmail, subject, body);
        }

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

        public static async Task SendReservationEmailASync(Reservation reservation, string toEmail, string attachment)
        {
            var subject = "Your Requested Reservation - Hotel Smart Management System";
            var body = $@"
            <html>
                <body style='font-family: Arial, sans-serif;'>
                    <div style='background-color: #f9f9f9; padding: 20px;'>
                        <h2 style='color: #3498db;'>Hotel Smart Management System</h2>
                        <p>Dear Staff Member,</p>
                        <p>You requested a copy of {reservation?.Guest?.FullName ?? "Unknown"}'s reservation under reference {reservation?.Reference ?? "Unknown"}.</p>
                        <p>This has been attached below.</p>
                        <br/>
                        <p>Best regards,<br/>Hotel Smart Management System Team</p>
                    </div>
                </body>
            </html>";

            await SendEmailAsync(toEmail, subject, body, new List<string>() { attachment });
        }

        private static async Task SendEmailAsync(string toEmail, string subject, string content, List<string>? attachments = null)
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

                if (attachments != null)
                {
                    foreach (string attachment in attachments)
                    {
                        Attachment newAttachment = new Attachment(attachment);
                        message.Attachments.Add(newAttachment);
                    }
                }

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

using AssignmentScheduler.Service.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AssignmentScheduler.Repository.Interfaces;

namespace AssignmentScheduler.Service;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public MailService(IConfiguration configuration,
                       ILogger<MailService> logger,
                       IUserRepository userRepository)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<(bool, string?)> SendConfirmationEmailAsync(string email, string firstName)
    {
        //var verificationCode = Generator.GenerateCode(10);
        //var emailContent = string.Format(MailContent.RegistrationConfirm, firstName, verificationCode);

        var mimeMessage = new MimeMessage()
        {
            Subject = "Verification mail",
            Body = new TextPart(TextFormat.Html)
            {
                Text = ""
            }
        };
        mimeMessage.From.Add(new MailboxAddress("TourGuide-noreply", "tourguideapp@outlook.com"));
        mimeMessage.To.Add(new MailboxAddress("Self", email));

        var success = await SendMailAsync(mimeMessage);
        if (!success)
        {
            _logger.LogError($"Failed to send email to: '{email}'");
            return (false, null);
        }

        return (true, "");
    }

    public async Task<(bool, string?)> SendPasswordResetEmailAsync(string email, string firstName)
    {
        //var exists = await _repositoryBase.EmailExistsAsync(email);
        //if (!exists)
        //{
        //    _logger.LogError($"Requested email '{email}' doesn't exists.");
        //    return (false, null);
        //}

        //var newPassword = Generator.GenerateCode(15);
        //var emailContent = string.Format(MailContent.PasswordReset, firstName, newPassword);

        var mimeMessage = new MimeMessage()
        {
            Subject = "Verification mail",
            Body = new TextPart(TextFormat.Html)
            {
                Text = ""
            }
        };
        mimeMessage.From.Add(new MailboxAddress("TourGuide-noreply", "tourguideapp@outlook.com"));
        mimeMessage.To.Add(new MailboxAddress("Self", email));

        var success = await SendMailAsync(mimeMessage);
        if (!success)
        {
            _logger.LogError($"Failed to send email to: '{email}'");
            return (false, null);
        }

        return (true, "");
    }

    private async Task<bool> SendMailAsync(MimeMessage message)
    {
        var mail = _configuration.GetSection("Mail");
        var smtp = mail.GetSection("Smtp").Value;
        int.TryParse(mail.GetSection("Port").Value, out var port);
        bool.TryParse(mail.GetSection("UseSsl").Value, out var useSsl);
        var username = mail.GetSection("Username").Value;
        var password = mail.GetSection("Password").Value;

        var attempts = 0;
        var success = false;
        while (attempts <= 3 && !success)
        {
            attempts++;
            using var smtpClient = new SmtpClient();
            try
            {
                await smtpClient.ConnectAsync(smtp, port, useSsl);
                await smtpClient.AuthenticateAsync(username, password);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);

                success = true;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Exception occured while trying to send an email. Exception message: {exception.Message}");
                success = false;
            }
        }

        return success;
    }
}

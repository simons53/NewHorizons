using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace NewHorizons.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // No-op for development (email not actually sent)
            return Task.CompletedTask;
        }
    }
}
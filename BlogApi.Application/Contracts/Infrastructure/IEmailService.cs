using Blog.Application.Models;

namespace Blog.Application.Contracts.Infrastructure
{
    interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}

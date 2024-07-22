using Microsoft.AspNetCore.Http;
using MimeKit;

namespace Application.Common.Interfaces.Services.Email;

public class Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
{
    public List<MailboxAddress> To { get; set; } = [.. to.Select(x => new MailboxAddress("Test", x))];
    public string Subject { get; set; } = subject;
    public string Content { get; set; } = content;
    public IFormFileCollection Attachments { get; set; } = attachments;
}

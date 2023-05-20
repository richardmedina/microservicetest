namespace MicroserviceTest.Api.Email.Models.Email
{
    public record EmailSendRequest(string From, string To, string Subject, string Content);
}

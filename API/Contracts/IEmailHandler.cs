namespace API.Contracts
{
    public interface IEmailHandler
    {
        public void SendEmail(string toEmail,string subject, string htmlMessage);
    }
}

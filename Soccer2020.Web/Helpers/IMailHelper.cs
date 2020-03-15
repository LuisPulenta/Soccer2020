using Soccer2020.Common.Models;

namespace Soccer2020.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
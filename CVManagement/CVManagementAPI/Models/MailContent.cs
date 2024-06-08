using CVManagementAPI.Helpers;
using Newtonsoft.Json;

namespace CVManagementAPI.Models
{
    public class MailContent
    {
        public int IdCvNeedToSendMail { get; set; }
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public string? Salary { get; set; } = "0";

        //Các email muốn cc tới
        public List<string>? EmailsCC { get; set; } = new List<string>() { Constants.Gmail.CC_DEFAULT };

        //Các email muốn cc tới
        public List<string>? EmailsBCC { get; set; } = new List<string>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementAPI.Models
{
    [Table("EmailMessage")]
    public class EmailMessage : BaseEntity
    {
        public string Subject { get; set; }
        public string Recipient { get; set; }
        public string SenderEmail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsSent { get; set; }
        public ColumnLayout ColumnLayout { get; set; }
    }
}

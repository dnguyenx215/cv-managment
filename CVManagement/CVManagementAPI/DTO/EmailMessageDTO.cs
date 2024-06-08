namespace CVManagementAPI.DTO
{
    public class EmailMessageDTO : BaseDTO
    {
        public string Subject { get; set; }
        public string Recipient { get; set; }
        public string SenderEmail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsSent { get; set; }
    }
}

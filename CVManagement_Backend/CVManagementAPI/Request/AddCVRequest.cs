namespace CVManagementAPI.Request
{
    public class AddCVRequest
    {
        public string NameCandidate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string School { get; set; }
        public string YearOfBirth { get; set; }
        public string? SourceOfCV { get; set; }
        public DateTime? DateReceiveCV { get; set; }
        public string? CVNote { get; set; }
        public int PositionId { get; set; }
        public int SourceId { get; set; }
        public string AppUserId { get; set; }
    }
}
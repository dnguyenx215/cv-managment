namespace CVManagementAPI.Request
{
    public class PutCVRequest
    {
        public int Id { get; set; }
        public string NameCandidate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string School { get; set; }
        public string YearOfBirth { get; set; }
        public string? SourceOfCV { get; set; }
        public DateTime? DateReceiveCV { get; set; }
        public string? TimeOfInterview { get; set; }
        public DateTime? DateOfInterview { get; set; }
        public string? ReviewCV { get; set; }
        public string? Status { get; set; }
        public string? CVEvaluate { get; set; }
        public string? CVNote { get; set; }
        public string? Rate { get; set; }
        public DateTime? DateAcceptJob { get; set; }
        public int PositionId { get; set; }
        public string? PositionName { get; set; }
        public int SourceId { get; set; }
        public string AppUserId { get; set; }
    }
}

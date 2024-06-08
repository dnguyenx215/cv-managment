using CVManagementAPI.Helpers;
using CVManagementAPI.Models;

namespace CVManagementAPI.DTO
{
    public class CvDTO : BaseDTO
    {
        public string NameCandidate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string School { get; set; }
        public string YearOfBirth { get; set; }
        public string? SourceOfCV { get; set; }
        public DateTime? DateReceiveCV { get; set; } = DateTime.Now;
        public string? TimeOfInterview { get; set; }
        public DateTime? DateOfInterview { get; set; }
        public string? ReviewCV { get; set; }
        public string? Status { get; set; } = Constants.StatusCv.REVIEW_CV_WAITING;
        public string? CVEvaluate { get; set; }
        public string? CVNote { get; set; }
        public string? Rate { get; set; }
        public DateTime? DateAcceptJob { get; set; }
        public int PositionId { get; set; }
        public string? PositionName { get; set; }

        public int SourceId { get; set; }
        public string? AppUserId { get; set; }
        public string? IsSendMail { get; set; } = Constants.EmailStatus.NOT_SENT;
        public int? ColumnId { get; set; } = 1;
        public MailContent? MailContent { get; set; }
        public bool? IsSelectedToSendMail { get; set; } = false;
        public bool? IsEmailContendUpdated { get; set; } = false;
    }
}

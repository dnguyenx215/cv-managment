using CVManagementAPI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementAPI.Models
{
    [Table("CV")]
    public class CV : BaseEntity
    {
        [Required]
        public string NameCandidate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
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
        public int? PositionId { get; set; }
        public int? SourceId { get; set; }

        public int? ColumnId { get; set; } = 1;
        public string? AppUserId { get; set; }
        public Position Position { get; set; } = null!;
        public Source Source { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public ColumnLayout ColumnLayout { get; set; }
        //not_send,sending,sent
        public string? IsSendMail { get; set; } = Constants.EmailStatus.NOT_SENT;

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CVManagementAPI.Models
{
    [Table("ColumnLayout")]
    public class ColumnLayout : BaseEntity
    {
        public string? ColumnName { get; set; }
        public ICollection<CV>? CVs { get; set; } = new List<CV>();

        /// <summary>
        /// Column nay co duoc sua columnGroupSetting hay la khong
        /// </summary>
        public bool? IsEditableSetting { get; set; }

        /// <summary>
        /// Check xem khi keo vao column nay co cho gui mail hay la khong
        /// </summary>
        public bool? IsSendMail { get; set; }

        [JsonIgnore]
        public ICollection<ColumnRelationship> Relationships { get; set; }

        /// <summary>
        /// Những column chấp nhận bản ghi FROM THIS COLUMN
        /// </summary>
        [JsonIgnore]
        public ICollection<ColumnRelationship>? PullColumns { get; } = new List<ColumnRelationship>();

        /// <summary>
        /// Những column được phép thả bản ghi IN THIS COLUMN
        /// </summary>
        [JsonIgnore]
        public ICollection<ColumnRelationship>? PutColumns { get; } = new List<ColumnRelationship>();

        public int? EmailMessageId { get; set; }

        public EmailMessage? EmailMessage { get; set; }
    }
}

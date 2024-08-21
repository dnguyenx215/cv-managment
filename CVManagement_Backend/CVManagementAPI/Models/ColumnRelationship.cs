using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementAPI.Models
{
    [Table("ColumnRelationship")]
    public class ColumnRelationship : BaseEntity
    {

        public int? ColumnLayoutId { get; set; }

        public ColumnLayout? ColumnOwnRelationship { get; set; }

        public int? PutColumnId { get; set; }

        public ColumnLayout? PutColumn { get; set; }

        public int? PullColumnId { get; set; }

        public ColumnLayout? PullColumn { get; set; }
    }
}

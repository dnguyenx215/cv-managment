using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementAPI.Models
{
    [Table("Source")]
    public class Source : BaseEntity
    {
        public string NameSource { get; set; }
        public ICollection<CV> CVs { get; } = new List<CV>();

    }
}

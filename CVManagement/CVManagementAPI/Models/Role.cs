using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementAPI.Models
{
    [Table("Role")]
    public class Role : BaseEntity
    {

        [Required]
        public string RoleName { get; set; }
    }
}

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementAPI.Models
{
    [Table("Position")]
    public class Position : BaseEntity
    {

        public string PositionName { get; set; }
        public ICollection<CV>? CVs { get; set; } = new List<CV>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

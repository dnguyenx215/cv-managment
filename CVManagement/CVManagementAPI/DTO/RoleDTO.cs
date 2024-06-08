using System.Text.Json;

namespace CVManagementAPI.DTO
{
    public class RoleDTO : BaseDTO
    {
        public string RoleName { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);

        }
    }
}

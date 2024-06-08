namespace CVManagementAPI.DTO
{
    public abstract class BaseDTO
    {
        public int? Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}

namespace CVManagementAPI.DTO
{
    public class ColumnRelationshipDTO : BaseDTO
    {
        public int? ColumnLayoutId { get; set; }

        public ColumnLayoutDTO? ColumnOwnRelationship { get; set; }
        public int? PutColumnId { get; set; }
        public ColumnLayoutDTO? PutColumn { get; set; }
        public int? PullColumnId { get; set; }
        public ColumnLayoutDTO? PullColumn { get; set; }
    }
}

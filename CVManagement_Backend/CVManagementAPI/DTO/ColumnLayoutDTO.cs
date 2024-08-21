namespace CVManagementAPI.DTO
{
    public class ColumnLayoutDTO : BaseDTO
    {
        public string? ColumnName { get; set; }

        /// <summary>
        /// Column nay co duoc sua columnGroupSetting hay la khong
        /// </summary>
        public bool? IsEditableSetting { get; set; }

        /// <summary>
        /// Check xem khi keo vao column nay co cho gui mail hay la khong
        /// </summary>
        public bool? IsSendMail { get; set; }

        public ICollection<CvDTO>? CVs { get; set; }

    }
}

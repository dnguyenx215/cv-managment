namespace CVManagementAPI.Services.Interface
{

    /// <summary>
    /// Interface điều khiển việc cache với redis
    /// </summary>
    public interface ICacheService
    {
        #region Methods
        /// <summary>
        /// Lấy dữ liệu trong cache bằng key
        /// </summary>
        /// <param name="key">key của cache</param>
        /// <returns>dữ liệu ứng với key</returns>
        TCacheData Get<TCacheData>(string key);

        /// <summary>
        /// Lấy dữ liệu trong cache bằng key bất đồng bộ
        /// </summary>
        /// <param name="key">key của cache</param>
        /// <returns>dữ liệu ứng với key</returns>
        Task<TCacheData> GetAsync<TCacheData>(string key);

        /// <summary>
        /// Gán dữ liệu cho key cùng với thời gian hết hạn
        /// </summary>
        /// <param name="key">key muốn gán</param>
        /// <param name="value">giá trị muốn gán</param>
        /// <param name="expirationTime">thời gian hết hạn</param>
        /// <returns>true - thành công, false - thất bại</returns>
        bool Set<TCacheData>(string key, TCacheData value, DateTimeOffset expirationTime);

        /// <summary>
        /// Gán dữ liệu cho key cùng với thời gian hết hạn bất đồng bộ
        /// </summary>
        /// <param name="key">key muốn gán</param>
        /// <param name="value">giá trị muốn gán</param>
        /// <param name="expirationTime">thời gian hết hạn</param>
        /// <returns>true - thành công, false - thất bại</returns>
        Task<bool> SetAsync<TCacheData>(string key, TCacheData value, DateTimeOffset expirationTime);

        /// <summary>
        /// Xóa data bằng key
        /// </summary>
        /// <param name="key">key của cache</param>
        /// <returns></returns>
        object Remove(string key);

        /// <summary>
        /// Xóa data bằng key bất đồng bộ
        /// </summary>
        /// <param name="key">key của cache</param>
        /// <returns></returns>
        Task<object> RemoveAsync(string key);
        #endregion
    }
}

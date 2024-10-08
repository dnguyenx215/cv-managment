//using CVManagementAPI.Services.Interface;
//using StackExchange.Redis;
//using System.Text.Json;

//namespace CVManagementAPI.Services
//{
//    public class CacheService : ICacheService
//    {
//        #region Fields
//        private readonly IDatabase _cacheDb;
//        #endregion

//        #region Constructors
//        public CacheService(string redisConnection)
//        {
//            ConfigurationOptions option = new ConfigurationOptions
//            {
//                AbortOnConnectFail = false,
//                EndPoints = { redisConnection }
//            };

//            var redis = ConnectionMultiplexer.Connect(option);
//            _cacheDb = redis.GetDatabase();


//            // Ping thử
//            //if (_cacheDb.Ping().TotalSeconds > 5)
//            //{
//            //    throw new TimeoutException("Server Redis không hoạt động");
//            //}


//            // _cacheDb.StringSetAsync("hello", JsonSerializer.Serialize(new ErrorObject { Field = "a", Message = "a" }));

//            // Đọc lại dữ liệu
//            //var vl = _cacheDb.StringGet("hello");
//            // Console.WriteLine(vl);

//        }
//        #endregion

//        #region Methods
//        /// <summary>
//        /// Lấy dữ liệu trong cache bằng key
//        /// </summary>
//        /// <param name="key">key của cache</param>
//        /// <returns>dữ liệu ứng với key</returns>

//        public TCacheData Get<TCacheData>(string key)
//        {
//            var value = _cacheDb.StringGet(key);

//            if (!string.IsNullOrEmpty(value))
//            {
//                return JsonSerializer.Deserialize<TCacheData>(value);
//            }
//            else
//            {
//                return default;
//            }
//        }

//        /// <summary>
//        /// Lấy dữ liệu trong cache bằng key bất đồng bộ
//        /// </summary>
//        /// <param name="key">key của cache</param>
//        /// <returns>dữ liệu ứng với key</returns>

//        public async Task<TCacheData> GetAsync<TCacheData>(string key)
//        {
//            var value = await _cacheDb.StringGetAsync(key);

//            if (!string.IsNullOrEmpty(value))
//            {
//                return JsonSerializer.Deserialize<TCacheData>(value);
//            }
//            else
//            {
//                return default;
//            }
//        }

//        /// <summary>
//        /// Gán dữ liệu cho key cùng với thời gian hết hạn
//        /// </summary>
//        /// <param name="key">key muốn gán</param>
//        /// <param name="value">giá trị muốn gán</param>
//        /// <param name="expirationTime">thời gian hết hạn</param>
//        /// <returns>true - thành công, false - thất bại</returns>

//        public bool Set<TCacheData>(string key, TCacheData value, DateTimeOffset expirationTime)
//        {
//            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);

//            // cần Serialize (tuần tự hóa) dữ liệu để chuyển sang dạng json, set vào key
//            var isSet = _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirtyTime);

//            return isSet;
//        }

//        /// <summary>
//        /// Gán dữ liệu cho key cùng với thời gian hết hạn bất đồng bộ
//        /// </summary>
//        /// <param name="key">key muốn gán</param>
//        /// <param name="value">giá trị muốn gán</param>
//        /// <param name="expirationTime">thời gian hết hạn</param>
//        /// <returns>true - thành công, false - thất bại</returns>

//        public async Task<bool> SetAsync<TCacheData>(string key, TCacheData value, DateTimeOffset expirationTime)
//        {
//            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);

//            // cần Serialize (tuần tự hóa) dữ liệu để chuyển sang dạng json, set vào key
//            var isSet = await _cacheDb.StringSetAsync(key, JsonSerializer.Serialize(value), expirtyTime);

//            return isSet;
//        }

//        /// <summary>
//        /// Xóa data bằng key
//        /// </summary>
//        /// <param name="key">key của cache</param>
//        /// <returns></returns>

//        public object Remove(string key)
//        {
//            var exist = _cacheDb.KeyExists(key);

//            if (exist)
//            {
//                return _cacheDb.KeyDelete(key);
//            }
//            else
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// Xóa data bằng key bất đồng bộ
//        /// </summary>
//        /// <param name="key">key của cache</param>
//        /// <returns></returns>

//        public async Task<object> RemoveAsync(string key)
//        {
//            var exist = await _cacheDb.KeyExistsAsync(key);

//            if (exist)
//            {
//                return await _cacheDb.KeyDeleteAsync(key);
//            }
//            else
//            {
//                return false;
//            }
//        }
//        #endregion
//    }
//}

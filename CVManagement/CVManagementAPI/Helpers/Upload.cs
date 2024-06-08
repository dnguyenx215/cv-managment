using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace CVManagementAPI.Helpers
{
    public class Upload
    {
            static public Account account = new Account("dnfnp1vdp", "651395863673797", "1f9u7M6yUZUemqVvLqT0sM6bvP4");
            static public Cloudinary _cloudinary = new Cloudinary(account);
            public static async Task<string> Upfile(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    return "";
                }
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = "pdf" + "_" + DateTime.Now.Ticks,
                    };
                    var uploadResult = await Upload._cloudinary.UploadAsync(uploadParams);
                    string url = uploadResult.SecureUrl.ToString();
                    return url;
                }
            }
    }
}

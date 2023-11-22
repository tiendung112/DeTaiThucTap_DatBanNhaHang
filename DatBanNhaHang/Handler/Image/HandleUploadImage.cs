using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace DatBanNhaHang.Handler.Image
{
    public class HandleUploadImage
    {
        static string cloudName = "dleakiid7";
        static string apiKey = "337694681628724";
        static string apiSecret = "Gx3gBG6eLlG_NZoMn3FuOX4KrpE";
        static public Account account = new Account(cloudName, apiKey, apiSecret);
        static public Cloudinary _cloudinary = new Cloudinary(account);
        public static async Task<string> Upfile(IFormFile file,string duongdan)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Không có tập tin được chọn.");
            }
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = $"{duongdan}/" + $"{duongdan}" + DateTime.Now.Ticks + "image",
                    Transformation = new Transformation().Width(300).Height(400).Crop("fill") 
                };
                var uploadResult = await HandleUploadImage._cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }
                string imageUrl = uploadResult.SecureUrl.ToString();
                return imageUrl;
            }
        }
    }
}

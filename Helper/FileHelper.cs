using ShopMVC.DTO;

namespace ShopMVC.Helper
{
    public class FileHelper
    {
        private static string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        private static long MaxSize = 3145728;
        public static async Task<Media> Save(IFormFile file, string folder )
        {
            if(file.Length > MaxSize)
            {
                return null;
            }
            string uploadsFolder = Path.Combine(_uploadsFolder, folder);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            string relativeFilePath = Path.Combine("images", Path.GetRelativePath(_uploadsFolder, filePath)) ;

            return new Media
            {
                Src = relativeFilePath,
                Type = IsVideo(file) ? Media.TypeEnum.VIDEO : Media.TypeEnum.IMAGE
            };
        }
        public static async Task<List<Media>> SaveRange(IEnumerable<IFormFile> files, string folder)
        {
            List<Task<Media>> tasks = new List<Task<Media>>();

            foreach (IFormFile file in files)
            {
                tasks.Add(Save(file, folder));
            }
            Media[] savedFilePaths = await Task.WhenAll(tasks);
            List<Media> result = savedFilePaths.ToList();
            return result;
        }

        public static bool IsImage(IFormFile file)
        {
            return file.ContentType.StartsWith("image");
        }

        public static bool IsVideo(IFormFile file)
        {
            return file.ContentType.StartsWith("video");
        }
    }
}

using Microsoft.AspNetCore.Http;

namespace XUnitBlog.Domain.Services;

public class FileService(string wwwrootPath)
{
    private readonly string _uploadsPath = Path.Combine(wwwrootPath, "uploads");

    public async Task<string> UploadAsync(IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_uploadsPath, fileName);

        if (!Directory.Exists(_uploadsPath))
        {
            Directory.CreateDirectory(_uploadsPath);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        if (!File.Exists(filePath))
        {
            throw new Exception("Erro ao salvar o arquivo");
        }

        return Path.Combine("uploads", fileName);
    }

    public bool Delete(string filePath)
    {
        try
        {
            var fileName = Path.GetFileName(filePath);
            var fullFilePath = Path.Combine(_uploadsPath, fileName);

            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

using System.Text;
using Microsoft.AspNetCore.Http;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.Test.Files;

public class FileServiceTest : IDisposable
{
    private readonly string _testUploadPath;
    private readonly FileService _fileService;

    public FileServiceTest()
    {
        _testUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "temp");
        _fileService = new FileService(_testUploadPath);
    }

    [Fact]
    public async Task ShouldSaveFileToFileSystem()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("Um arquivo de teste");
        var file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "teste.txt");

        // Action
        var filePath = await _fileService.UploadAsync(file);

        // Assert
        Assert.NotEmpty(filePath);
    }

    [Fact]
    public async Task ShouldSaveFileWhenUploadPathDoesNotExist()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("Um arquivo de teste");
        var file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "teste.txt");

        // Action
        if (Directory.Exists(_testUploadPath))
        {
            Directory.Delete(_testUploadPath, true);
        }

        var filePath = await _fileService.UploadAsync(file);

        // Assert
        Assert.NotEmpty(filePath);
    }

    [Fact]
    public async Task ShouldDeleteFile()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("Um arquivo de teste");
        var file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "teste.txt");

        // Action
        var filePath = await _fileService.UploadAsync(file);
        var result = _fileService.Delete(filePath);

        Assert.True(result);
    }

    public void Dispose()
    {
        Directory.Delete(_testUploadPath, true);
    }
}

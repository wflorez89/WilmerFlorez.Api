using Microsoft.AspNetCore.Http;

namespace WilmerFlorez.Logic.Interfaces
{
    public interface IImageLogic
    {
        string Upload(IFormFile input, string webRootPath, string host);
    }
}

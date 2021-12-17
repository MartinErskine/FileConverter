using FileConverter.Service.Models;

namespace FileConverter.Service
{
    public interface ICsvService
    {
        string ParseFile(ApplicationArguments applicationArguments);
    }
}
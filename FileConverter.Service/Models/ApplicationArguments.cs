namespace FileConverter.Service.Models
{
    public class ApplicationArguments
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Filename { get; set; }

        public string FromDatabase { get; set; }
    }
}
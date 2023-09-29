namespace Radiao.Api.ViewModels
{
    public class ErrorResponseViewModel
    {
        public bool Success { get; set; } = false;

        public List<string> Errors { get; set; }
    }
}

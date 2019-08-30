namespace GD6.Common
{
    public interface IRequestSelect : IRequest
    {
        string Term { get; set; }
        int SkipCount { get; set; }
        int MaxResultCount { get; set; }
    }
}
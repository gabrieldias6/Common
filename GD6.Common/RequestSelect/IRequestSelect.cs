namespace GD6.Common
{
    public interface IRequestSelect : IRequest
    {
        int? Id { get; set; }
        string Term { get; set; }
        int SkipCount { get; set; }
        int MaxResultCount { get; set; }
    }
}
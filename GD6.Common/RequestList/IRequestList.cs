namespace GD6.Common
{
    public interface IRequestList : IRequest
    {
        int Draw { get; set; }
        string Sorting { get; set; }
        int SkipCount { get; set; }
        int MaxResultCount { get; set; }
    }
}
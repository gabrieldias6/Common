namespace GD6.Common
{
    public interface IRequestList : IRequest
    {
        int Draw { get; set; }
        string Sorting { get; set; }
        int Start { get; set; }
        int Length { get; set; }
        string Search { get; set; }
    }
}
namespace GD6.Common
{
    public class RequestList : IRequestList
    {
        public int Draw { get; set; }
        public virtual string Value { get; set; }
        public virtual string Sorting { get; set; }
        public virtual int SkipCount { get; set; } = 0;
        public virtual int MaxResultCount { get; set; } = 10;
    }
}
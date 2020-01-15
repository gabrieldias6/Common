namespace GD6.Common
{
    public class RequestList : IRequestList
    {
        public int Draw { get; set; }
        public virtual string Value { get; set; }
        public virtual string Sorting { get; set; }
        public virtual int Start { get; set; } = 0;
        public virtual int Length { get; set; } = 10;
        public virtual string Search { get; set; }
    }
}
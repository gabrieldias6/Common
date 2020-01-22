namespace GD6.Common
{
    public class RequestSelect : IRequestSelect
    {
        public int? Id { get; set; }

        private string _value;
        public virtual string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _value = value;
            }
        }
        public virtual string Term
        {
            get
            {
                return _value;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _value = value;
            }
        }
        public virtual int SkipCount { get; set; } = 0;
        public virtual int MaxResultCount { get; set; } = 10;
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public class RequestSelect : IRequestSelect
    {
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
    }
}

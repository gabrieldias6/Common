using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public interface IRequestList
    {
        int Draw { get; set; }
        string Value { get; set; }
        string Sorting { get; set; }
        int SkipCount { get; set; }
        int MaxResultCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public interface IListResultDto<T> where T : class
    {
        int TotalCount { get; set; }
        IReadOnlyList<T> Items { get; set; }
    }
}

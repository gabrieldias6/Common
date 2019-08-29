using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public class ListResultDto<T> : IListResultDto<T> where T : class
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public ListResultDto(int totalCount, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}
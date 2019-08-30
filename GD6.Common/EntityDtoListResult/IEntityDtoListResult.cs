using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public interface IEntityDtoListResult<TEntityDtoList> 
        where TEntityDtoList : class, IEntityDtoList
    {
        int Draw { get; set; }
        int RecordsTotal { get; set; }
        int RecordsFiltered { get; set; }
        IReadOnlyList<TEntityDtoList> Data { get; set; }
    }
}

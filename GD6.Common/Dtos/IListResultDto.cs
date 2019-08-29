using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public interface IListResultDto<TEntityList> 
        where TEntityList : class, ILista
    {
        int Draw { get; set; }
        int RecordsTotal { get; set; }
        int RecordsFiltered { get; set; }
        IReadOnlyList<TEntityList> Data { get; set; }
    }
}

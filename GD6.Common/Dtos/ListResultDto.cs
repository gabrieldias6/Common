using System.Collections.Generic;

namespace GD6.Common
{
    public class ListResultDto<TEntityList> : IListResultDto<TEntityList> 
        where TEntityList : class, ILista
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IReadOnlyList<TEntityList> Data { get; set; }

        public ListResultDto() { }

        public ListResultDto(int recordsTotal, IReadOnlyList<TEntityList> data)
        {
            RecordsTotal = recordsTotal;
            Data = data;
        }
    }
}
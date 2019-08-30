using System.Collections.Generic;

namespace GD6.Common
{
    public class EntityDtoListResult<TEntityList> : IEntityDtoListResult<TEntityList> 
        where TEntityList : class, IEntityDtoList
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IReadOnlyList<TEntityList> Data { get; set; }

        public EntityDtoListResult() { }

        public EntityDtoListResult(int recordsTotal, IReadOnlyList<TEntityList> data)
        {
            RecordsTotal = recordsTotal;
            Data = data;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GD6.Common
{
    public interface IServiceBase<TEntityDto, TEntityDtoList, TEntityDtoSelect, TRequestList, TRequestSelect>
        where TEntityDto : class, IEntityBaseDto
        where TEntityDtoList : class, IEntityDtoList
        where TEntityDtoSelect : class, IEntityDtoSelect
        where TRequestList : class, IRequestList
        where TRequestSelect : class, IRequestSelect
    {
        Task<TEntityDto> GetById(int id);

        IEntityDtoListResult<TEntityDtoList> GetAll(TRequestList request);

        IEnumerable<TEntityDtoSelect> GetAllSelect(TRequestSelect request);

        Task<TEntityDto> Create(TEntityDto input);

        Task<IEnumerable<TEntityDto>> CreateMany(IEnumerable<TEntityDto> entitiesDtos);

        Task<TEntityDto> Update(int id, TEntityDto input);

        Task<IEnumerable<TEntityDto>> UpdateMany(IEnumerable<TEntityDto> entitiesDtos);

        Task Delete(int id);

        Task DeleteMany(IEnumerable<int> ids);
    }
}

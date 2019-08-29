using System.Collections.Generic;
using System.Threading.Tasks;

namespace GD6.Common
{
    public interface IService<TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect>
        where TEntityDto : class, IEntityDto
        where TEntityList : class, ILista
        where TEntitySelect : class, ISelect
        where TRequest : class, IRequestList
        where TRequestSelect : class, IRequestSelect
    {
        Task<TEntityDto> GetById(int id);

        IListResultDto<TEntityList> GetAll(TRequest request);

        IEnumerable<TEntitySelect> GetAllSelect(TRequestSelect request);

        Task<TEntityDto> Create(TEntityDto input);

        Task<IEnumerable<TEntityDto>> CreateMany(IEnumerable<TEntityDto> entitiesDtos);

        Task<TEntityDto> Update(int id, TEntityDto input);

        Task UpdateMany(IEnumerable<TEntityDto> entitiesDtos);

        Task Delete(int id);

        Task DeleteMany(IEnumerable<int> ids);
    }
}

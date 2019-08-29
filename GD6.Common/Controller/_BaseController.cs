using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GD6.Common
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseGD6Controller : Controller
    {
        public BaseGD6Controller()
        {
        }

        public int? GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
                return null;

            return Convert.ToInt32(userId);
        }
    }
    
    public class BaseController<TEntityDto, TEntityList> :
        BaseController<TEntityDto, TEntityList, ISelect, Request, RequestSelect>
        where TEntityDto : class, IEntityDto
        where TEntityList : class, ILista
    {
        public BaseController(IService<TEntityDto, TEntityList, ISelect, Request, RequestSelect> service) : base(service)
        {
        }
    }
    public class BaseController<TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect> : BaseGD6Controller
    where TEntityDto : class, IEntityDto
    where TEntityList : class, ILista
    where TEntitySelect : class, ISelect
    where TRequest : class, IRequestList
    where TRequestSelect : class, IRequestSelect
    {
        protected IService<TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect> Service { get; set; }

        public BaseController(IService<TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect> service)
        {
            Service = service;
        }

        [HttpGet("{id}")]
        public virtual async Task<TEntityDto> GetById(int id) => await Service.GetById(id);

        [HttpPost]
        public virtual async Task<TEntityDto> Create([FromBody] TEntityDto entity) => await Service.Create(entity);

        [HttpPut("{id}")]
        public virtual async Task Update([FromRoute] int id, [FromBody] TEntityDto entity) => await Service.Update(id, entity);

        [HttpDelete("{id}")]
        public virtual async Task DeleteById(int id) => await Service.Delete(id);

        [HttpPost("list")]
        public virtual IListResultDto<TEntityList> GetListaTabela([FromBody] TRequest request)
        {
            if (request == null)
                return new ListResultDto<TEntityList>();

            var entitiesListTable = Service.GetAll(request);

            return entitiesListTable;
        }

        [AllowAnonymous]
        [HttpPost("select")]
        public virtual IEnumerable<TEntitySelect> GetListaSelect([FromBody] TRequestSelect requestSelect) => Service.GetAllSelect(requestSelect);
    }
}
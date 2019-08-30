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
        BaseController<TEntityDto, TEntityList, IEntityDtoSelect, RequestList, RequestSelect>
        where TEntityDto : class, IEntityDto
        where TEntityList : class, IEntityDtoList
    {
        public BaseController(IService<TEntityDto, TEntityList, IEntityDtoSelect, RequestList, RequestSelect> service) : base(service)
        {
        }
    }
    public class BaseController<TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect> : BaseGD6Controller
    where TEntityDto : class, IEntityDto
    where TEntityList : class, IEntityDtoList
    where TEntitySelect : class, IEntityDtoSelect
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
        public virtual IEntityDtoListResult<TEntityList> List([FromBody] TRequest request) => Service.GetAll(request);

        [AllowAnonymous]
        [HttpPost("select")]
        public virtual IEnumerable<TEntitySelect> Select([FromBody] TRequestSelect requestSelect) => Service.GetAllSelect(requestSelect);
    }
}
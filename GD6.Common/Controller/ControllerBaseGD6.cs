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
    public class ControllerBaseGD6<TService, TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect> : Controller
        where TService : IServiceBase<TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect>
        where TEntityDto : class, IEntityBaseDto
        where TEntityList : class, IEntityDtoList
        where TEntitySelect : class, IEntityDtoSelect
        where TRequest : class, IRequestList, new()
        where TRequestSelect : class, IRequestSelect
    {
        protected TService Service { get; set; }

        public ControllerBaseGD6(TService service)
        {
            Service = service;
        }

        protected int? GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
                return null;

            return Convert.ToInt32(userId);
        }

        [HttpGet("{id}")]
        public virtual async Task<TEntityDto> GetById(int id) => await Service.GetById(id);

        [HttpPost]
        public virtual async Task<TEntityDto> Create([FromBody] TEntityDto entity) => await Service.Create(entity);

        [HttpPut("save")]
        public virtual async Task<TEntityDto> Save([FromBody] TEntityDto entity)
        {
            if (entity == null)
                throw new ErroException("Não passou uma entidade!");

            if (entity.Id > 0)
                return await Update(entity.Id, entity);
            else
                return await Create(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<TEntityDto> Update([FromRoute] int id, [FromBody] TEntityDto entity) => await Service.Update(id, entity);

        [HttpDelete("{id}")]
        public virtual async Task DeleteById(int id) => await Service.Delete(id);

        [HttpPost("list")]
        public virtual IEntityDtoListResult<TEntityList> List([FromBody] TRequest request) => Service.GetAll(request);

        [AllowAnonymous]
        [HttpGet("select")]
        public virtual IEnumerable<TEntitySelect> Select([FromQuery] TRequestSelect requestSelect) => Service.GetAllSelect(requestSelect);
    }

    public class ControllerBaseGD6<TService, TEntityDto, TEntityList> :
        ControllerBaseGD6<TService, TEntityDto, TEntityList, IEntityDtoSelect, RequestList, RequestSelect>
        where TService : IServiceBase<TEntityDto, TEntityList, IEntityDtoSelect, RequestList, RequestSelect>
        where TEntityDto : class, IEntityBaseDto
        where TEntityList : class, IEntityDtoList
    {
        public ControllerBaseGD6(TService service) : base(service)
        {
        }
    }
}
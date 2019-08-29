using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace GD6.Common
{
    public abstract class Service<TEntity, TEntityDto, TEntityList> :
        Service<TEntity, TEntityDto, TEntityList, ISelect, Request, RequestSelect>
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
        where TEntityList : class, ILista
    {
        public Service(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    public abstract class Service<TEntity, TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect> :
        IService<TEntityDto, TEntityList, TEntitySelect, TRequest, TRequestSelect>
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
        where TEntityList : class, ILista
        where TEntitySelect : class, ISelect
        where TRequest : class, IRequestList
        where TRequestSelect : class, IRequestSelect
    {
        protected readonly IRepository<TEntity> Repository;

        public Service(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        protected virtual IQueryable<TEntity> GetByIdInclue(IQueryable<TEntity> query)
        {
            return query;
        }

        public virtual async Task<TEntityDto> GetById(int id)
        {
            var query = Repository.GetAll();

            query = GetByIdInclue(query);

            var entityDto = await query
                .ProjectTo<TEntityDto>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entityDto;
        }
        
        public virtual async Task<TEntity> GetEntityById(int id)
        {
            var query = Repository.GetAll();

            query = GetByIdInclue(query);

            var entity = await query
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual IListResultDto<TEntityList> GetAll(TRequest request)
        {
            var query = Repository.GetAll();

            query = CreateInclude(query);

            query = CreateFilter(query, request);

            var totalCount = query.Count();

            query = ApplySorting(query, request);
            query = ApplyPaging(query, request);

            var entities = query.ProjectTo<TEntityList>().ToList();

            var result = new ListResultDto<TEntityList>
            {
                Data = entities,
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,
                Draw = request.Draw
            };

            return result;
        }

        public virtual IEnumerable<TEntitySelect> GetAllSelect(TRequestSelect request)
        {
            var query = Repository.GetAll();

            query = CreateFilterSelect(query, request);

            query = ApplySortingSelect(query, request);

            var entities = query.Take(100).ToList();
            var entitiesSelectList = Mapper.Map<IEnumerable<TEntitySelect>>(entities);

            return entitiesSelectList;
        }

        public async Task<TEntityDto> Create(TEntityDto input)
        {
            await DoBeforeCreate(input);

            var entity = Mapper.Map<TEntity>(input);

            await Repository.Create(entity);

            await DoAfterCreate(entity, input);

            input = Mapper.Map<TEntityDto>(entity);

            return input;
        }

        public async Task<TEntity> Create(TEntity input)
        {
            await Repository.Create(input);
            return input;
        }

        public async Task<IEnumerable<TEntityDto>> CreateMany(IEnumerable<TEntityDto> entitiesDto)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(entitiesDto);

            await Repository.CreateMany(entities);

            return Mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        protected virtual async Task DoBeforeCreate(TEntityDto entityDto)
        {
        }

        protected virtual async Task DoAfterCreate(TEntity entity, TEntityDto entityDto)
        {
        }

        public virtual async Task<TEntityDto> Update(int id, TEntityDto input)
        {
            var entity = await Repository.GetById(id);

            await DoBeforeUpdate(entity, input);

            Mapper.Map(input, entity);

            await Repository.Update(id, entity);

            await DoAfterUpdate(entity, input);

            return Mapper.Map<TEntityDto>(entity);
        }

        public virtual async Task<TEntity> Update(int id, TEntity input)
        {
            await Repository.Update(id, input);
            return input;
        }

        public async Task UpdateMany(IEnumerable<TEntityDto> entitiesDtos)
        {
            foreach (var entityDto in entitiesDtos)
                await Update(entityDto.Id, entityDto);
        }

        public async Task UpdateMany(IEnumerable<TEntity> entitiesDtos)
        {
            foreach (var entityDto in entitiesDtos)
                await Update(entityDto.Id, entityDto);
        }

        protected virtual async Task DoBeforeUpdate(TEntity entity, TEntityDto entityDto)
        {
        }
        protected virtual async Task DoAfterUpdate(TEntity entity, TEntityDto entityDto)
        {
        }

        public virtual async Task Delete(int id)
        {
            await Repository.Delete(id);
        }

        public async Task DeleteMany(IEnumerable<int> ids)
        {
            foreach (var id in ids)
                await Delete(id);
        }

        protected virtual IQueryable<TEntity> CreateInclude(IQueryable<TEntity> query)
        {
            return query;
        }

        protected abstract IQueryable<TEntity> CreateFilter(IQueryable<TEntity> query, TRequest request);

        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TRequest request) => query;//.OrderBy(request.Sorting);

        protected virtual IQueryable<TEntity> ApplySortingSelect(IQueryable<TEntity> query, TRequestSelect request) => query;//.OrderBy(request.Sorting);

        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TRequest request)
        {
            return query.Skip(request.SkipCount).Take(request.MaxResultCount);
        }

        protected virtual IQueryable<TEntity> CreateFilterSelect(IQueryable<TEntity> query, TRequestSelect requestSelect)
        {
            return query;
        }
    }
}
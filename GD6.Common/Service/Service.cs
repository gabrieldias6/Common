using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace GD6.Common
{
    public abstract class Service<TEntity, TEntityDto, TEntityList> :
        Service<TEntity, TEntityDto, TEntityList, IEntityDtoSelect, RequestList, RequestSelect>
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
        where TEntityList : class, IEntityDtoList
    {
        public Service(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    public abstract class Service<TEntity, TEntityDto, TEntityList, TEntitySelect, TRequestList, TRequestSelect> :
        IService<TEntityDto, TEntityList, TEntitySelect, TRequestList, TRequestSelect>
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
        where TEntityList : class, IEntityDtoList
        where TEntitySelect : class, IEntityDtoSelect
        where TRequestList : class, IRequestList
        where TRequestSelect : class, IRequestSelect
    {
        protected readonly IRepository<TEntity> Repository;

        public Service(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        protected virtual IQueryable<TEntity> GetByIdInclue(IQueryable<TEntity> query) => query;
        public virtual async Task<TEntityDto> GetById(int id)
        {
            var query = Repository.GetAll();

            query = GetByIdInclue(query);

            var entityDto = await query
                .ProjectTo<TEntityDto>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entityDto;
        }

        protected virtual IQueryable<TEntity> GetEntityByIdInclue(IQueryable<TEntity> query) => query;
        public virtual async Task<TEntity> GetEntityById(int id)
        {
            var query = Repository.GetAll();

            query = GetEntityByIdInclue(query);

            var entity = await query
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        protected virtual IQueryable<TEntity> GetAllInclude(IQueryable<TEntity> query) => query;
        protected virtual IQueryable<TEntity> GetAllFilter(IQueryable<TEntity> query, TRequestList request) => query;
        protected virtual IQueryable<TEntity> GetAllSorting(IQueryable<TEntity> query, TRequestList request) => query;//.OrderBy(request.Sorting);
        protected virtual IQueryable<TEntity> GetAllPaging(IQueryable<TEntity> query, TRequestList request) => query.Skip(request.SkipCount).Take(request.MaxResultCount);
        public virtual IEntityDtoListResult<TEntityList> GetAll(TRequestList request)
        {
            var query = GetAll();

            query = GetAllInclude(query);
            query = GetAllFilter(query, request);

            var totalCount = query.Count();

            query = GetAllSorting(query, request);
            query = GetAllPaging(query, request);

            var entities = query.ProjectTo<TEntityList>().ToList();

            var result = new EntityDtoListResult<TEntityList>
            {
                Data = entities,
                RecordsFiltered = entities.Count,
                RecordsTotal = totalCount,
                Draw = request.Draw
            };

            return result;
        }

        protected virtual IQueryable<TEntity> GetAllSelectFilter(IQueryable<TEntity> query, TRequestSelect requestSelect) => query;
        protected virtual IQueryable<TEntity> GetAllSelectSorting(IQueryable<TEntity> query, TRequestSelect request) => query;//.OrderBy(request.Sorting);
        protected virtual IQueryable<TEntity> GetAllSelectPaging(IQueryable<TEntity> query, TRequestSelect request) => query.Skip(request.SkipCount).Take(request.MaxResultCount);
        public virtual IEnumerable<TEntitySelect> GetAllSelect(TRequestSelect request)
        {
            var query = GetAll();

            query = GetAllSelectFilter(query, request);
            query = GetAllSelectSorting(query, request);

            query = GetAllSelectPaging(query, request);

            var entities = query.ProjectTo<TEntitySelect>().ToList();

            //var entities = query.Take(100).ToList();
            //var entitiesSelectList = Mapper.Map<IEnumerable<TEntitySelect>>(entities);

            return entities;
        }


        protected virtual async Task CreateDoBefore(TEntityDto entityDto) { }
        protected virtual async Task CreateDoAfter(TEntity entity, TEntityDto entityDto) { }
        public async Task<TEntityDto> Create(TEntityDto entityDto)
        {
            await CreateDoBefore(entityDto);

            var entity = Mapper.Map<TEntity>(entityDto);

            await Repository.Create(entity);

            await CreateDoAfter(entity, entityDto);

            entityDto = Mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        public async Task<TEntity> Create(TEntity input)
        {
            await Repository.Create(input);
            return input;
        }

        protected virtual async Task CreateManyDoBefore(IEnumerable<TEntityDto> entitiesDto) { }
        protected virtual async Task CreateManyDoAfter(IEnumerable<TEntity> entities, IEnumerable<TEntityDto> entitiesDto) { }
        public async Task<IEnumerable<TEntityDto>> CreateMany(IEnumerable<TEntityDto> entitiesDto)
        {
            await CreateManyDoBefore(entitiesDto);

            var entities = Mapper.Map<IEnumerable<TEntity>>(entitiesDto);

            await Repository.CreateMany(entities);

            await CreateManyDoAfter(entities, entitiesDto);

            return Mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public async Task CreateMany(IEnumerable<TEntity> entities)
        {
            await Repository.CreateMany(entities);
        }
        protected virtual async Task UpdateDoBefore(TEntity entity, TEntityDto entityDto) { }
        protected virtual async Task UpdateDoAfter(TEntity entity, TEntityDto entityDto) { }
        public virtual async Task<TEntityDto> Update(int id, TEntityDto input)
        {
            var entity = await Repository.GetById(id);

            await UpdateDoBefore(entity, input);

            Mapper.Map(input, entity);

            await Repository.Update(id, entity);

            await UpdateDoAfter(entity, input);

            return Mapper.Map<TEntityDto>(entity);
        }

        public virtual async Task<TEntity> Update(int id, TEntity input)
        {
            await Repository.Update(id, input);
            return input;
        }

        protected virtual async Task UpdateManyDoBefore(IEnumerable<TEntityDto> entitiesDto) { }
        protected virtual async Task UpdateManyDoAfter(IEnumerable<TEntity> entities, IEnumerable<TEntityDto> entitiesDto) { }
        public async Task<IEnumerable<TEntityDto>> UpdateMany(IEnumerable<TEntityDto> entitiesDto)
        {
            await UpdateManyDoBefore(entitiesDto);

            var entities = Mapper.Map<IEnumerable<TEntity>>(entitiesDto);

            await Repository.UpdateMany(entities);

            await UpdateManyDoAfter(entities, entitiesDto);

            return Mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public async Task UpdateMany(IEnumerable<TEntity> entities)
        {
            await Repository.UpdateMany(entities);
        }

        protected virtual async Task DeleteDoBefore(TEntity entity) { }
        public virtual async Task Delete(int id)
        {
            var entity = await GetEntityById(id);

            await DeleteDoBefore(entity);

            await Repository.Delete(entity);
        }

        protected virtual async Task DeleteManyDoBefore(IEnumerable<TEntity> entity) { }
        public async Task DeleteMany(IEnumerable<int> ids)
        {
            var entities = await GetAll().Where(x => ids.Contains(x.Id)).ToListAsync();

            await DeleteManyDoBefore(entities);

            await Repository.DeleteMany(entities);
        }
    }
}
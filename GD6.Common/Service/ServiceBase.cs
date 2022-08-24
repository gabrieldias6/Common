using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace GD6.Common
{
    public abstract class ServiceBase<TRepository, TEntity, TEntityDto, TEntityList> :
        ServiceBase<TRepository, TEntity, TEntityDto, TEntityList, IEntityDtoSelect, RequestList, RequestSelect>
        where TRepository : class, IRepositoryBase<TEntity>
        where TEntity : class, IEntityBase
        where TEntityDto : class, IEntityBaseDto
        where TEntityList : class, IEntityDtoList
    {
        public ServiceBase(TRepository repository, IMapper mapper, ContextApp contextApp)
            : base(repository, mapper, contextApp)
        {
        }
    }

    public abstract class ServiceBase<TRepository, TEntity, TEntityDto, TEntityList, TEntitySelect, TRequestList, TRequestSelect> :
        IServiceBase<TEntityDto, TEntityList, TEntitySelect, TRequestList, TRequestSelect>
        where TRepository : class, IRepositoryBase<TEntity>
        where TEntity : class, IEntityBase
        where TEntityDto : class, IEntityBaseDto
        where TEntityList : class, IEntityDtoList
        where TEntitySelect : class, IEntityDtoSelect
        where TRequestList : class, IRequestList, new()
        where TRequestSelect : class, IRequestSelect
    {
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;
        protected readonly ContextApp ContextApp;

        public ServiceBase(TRepository repository, IMapper mapper, ContextApp contextApp)
        {
            Repository = repository;
            Mapper = mapper;
            ContextApp = contextApp;
        }

        protected virtual IQueryable<TEntity> GetByIdInclue(IQueryable<TEntity> query) => query;
        public virtual async Task<TEntityDto> GetById(int id)
        {
            var query = GetAll();

            query = GetByIdInclue(query);

            // Assim estava retornando todos os campos, e depois pasando pro Dto
            //var entityDto = await query
            //    .FirstOrDefaultAsync(x => x.Id == id);
            //return Mapper.Map<TEntityDto>(entityDto);


            var entityDto = await query
                .Where(x => x.Id == id)
                .ProjectTo<TEntityDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            
            if (entityDto != null)
                entityDto = await GetByIdDoAfter(entityDto);

            return entityDto;

        }

        public async Task<TDto> GetById<TDto>(int id)
             where TDto : class, IEntityBaseDto
        {
            var entityDto = await GetAll()
                .Where(x => x.Id == id)
                .ProjectTo<TDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return entityDto;

        }

        protected virtual async Task<TEntityDto> GetByIdDoAfter(TEntityDto entityDto) => entityDto;

        protected virtual IQueryable<TEntity> GetEntityByIdInclue(IQueryable<TEntity> query) => query;
        public virtual async Task<TEntity> GetEntityById(int id)
        {
            var query = GetAll();

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
        protected virtual IQueryable<TEntity> GetAllPaging(IQueryable<TEntity> query, TRequestList request)
        {
            if (request.Length <= 0)
                return query;

            return query.Skip(request.Start).Take(request.Length);
        }
        protected virtual void GetAllAfter(List<TEntityList> entities, TRequestList request) { }
        public virtual IEntityDtoListResult<TEntityList> GetAll(TRequestList request)
        {
            if (request == null)
                return new EntityDtoListResult<TEntityList>
                {
                    Data = new List<TEntityList>(),
                    RecordsFiltered = 0,
                    RecordsTotal = 0,
                    Draw = 0
                };

            var query = GetAll();

            query = GetAllInclude(query);
            query = GetAllFilter(query, request);

            var totalCount = query.Count();

            if (totalCount < request.Start)
                request.Start = 0;

            query = GetAllSorting(query, request);
            query = GetAllPaging(query, request);

            var entities = query.ProjectTo<TEntityList>(Mapper.ConfigurationProvider).ToList();

            GetAllAfter(entities, request);

            var result = new EntityDtoListResult<TEntityList>
            {
                Data = entities,
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,
                Draw = request.Draw
            };

            return result;
        }

        protected virtual IQueryable<TEntity> GetAllSelectFilter(IQueryable<TEntity> query, TRequestSelect requestSelect) => query;
        protected virtual IQueryable<TEntity> GetAllSelectFilterId(IQueryable<TEntity> query, int id) => Repository.GetAll().IgnoreQueryFilters().Where(x => x.Id == id);
        protected virtual IQueryable<TEntity> GetAllSelectSorting(IQueryable<TEntity> query, TRequestSelect request) => query;//.OrderBy(request.Sorting);
        protected virtual IQueryable<TEntity> GetAllSelectPaging(IQueryable<TEntity> query, TRequestSelect request)
        {
            if (request.MaxResultCount <= 0)
                return query;

            return query.Skip(request.SkipCount).Take(request.MaxResultCount);
            //return query.Skip(request.SkipCount).Take(request.MaxResultCount);
        }

        protected virtual void GetAllSelectAfter(List<TEntitySelect> entities, TRequestSelect request)
        {
            if (entities != null && entities.Any())
                entities.ForEach(entitie =>
                {
                    if (entitie.Excluido)
                        entitie.Nome += " (EXCLUIDO)";
                });
        }

        public virtual IEnumerable<TEntitySelect> GetAllSelect(TRequestSelect request)
        {
            if (request == null)
                return new List<TEntitySelect>();

            var query = GetAll();

            // Verifica se passou Id
            if (request.Id.HasValue)
                // Temos q retornar só este item
                query = GetAllSelectFilterId(query, request.Id.Value);
            else
            {

                query = GetAllSelectFilter(query, request);
                query = GetAllSelectSorting(query, request);
                query = GetAllSelectPaging(query, request);
            }

            var entities = query.ProjectTo<TEntitySelect>(Mapper.ConfigurationProvider).ToList();

            GetAllSelectAfter(entities, request);
            //var entities = query.Take(100).ToList();
            //var entitiesSelectList = Mapper.Map<IEnumerable<TEntitySelect>>(entities);

            return entities;
        }


        protected virtual async Task CreateDoBefore(TEntity entity, TEntityDto entityDto) { }
        protected virtual async Task CreateDoAfter(TEntity entity, TEntityDto entityDto) { }
        public virtual async Task<TEntityDto> Create(TEntityDto entityDto)
        {
            var entity = Mapper.Map<TEntity>(entityDto);

            await CreateDoBefore(entity, entityDto);

            await Create(entity);

            await CreateDoAfter(entity, entityDto);

            entityDto = Mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        protected virtual async Task<TEntity> Create(TEntity entity)
        {
            await Repository.Create(entity);
            ClearCache();
            return entity;
        }

        protected virtual async Task CreateManyDoBefore(IEnumerable<TEntityDto> entitiesDto) { }
        protected virtual async Task CreateManyDoAfter(IEnumerable<TEntity> entities, IEnumerable<TEntityDto> entitiesDto) { }
        public virtual async Task<IEnumerable<TEntityDto>> CreateMany(IEnumerable<TEntityDto> entitiesDto)
        {
            await CreateManyDoBefore(entitiesDto);

            var entities = Mapper.Map<IEnumerable<TEntity>>(entitiesDto);

            await CreateMany(entities);

            await CreateManyDoAfter(entities, entitiesDto);

            return Mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public virtual async Task CreateMany(IEnumerable<TEntity> entities)
        {
            await Repository.CreateMany(entities);
            ClearCache();
        }
        protected virtual async Task UpdateDoBefore(TEntity entity, TEntityDto entityDto) { }
        protected virtual async Task UpdateDoBeforeMap(TEntity entity, TEntityDto entityDto) { }
        protected virtual async Task<TEntity> UpdateDoAfter(TEntity entity, TEntityDto entityDto) { return entity; }
        public virtual async Task<TEntityDto> Update(int id, TEntityDto input)
        {
            if (input == null)
                throw new ErroException("Não passou a entidade!");

            var entity = await GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new ErroException("Registro não localizado. Verifique se foi deletado por outro usuário!", $"Id: {id}");
             
            await UpdateDoBefore(entity, input);

            Mapper.Map(input, entity);

            await UpdateDoBeforeMap(entity, input);

            await Update(id, entity);

            entity = await UpdateDoAfter(entity, input);

            return await GetById(id); //Mapper.Map<TEntityDto>(entity);
        }

        public virtual async Task<TEntity> Update(int id, TEntity input)
        {
            await Repository.Update(id, input);
            ClearCache();
            return input;
        }

        protected virtual async Task UpdateManyDoBefore(IEnumerable<TEntity> entities, IEnumerable<TEntityDto> entitiesDto) { }
        protected virtual async Task UpdateManyDoAfter(IEnumerable<TEntity> entities, IEnumerable<TEntityDto> entitiesDto) { }
        public virtual async Task<IEnumerable<TEntityDto>> UpdateMany(IEnumerable<TEntityDto> entitiesDto)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(entitiesDto);

            await UpdateManyDoBefore(entities, entitiesDto);

            await UpdateMany(entities);

            await UpdateManyDoAfter(entities, entitiesDto);

            return Mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public virtual async Task UpdateMany(IEnumerable<TEntity> entities)
        {
            await Repository.UpdateMany(entities);
            ClearCache();
        }

        protected virtual async Task DeleteDoBefore(TEntity entity) { }
        public virtual async Task Delete(int id)
        {
            var entity = await GetEntityById(id);

            await DeleteDoBefore(entity);

            await Repository.Delete(entity);
            ClearCache();
        }

        protected virtual async Task DeleteManyDoBefore(IEnumerable<TEntity> entity) { }
        public virtual async Task DeleteMany(IEnumerable<int> ids)
        {
            var entities = await GetAll().Where(x => ids.Contains(x.Id)).ToListAsync();

            await DeleteManyDoBefore(entities);

            await Repository.DeleteMany(entities);
            ClearCache();
        }

        protected virtual void ClearCache() { }
    }
}
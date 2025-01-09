using Microsoft.EntityFrameworkCore;
using TrainingProgram.Entities.IEntity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainingprogram.RepositoriesAbstractions.Courses.MongoRepository;

namespace TrainingProgram.Infrastructure.MongoCourse.Infrastructure.Repositories.Implementations
{
    public abstract class RepositoryMongo<T, TPrimaryKey> : IMongoRepository<T, TPrimaryKey> where T
            : class, IEntity<TPrimaryKey>
    {
        public DbContext Context { get; }
        private readonly DbSet<T> _entitySet;

        protected RepositoryMongo(DbContext context)
        {
            Context = context;
            _entitySet = Context.Set<T>();
        }

        #region Get

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Сущность. </returns>
        public virtual T Get(TPrimaryKey id)
        {
            return _entitySet.Find(id);
        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <returns> Сущность. </returns>
        public virtual async Task<T> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            return await _entitySet.FindAsync(new object[] { id }, cancellationToken);
        }

        #endregion

        #region GetAll

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> IQueryable массив сущностей. </returns>
        public virtual IQueryable<T> GetAll(bool asNoTracking = false)
        {
            return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
        }

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            return await GetAll(asNoTracking).ToListAsync(cancellationToken);
        }

        #endregion

        #region Create

        /// <summary>
        /// Добавить в базу сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        public virtual T Add(T entity)
        {
            var objToReturn = _entitySet.Add(entity);
            return objToReturn.Entity;
        }

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            return (await _entitySet.AddAsync(entity)).Entity;
        }

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        public virtual void AddRange(List<T> entities)
        {
            _entitySet.AddRange(entities);
        }

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }

            await _entitySet.AddRangeAsync(entities);
        }

        #endregion

        #region Update

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        public virtual void Update(T entity)
        {
            _entitySet.Update(entity);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Удалить сущность по Id.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns>
        public virtual bool Delete(TPrimaryKey id)
        {
            var obj = _entitySet.Find(id);
            if (obj == null)
            {
                return false;
            }
            _entitySet.Remove(obj);
            return true;
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Сущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        public virtual bool Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            _entitySet.Remove(entity);
            return true;
        }

        /// <summary>
        /// Удалить сущности.
        /// </summary>
        /// <param name="entities"> Коллекция сущностей для удаления. </param>
        /// <returns> Была ли операция завершена успешно. </returns>
        public virtual bool DeleteRange(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return false;
            }
            _entitySet.RemoveRange(entities);
            return true;
        }

        #endregion

        #region SaveChanges

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.State == EntityState.Modified)
                    {
                        var currentValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        // Проверяем, была ли версия данных обновлена в базе данных
                        if (databaseValues != null)
                        {
                            int currentVersion = currentValues.GetValue<int>("Version");
                            int databaseVersion = databaseValues.GetValue<int>("Version");

                            if (currentVersion != databaseVersion)
                            {
                                // Конфликт версий, необходимо решить, как поступить
                                // Например, выбрасываем ошибку, либо пытаемся обновить данные с учетом изменений
                                throw new DbUpdateConcurrencyException("Конфликт версий при сохранении.");
                            }
                        }
                    }
                }

                // Если конфликты не разрешены, повторное сохранение данных может быть выполнено
                await Context.SaveChangesAsync(cancellationToken);
            }

            #endregion
        }
    }
}

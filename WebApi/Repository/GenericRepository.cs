using System.Reflection.Emit;
using WebApi.Models;

namespace WebApi.Repository
{
    public class GenericRepository<T> where T : class, IIdentity<int>
    {
        private readonly IntGenerator _idGenerator;

        public GenericRepository(IntGenerator intGenerator)
        {
            _idGenerator = intGenerator;
        }
        private Dictionary<int, T> _data = new();
        public IEnumerable<T> Find(ISpecification<T> specification = null)
        {
            if (specification is not null)
                return MemorySpecificationEvaluator<T>.GetQuery(_data.Values.AsQueryable(), specification);
            return FindAll();
        }

        public Task<T?> FindByIdAsync(int id)
        {
            return Task.FromResult(_data.ContainsKey(id) ? _data[id] : null);
        }

        public Task<List<T>> FindAllAsync()
        {
            return Task.FromResult(_data.Values.ToList());
        }

        public T? FindById(int id)
        {
            try
            {
                return _data[id];
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }
        }

        public List<T> FindAll()
        {
            return _data.Values.ToList();
        }

        public T Add(T entity)
        {
            _data[_idGenerator.Next] = entity;
            entity.Id = _idGenerator.Current;
            return entity;
        }

        public void RemoveById(int id)
        {
            _data.Remove(id);
        }

        public void Update(int id, T o)
        {
            if (_data.ContainsKey(id))
            {
                _data[id] = o;
            }
        }

    }
}

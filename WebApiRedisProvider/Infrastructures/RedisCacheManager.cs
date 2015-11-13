using Newtonsoft.Json;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApiRedisProvider.Infrastructures
{
    public class RedisCacheManager
    {
        private readonly IRedisClient _redisClient;        

        public RedisCacheManager(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public T Get<T>(string id)
        {
            return _redisClient.As<T>().GetById(id.ToLower());
        }

        public IQueryable<T> GetAll<T>()
        {
            return _redisClient.As<T>().GetAll().AsQueryable();            
        }

        public IQueryable<T> GetAll<T>(string hash, string value, Expression<Func<T, bool>> filter)
        {
            var filtered = _redisClient.GetAllEntriesFromHash(hash).Where(c => c.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            var ids = filtered.Select(c => c.Key);

            var ret = _redisClient.As<T>().GetByIds(ids).AsQueryable()
                                .Where(filter);

            return ret;
        }

        public IQueryable<T> GetAll<T>(string hash, string value)
        {
            var filtered = _redisClient.GetAllEntriesFromHash(hash).Where(c => c.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            var ids = filtered.Select(c => c.Key);

            var ret = _redisClient.As<T>().GetByIds(ids).AsQueryable();
            return ret;
        }

        public void Set<T>(T item)
        {
            _redisClient.As<T>().Store(item);
        }

        public void Set<T>(T item, string hash, string value, string keyName)
        {
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty(keyName);

            _redisClient.SetEntryInHash(hash, prop.GetValue(item).ToString(), value.ToLower());

            _redisClient.As<T>().Store(item);
        }

        public void Set<T>(T item, List<string> hash, List<string> value, string keyName)
        {
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty(keyName);

            for (int i = 0; i < hash.Count; i++)
            {
                _redisClient.SetEntryInHash(hash[i], prop.GetValue(item).ToString(), value[i].ToLower());
            }

            _redisClient.As<T>().Store(item);
        }

        public void SetAll<T>(List<T> listItems)
        {
            _redisClient.As<T>().StoreAll(listItems);
        }

        public void SetAll<T>(List<T> list, string hash, string value, string keyName)
        {
            foreach (var item in list)
            {
                Type t = item.GetType();
                PropertyInfo prop = t.GetProperty(keyName);

                _redisClient.SetEntryInHash(hash, prop.GetValue(item).ToString(), value.ToLower());

                _redisClient.As<T>().StoreAll(list);
            }
        }

        public void SetAll<T>(List<T> list, List<string> hash, List<string> value, string keyName)
        {
            foreach (var item in list)
            {
                Type t = item.GetType();
                PropertyInfo prop = t.GetProperty(keyName);

                for (int i = 0; i < hash.Count; i++)
                {
                    _redisClient.SetEntryInHash(hash[i], prop.GetValue(item).ToString(), value[i].ToLower());
                }

                _redisClient.As<T>().StoreAll(list);
            }
        }

        public void Delete<T>(T item)
        {
            _redisClient.As<T>().Delete(item);
        }

        public void DeleteAll<T>(T item)
        {
            _redisClient.As<T>().DeleteAll();
        }

        public long PublishMessage(string channel, object item)
        {
            var ret = _redisClient.PublishMessage(channel, JsonConvert.SerializeObject(item));
            return ret;
        }
    }
}

using Newtonsoft.Json;
using ProtoDefinitions;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace ApiApplication
{
    public class ApiClientGrpc
    {
        private const string CacheKey = "MoviesList";
        private readonly MoviesApi.MoviesApiClient _client;
        private readonly IDatabase _redisCache;

        public ApiClientGrpc(MoviesApi.MoviesApiClient client, IDatabase redisCache)
        {
            _client = client;
            _redisCache = redisCache;
        }

        public async Task<showListResponse> GetAll()
        {
            try
            {                
                var all = await _client.GetAllAsync(new Empty());
                all.Data.TryUnpack<showListResponse>(out var data);
                _redisCache.StringSet(CacheKey, JsonConvert.SerializeObject(data));             
                return data;
            }
            catch (Exception)
            {
                var json = _redisCache.StringGet(CacheKey);
                var data = JsonConvert.DeserializeObject<showListResponse>(json);
                return data;
            }            
        }

        public async Task<showResponse> GetById(string id)
        {
            var all = await _client.GetByIdAsync(new IdRequest { Id = id });
            all.Data.TryUnpack<showResponse>(out var data);
            return data;
        }
    }
}
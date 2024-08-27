using Microsoft.Extensions.Caching.Memory;

namespace HomeworkGB10.Abstractions
{
    public interface IRepository
    {
        public MemoryCacheStatistics? GetCacheStatistics();
        public string GetCacheStatisticsCsvUrl();
    }
}
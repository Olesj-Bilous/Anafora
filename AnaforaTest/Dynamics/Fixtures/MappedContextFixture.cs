using AnaforaDataLab.Context.Dynamics;
using AnaforaDataLab.Utils;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AnaforaTest.Dynamics.Fixtures
{
    public class MappedContextFixture
    {
        public MappedContextFixture()
        {
            lock (_lock)
            {
                if (!_dbInitialized)
                {
                    using var interCtx = InternalDynamicContext.New();
                    using var globCtx = GlobalDynamicContext.New();
                    Task.WaitAll(Task.Run(() => interCtx.SeedAsync()), Task.Run(() => globCtx.SeedAsync()));
                    _dbInitialized = true;
                }
            }
        }

        public async Task<InternalMappedContext> GetInternalMappedContextAsync() => await _internalPool.CreateDbContextAsync();
        public async Task<GlobalMappedContext> GetGlobalMappedContextAsync() => await _globalPool.CreateDbContextAsync();
        public async Task<InternalDynamicContext> GetInternalDynamicContextAsync() => await _internalDynamicPool.CreateDbContextAsync();
        public async Task<GlobalDynamicContext> GetGlobalDynamicContextAsync() => await _globalDynamicPool.CreateDbContextAsync();

        private static readonly object _lock = new();
        private static bool _dbInitialized = false;

        private static readonly PooledDbContextFactory<InternalMappedContext> _internalPool = new (InternalMappedContext.Options());
        private static readonly PooledDbContextFactory<GlobalMappedContext> _globalPool = new(GlobalMappedContext.Options());
        private static readonly PooledDbContextFactory<InternalDynamicContext> _internalDynamicPool = new(InternalDynamicContext.Options());
        private static readonly PooledDbContextFactory<GlobalDynamicContext> _globalDynamicPool = new(GlobalDynamicContext.Options());
    }
}
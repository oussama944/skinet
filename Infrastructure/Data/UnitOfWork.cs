using System;
using System.Collections.Concurrent;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class UnitOfWork(StoreContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public IGenericRepository<TEnity> Repository<TEnity>() where TEnity : BaseEntity
    {
        var type = typeof(TEnity).Name;

        return (IGenericRepository<TEnity>)_repositories.GetOrAdd(type, t =>
        {
            var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(TEnity));
            return Activator.CreateInstance(repositoryType, context) ??
                throw new InvalidCastException($"Could not create repository instance for {t}");
        });
    }
}

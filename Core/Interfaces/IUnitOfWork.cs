using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEnity> Repository<TEnity>()where TEnity:BaseEntity;
    Task<bool> Complete();
}

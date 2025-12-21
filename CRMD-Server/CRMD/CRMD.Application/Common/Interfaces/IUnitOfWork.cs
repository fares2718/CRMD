namespace CRMD.Application.Common.Interfaces;

public interface IUnitOfWork
{
    public Task CommitChangesAsync();
}
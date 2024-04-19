namespace DesignsAndBuild.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DesignsAndBuildContext _designsAndBuildContext;

    private Hashtable _repositories;

    public UnitOfWork(DesignsAndBuildContext  designsAndBuildContext) //Ask CLR To create object from DB Context Implecitly
    {
        _designsAndBuildContext = designsAndBuildContext;
        _repositories = new Hashtable();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity  //This Method to create repository per request
    {
        var key = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(key))
        {
            var repository = new GenericRepository<TEntity>(_designsAndBuildContext);
            _repositories.Add(key, repository);
        }

        return _repositories[key] as IGenericRepository<TEntity>;
    }

    public async Task<int> CompleteAsync()
        => await _designsAndBuildContext.SaveChangesAsync();

    public async ValueTask DisposeAsync()
    => await _designsAndBuildContext.DisposeAsync();
}
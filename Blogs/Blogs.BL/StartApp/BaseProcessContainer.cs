using Blogs.BL.Abstractions;
using Blogs.BL.Abstractions.Factories;
using Blogs.BL.Infrastructure;
using Blogs.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.StartApp
{
    //public abstract class BaseProcessContainer<DTOEntity> : IDisposable
    //{
    //    private bool isDisposed = false;

    //    protected FileSystemWatcher Watcher;
    //    protected IConnectionFactory _connectionFactory;
    //    protected IRepositoryFactory _repoFactory;
    //    protected IDTOParserFactory<BlogDataSourceDTO> _parserFactory;

    //    protected EntityConcurrencyHandler _entityConcurrencyHandler;

    //    protected IDataSourceFactory<BlogDataSourceDTO> dataSourceFactory;

    //    protected IDataItemHandlerFactory<BlogDataSourceDTO> dataItemHandlerFactory;
    //    protected IDataSourceHandlerFactory<BlogDataSourceDTO> dataSourceHandlerFactory;

    //    protected TokenSourceSet _tokenSources;
    //    protected AppOptions _appOptions;

    //    protected IProcessHandler<BlogDataSourceDTO> _folderManager;
    //    protected IProcessHandler<BlogDataSourceDTO> _eventedManager;

    //    public BaseProcessContainer(AppOptions folderOptions)
    //    {
            
    //        _appOptions = folderOptions;
    //        _tokenSources = new TokenSourceSet(stop: new CancellationTokenSource(), cancel: new CancellationTokenSource());
    //    }

    //    protected virtual void Initialize()
    //    {
    //        InitWatcher();
    //        InitConnectionFactory();
    //        InitDataSourceFactory();
    //        InitDbContextFactory();
    //        InitRepositoryFactory();
    //        InitParserFactory();
    //        InitConcurrencyHandler();

    //        EnsureDataBase();

    //        InitDataItemHandlerFactory();
    //        InitDataSourceHandlerFactory();
    //        InitManagers();
    //    }

    //    protected abstract void InitConnectionFactory();
    //    protected abstract void InitRepositoryFactory();
    //    protected abstract void InitDbContextFactory();

    //    protected abstract void InitParserFactory();
    //    protected abstract void InitConcurrencyHandler();
    //    protected abstract void InitDataItemHandlerFactory();
    //    protected abstract void InitManagers();
    //    protected virtual void Dispose(bool isDisposing)
    //    {
    //        if (isDisposed) return;
    //        if (isDisposing)
    //        {
    //            if (_entityConcurrencyHandler != null)
    //            {
    //                _entityConcurrencyHandler.Dispose();
    //                _entityConcurrencyHandler = null;
    //            }
    //            if (Watcher != null)
    //            {
    //                Watcher.Dispose();
    //                Watcher = null;
    //            }

    //            IDisposable temp = _eventedManager as IDisposable;
    //            if (temp != null)
    //            {
    //                temp .Dispose();
    //                _eventedManager = null;
    //            }
    //        }
    //        isDisposed = true;
    //    }
    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    ~BaseProcessContainer()
    //    {
    //        Dispose(false);
    //    }
    //}
}

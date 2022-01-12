using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Collections.Generic;

namespace Blogs.BL.ControlPanels
{
    public class AsyncControlPanelFactory<DTOEntity>
    {
        Func<ICollection<IAsyncAdapter<DTOEntity>>> _getCollectionAction;
        Func<TokenSourceSet> _getTokenSourceSet;
        Func<int> _getTimeout;

        public AsyncControlPanelFactory(
            Func<ICollection<IAsyncAdapter<DTOEntity>>> getCollectionAction,
            Func<TokenSourceSet> getTokenSourceSet,
            Func<int> getTimeout
            )
        {
            _getCollectionAction = getCollectionAction;
            _getTokenSourceSet = getTokenSourceSet;
            _getTimeout = getTimeout;
        }

        public AsyncControlPanel<DTOEntity> CreateInstance()
        {
            return new AsyncControlPanel<DTOEntity>(
                _getCollectionAction(), _getTokenSourceSet(), _getTimeout());
        }
    }
}

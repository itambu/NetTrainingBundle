using Blogs.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Infrastructure
{
    public class AsyncControlPanelFactory<DTOEntity>
    {
        Func<ICollection<IAsyncHandler<DTOEntity>>> _getCollectionAction;
        Func<TokenSourceSet> _getTokenSourceSet;
        Func<TaskBlocker> _getTaskBlocker;
        Func<int> _getTimeout;

        public AsyncControlPanelFactory(
            Func<ICollection<IAsyncHandler<DTOEntity>>> getCollectionAction,
            Func<TokenSourceSet> getTokenSourceSet,
            Func<TaskBlocker> getTaskBlocker,
            Func<int> getTimeout
            )
        {
            _getCollectionAction = getCollectionAction;
            _getTokenSourceSet = getTokenSourceSet;
            _getTaskBlocker = getTaskBlocker;
            _getTimeout = getTimeout;
        }

        public AsyncControlPanel<DTOEntity> CreateInstance()
        {
            return new AsyncControlPanel<DTOEntity>(
                _getCollectionAction(), _getTokenSourceSet(), _getTaskBlocker(), _getTimeout());
        }
    }
}

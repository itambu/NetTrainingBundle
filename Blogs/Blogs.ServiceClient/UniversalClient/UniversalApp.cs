using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using Blogs.BL.StartApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.ServiceClient.UniversalClient
{
    public class UniversalApp : ConfiguredApp
    {
        public void Configure(
            EventHandler<IBlogDataSource<BlogDataSourceDTO>> forFailed,
            EventHandler<IBlogDataSource<BlogDataSourceDTO>> forCompleted,
            EventHandler<IBlogDataSource<BlogDataSourceDTO>> forInterrupted
            )
        {
            _folderManager.TaskFailed += forFailed;
            _folderManager.TaskCompleted += forCompleted;
            _folderManager.TaskInterrupted += forInterrupted;

            _eventedManager.TaskFailed += forFailed;
            _eventedManager.TaskCompleted += forCompleted;
            _eventedManager.TaskInterrupted += forInterrupted;
        }

        public UniversalApp(AppOptions appOptions, 
            EventHandler<IBlogDataSource<BlogDataSourceDTO>> forFailed,
            EventHandler<IBlogDataSource<BlogDataSourceDTO>> forCompleted,
            EventHandler<IBlogDataSource<BlogDataSourceDTO>> forInterrupted
            ) : base(appOptions)
        {
            Configure(forFailed, forCompleted, forInterrupted);
        }

    }
}

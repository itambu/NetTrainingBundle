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
    //public class UniversalApp : ConfiguredApp
    //{
    //    public virtual void Configure(
    //        EventHandler<IDataSource<BlogDataSourceDTO>> forFailed,
    //        EventHandler<IDataSource<BlogDataSourceDTO>> forCompleted,
    //        EventHandler<IDataSource<BlogDataSourceDTO>> forInterrupted
    //        )
    //    {
    //        TaskFailed += forFailed;
    //        TaskCompleted += forCompleted;
    //        TaskInterrupted += forInterrupted;
    //    }

    //    public UniversalApp(AppOptions appOptions, 
    //        EventHandler<IDataSource<BlogDataSourceDTO>> forFailed,
    //        EventHandler<IDataSource<BlogDataSourceDTO>> forCompleted,
    //        EventHandler<IDataSource<BlogDataSourceDTO>> forInterrupted
    //        ) : base(appOptions)
    //    {
    //        Configure(forFailed, forCompleted, forInterrupted);
    //    }

    //}
}

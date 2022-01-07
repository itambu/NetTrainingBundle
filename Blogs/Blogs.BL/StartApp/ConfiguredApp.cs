using Blogs.BL.Abstractions;
using Blogs.BL.Abstractions.Factories;
using Blogs.BL.AsyncHandlers;
using Blogs.BL.ConnectionFactories;
using Blogs.BL.ConsistancyHandlers;
using Blogs.BL.DataItemHandlers;
using Blogs.BL.DataSourceFactories;
using Blogs.BL.DataSourceHandlers;
using Blogs.BL.DTOEntityParsers;
using Blogs.BL.FolderDataSourceProviders;
using Blogs.BL.Infrastructure;
using Blogs.BL.ProcessManagers;
using Blogs.DAL.Abstractions;
using Blogs.DAL.BlogContextFactories;
using Blogs.DAL.RepositotyFactories;
using Blogs.Persistence.Contexts;
using ChinhDo.Transactions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.StartApp
{
    //public class ConfiguredApp : BaseAsyncApp<BlogDataSourceDTO>
    //{

    //    public ConfiguredApp(AppOptions appOptions) : base(,appOptions)
    //    {
    //        Initialize();
    //    }

    //    #region Configuration

    //    protected override void InitManagers()
    //    {
 
    //    }

    //    protected override void InitAsyncHandlers()
    //    {

    //    }

    //    #endregion


    //}
}

﻿using Blogs.BL.Infrastructure;
using Blogs.BL.StartApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Demo.ConsoleApp
{
    //public class ConsoleClientApp : ConfiguredApp
    //{
    //    protected void Configure()
    //    {
    //        base.ConfigureStopEventOnEventedManager();
    //        _folderManager.TaskFailed += (obj, ds) => { Console.WriteLine("Failed"); };
    //        _folderManager.TaskCompleted += (obj, ds) => { Console.WriteLine("Completed"); };
    //        _folderManager.TaskInterrupted += (obj, ds) => { Console.WriteLine("Interrupted"); };

    //        _eventedManager.TaskFailed += (obj, ds) => { Console.WriteLine("Failed"); };
    //        _eventedManager.TaskCompleted += (obj, ds) => { Console.WriteLine("Completed"); };
    //        _eventedManager.TaskInterrupted += (obj, ds) => { Console.WriteLine("Interrupted"); };
    //    }

    //    public ConsoleClientApp(AppOptions appOptions) : base(appOptions)
    //    {
    //        Configure();
    //    }

    //}
}

using BlogExample.BL.Custom;
using BlogExample.BL.Custom.Builders;
using BlogExample.BL.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.ConsoleClient
{
    public class ConloseMessageBuilder : BackupFeatureBuilder
    {
        protected override void Building()
        {
            base.Building();
            BildConsoleMessaging();
        }

        protected virtual void BildConsoleMessaging()
        {
            TaskStrategyFactory.ActionContainer.OnCompleted += new EventHandler<CustomLogicTaskContext>(
            (sender, context) => { Console.WriteLine("completed"); });
            TaskStrategyFactory.ActionContainer.OnFaulted += new EventHandler<CustomLogicTaskContext>(
            (sender, context) => { Console.WriteLine($"faulted on while processing {context.DataItem}"); });
            TaskStrategyFactory.ActionContainer.OnCancelled += new EventHandler<CustomLogicTaskContext>(
            (sender, context) => { Console.WriteLine("cancelled"); });
        }

    }
}

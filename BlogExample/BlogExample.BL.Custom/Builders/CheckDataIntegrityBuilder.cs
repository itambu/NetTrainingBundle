using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.Builders
{
    public class CheckDataIntegrityBuilder : CustomProcessStrategyBuilder
    {
        protected override void Building()
        {
            base.Building();
            BuildCheckDataIntegrityFeature();
            BuildProcessDataIntegrityCheck();
        }

        protected virtual void BuildCheckDataIntegrityFeature()
        { 
            var commiter = new LogicTaskDataCommiter(DbContextFactory, RepositoryFactory);
            TaskStrategyFactory.ActionContainer.OnCompleted += new EventHandler<CustomLogicTaskContext>(
            (sender, context) => { commiter.TryPostCsvFileData(context); });
            TaskStrategyFactory.ActionContainer.OnFaulted += new EventHandler<CustomLogicTaskContext>(
                (sender, context) => { commiter.RollBack(context); });
        }

        protected virtual void BuildProcessDataIntegrityCheck()
        {
            var dataIntegrityHandler = new ProcessStartDataIntegrityHandler(DbContextFactory, RepositoryFactory);
            ProcessStrategy.PreProcessing += dataIntegrityHandler.OnProcessStartRecoveryHandler;
        }
    }
}

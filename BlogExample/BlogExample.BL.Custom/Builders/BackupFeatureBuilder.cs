using BlogExample.BL.CSVParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.Builders
{
    public class BackupFeatureBuilder : CheckDataIntegrityBuilder
    {
        protected override void Building()
        {
            base.Building();
            BuildBackupFeature();
        }

        protected virtual void BuildBackupFeature()
        {
            TaskStrategyFactory.ActionContainer.OnCompleted += new EventHandler<CustomLogicTaskContext>(
            (sender, context) => { (context.DataSource as IBackupable)?.BackUp(); });
        }
    }
}

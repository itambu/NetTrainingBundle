using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BL.PatientCardRecords
{
    public abstract class PatientCardRecord
    {
        public DateTime Created { get; private set; }
        public string Text { get; private set; }
        public Employees.Employee CreatedBy { get; private set; }
        
    }
}

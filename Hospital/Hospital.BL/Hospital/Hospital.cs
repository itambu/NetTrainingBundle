using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.BL.PatientProfile;

namespace Hospital.BL.Hospital
{
    public class Hospital
    {
        public ICollection<PatientProfile.PatientProfile> Profiles { get; private set; }

    }
}

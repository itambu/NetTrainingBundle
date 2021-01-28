using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BL.PatientProfile
{
    public class PatientProfile
    {
        public ICollection<PatientCards.PatientCard> Cards { get; private set; }

        public PatientPersonalInfo PersonalInfo { get; private set; }

        public PatientProfile(PatientPersonalInfo info, ICollection<PatientCards.PatientCard> cards)
        {
            PersonalInfo = info;
            Cards = cards;
        }
    }
}

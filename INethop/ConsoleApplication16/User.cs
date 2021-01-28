using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public class User
    {
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            private set 
            {
                if (value != null && value.Length > 0)
                {
                    firstName = value.Substring(0, 1).ToUpper() + value.Substring(1);
                }
                else
                {
                    throw new ArgumentException("Wrong argument", "firstName");
                }
            }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            private set { lastName = value; }
        }

        private Gender gender;

        public Gender Gender
        {
            get { return gender; }
            private set { gender = value; }
        }

        public User(string firstName, string lastName, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
        }
    }
}

using System;

namespace OOP
{
    public class Prototype
    {
        public class Person
        {
            public int Age;
            public DateTime BirthDate;
            public string Name;
            public IdInfo IdInfo;

            public Person ShallowCopy()
            {
                return (Person) this.MemberwiseClone();
            }

            [Obsolete]
            public Person DeepCopy()
            {
                Person clone = (Person) this.MemberwiseClone();
                clone.IdInfo = new IdInfo(IdInfo.IdNumber);
                clone.Name = string.Copy(Name);
                return clone;
            }
        }

        public class IdInfo
        {
            public int IdNumber;

            public IdInfo(int idNumber)
            {
                this.IdNumber = idNumber;
            }
        }
        
        
    }
}
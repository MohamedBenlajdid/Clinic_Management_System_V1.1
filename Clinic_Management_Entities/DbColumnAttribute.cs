using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DbColumnAttribute : Attribute
    {
        public string Name { get; }

        public DbColumnAttribute(string name)
            => Name = name;
    }


}

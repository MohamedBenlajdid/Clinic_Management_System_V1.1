using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.ValidationResult
{
    public sealed class ValidationResult
    {
        private readonly List<string> _errors = new();

        public bool IsValid => _errors.Count == 0;

        public IReadOnlyList<string> Errors => _errors;

        public void Add(string error) => _errors.Add(error);

        public void AddIf(bool condition, string error)
        {
            if (condition)
                _errors.Add(error);
        }

        public static ValidationResult Success() => new ValidationResult();   
    }

}

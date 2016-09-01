using Architecture.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Architecture.UnitTests
{
    internal class ValidatableRequest : IRequest, IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        public bool IsValid { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (!IsValid)
            {
                results.Add(new ValidationResult("Request is not valid."));
            }
            return results;
        }
    }
}

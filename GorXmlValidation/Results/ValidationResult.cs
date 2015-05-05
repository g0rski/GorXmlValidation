using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorXmlValidation.Results
{
    public sealed class ValidationResult
    {
        public bool IsValid
        {
            get
            {
                if (Errors == null)
                    return true;
                return !Errors.Any();
            }
        }
        public Dictionary<string,List<string>> Errors { get; set; }
    }
}

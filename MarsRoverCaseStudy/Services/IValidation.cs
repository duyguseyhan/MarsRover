using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverCaseStudy.Models;

namespace MarsRoverCaseStudy.Services
{
    public interface IValidation
    {
        ValidationResult CheckValidity(MarsRoverInput input);
    }
}

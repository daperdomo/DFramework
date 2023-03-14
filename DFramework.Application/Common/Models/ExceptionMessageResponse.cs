using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFramework.Application.Common.Models
{
    public class ExceptionMessageResponse
    {
        public bool HasError { get; set; } = true;
        public object Message { get; set; } = null!;
    }
}

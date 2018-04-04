using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class Constant
    {
        public static readonly int SuccessMessageCode = 1;
        public static readonly int ErrorMessageCode = 0;
        public static readonly string MessageSuccess = "Success";
        public static readonly string MessageTaskmateError = "An error has occured. Please contact helpdesk and quote number {0}.";
        public static readonly string MessageGeneralError = "General error has occured. Please contact helpdesk.";
        public static readonly string MessageFileTypeError = "Please upload a valid file type.";
    }
}

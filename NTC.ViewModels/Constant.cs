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

        public static readonly int PROVIDER_RSA_FULL = 1;
        public static readonly string PUBLIC_KEY_XML = @"<RSAKeyValue><Modulus>u1YNlhruaNL83h+a2m3pDGA6u6mnG0nYxi5DyYoBwNxcxLYIAnpyX7VvXk8YUgyV+p2kTNvXYv3oj3JWSxjOyGaSqvsClnleCcp2GWpGECC7rpF4NzdFzHyP3EXGYrcnbDQVxOr35+FkDa4WPL+jnyrWgniF8CeGnA2ExPSDGLU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static readonly string PRIVATE_KEY_XML = @"<RSAKeyValue><Modulus>u1YNlhruaNL83h+a2m3pDGA6u6mnG0nYxi5DyYoBwNxcxLYIAnpyX7VvXk8YUgyV+p2kTNvXYv3oj3JWSxjOyGaSqvsClnleCcp2GWpGECC7rpF4NzdFzHyP3EXGYrcnbDQVxOr35+FkDa4WPL+jnyrWgniF8CeGnA2ExPSDGLU=</Modulus><Exponent>AQAB</Exponent><P>9kFlTyj+hlvH/BjgvbF5K0AcetvTM74/7OFS69C3fiHYhyQ1Wseykg/8+B/jcjEEaKXCauRzCTTkQ10/Z11cyw==</P><Q>wr/Lx0InC2rpQ3wwK+l60Dm/aDV7wR4Wm+4Qhnp4RSRzS0JlImdSVO75VAWBxI5rbYJNb3PKhYq2H3bnteQwfw==</Q><DP>AxDtYf7wrFuYMdtdcP80swUpVZ5HEu3bfeeQUL9YiYmCWKxqvaae4pKwY6aB/nn9xA/MS+hsRkESVoRitbbD4Q==</DP><DQ>TfKkZnmU1R4ShT9UrI7D6Enk0ZnukYowdHLhzGGCd/Ix72KAxjdinboUs0uM+BYk62zm/3/yBGdTo95cudG+kw==</DQ><InverseQ>WEikzJS1LPVdYYikIm2kJB60mgee62E2qLNkicSPxO1vq3ObQTaOQgNx/FT+rm6OBqOM0Mr4cC/2b/kGaF6Fgw==</InverseQ><D>Sp1oxeuuH7RBoVsnO27hjttZKVvWQik0nSLaPxCnyCA9FuCF6i7JFVPXqtGIF04KAor9YNuDO1t0YIYi1sBnw3xoYAtrQj4L6ulN3iwZexlf0UFDnaiuBpvygm2BOXcnqNUfhp+5jqY604hf/Aj7EdsBrIdSHjt5sDFDLWsKITU=</D></RSAKeyValue>";

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OtherServices
{
    public interface IPINCode
    {
        string GeneratedPinCode();

        string PinCodeHtmlContent(string pinCode);
    }
}

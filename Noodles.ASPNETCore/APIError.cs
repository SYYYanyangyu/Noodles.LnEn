using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noodles.ASPNETCore
{
    public record APIError(int Code, string Message);
}

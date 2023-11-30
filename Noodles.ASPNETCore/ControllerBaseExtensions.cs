using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noodles.ASPNETCore
{
    public static class ControllerBaseExtensions
    {
        public static BadRequestObjectResult APIError(this ControllerBase controller, int code, string message)
        {
            return controller.BadRequest(new APIError(code, message));
        }
    }
}

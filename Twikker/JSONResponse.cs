using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twikker.Web
{
    public class JSONResponse
    {        

        public JSONResponse(bool successful, object responseData = null)
        {
            this.Successful = successful;
            this.ResponseData = responseData;
        }

        public bool Successful { get; private set; }

        public object ResponseData { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryEinsteinGame.Domain
{
    public class EinsteinGameExceptions:Exception
    {

        public EinsteinGameExceptions()
           : base()
        {
        }

        public EinsteinGameExceptions(String message)
            : base(message)
        {
        }

        public EinsteinGameExceptions(string message, Exception inner)
        : base(message, inner)
        {
        }

        




    }
}

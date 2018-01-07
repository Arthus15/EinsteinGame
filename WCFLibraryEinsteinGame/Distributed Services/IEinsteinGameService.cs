using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFLibraryEinsteinGame
{
    [ServiceContract]
    public interface IEinsteinGameService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/runGame?cheat={cheat}")]
        EinsteinGameDto RunGame(int cheat);
    }

}

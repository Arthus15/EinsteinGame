using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFLibraryEinsteinGame
{
    [DataContract(Namespace = "")]
    public class EinsteinGameDto 
    {

        public EinsteinGameDto(List<String> output)
        {
            OutPut = output;
        }

        [DataMember]
        public List<String> OutPut { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unreal.Core.Contracts;

namespace Unreal.Core.Models
{
    //Using for arrays
    public class FUniqueNetIdRepl : IProperty
    {
        public string NetId { get; set; }

        public void Serialize(NetBitReader reader)
        {
            NetId = reader.SerializePropertyNetId();
        }
    }
}

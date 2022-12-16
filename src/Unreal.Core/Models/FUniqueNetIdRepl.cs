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
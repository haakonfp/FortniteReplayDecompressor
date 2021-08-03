using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unreal.Core;
using Unreal.Core.Models;

namespace FortniteReplayReader.Models
{
    public class ExternalFortniteData : ExternalData
    {
        public byte Unknown1 { get; private set; }
        public ExternalPlayerNameData EncodedNameData { get; private set; }

        public override void Serialize(FArchive reader, int numBytes)
        {
            Unknown1 = reader.ReadByte();
            EncodedNameData = new ExternalPlayerNameData
            {
                Unknown1 = reader.ReadByte(),
                IsPlayer = reader.ReadBoolean()
            };

            string encodedName = reader.ReadFString();

            EncodedNameData.EncodedName = encodedName;

            if (EncodedNameData.IsPlayer)
            {
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < encodedName.Length; i++)
                {
                    var shift = (encodedName.Length % 4 * 3 % 8 + 1 + i) * 3 % 8;

                    int characterValue = encodedName[i] + shift;

                    builder.Append((char)characterValue);
                }

                EncodedNameData.DecodedName = builder.ToString();
            }
            else
            {
                EncodedNameData.DecodedName = EncodedNameData.EncodedName;
            }
        }
    }

    public class ExternalPlayerNameData
    {
        public byte Unknown1 { get; internal set; }
        public bool IsPlayer { get; internal set; }
        public string EncodedName { get; internal set; }
        public string DecodedName { get; internal set; }
    }
}

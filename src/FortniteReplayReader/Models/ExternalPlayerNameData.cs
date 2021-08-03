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
    public class ExternalPlayerNameData
    {
        public byte Handle { get; private set; }
        public byte Unknown1 { get; private set; }
        public bool IsPlayer { get; private set; }
        public string EncodedName { get; private set; }
        public string DecodedName { get; private set; }

        public static ExternalPlayerNameData Parse(byte[] data)
        {
            using Unreal.Core.BinaryReader reader = new Unreal.Core.BinaryReader(new MemoryStream(data));

            ExternalPlayerNameData nameData = new ExternalPlayerNameData
            {
                Handle = reader.ReadByte(),
                Unknown1 = reader.ReadByte(),
                IsPlayer = reader.ReadBoolean(),
                EncodedName = reader.ReadFString()
            };

            string decodedName = String.Empty;

            if (nameData.IsPlayer)
            {
                for (int i = 0; i < nameData.EncodedName.Length; i++)
                {
                    int shift = (nameData.EncodedName.Length % 4 * 3 % 8 + 1 + i) * 3 % 8;

                    decodedName += (char)(nameData.EncodedName[i] + shift);
                }
            }
            else
            {
                nameData.DecodedName = nameData.EncodedName;
            }

            nameData.DecodedName = decodedName;

            return nameData;
        }
    }
}

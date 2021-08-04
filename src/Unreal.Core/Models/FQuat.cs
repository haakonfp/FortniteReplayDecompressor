using System;
using System.Collections.Generic;
using System.Text;
using Unreal.Core.Contracts;

namespace Unreal.Core.Models
{

    public class FQuat : IProperty
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
        public float W { get; private set; }

        public FQuat()
        {

        }

        public FQuat(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        //bool FQuat::NetSerialize(FArchive& Ar, class UPackageMap*, bool& bOutSuccess)
        public void Serialize(NetBitReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();

            float XYZMagSquared = (X * X + Y * Y + Z * Z);
            float WSquared = 1.0f - XYZMagSquared;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}, W: {W}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Unreal.Core.Contracts;

namespace Unreal.Core.Models
{

    public class FQuat : IProperty
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double W { get; private set; }

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

        public FQuat(double x, double y, double z, double w)
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

            double XYZMagSquared = (X * X + Y * Y + Z * Z);
            double WSquared = 1.0f - XYZMagSquared;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}, W: {W}";
        }
    }
}

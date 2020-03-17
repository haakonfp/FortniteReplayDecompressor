﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unreal.Core
{
    public class FBitArray
    {
        public bool[] Items { get; private set; }

        public int Length => Items.Length;
        public bool IsReadOnly => false;
        public byte[] ByteArrayUsed;

        public FBitArray(bool[] bits)
        {
            Items = bits;
        }

        public unsafe FBitArray(byte[] bytes)
        {
            Items = new bool[bytes.Length * 8];
            ByteArrayUsed = bytes;

            fixed (byte* bytePtr = bytes)
            fixed (bool* itemPtr = Items)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    int offset = i * 8;

                    byte b = *(bytePtr + i);

                    for (int z = 0; z < 8; z++)
                    {
                        *(itemPtr + offset + z) = (b & 0x01) == 0x01;

                        b >>= 1;
                    }
                }
            }
        }

        public bool this[int index]
        { 
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }

        public Span<bool> AsSpan(int start, int count)
        {
            return Items.AsSpan(start, count);
        }
        public void CopyTo(bool[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public void Append(bool[] after)
        {
            bool[] newArray = new bool[Items.Length + after.Length];

            Array.Copy(Items, 0, newArray, 0, Items.Length);
            Array.Copy(after, 0, newArray, Items.Length, after.Length);

            Items = newArray;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1591
namespace Unimpressive.Core
{
    public static class StreamExtensions
    {
        public static bool ReadBool(this Stream input)
        {
            var tmp = new byte[1];
            input.Read(tmp, 0, 1);
            return BitConverter.ToBoolean(tmp, 0);
        }
        public static ushort ReadUshort(this Stream input)
        {
            var tmp = new byte[2];
            input.Read(tmp, 0, 2);
            return BitConverter.ToUInt16(tmp, 0);
        }
        public static uint ReadUint(this Stream input)
        {
            var tmp = new byte[4];
            input.Read(tmp, 0, 4);
            return BitConverter.ToUInt32(tmp, 0);
        }
        public static ulong ReadUlong(this Stream input)
        {
            var tmp = new byte[8];
            input.Read(tmp, 0, 8);
            return BitConverter.ToUInt64(tmp, 0);
        }
        public static short ReadShort(this Stream input)
        {
            var tmp = new byte[2];
            input.Read(tmp, 0, 2);
            return BitConverter.ToInt16(tmp, 0);
        }
        public static int ReadInt(this Stream input)
        {
            var tmp = new byte[4];
            input.Read(tmp, 0, 4);
            return BitConverter.ToInt32(tmp, 0);
        }
        public static long ReadLong(this Stream input)
        {
            var tmp = new byte[8];
            input.Read(tmp, 0, 8);
            return BitConverter.ToInt64(tmp, 0);
        }
        public static float ReadFloat(this Stream input)
        {
            var tmp = new byte[4];
            input.Read(tmp, 0, 4);
            return BitConverter.ToSingle(tmp, 0);
        }
        public static double ReadDouble(this Stream input)
        {
            var tmp = new byte[8];
            input.Read(tmp, 0, 8);
            return BitConverter.ToDouble(tmp, 0);
        }

        public static void WriteBool(this Stream input, bool data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteUshort(this Stream input, ushort data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteUint(this Stream input, uint data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteUlong(this Stream input, ulong data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteShort(this Stream input, short data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteInt(this Stream input, int data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteLong(this Stream input, long data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteFloat(this Stream input, float data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteDouble(this Stream input, double data)
        {
            var tmp = BitConverter.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }


        public static string ReadASCII(this Stream input, int length)
        {
            var tmp = new byte[length];
            input.Read(tmp, 0, length);
            return Encoding.ASCII.GetString(tmp);
        }
        public static string ReadUTF8(this Stream input, int length)
        {
            var tmp = new byte[length];
            input.Read(tmp, 0, length);
            return Encoding.UTF8.GetString(tmp);
        }
        public static string ReadUnicode(this Stream input, int length)
        {
            var tmp = new byte[length];
            input.Read(tmp, 0, length);
            return Encoding.Unicode.GetString(tmp);
        }

        public static void WriteASCII(this Stream input, string data)
        {
            var tmp = Encoding.ASCII.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteUTF8(this Stream input, string data)
        {
            var tmp = Encoding.UTF8.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }
        public static void WriteUnicode(this Stream input, string data)
        {
            var tmp = Encoding.Unicode.GetBytes(data);
            input.Write(tmp, 0, tmp.Length);
        }

        public static uint ASCIILength(this string s)
        {
            var tmp = Encoding.ASCII.GetBytes(s);
            return (uint)tmp.Length;
        }
        public static uint UTF8Length(this string s)
        {
            var tmp = Encoding.UTF8.GetBytes(s);
            return (uint)tmp.Length;
        }
        public static uint UnicodeLength(this string s)
        {
            var tmp = Encoding.Unicode.GetBytes(s);
            return (uint)tmp.Length;
        }

        public static Stream ToStream(this byte[] b)
        {
            Stream s = new MemoryStream();
            s.Read(b, 0, b.Length);
            return s;
        }
    }
}
#pragma warning restore CS1591
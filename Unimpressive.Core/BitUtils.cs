using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1591
namespace Unimpressive.Core
{
    public static class BitUtils
    {
        public static uint Or(params uint[] vals) => vals.Aggregate<uint, uint>(0x0, (current, v) => current | v);
        public static uint And(params uint[] vals) => vals.Aggregate(vals.First(), (current, v) => current & v);
        public static uint Xor(params uint[] vals) => vals.Aggregate(vals.First(), (current, v) => current ^ v);

        public static uint Join(params bool[] bools)
        {
            uint res = 0x0;
            int i = 0;
            foreach (bool b in bools)
            {
                uint mask = 0x80000000;
                mask = mask >> (32 - bools.Count());
                mask = mask >> i;
                if (b) res = res | mask;
                i++;
            }
            return res;
        }

        public static bool[] Split(uint val)
        {
            bool[] res = new bool[32];
            for (int i = 0; i < 32; i++)
            {
                uint mask = 0x80000000;
                mask = mask >> i;
                uint masked = val & mask;
                res[i] = masked > 0;
            }
            return res;
        }
    }
}
#pragma warning restore CS1591

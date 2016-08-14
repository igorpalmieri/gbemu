using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmulator.Model
{
    public class Memory
    {
        private byte[] mem;

        public Memory(int size)
        {
            mem = new byte[size];
        }
        public byte read(ushort address)
        {
            return mem[address];
        }

        public ushort read16(ushort address)
        {
            return (ushort)(mem[address] & (mem[address + 1] << 8));
        }

        public void write(ushort address, byte data)
        {
            mem[address] = data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmulator.Model
{
    public class RegisterBank
    {
        private byte[] registers;
        public bool Z, N, H, C;

        public RegisterBank()
        {
            registers = new byte[7];
        }

        public int Increment(string regs)
        {           
            if(regs.Length == 1)
            {
                N = false;
                if (++registers[getValue(regs[0])] == 0)
                    Z = true;
                else
                    Z = false;

                if (registers[getValue(regs[0])] == 0x10)
                    H = true;
                else
                    H = false;
                return 4;
            }
            else if (++registers[getValue(regs[1])] == 0)            
                ++registers[getValue(regs[0])];
            return 8;        
        }

        public int Decrement(string regs)
        {
            return 0; //TODO
        }

        public int Load(char reg, byte d8)
        {
           registers[getValue(reg)] = d8;
           return 4;
        }

        public int Load(char reg1, char reg2)
        {
            registers[getValue(reg1)] = registers[getValue(reg2)];
            return 4;
        }

        public int Load(string regs, ushort d16)
        {
            if (regs.Length == 2)
            {
                registers[getValue(regs[0])] = (byte)(d16 >> 8);
                registers[getValue(regs[0])] = (byte)(d16 & 0xFF);
                return 12;
            }
            else
                throw new Exception();
        }

        public void rotateLeft(char reg, bool withCarry)
        {
            var temp = registers[getValue(reg)] << 1;
            var bit = (registers[getValue(reg)] >> 7) & 0x1;
            if(withCarry)
                registers[getValue(reg)] = (byte)(temp | bit);
            else if(C)
                registers[getValue(reg)] = (byte)(temp | 0x1);
            else
                registers[getValue(reg)] = (byte)(temp);
            N = false;
            H = false;
            Z = false;

            if (bit == 1)
                C = true;
            else
                C = false;
        }

        public void rotateRight(char reg, bool withCarry)
        {
            var temp = registers[getValue(reg)] >> 1;
            var bit = (registers[getValue(reg)] & 0x1) << 7;
            if (withCarry)
                registers[getValue(reg)] = (byte)(temp | bit);
            else if (C)
                registers[getValue(reg)] = (byte)(temp | 0xA0);
            else
                registers[getValue(reg)] = (byte)(temp);
            N = false;
            H = false;
            Z = false;

            if (bit == 1)
                C = true;
            else
                C = false;
        }

        public int Add(char reg1, char reg2)
        {
            registers[reg1] += registers[reg2];
            if (registers[reg1] == 0)
                Z = true;
            else
                Z = false;
            N = false;

            if (registers[reg1] < registers[reg2])
                C = true;
            else
                C = false;

            //TODO H
            return 4;
        }
        public int Add(char reg1, byte d8)
        {
            registers[reg1] += d8;
            if (registers[reg1] == 0)
                Z = true;
            else
                Z = false;
            N = false;

            if (registers[reg1] < d8)
                C = true;
            else
                C = false;

            //TODO H
            return 4;
        }
        public int Adc(char reg1, char reg2)
        {
            registers[reg1] += registers[reg2];
            if (C)
                registers[reg1]++;
            
            if (registers[reg1] == 0)
                Z = true;
            else
                Z = false;
            N = false;

            if (registers[reg1] < registers[reg2])
                C = true;
            else
                C = false;

            //TODO H
            return 4;
        }
        public int Adc(char reg1, byte d8)
        {
            registers[reg1] += d8;
            if (C)
                registers[reg1]++;
            if (registers[reg1] == 0)
                Z = true;
            else
                Z = false;
            N = false;

            if (registers[reg1] < d8)
                C = true;
            else
                C = false;

            //TODO H
            return 4;
        }

        public int Sub(char reg1, char reg2)
        {
            if (registers[reg1] < registers[reg2])
                C = false;
            else
                C = true;

            registers[reg1] -= registers[reg2];
            if (registers[reg1] == 0)
                Z = true;
            else
                Z = false;
            N = true;

            //TODO H
            return 4;
        }
        public int Sub(char reg1, byte d8) 
        {
            if (registers[reg1] < d8)
                C = false;
            else
                C = true;

            registers[reg1] -= d8;
            if (registers[reg1] == 0)
                Z = true;
            else
                Z = false;
            N = true;

            //TODO H
            return 4;
        }
        public int Sbc(char reg1, char reg2)
        {
            registers[reg1] -= registers[reg2];
            if (C)
                registers[reg1]--;

            if (registers[reg1] == 0)
                Z = true;
            else
                Z = false;           

            if (registers[reg1] > registers[reg2])
                C = false;
            else
                C = true;
            
            N = true;

            //TODO H
            return 4;
        }
        public int Sbc(char reg1, byte d8)
        {
            registers[reg1] -= d8;
            if (C)
                registers[reg1]--;

            if (registers[reg1] == 0)
                Z = true;
            else
                Z = false;

            if (registers[reg1] > d8)
                C = false;
            else
                C = true;

            N = true;

            //TODO H
            return 4;
        }

        public int And(char reg1, char reg2)
        {
            registers[getValue(reg1)] = (byte)(registers[getValue(reg1)] & registers[getValue(reg2)]);
            N = false;
            H = true;
            C = false;
            if (registers[getValue(reg1)] == 0)
                Z = true;
            else
                Z = false;
            return 4;
        }

        public int Or(char reg1, char reg2)
        {
            registers[getValue(reg1)] = (byte)(registers[getValue(reg1)] | registers[getValue(reg2)]);
            N = false;
            H = false;
            C = false;
            if (registers[getValue(reg1)] == 0)
                Z = true;
            else
                Z = false;
            return 4;
        }

        public int Xor(char reg1, char reg2)
        {
            registers[getValue(reg1)] = (byte)(registers[getValue(reg1)] ^ registers[getValue(reg2)]);
            N = false;
            H = false;
            C = false;
            if (registers[getValue(reg1)] == 0)
                Z = true;
            else
                Z = false;
            return 4;
        }
        public ushort get(string regs)
        {
            if (regs.Length == 2)
            {
                return (ushort)((registers[getValue(regs[0])] << 8) & registers[getValue(regs[1])]);
            }
            else
                throw new Exception();
        }
        public byte get(char c)
        {
            return registers[getValue(c)];
        }
        private int getValue(char c)
        {
            switch (c)
            {
                case 'A':
                    return 0;
                case 'B':
                    return 1;
                case 'C':
                    return 2;
                case 'D':
                    return 3;
                case 'E':
                    return 4;
                case 'F':
                    return 5;
                case 'H':
                    return 6;
                case 'L':
                    return 7;
                default:
                    throw new Exception();
            }
        }

    }
}

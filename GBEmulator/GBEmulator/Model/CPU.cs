using GBEmulator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmulator.Model
{
    public class CPU
    {
        private ushort PC, SP;
        private byte[] register;
        private bool isPaused;
        private Memory workRAM;
        private RegisterBank regs;
        private byte currentInstruction;

        public CPU()
        {
            register = new byte[8];
            regs = new RegisterBank();
            isPaused = false;
        }

        public void Load()
        {

        }

        public void ReadNext()
        {
            if (isPaused)
                return;
            currentInstruction = workRAM.read(PC++);
            switch (currentInstruction >> 4)
            {
                #region 0x
                case 0x00:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                                return;
                            case 0x01:
                                regs.Load("BC", workRAM.read16(PC++));
                                PC++;
                                return;
                            case 0x02:
                                workRAM.write(regs.get("BC"), regs.get('A'));
                                return;
                            case 0x03:
                                regs.Increment("BC");
                                return;
                            case 0x04:
                                regs.Increment("B");
                                return;
                            case 0x05:
                                regs.Decrement("B");
                                return;
                            case 0x06:
                                regs.Load('B', workRAM.read(PC++));
                                return;
                            case 0x07:
                                regs.rotateLeft('A', true);
                                return;
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                                regs.Load('A', workRAM.read(regs.get("BC")));
                                return;
                            case 0x0B:
                                regs.Decrement("BC");
                                return;
                            case 0x0C:
                                regs.Increment("C");
                                return;
                            case 0x0D:
                                regs.Decrement("C");
                                return;
                            case 0x0E:
                                regs.Load('C', workRAM.read(PC++));
                                return;
                            case 0x0F:
                                regs.rotateRight('A', true);
                                return;
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 1x
                case 0x01:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                                isPaused = true;
                                return;
                            case 0x01:
                                regs.Load("DE", workRAM.read16(PC++));
                                PC++;
                                return;
                            case 0x02:
                                workRAM.write(regs.get("DE"), regs.get('A'));
                                return;
                            case 0x03:
                                regs.Increment("DE");
                                return;
                            case 0x04:
                                regs.Increment("D");
                                return;
                            case 0x05:
                                regs.Decrement("D");
                                return;
                            case 0x06:
                                regs.Load('D', workRAM.read(PC++));
                                return;
                            case 0x07:
                                regs.rotateLeft('A', false);
                                return;
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                                regs.Load('A', workRAM.read(regs.get("DE")));
                                return;
                            case 0x0B:
                                regs.Decrement("DE");
                                return;
                            case 0x0C:
                                regs.Increment("E");
                                return;
                            case 0x0D:
                                regs.Decrement("E");
                                return;
                            case 0x0E:
                                regs.Load('E', workRAM.read(PC++));
                                return;
                            case 0x0F:
                                regs.rotateRight('A', false);
                                return;
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 2x
                case 0x02:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                                regs.Load("HL", workRAM.read16(PC++));
                                PC++;
                                return;
                            case 0x02:
                            case 0x03:
                                regs.Increment("HL");
                                return;
                            case 0x04:
                                regs.Increment("H");
                                return;
                            case 0x05:
                                regs.Decrement("H");
                                return;
                            case 0x06:
                                regs.Load('H', workRAM.read(PC++));
                                return;
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                                regs.Decrement("HL");
                                return;
                            case 0x0C:
                                regs.Increment("L");
                                return;
                            case 0x0D:
                                regs.Decrement("L");
                                return;
                            case 0x0E:
                                regs.Load('L', workRAM.read(PC++));
                                return;
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 3x
                case 0x03:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                                SP = workRAM.read16(PC++);
                                PC++;
                                return;
                            case 0x02:
                            case 0x03:
                                SP++;
                                return;
                            case 0x04:                                
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                                SP--;
                                return;
                            case 0x0C:
                                regs.Increment("A");
                                return;
                            case 0x0D:
                                regs.Decrement("A");
                                return;
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 4x DONE
                case 0x04:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                                regs.Load('B', 'B');
                                return;
                            case 0x01:
                                regs.Load('B', 'C');
                                return;
                            case 0x02:
                                regs.Load('B', 'D');
                                return;
                            case 0x03:
                                regs.Load('B', 'E');
                                return;
                            case 0x04:
                                regs.Load('B', 'H');
                                return;
                            case 0x05:
                                regs.Load('B', 'L');
                                return;
                            case 0x06:
                                regs.Load('B', workRAM.read(regs.get("HL")));
                                return;
                            case 0x07:
                                regs.Load('B', 'A');
                                return;
                            case 0x08:
                                regs.Load('C', 'B');
                                return;
                            case 0x09:
                                regs.Load('C', 'C');
                                return;
                            case 0x0A:
                                regs.Load('C', 'D');
                                return;
                            case 0x0B:
                                regs.Load('C', 'E');
                                return;
                            case 0x0C:
                                regs.Load('C', 'H');
                                return;
                            case 0x0D:
                                regs.Load('C', 'L');
                                return;
                            case 0x0E:
                                regs.Load('C', workRAM.read(regs.get("HL")));
                                return;
                            case 0x0F:
                                regs.Load('C', 'A');
                                return;
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 5x DONE
                case 0x05:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                                regs.Load('D', 'B');
                                return;
                            case 0x01:
                                regs.Load('D', 'C');
                                return;
                            case 0x02:
                                regs.Load('D', 'D');
                                return;
                            case 0x03:
                                regs.Load('D', 'E');
                                return;
                            case 0x04:
                                regs.Load('D', 'H');
                                return;
                            case 0x05:
                                regs.Load('D', 'L');
                                return;
                            case 0x06:
                                regs.Load('D', workRAM.read(regs.get("HL")));
                                return;
                            case 0x07:
                                regs.Load('D', 'A');
                                return;
                            case 0x08:
                                regs.Load('E', 'B');
                                return;
                            case 0x09:
                                regs.Load('E', 'C');
                                return;
                            case 0x0A:
                                regs.Load('E', 'D');
                                return;
                            case 0x0B:
                                regs.Load('E', 'E');
                                return;
                            case 0x0C:
                                regs.Load('E', 'H');
                                return;
                            case 0x0D:
                                regs.Load('E', 'L');
                                return;
                            case 0x0E:
                                regs.Load('E', workRAM.read(regs.get("HL")));
                                return;
                            case 0x0F:
                                regs.Load('E', 'A');
                                return;
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 6x DONE
                case 0x06:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                                regs.Load('H', 'B');
                                return;
                            case 0x01:
                                regs.Load('H', 'C');
                                return;
                            case 0x02:
                                regs.Load('H', 'D');
                                return;
                            case 0x03:
                                regs.Load('H', 'E');
                                return;
                            case 0x04:
                                regs.Load('H', 'H');
                                return;
                            case 0x05:
                                regs.Load('H', 'L');
                                return;
                            case 0x06:
                                regs.Load('H', workRAM.read(regs.get("HL")));
                                return;
                            case 0x07:
                                regs.Load('H', 'A');
                                return;
                            case 0x08:
                                regs.Load('L', 'B');
                                return;
                            case 0x09:
                                regs.Load('L', 'C');
                                return;
                            case 0x0A:
                                regs.Load('L', 'D');
                                return;
                            case 0x0B:
                                regs.Load('L', 'E');
                                return;
                            case 0x0C:
                                regs.Load('L', 'H');
                                return;
                            case 0x0D:
                                regs.Load('L', 'L');
                                return;
                            case 0x0E:
                                regs.Load('L', workRAM.read(regs.get("HL")));
                                return;
                            case 0x0F:
                                regs.Load('L', 'A');
                                return;
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 7x DONE
                case 0x07:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                                workRAM.write(regs.get("HL"), regs.get('B'));
                                return;
                            case 0x01:
                                workRAM.write(regs.get("HL"), regs.get('C'));
                                return;
                            case 0x02:
                                workRAM.write(regs.get("HL"), regs.get('D'));
                                return;
                            case 0x03:
                                workRAM.write(regs.get("HL"), regs.get('E'));
                                return;
                            case 0x04:
                                workRAM.write(regs.get("HL"), regs.get('H'));
                                return;
                            case 0x05:
                                workRAM.write(regs.get("HL"), regs.get('L'));
                                return;
                            case 0x06:
                                isPaused = true;
                                return;
                            case 0x07:
                                workRAM.write(regs.get("HL"), regs.get('A'));
                                return;
                            case 0x08:
                                regs.Load('A', 'B');
                                return;
                            case 0x09:
                                regs.Load('A', 'C');
                                return;
                            case 0x0A:
                                regs.Load('A', 'D');
                                return;
                            case 0x0B:
                                regs.Load('A', 'E');
                                return;
                            case 0x0C:
                                regs.Load('A', 'H');
                                return;
                            case 0x0D:
                                regs.Load('A', 'L');
                                return;
                            case 0x0E:
                                regs.Load('A', workRAM.read(regs.get("HL")));
                                return;
                            case 0x0F:
                                regs.Load('A', 'A');
                                return;
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 8x
                case 0x08:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                            case 0x0D:
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region 9x
                case 0x09:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                            case 0x0D:
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region Ax
                case 0x0A:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                            case 0x0D:
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region Bx
                case 0x0B:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                            case 0x0D:
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region Cx
                case 0x0C:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                            case 0x0D:
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region Dx
                case 0x0D:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                            case 0x0D:
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region Ex
                case 0x0E:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                            case 0x0D:
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion
                #region Fx
                case 0x0F:
                    {
                        switch (currentInstruction & 0xF)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x08:
                            case 0x09:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                            case 0x0D:
                            case 0x0E:
                            case 0x0F:
                            default:
                                throw new CPUException();
                        }
                    }
                #endregion

                default:
                    throw new CPUException();
            }
        }

    

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmulator.Exceptions
{
    class CPUException : Exception
    {

        public CPUException() : base("Error to read Instruction")
        {

        }
    }
}

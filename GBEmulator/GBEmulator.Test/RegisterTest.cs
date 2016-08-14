using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GBEmulator.Model;

namespace GBEmulator.Test
{
    [TestClass]
    public class RegisterTest
    {
        RegisterBank registers;
        [TestInitialize]
        public void StartUp()
        {
            registers = new RegisterBank();
        }

        [TestMethod]
        public void IncrementTest()
        {
            byte temp = registers.get('A');
            registers.Increment("A");
            Assert.IsTrue(registers.get('A') == (temp + 1));
            temp = registers.get('B');
            registers.Increment("B");
            Assert.IsTrue(registers.get('B') == (temp + 1));
            temp = registers.get('C');
            registers.Increment("C");
            Assert.IsTrue(registers.get('C') == (temp + 1));
            temp = registers.get('D');
            registers.Increment("D");
            Assert.IsTrue(registers.get('D') == (temp + 1));
            temp = registers.get('E');
            registers.Increment("E");
            Assert.IsTrue(registers.get('E') == (temp + 1));
            temp = registers.get('F');
            registers.Increment("F");
            Assert.IsTrue(registers.get('F') == (temp + 1));
        }

        [TestMethod]
        public void IncrementTestWithFlags()
        {
            registers.Load('B', 0xF);
            registers.Increment("B");
            Assert.IsTrue(registers.H);
            Assert.IsFalse(registers.Z);
            Assert.IsFalse(registers.N);

            registers.Load('B', 0xFF);
            registers.Increment("B");
            Assert.IsFalse(registers.H);
            Assert.IsTrue(registers.Z);
            Assert.IsFalse(registers.N);

        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsigmentConstruct;
using System.IO;
using System;

namespace ConsigmentTestUnit
{
    [TestClass]
    public class ConsigmentTests
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            var myConsigment = CreateTestConsigment();

            double expectedCost = 185.25;

            Assert.AreEqual(15, myConsigment.Quantity);
            Assert.AreEqual(12.35, myConsigment.Price);
            Assert.AreEqual(expectedCost, myConsigment.Cost);
        }

        [TestMethod]
        public void ToStringTestMethod()
        {
            Assert.AreEqual("15 ��. �� 12,35 ���.", CreateTestConsigment().ToString());
        }

        [TestMethod]
        public void DisplayTestMethod()
        {
            var c1 = CreateTestConsigment();
            var c2 = new Consigment(11, 165.46);

            var consoleOut = new[]
            {
                "��������� Consigment, 15 ���� �� 12,35 ������",
                "��������� Consigment, 11 ���� �� 165,46 ������",
            };

            TextWriter oldOut = Console.Out;

            StringWriter output = new StringWriter();
            Console.SetOut(output);

            c1.Display();
            c2.Display();

            Console.SetOut(oldOut);

            var outputArray = output.ToString().Split(new[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(2, outputArray.Length);
            for (var i = 0; i < consoleOut.Length; i++)
                Assert.AreEqual(consoleOut[i], outputArray[i]);
        }

        [TestMethod]
        public void EqualsTestMethod()
        {
            Consigment c1 = new Consigment(12, 15);
            Consigment c2 = new Consigment(12, 15);

            Consigment c3 = new Consigment(12, 14);

            Assert.AreEqual(true, c1.Equals(c2));
            Assert.AreEqual(false, c1.Equals(c3));
        }

        [TestMethod]
        public void AdditionTestMethod()
        {
            Consigment c1 = new Consigment(15, 3.2);

            Consigment c2 = new Consigment(16, 3.2);

            var c3 = c1 + c2;

            Assert.AreEqual("31 ��. �� 3,2 ���.", c3.ToString());
        }

        [TestMethod]
        public void SubTestMethod()
        {
            Consigment c1 = new Consigment(15, 3.2);

            Consigment c2 = new Consigment(14, 3.2);

            var c3 = c1 - c2;

            Assert.AreEqual("1 ��. �� 3,2 ���.", c3.ToString());

        }

        private Consigment CreateTestConsigment()
        {
            return new Consigment(15, 12.35);
        }
    }
}

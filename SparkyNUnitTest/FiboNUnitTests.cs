using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class FiboNUnitTests
    {
        private Fibo fibo;
        [SetUp]
        public void Setup()
        {
            fibo = new Fibo();
        }

        [Test]
        public void GetFibo_Input1_ReturnFiboScenarios()
        {
            List<int> excpectedRange = new List<int> { 0 };
            fibo.Range = 1;
            List<int> result = fibo.GetFiboSeries();
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.EquivalentTo(excpectedRange));
        }

        [Test]
        public void GetFibo_Input6_ReturnFiboScenarios()
        {
            List<int> excpectedRange = new List<int> { 0, 1, 1, 2, 3, 5 };

            fibo.Range = 6;

            List<int> result = fibo.GetFiboSeries();


            Assert.Multiple(() =>
            {
                Assert.That(result, Does.Contain(3));
                Assert.That(result.Count, Is.EqualTo(6));
                Assert.That(result, Does.Not.Contain(4));
                Assert.That(result, Has.No.Member(4));
                Assert.That(result, Is.EquivalentTo(excpectedRange));
            });
        }
    }
}

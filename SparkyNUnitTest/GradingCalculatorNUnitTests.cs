using NUnit.Framework;

namespace Sparky
{
    [TestFixture]
    public class GradingCalculatorNUnitTests
    {
        private GradingCalculator GradingCalc;
        [SetUp]
        public void Setup()
        {
            GradingCalc = new GradingCalculator();
        }
        [Test]
        public void GetGrade_InputScore95Attendance90_GetAGrade() 
        {
            GradingCalc.Score = 90;
            GradingCalc.AttendancePercentage = 95;

            Assert.That(GradingCalc.GetGrade(), Is.EqualTo("A"));
        }

        [Test]
        [TestCase(95, 55)]
        [TestCase(65, 55)]
        [TestCase(50, 90)]
        public void GetGrade_FailSureScenarios_GetFGrade(int score, int attendance)
        {
            GradingCalc.Score = score;
            GradingCalc.AttendancePercentage = attendance;

            Assert.That(GradingCalc.GetGrade(), Is.EqualTo("F"));
        }


        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string Get_Grade_InputScoreAndAttendance_GetExpectedGrade(int score, int attendance)
        {
            GradingCalc.Score = score;
            GradingCalc.AttendancePercentage = attendance;
            return GradingCalc.GetGrade();
        }
    }
}

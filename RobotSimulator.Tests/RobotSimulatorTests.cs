namespace RobotSimulator.Tests;

public class RobotSimulatorTests
{
    [Fact]
    public void Sum_TwoNumbers_ShouldReturnCorrectSum()
    {
        int number1 = 5;
        int number2 = 10;

        int result = number1 + number2;

        Assert.Equal(15, result);
    }
}
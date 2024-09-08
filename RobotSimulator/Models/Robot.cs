namespace RobotSimulator.Models;

public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }
    public bool IsPlaced { get; set; }
}
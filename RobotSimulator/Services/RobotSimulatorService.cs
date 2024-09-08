using RobotSimulator.Models;

namespace RobotSimulator.Services;

public class RobotSimulatorService
{
    private readonly Robot _robot;

    public RobotSimulatorService(Robot robot)
    {
        _robot = robot;
    }
    
    public bool Place(int x, int y, Direction direction)
    {
        if (!IsValidPosition(x, y)) return false;
        
        _robot.X = x;
        _robot.Y = y;
        _robot.Direction = direction;
        _robot.IsPlaced = true;
        
        return true;
    }
    
    public void Move()
    {
        if (!_robot.IsPlaced) return;

        var (xChange, yChange) = _robot.Direction switch
        {
            Direction.North => (0, 1),
            Direction.East => (1, 0),
            Direction.South => (0, -1),
            Direction.West => (-1, 0),
            _ => throw new NotImplementedException("Unhandled direction")
        };

        int newX = _robot.X + xChange;
        int newY = _robot.Y + yChange;

        if (!IsValidPosition(newX, newY)) return;
        
        _robot.X = newX;
        _robot.Y = newY;
    }

    public void Left()
    {
        if (!_robot.IsPlaced) return;

        _robot.Direction = _robot.Direction switch
        {
            Direction.North => Direction.West,
            Direction.West => Direction.South,
            Direction.South => Direction.East,
            Direction.East => Direction.North,
            _ => _robot.Direction
        };
    }

    public void Right()
    {
        if (!_robot.IsPlaced) return;

        _robot.Direction = _robot.Direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => _robot.Direction
        };
    }

    public string Report()
    {
        if (!_robot.IsPlaced) return "Not placed yet";

        return $"{_robot.X},{_robot.Y},{_robot.Direction}";
    }

    private static bool IsValidPosition(int x, int y)
    {
        const int gridSize = 5;

        return x >= 0 && x < gridSize && y >= 0 && y < gridSize;
    }
}
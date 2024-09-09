using RobotSimulator.Models;
using RobotSimulator.Services;

namespace RobotSimulator.Tests;

public class RobotSimulatorTests
{
    private static RobotSimulatorService CreateService()
    {
        Robot robot = new Robot();
        return new RobotSimulatorService(robot);
    }

    #region file inputs

    [Fact]
    public void ProcessCommandsFromFile_EmptyFile_ShouldNotifyEmptyFile()
    {
        RobotSimulatorService robotService = CreateService();
        var tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, string.Empty);

        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        Program.ProcessCommandsFromFile(tempFilePath, robotService);

        var result = consoleOutput.ToString();
        Assert.Contains("The file is empty. No commands to process.", result);

        // clean up
        File.Delete(tempFilePath);
    }
    
    [Fact]
    public void ProcessCommandsFromFile_ValidCommands_ShouldProcessCommands()
    {
        Robot robot = new Robot();
        RobotSimulatorService robotService = CreateService();
        var tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, "PLACE,0,0,NORTH\nMOVE\nREPORT");

        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        Program.ProcessCommandsFromFile(tempFilePath, robotService);

        var result = consoleOutput.ToString();
        Assert.Contains("Robot placed at (0, 0) facing North.", result);
        Assert.Contains("Robot moved.", result);

        // clean up
        File.Delete(tempFilePath);
    }

    #endregion

    #region command inputs

    [Fact]
    public void PlaceAndMoveNorth_ShouldUpdatePositionToNorth()
    {
        RobotSimulatorService service = CreateService();
        service.Place(0, 0, Direction.North);
        
        service.Move();
        string report = service.Report();
        
        Assert.Equal("0,1,North", report);
    }

    [Fact]
    public void PlaceAndTurnLeft_ShouldChangeDirectionToWest()
    {
        RobotSimulatorService service = CreateService();
        service.Place(0, 0, Direction.North);
        
        service.Left();
        string report = service.Report();
        
        Assert.Equal("0,0,West", report);
    }

    [Fact]
    public void PlaceMoveMoveLeftMove_ShouldEndUpAtPosition3_3FacingNorth()
    {
        RobotSimulatorService service = CreateService();
        service.Place(1, 2, Direction.East);
        
        service.Move(); // move to (2,2)
        service.Move(); // move to (3,2)
        service.Left(); // direction changes to north
        service.Move(); // move to (3,3)
        string report = service.Report();
        
        Assert.Equal("3,3,North", report);
    }
    
    #region edge cases
    
    [Fact]
    public void PlaceAndMoveNorthFromEdge_ShouldNotMoveOutOfBounds()
    {
        RobotSimulatorService service = CreateService();
        service.Place(0, 4, Direction.North); // place at the edge of the table

        service.Move(); // attempt to move out of bounds
        string report = service.Report();

        // the robot should not have moved
        Assert.Equal("0,4,North", report); 
    }
    
    [Fact]
    public void PlaceAndMoveMultipleTimes_ShouldHandleSequentialMovesCorrectly()
    {
        RobotSimulatorService service = CreateService();
        service.Place(2, 2, Direction.North);

        service.Move(); // moves to (2, 3)
        service.Move(); // moves to (2, 4) – edge of the table
        service.Move(); // should not move – out of bounds
        service.Left(); // changes direction to west
        service.Move(); // moves to (1, 4) – still within bounds
        string report = service.Report();

        // final position after all moves
        Assert.Equal("1,4,West", report); 
    }
    
    #endregion
    
    #endregion
}
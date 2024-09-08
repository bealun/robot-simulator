using RobotSimulator.Models;
using RobotSimulator.Services;

namespace RobotSimulator;

public class Program
{
    static void Main()
    {
        Robot robot = new Robot();
        RobotSimulatorService robotService = new RobotSimulatorService(robot);

        DisplayUserInstructions();
        
        while (true)
        {
            string? command = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(command))
            {
                HandleEmptyCommand();
                continue;
            }

            CommandType commandType = ParseCommandType(command, out string[] commandParts);

            if (commandType == CommandType.Exit)
            {
                Console.WriteLine("Exiting...");
                break;
            }
            
            ProcessCommand(commandType, commandParts, robotService);
        }
    }
    
    static CommandType ParseCommandType(string command, out string[] commandParts)
    {
        commandParts = command.Split(',');

        if (commandParts.Length == 0)
        {
            return CommandType.Unknown;
        }

        return Enum.TryParse<CommandType>(commandParts[0].Trim(), true, out var commandType)
            ? commandType
            : CommandType.Unknown;
    }

    static void ProcessCommand(CommandType commandType, string[] commandParts, RobotSimulatorService robotService)
    {
        switch (commandType)
        {
            case CommandType.Place:
                if (commandParts.Length == 4 &&
                    int.TryParse(commandParts[1], out var x) &&
                    int.TryParse(commandParts[2], out var y) &&
                    Enum.TryParse<Direction>(commandParts[3], true, out var direction))
                {
                    bool success = robotService.Place(x, y, direction);
                    
                    if (success)
                        Console.WriteLine($"Robot placed at ({x}, {y}) facing {direction}.");
                    else
                        Console.WriteLine("Invalid position. Coordinates must be within the 5x5 grid (0-4)."
                            + " Please try again.");
                }
                else
                {
                    Console.WriteLine("Invalid PLACE command format. Example: PLACE,X,Y,F");
                }
                break;

            case CommandType.Move:
                robotService.Move();
                Console.WriteLine("Robot moved.");
                break;

            case CommandType.Left:
                robotService.Left();
                Console.WriteLine("Robot turned left.");
                break;

            case CommandType.Right:
                robotService.Right();
                Console.WriteLine("Robot turned right.");
                break;

            case CommandType.Report:
                Console.WriteLine(robotService.Report());
                break;
            
            case CommandType.Unknown:
            default:
                Console.WriteLine("Unknown command. Please use one of the following: PLACE, MOVE, LEFT, RIGHT, REPORT.");
                break;
        }
    }
    
    
    private static void DisplayUserInstructions()
    {
        
        Console.WriteLine("Welcome to the Robot Simulator!");
        Console.WriteLine("You can control the robot within a 5x5 grid using the following commands:");
        Console.WriteLine("PLACE,X,Y,F - Place the robot at position (X, Y) facing direction F.");
        Console.WriteLine("   Where F can be NORTH, SOUTH, EAST, or WEST.");
        Console.WriteLine("MOVE - Move the robot one unit forward in the direction it is currently facing.");
        Console.WriteLine("LEFT - Turn the robot 90 degrees to the left.");
        Console.WriteLine("RIGHT - Turn the robot 90 degrees to the right.");
        Console.WriteLine("REPORT - Output the current position and direction of the robot.");
        Console.WriteLine();
        Console.WriteLine("To exit the application, type EXIT.");
        Console.WriteLine("Example commands:");
        Console.WriteLine("   PLACE,0,0,NORTH");
        Console.WriteLine("   MOVE");
        Console.WriteLine("   REPORT");
        Console.WriteLine();
    }
    
    private static void HandleEmptyCommand()
    {
        Console.WriteLine("Empty command. Please enter valid command.");
    }
}
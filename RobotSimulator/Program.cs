using RobotSimulator.Models;
using RobotSimulator.Services;

namespace RobotSimulator;

public class Program
{
    private const string DefaultFileDirectory = "RobotSimulator/SampleInputs";
    
    static void Main(string[] args)
    {
        Robot robot = new Robot();
        RobotSimulatorService robotService = new RobotSimulatorService(robot);

        DisplayUserInstructions();

        try
        {
            RunInteractiveMode(robotService);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    private static void RunInteractiveMode(RobotSimulatorService robotService)
    {
        while (true)
        {
            Console.WriteLine("Enter commands or type 'file' to input a file path:");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                HandleEmptyCommand();
                continue;
            }

            if (input.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                HandleFileSelection(robotService);
                continue;
            }

            var (commandType, commandParts) = ParseCommand(input);

            if (commandType == CommandType.Exit)
            {
                Console.WriteLine("Exiting...");
                break;
            }

            ProcessCommand(commandType, commandParts, robotService);
        }
    }
    
    private static void HandleFileSelection(RobotSimulatorService robotService)
    {
        Console.WriteLine("Enter the filename (e.g., test1.txt):");
        
        string? fileName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine("Filename cannot be empty. Please provide a valid filename.");
            return;
        }
        
        string filePath = Path.Combine(DefaultFileDirectory, fileName);

        if (!string.IsNullOrEmpty(fileName) && File.Exists(filePath))
        {
            ProcessCommandsFromFile(filePath, robotService);
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
    
    public static void ProcessCommandsFromFile(string filePath, RobotSimulatorService robotService)
    {
        string[] commands = File.ReadAllLines(filePath);
        
        if (commands.Length == 0)
        {
            Console.WriteLine("The file is empty. No commands to process.");
            return;
        }

        foreach (string command in commands)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                Console.WriteLine("Empty command in file. Skipping...");
                continue;
            }

            var (commandType, commandParts) = ParseCommand(command);

            if (commandType == CommandType.Exit)
            {
                Console.WriteLine("Exiting...");
                break; 
            }

            ProcessCommand(commandType, commandParts, robotService);
        }
    }
    
    static (CommandType, string[]) ParseCommand(string command)
    {
        string[] commandParts = command.Split(',');
        CommandType commandType = CommandType.Unknown;

        if (commandParts.Length > 0)
        {
            Enum.TryParse(commandParts[0].Trim(), true, out commandType);
        }

        return (commandType, commandParts);
    }

    static void ProcessCommand(CommandType commandType, string[] commandParts, RobotSimulatorService robotService)
    {
        switch (commandType)
        {
            case CommandType.Place:
                ProcessPlaceCommand(commandParts, robotService);
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
    
    static void ProcessPlaceCommand(string[] commandParts, RobotSimulatorService robotService)
    {
        if (commandParts.Length == 4 &&
            int.TryParse(commandParts[1], out int x) &&
            int.TryParse(commandParts[2], out int y) &&
            Enum.TryParse<Direction>(commandParts[3], true, out Direction direction))
        {
            bool success = robotService.Place(x, y, direction);

            if (success)
                Console.WriteLine($"Robot placed at ({x}, {y}) facing {direction}.");
            else
                Console.WriteLine("Invalid position. Coordinates must be within the 5x5 grid (0-4). Please try again.");
        }
        else
        {
            Console.WriteLine("Invalid PLACE command format. Example: PLACE,X,Y,F");
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
        Console.WriteLine("To process a file, type 'file' and provide the file path when prompted.");
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
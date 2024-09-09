# Robot Simulator

## Overview

This is a simple robot simulator that allows you to control a robot on a 5x5 grid. You can issue commands to place the robot, move it, turn it, and report its position. The simulator supports both interactive command entry and command processing from a file.

## Features

- **Place:** Place the robot at a specific position on the grid with a given direction.
- **Move:** Move the robot one step forward in its current direction.
- **Left:** Turn the robot 90 degrees to the left.
- **Right:** Turn the robot 90 degrees to the right.
- **Report:** Output the current position and direction of the robot.

## Commands

The simulator accepts the following commands within the grid:

- `PLACE,X,Y,DIRECTION` - Place the robot at coordinates (X, Y) facing DIRECTION. Where `DIRECTION` can be NORTH, SOUTH, EAST, or WEST.
- `MOVE` - Move the robot forward.
- `LEFT` - Turn the robot left.
- `RIGHT` - Turn the robot right.
- `REPORT` - Print the robot's current position and direction.
- `EXIT` - Exit the application.

## File Input

You can also process commands from a file. The file should contain commands separated by newlines. To process a file, type `file` at the command prompt and provide the path to the file when prompted. 
The file path now points directly to pre-made test files under `RobotSimulator/SampleInputs/`.


### Example

- `PLACE,0,0,NORTH`
- `MOVE`
- `REPORT`

## Setup

To run locally:
1. **Ensure you have [.NET 8](https://dotnet.microsoft.com/en-us/download) installed**
2. **Clone the repository**
3. **Navigate to the Project Directory and Restore Dependencies**
```
cd robot-simulator
dotnet restore
```
4. **Run the Tests**
```
dotnet test
```
5. **Build and Run the Application**
```
dotnet run --project RobotSimulator/RobotSimulator.csproj
```
1. **Interactive Mode**:
    - Run the application.
    - Enter commands directly or type `file` to input a file path.

2. **File Mode**:
    - Run the application.
    - When prompted, type `file` and provide the path to the file. 



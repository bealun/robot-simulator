# Robot Simulator

## Overview
The Robot Simulator is a simple command-line application where you can control a robot on a 5x5 grid. You can place the robot, move it forward, rotate it, and check its current position and direction.

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



# Bubbles Dive Planner

The idea behind Bubbles Dive Planner is to aim for scuba divers to be able to perform the safe planning of scuba diving activities. Currently supports the Bulhmann dive model with more planned.

It uses dotnet 8.0 along with Moq, XUnit and Newtonsoft for the backend and Avalonia with ReactiveUI components for the frontend.

## Running Bubbles Dive Planner

> cd ~/Bubbles-Dive-Planner/BubblesDivePlanner
>
> dotnet restore
>
> dotnet build
>
> dotnet run

Or set BubblesDivePlanner.csproj as the launch project in your IDE.

This application has been tested to run on debain derived Linux, Windows 10 and Mac OS 10 beyond this scope your expierences may vary.

## Testing Bubbles Dive Planner

> cd ~/Bubbles-Dive-Planner/BubblesDivePlannerTests
>
> dotnet restore
>
> dotnet test

Or set the BubblesDivePlannerTests.csproj up in your testing enviroment in your IDE of choice.

## Dependencies

Follow the steps for installing dotnet 8.0 runtime for your given operating system.

> <https://dotnet.microsoft.com/download/dotnet/8.0>

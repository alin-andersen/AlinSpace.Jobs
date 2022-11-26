<img src="https://github.com/onixion/AlinSpace.Jobs/blob/main/Assets/Icon.png" width="200" height="200">

# AlinSpace.Commands
[![NuGet version (AlinSpace.Commands)](https://img.shields.io/nuget/v/AlinSpace.Commands.svg?style=flat-square)](https://www.nuget.org/packages/AlinSpace.Commands/)

An asynchronous command and command manager implementation.

[NuGet package](https://www.nuget.org/packages/AlinSpace.Commands/)

# Why?

I needed a **clean and simple** job scheduler for my purposes.
I tried Quartz, but I don't like some design choices (ported from the Java world) and I had issues with it.

# Features

- **Triggers**:
    - **OneShot**: Execute the job only once in the future.
    - **Recurring**: Execute the job multiple times in a fixed interval.
        - **Quota**: Define the interval using a quota (e.g. 5 times in 5 months).

# Maybe Features

- **Persistent jobs**: Save the execution details of the jobs persistent.


## Examples

```csharp
// Create the scheduler.
using var scheduler = new Scheduler();

// Start the scheduler.
scheduler.Start();

// Schedules a recurring job (every 5 seconds).
scheduler.ScheduleJob<MyJob>(Trigger.Recurring(TimeSpan.FromSeconds(5));

// Schedules a recurring job using a quota (5 times a year).
scheduler.ScheduleJob<MyJob>(Trigger.Recurring(Quota.Year(5));

// ...

// Stop the scheduler.
scheduler.Stop();

```

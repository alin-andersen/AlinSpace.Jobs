# AlinSpace.Jobs
[![NuGet version (AlinSpace.Jobs)](https://img.shields.io/nuget/v/AlinSpace.Jobs.svg?style=flat-square)](https://www.nuget.org/packages/AlinSpace.Jobs/)

A clean and simple job scheduler.

[AlinSpace.Jobs NuGet package](https://www.nuget.org/packages/AlinSpace.Jobs/)

# Why?

I needed a **clean and simple** job scheduler for my purposes.

I tried **Quartz.NET**, but I don't like some design choices (it was ported from the Java world) and I had issues using it.

**Hangfire** was not too bad, but I do not like that they have a *free* and *pro* version.

# Features

- **Triggers**:
    - **OneShot**: Execute the job only once in the future.
    - **Recurring**: Execute the job multiple times in a fixed interval.
        - **Quota**: Define the interval using a quota (e.g. 5 times in 5 months).

## Maybe Features

- **Persistent jobs**: Save the execution details of the jobs persistent.

# Packages

- **AlinSpace.Jobs**: Main package
- **AlinSpace.Jobs.DryIoc**: Job factory for *DryIoc*.

# Examples

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

# Week 06: async and concurrency

This week contains one solution with two independent console projects.

## Projects

- `01-task-async-await` demonstrates one asynchronous operation.
- `02-async-composition` demonstrates combining independent operations with `Task.WhenAll`.

The solution groups projects for convenient build and run management. It does not mean that the projects depend on each other. There are no project references between them.

## Commands

```powershell
dotnet sln .\Week06.slnx list
dotnet build .\Week06.slnx
dotnet run --project .\01-task-async-await
dotnet run --project .\02-async-composition
```
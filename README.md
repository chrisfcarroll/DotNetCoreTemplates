# DotNetCoreTemplates

Consoleable Templates to be turned into dotnet new templates

## Opinions

* Components should be able to log
* Components should be Configurable
* Components know nothing of dependency injection. DI is an application-level concern, telling an application which components to use and with what lifecycle. It is not a component-level concern
* Components should be testable
* Running a component from the console is nice
* Serilog, because structured logging

```
 assertableSpecs
	.ShouldBeOnTheTipOfYour("Intellisense")
	.ShouldBeEasilyExtensible()
	.ShouldBeReadable();
```
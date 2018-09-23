# DotNetCoreTemplates

Consoleable Templates to be turned into dotnet new templates

## Opinions

* Components should be able to Log and should be logging provider agnostic.
* Components should be Configurable
* Components know nothing of dependency injection. DI is an application-level concern, telling an application which components to use and with what lifecycle. It is not a component-level concern
* Components should be testable
* Running a component from the console is nice


### Logging options

Alas, to be logging provider agnostic with a strongly typed language, means choosing an abstraction layer.

* `Consoleable.Component` uses Microsoft.Extensions.Logging.Abstractions. There's an example in the Specs of 
how to use Serilog via the Serilog.Extensions.Logging package.

* `Consoleable.Component.With.LibLog` uses LibLog. If you're using LibLog, you already know how to use 
your favourite provider.

## Opinions on Testing

`Spec` is a good word. It reminds you that a test should be traceable to a meaningful specification.

```
 assertableSpecs
	.ShouldBeOnTheTipOfYour("Intellisense")
	.ShouldBeEasilyExtensible()
	.ShouldBeReadable();
```

using System.Reflection;
using BenchmarkDotNet.Running;

var arguments = args is { Length: > 0 } ? args : new[] { "--filter", "*" };
BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(arguments);

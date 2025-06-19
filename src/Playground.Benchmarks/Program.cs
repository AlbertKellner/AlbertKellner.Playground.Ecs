using System.Reflection;
using BenchmarkDotNet.Running;

var arguments = args.Length > 0 ? args : new[] { "--filter", "*" };
BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(arguments);

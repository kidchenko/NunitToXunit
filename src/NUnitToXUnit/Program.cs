// Copyright (c) 2018 Jetabroad Pty Limited. All Rights Reserved.
// Licensed under the MIT license. See the LICENSE.md file in the project root for license information.

using System.IO;
using System.Text;
using DocoptNet;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnitToXUnit.Visitor;

namespace NUnitToXUnit
{
    public static class Program
    {
        private const string usage = @"NUnitToXUnit.

    Usage:
      NUnitToXUnit.exe convert [--cwd=<cwd>]
      NUnitToXUnit.exe (-h | --help)
      NUnitToXUnit.exe --version

    Options:
      -h --help     Show this screen.
      --version     Show version.
      --cwd=<cwd>   Working directory (default is current directory) [default: .].

    ";

        private static void Main(string[] args)
        {
            var cliOptions = ParseCliOptions(args);

            var pathToLookup = cliOptions.Cwd;
            const bool convertAssert = false;

            var files = Directory.GetFiles(pathToLookup, "*.cs", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var nUnitTree = CSharpSyntaxTree.ParseText(File.ReadAllText(file)).GetRoot();
                var xUnitTree = new NUnitToXUnitVisitor(new ConverterOptions { ConvertAssert = convertAssert }).Visit(nUnitTree).NormalizeWhitespace();
                File.WriteAllText(file, xUnitTree.ToFullString(), Encoding.UTF8);
            }
        }

        private static ICliOptions ParseCliOptions(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, version: "NUnitToXUnit 1.0.0", exit: true);
            return new DocoptOptions(arguments);
        }
    }
}

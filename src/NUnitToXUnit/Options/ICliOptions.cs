// Copyright (c) 2018 Jetabroad Pty Limited. All Rights Reserved.
// Licensed under the MIT license. See the LICENSE.md file in the project root for license information.

using System.Collections.Generic;
using DocoptNet;

namespace NUnitToXUnit
{
    public interface ICliOptions
    {
        string Cwd { get; set; }
    }

    public class DocoptOptions : ICliOptions
    {
        public DocoptOptions(IDictionary<string, ValueObject> arguments)
        {
            Cwd = arguments["--cwd"].ToString();
        }

        public string Cwd { get; set; }
    }
}

//---------------------------------------------------------------------------
// Copyright (c) 2004-2022 Michael G. Brehm
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

using zuki.ronin.data;

namespace zuki.ronin
{
	internal class main
	{
		/// <summary>
		/// Shows application usage information
		/// </summary>
		private static void ShowUsage()
		{
			Console.WriteLine();
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName.ToUpper() + " importdir outfile");
			Console.WriteLine();
			Console.WriteLine("gendb - Generate the RONIN database");
			Console.WriteLine();
			Console.WriteLine("  importdir    : Base directory of the import files");
			Console.WriteLine("  outfile      : output database file name");
		}

		/// <summary>
		/// Application entry point
		/// </summary>
		/// <param name="arguments">Array of comamnd line arguments</param>
		private static int Main(string[] arguments)
		{
			try
			{
				// Parse the command line arguments and switches
				CommandLine commandline = new CommandLine(arguments);

				// If insufficient arguments specified or help was requested, display the usage and exit
				if((commandline.Arguments.Count == 0) || commandline.Switches.ContainsKey("?"))
				{
					ShowUsage();
					return 0;
				}

				// There has to be exactly two command line arguments
				if(commandline.Arguments.Count != 2) throw new ArgumentException("Invalid command line arguments");
				string importdir = Path.GetFullPath(commandline.Arguments[0]);
				string outputfile = Path.GetFullPath(commandline.Arguments[1]);

				// The specified import directory must exist
				if(!Directory.Exists(importdir)) throw new ArgumentException("Specified import directory [" + importdir + "] does not exist");

				// Attempt to create the output directory if it does not exist
				string outdir = Path.GetDirectoryName(outputfile);
				if(!Directory.Exists(outdir)) Directory.CreateDirectory(outdir);

				Console.WriteLine();
				Console.WriteLine("Generating RONIN database " + outputfile + " from import directory " + importdir + " ...");
				Console.WriteLine();

				// Dump how long this operation takes to the console
				DateTime start = DateTime.Now;

				// Attempt to generate the database from the import folder
				using(Database db = Database.Import(importdir, outputfile)) { }

				Console.WriteLine("Database successfully generated in " + (DateTime.Now - start).TotalSeconds + " seconds.");
				Console.WriteLine();

				return 0;
			}

			catch(Exception ex)
			{
				Console.WriteLine();
				Console.WriteLine("ERROR: " + ex.Message);
				Console.WriteLine();

				return unchecked((int)0x80004005);      // <-- E_FAIL
			}
		}
	}
}
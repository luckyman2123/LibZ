﻿using System.Collections.Generic;
using LibZ.Tool.Tasks;
using ManyConsole;

namespace LibZ.Tool.Commands
{
	public class InstrumentCommand: ConsoleCommand
	{
		private string _mainFileName;
		private bool _allAsmZResources;
		private bool _allLibZResources;
		private string _keyFileName;
		private string _keyFilePassword;
		private readonly List<string> _libzFiles = new List<string>();
		private readonly List<string> _libzPatterns = new List<string>();

		public InstrumentCommand()
		{
			IsCommand("instrument", "Instruments assembly with initialization code");
			HasRequiredOption("a|assembly=", "assembly to be instrumented", s => _mainFileName = s);
			HasOption("asmz", "adds embedded assembly resolver", _ => _allAsmZResources = true);
			HasOption("libz-resources", "registers embedded LibZ container on startup", _ => _allLibZResources = true);
			HasOption("libz-file", "registers file on startup", s => _libzFiles.Add(s));
			HasOption("libz-pattern", "registers multiple files on startup (wildcards)", s => _libzPatterns.Add(s));
			HasOption("k|key=", "key file name", s => _keyFileName = s);
			HasOption("p|password=", "password for password protected key file", s => _keyFilePassword = s);
		}

		public override int Run(string[] remainingArguments)
		{
			var task = new InstrumentTask();
			task.Execute(
				_mainFileName, 
				_allAsmZResources, 
				_allLibZResources, _libzFiles, _libzPatterns, 
				_keyFileName, _keyFilePassword);

			return 0;
		}
	}
}
namespace MagiWol;

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
using Microsoft.Extensions.Logging;

internal static partial class App {

    internal static int Main(string[] args) {
        var logLevel = LogLevel.Information;  // default log level
        var verboseOption = new Option<bool>("--verbose", "-v") {
            Description = "Enable verbose output",
            Arity = ArgumentArity.Zero,
            DefaultValueFactory = _ => false,
        };
        verboseOption.Validators.Add(result => {
            logLevel = result.IdentifierTokenCount switch {
                1 => LogLevel.Debug,
                _ => LogLevel.Trace,
            };
        });

        var wakeOption = new Option<bool>("--wake", "-w", "/wake") {
            Description = "Send wake command",
            Arity = ArgumentArity.Zero,
            DefaultValueFactory = _ => false,
        };

        var shutOption = new Option<bool>("--shut", "-s", "/shut") {
            Description = "Send wake command",
            Arity = ArgumentArity.Zero,
            DefaultValueFactory = _ => false,
            Hidden = true
        };

        var fileArgument = new Argument<FileInfo>("file") {
            Description = "File to use",
            Arity = ArgumentArity.ExactlyOne
        };
        fileArgument.Validators.Add(result => {
            var value = result.GetValueOrDefault<FileInfo>();
            if (!value.Exists) {
                result.AddError($"File \"{value.FullName}\" doesn't exist");
            }
        });

        var macArgument = new Argument<string[]>("mac") {
            Description = "MAC address",
            Arity = ArgumentArity.OneOrMore
        };


        // Default
        var rootCommand = new RootCommand("Wake-on-LAN tool") {
            verboseOption,
            wakeOption,
            shutOption,
            macArgument,
        };
        rootCommand.TreatUnmatchedTokensAsErrors = true;
        rootCommand.Validators.Add(result => {
            if (result.GetValue(wakeOption) && result.GetValue(shutOption)) {
                result.AddError("Cannot specify both --wake and --shut actions");
            }
            if (!result.GetValue(wakeOption) && !result.GetValue(shutOption)) {
                result.AddError("Must specify action");
            }
            if ((result.GetValue(macArgument)?.Length ?? 0) == 0) {
                result.AddError("Must specify MAC addresses");
            }
        });
        rootCommand.SetAction(result => {
            var macs = result.GetValue(macArgument)!;
            if (result.GetValue(wakeOption)) {  // wake based on macs
                App.Wake(macs, logLevel);
            } else if (result.GetValue(wakeOption)) {  // shutdown based on macs
                App.Shut(macs, logLevel);
            }
        });

        // Wake
        var wakeCommand = new Command("wake", "Wakes the computers") {
            verboseOption,
            fileArgument,
        };
        wakeCommand.SetAction(result => {
            App.Wake(
                result.GetValue(fileArgument)!,  // handled by parser
                logLevel
            );
        });
        rootCommand.Add(wakeCommand);

        // Shut
        var shutCommand = new Command("shut", "Shuts down the computers") {
            verboseOption,
            fileArgument,
        };
        shutCommand.Hidden = true;  // not an official command
        shutCommand.SetAction(result => {
            App.Shut(
                result.GetValue(fileArgument)!,  // handled by parser
                logLevel
            );
        });
        rootCommand.Add(shutCommand);

        return rootCommand.Parse(["/wake","1:2:3:4:5"]).Invoke();
    }

}

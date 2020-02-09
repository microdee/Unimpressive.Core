using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Text;
using Unimpressive.Core;

namespace Unimpressive.Poweshell
{
    public static class PowershellExtensions
    {
        public static PowerShell WithStreamsOutput(this PowerShell ps, PSHostUserInterface psui)
        {
            ps.Streams.Information.DataAdded += (sender, args) =>
            {
                var data = ((PSDataCollection<InformationRecord>) sender)[args.Index];
                psui.WriteInformation(data);
            };
            ps.Streams.Progress.DataAdded += (sender, args) =>
            {
                var data = ((PSDataCollection<ProgressRecord>)sender)[args.Index];
                psui.WriteProgress(data.ActivityId, data);
            };
            ps.Streams.Debug.DataAdded += (sender, args) =>
            {
                var data = ((PSDataCollection<DebugRecord>)sender)[args.Index];
                psui.WriteDebugLine(data.Message);
            };
            ps.Streams.Verbose.DataAdded += (sender, args) =>
            {
                var data = ((PSDataCollection<VerboseRecord>)sender)[args.Index];
                psui.WriteVerboseLine(data.Message);
            };
            ps.Streams.Warning.DataAdded += (sender, args) =>
            {
                var data = ((PSDataCollection<WarningRecord>)sender)[args.Index];
                psui.WriteWarningLine(data.Message);
            };
            ps.Streams.Error.DataAdded += (sender, args) =>
            {
                var data = ((PSDataCollection<ErrorRecord>)sender)[args.Index];
                psui.WriteErrorLine(data.ErrorDetails?.Message ?? "Error Occured:");
                psui.WriteErrorLine("    Id: " + data.FullyQualifiedErrorId);
                psui.WriteErrorLine("    Category: " + data.CategoryInfo.Category);
                psui.WriteErrorLine("    Exception:");
                psui.WriteErrorLine(data.Exception.Print(2));

                if(!string.IsNullOrWhiteSpace(data.ErrorDetails?.RecommendedAction))
                    psui.WriteErrorLine("    Recommendation" + data.ErrorDetails?.RecommendedAction);
            };
            return ps;
        }
    }
}

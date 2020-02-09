using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.CompilerServices;
using System.Text;

namespace Unimpressive.Poweshell
{
    public static class CmdletExtensions
    {
        /// <summary>
        /// Resolve potentially relative path to the current Powershell session's working directory
        /// </summary>
        /// <param name="cmdlet"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string ResolvePath(this PSCmdlet cmdlet, string filename) =>
            Path.IsPathRooted(filename) ? filename :
                Path.GetFullPath(
                    Path.Combine(cmdlet.SessionState.Path.CurrentFileSystemLocation.Path, filename)
                );

        /// <summary>
        /// Shortcut to running a script in the context of a Cmdlet
        /// </summary>
        /// <param name="cmdlet"></param>
        /// <param name="script"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Collection<PSObject> Run(this PSCmdlet cmdlet, string script, params object[] arguments) =>
            cmdlet.SessionState.InvokeCommand.InvokeScript(script, false, PipelineResultTypes.Output, null, arguments);

        /// <summary>
        /// Import and get the information of a Powershell module
        /// </summary>
        /// <param name="cmdlet"></param>
        /// <param name="filename"></param>
        /// <param name="moduleName">Optionally provide a module name in the very unlikely case when it cannot be set from the filename</param>
        /// <returns></returns>
        public static PSModuleInfo ImportModule(this PSCmdlet cmdlet, string filename, string moduleName = null)
        {
            var mf = cmdlet.ResolvePath(filename);
            if (!File.Exists(mf))
                throw new FileNotFoundException($"Module file {mf} was not found.");

            moduleName ??= Path.GetFileNameWithoutExtension(mf);
            var currdir = cmdlet.SessionState.Path.CurrentFileSystemLocation.Path;

            cmdlet.Run($"Set-Location {Path.GetDirectoryName(mf)}");
            cmdlet.Run($"Import-Module {mf}");

            PSModuleInfo res = null;
            foreach (var psObject in cmdlet.Run($"Get-Module {moduleName}"))
            {
                res = psObject.BaseObject as PSModuleInfo;
            }

            cmdlet.Run($"Set-Location {currdir}");

            if (res != null) return res;
            throw new ItemNotFoundException($"Get-Module operation for {moduleName} yielded no results.");
        }

        /// <summary>
        /// Remove a module
        /// </summary>
        /// <param name="cmdlet"></param>
        /// <param name="moduleName"></param>
        public static void RemoveModule(this PSCmdlet cmdlet, string moduleName)
        {
            cmdlet.Run($"Remove-Module {moduleName}");
        }

        /// <summary>
        /// Shortcut to <see cref="PSCmdlet.WriteError"/>
        /// </summary>
        /// <param name="cmdlet"></param>
        /// <param name="error"></param>
        /// <param name="errorCat"></param>
        /// <param name="targetObj"></param>
        public static void WriteError(this PSCmdlet cmdlet, string error, ErrorCategory errorCat = ErrorCategory.InvalidOperation, object targetObj = null)
        {
            targetObj ??= cmdlet;
            var errrec = new ErrorRecord(new Exception(error), "SimpleStringError", errorCat, targetObj);
            cmdlet.WriteError(errrec);
        }
    }
}

using Humanizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using System.Runtime.CompilerServices;
using System.Text;

namespace Unimpressive.Poweshell
{
    public abstract class PSCmdletExtra : PSCmdlet
    {
        [Parameter(
            HelpMessage = "This Cmdlet will not ask user input and will run with default choices."
        )]
        public SwitchParameter Unattended { get; set; }
    }

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

        /// <summary>
        /// Use when the user needs to decide on possible options
        /// It invokes <see cref="OnQuestion"/> where the implementer can either block uppm with user query or use the default value.
        /// </summary>
        /// <param name="question">Question to be asked from user</param>
        /// <param name="caption">Displayed on top of the prompt</param>
        /// <param name="possibilities">If null any input will be accepted. Otherwise input is compared to these possible entries.</param>
        /// <param name="defaultValue">This value is used when user submits an empty input or in a potential unattended mode.</param>
        /// <returns>User answer or default</returns>
        public static string PromptForChoice(
            this PSCmdlet cmdlet,
            string question,
            string caption = "",
            IEnumerable<string> possibilities = null,
            string defaultValue = "")
        {
            if (cmdlet is PSCmdletExtra cmdletex && cmdletex.Unattended.IsPresent)
                return defaultValue;

            if (string.IsNullOrWhiteSpace(caption)) caption = "User input is needed:";

            if(possibilities == null)
            {
                var answer = cmdlet.Host.UI.Prompt(caption, question, new Collection<FieldDescription> {
                    new FieldDescription("Answer")
                    {
                        DefaultValue = new PSObject(defaultValue),
                        IsMandatory = true
                    }
                });
                return answer["Answer"].BaseObject.ToString();
            }
            else
            {
                int defChoiceId = -1;
                int i = 0;
                foreach(var ch in possibilities)
                {
                    if (ch == defaultValue)
                    {
                        defChoiceId = i;
                        break;
                    }
                    i++;
                }
                if (defChoiceId < 0)
                {
                    possibilities = possibilities.Prepend(defaultValue);
                    defChoiceId = 0;
                }

                var choices = new Collection<ChoiceDescription>(possibilities.Select(p => new ChoiceDescription(p)).ToList());
                var answerId = cmdlet.Host.UI.PromptForChoice(caption, question, choices, defChoiceId);
                return choices[answerId].Label;
            }
        }

        /// <summary>
        /// Use when the user needs to decide on possible options
        /// It invokes <see cref="OnQuestion"/> where the implementer can either block uppm with user query or use the default value.
        /// </summary>
        /// <typeparam name="T">Must be an enum</typeparam>
        /// <param name="question">Question to be asked from user</param>
        /// <param name="caption">Displayed on top of the prompt</param>
        /// <param name="possibilities">If null any input will be accepted. Otherwise input is compared to these possible entries.</param>
        /// <param name="defaultValue">This value is used when user submits an empty input or in a potential unattended mode.</param>
        /// <returns>User answer or default</returns>
        public static T PromptForEnum<T>(
            this PSCmdlet cmdlet,
            string question,
            string caption = "",
            IEnumerable<T> possibilities = null,
            T defaultValue = default(T)) where T : struct
        {
            if (typeof(T).IsEnum) throw new ArgumentException($"{typeof(T)} type is not enum.");
            var poss = possibilities == null ? Enum.GetNames(typeof(T)) : possibilities.Select(p => p.ToString());
            var resstr = cmdlet.PromptForChoice(question, caption, poss, defaultValue.ToString());
            return Enum.TryParse<T>(resstr, true, out var res) ? res : defaultValue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace Unimpressive.Poweshell
{
    public static class ModuleInfoExtensions
    {
        public static ScriptBlock GetFunctionFromModule(this PSModuleInfo modinfo, string name)
        {
            if (modinfo.ExportedFunctions.TryGetValue(name, out var func))
            {
                return func.ScriptBlock;
            }
            throw new ItemNotFoundException($"Function {name} in {modinfo.Name} was not found.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Unimpressive.Core
{
    public static class ExceptionExtensions
    {
        public static string Print(this Exception exc, int indent = 0, int maxRecurseDepth = 10, int currRecurseDepth = 0)
        {
            if (exc == null) return "Exception was null";
            var res = $"{"    ".RepeatN(indent)}Message: {exc.Message}" + Environment.NewLine;

            if(!string.IsNullOrWhiteSpace(exc.Source))
                res += $"{"    ".RepeatN(indent)}Source: {exc.Source}" + Environment.NewLine;

            if (!string.IsNullOrWhiteSpace(exc.HelpLink))
                res += $"{"    ".RepeatN(indent)}Help: {exc.HelpLink}" + Environment.NewLine;

            if (exc.StackTrace != null)
            {
                res += $"{"    ".RepeatN(indent)}Stack-trace:" + Environment.NewLine;
                foreach (var stline in exc.StackTrace.Split(Environment.NewLine))
                {
                    res += "    ".RepeatN(indent + 1) + stline + Environment.NewLine;
                }
            }

            if (exc.InnerException != null && currRecurseDepth <= maxRecurseDepth)
            {
                res += $"{"    ".RepeatN(indent)}Inner Exception:" + Environment.NewLine;
                res += exc.InnerException.Print(indent + 1, maxRecurseDepth, currRecurseDepth + 1);
            }

            return res;
        }
    }
}
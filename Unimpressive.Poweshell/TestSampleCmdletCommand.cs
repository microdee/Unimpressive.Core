using System;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Unimpressive.Poweshell
{
    [Cmdlet(VerbsDiagnostic.Test,"SampleCmdlet")]
    [OutputType(typeof(FavoriteStuff))]
    public class TestSampleCmdletCommand : PSCmdlet
    {
        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteObject("Begin!");
            
            var ps = PowerShell.Create(RunspaceMode.CurrentRunspace)
                .WithStreamsOutput(Host.UI);
            ps.AddScript(
@"
""Does plain string work?""
Write-Host ""This is directly written to host""
Read-Host ""Is this real? (Type yes)""
for ($i = 1; $i -le 100; $i++ )
{
    Start-Sleep -Milliseconds 10
    Write-Progress -Activity ""Search in Progress"" -Status ""$i % Complete:"" -PercentComplete $i;
}
Write-Debug ""This is some debug info""
Write-Verbose ""Verbose line written""
Write-Warning ""Something is fishy""
Write-Error ""Shit hit the fan""
", true);
            foreach (var psObject in ps.Invoke())
            {
                WriteObject(psObject.BaseObject);
            }
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {

        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteObject("End!");
        }
    }

    public class FavoriteStuff
    {
        public int FavoriteNumber { get; set; }
        public string FavoritePet { get; set; }
    }
}

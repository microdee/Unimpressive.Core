[string] $moduleVar = "myVar"

function Install {
    param (
        [string] $message
    )
    "yo $message $moduleVar" 
}

Export-ModuleMember -Function Install
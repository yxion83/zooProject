# Make sure we are in an OOP/PC program.
$currentFolder = $PSScriptRoot
$success = $false
if ($currentFolder.ToLower().Contains("first") -or $currentFolder.ToLower().Contains("zoo") -or $currentFolder.ToLower().Contains("date") -or $currentFolder.ToLower().Contains("airline") -or $currentFolder.ToLower().Contains("grade") -or $currentFolder.ToLower().Contains("scratchpad")) {
    # Check for the .sln file.
    $slnFiles = Get-ChildItem -Filter "*.sln"
    if ($slnFiles.Length -ge 1) {
        # We are next to the SLN file. Unblock everything.
        gci -Recurse | Unblock-File
        $success = $true
    }
}

# Print message.
if ($success -eq $true) {
    echo "Your files have been unblocked. The warnings about not being able to load the dlls should disappear. If you continue having issues, please let us know through your f channel."
} else {
    echo "This script unblocks all of the project's files. Please put this script in your solution, next to the .sln file."
}
Read-Host -Prompt "Press enter to exit"
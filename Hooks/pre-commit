# Get the assets directory from git config or default to "Assets"
$assetsDir = git config --get unity3d.assets-dir
if (-not $assetsDir) {
    $assetsDir = "Assets"
}

# Determine the reference to diff against
if (git rev-parse --verify HEAD -ErrorAction SilentlyContinue) {
    $against = "HEAD"
} else {
    # Initial commit: diff against an empty tree object
    $against = "4b825dc642cb6eb9a060e54bf8d69288fbee4904"
}

# Redirect output to stderr
$ErrorActionPreference = "Stop"

# Check for redundant meta files
$addedFiles = git -c diff.renames=false diff --cached --name-only --diff-filter=A -z $against -- $assetsDir
$addedFiles -split "`0" | ForEach-Object {
    $f = $_
    $ext = [System.IO.Path]::GetExtension($f).TrimStart('.')
    $base = [System.IO.Path]::ChangeExtension($f, $null)
    $filename = [System.IO.Path]::GetFileName($f)

    if ($ext -eq "meta") {
        if (-not (git ls-files --cached -- $base)) {
            Write-Host "Error: Redundant meta file."
            Write-Host "Meta file `$f` is added, but `$base` is not in the git index."
            Write-Host "Please add `$base` to git as well."
            exit 1
        }
    } elseif ($filename -match '\.') {
        $p = $f
        while ($p -ne $assetsDir) {
            if (-not (git ls-files --cached -- "$p.meta")) {
                Write-Host "Error: Missing meta file."
                Write-Host "Asset `$f` is added, but `$p.meta` is not in the git index."
                Write-Host "Please add `$p.meta` to git as well."
                exit 1
            }
            $p = [System.IO.Path]::GetDirectoryName($p)
        }
    }
}

# Check for missing meta files
$deletedFiles = git -c diff.renames=false diff --cached --name-only --diff-filter=D -z $against -- $assetsDir
$deletedFiles -split "`0" | ForEach-Object {
    $f = $_
    $ext = [System.IO.Path]::GetExtension($f).TrimStart('.')
    $base = [System.IO.Path]::ChangeExtension($f, $null)

    if ($ext -eq "meta") {
        if (git ls-files --cached -- $base) {
            Write-Host "Error: Missing meta file."
            Write-Host "Meta file `$f` is removed, but `$base` is still in the git index."
            Write-Host "Please revert the meta file or remove the asset file."
            exit 1
        }
    } else {
        $p = $f
        while ($p -ne $assetsDir) {
            if (-not (git ls-files --cached -- $p) -and (git ls-files --cached -- "$p.meta")) {
                Write-Host "Error: Redundant meta file."
                Write-Host "Asset `$f` is removed, but `$p.meta` is still in the git index."
                Write-Host "Please remove `$p.meta` from git as well."
                exit 1
            }
            $p = [System.IO.Path]::GetDirectoryName($p)
        }
    }
}

Write-Host "All checks passed. Proceeding with commit."

$CurrentPath = (Get-Location).path
$ServicesPath = Join-Path (Get-Location).path 'Services'
$SearchService = Read-Host "Please Enter Folder Name to Search"
write-Host "---------------------------------------------" -ForegroundColor Yellow
"`n"

if ([string]::IsNullOrEmpty($SearchService))
	{
		Throw "The services name is required !"
	}

foreach ($Services in (Get-ChildItem $ServicesPath -Directory -Recurse))
{
If(($Services).Name -like "*$SearchService*")
	{

		$BuildService = Join-Path $ServicesPath ($Services).Name 
		Set-Location $BuildService

		Write-Host "Running build script for $Services " -ForegroundColor Green

		dotnet build -c Debug
		dotnet run

		Set-Location $CurrentPath
	}
}

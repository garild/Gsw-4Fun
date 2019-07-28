
param( 
[Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)] 
[string]$Config 
)

$CurrentPath = (Get-Location).path
$ServicesPath = Join-Path (Get-Location).path 'src/Services'

"`n"
Write-Host "........... Build mode set as $Config configuration ..........." -ForegroundColor Green
"`n"

foreach ($Services in (Get-ChildItem $ServicesPath -Directory ))
{
	$BuildService = Join-Path $ServicesPath ($Services).Name 
	Set-Location $BuildService
		
	if (($Services).Name -ne "Published")
	{
	
		Write-Host $BuildService
		"`n"
		Write-Host "........... Running build script for $Services service ..........." -ForegroundColor Green
		"`n"
		
		$PublishPath = Join-Path (Join-Path $CurrentPath Published) ($Services).Name 
		dotnet restore
		dotnet publish -c $Config -o $PublishPath  -v m
				
		Set-Location $CurrentPath
	}
	
		"`n"
		Write-Host "........... Build for $Services service finished ..........." -ForegroundColor Green
		"`n"
}

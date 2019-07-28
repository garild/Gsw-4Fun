$src = Join-Path (Get-Item -Path ".\" -Verbose).FullName 'Published';

Write-Host $src

Get-ChildItem $src -directory | where {$_.PsIsContainer} | Select-Object -Property Name | ForEach-Object {
   
	$cdProjectDir = [string]::Format("cd /d {0}\{1}",$src, $_.Name);
   
	Write-Host $cdProjectDir 

	$params=@("/c"; $cdProjectDir; " && dotnet run"; )
	Start-Process -Verb runas "cmd.exe" $params;
} 


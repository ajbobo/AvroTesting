Write-Host "Generating C# code from .avdl files"

Get-Item .\Avro_Definitions\*.avdl | 
	% { java -jar avro-tools-1.8.1.jar idl2schemata $_ Avro_Generated }

Get-Item .\Avro_Generated\*.avsc |
	%{ C:\Dev\VSProjects\hadoopsdk\Bin\Unsigned\Debug\Microsoft.Hadoop.Avro.Tools\Microsoft.Hadoop.Avro.Tools.exe codegen /i:$_ /o:.\Avro_Generated }
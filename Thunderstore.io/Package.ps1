$ProjectName = "StackFurther"
Copy-Item "..\src\bin\Release\net4.6.1\$ProjectName.dll" -Destination .
Copy-Item ..\README.md
Get-ChildItem -Exclude *.zip,*.ps1 | Compress-Archive -Force -DestinationPath ./$ProjectName.zip

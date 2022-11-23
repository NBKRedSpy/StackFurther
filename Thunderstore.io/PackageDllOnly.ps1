$ProjectName = "StackFurther"
Copy-Item "..\src\bin\Release\net4.6.1\$ProjectName.dll" -Destination .
Compress-Archive -Path "$ProjectName.dll" -Force -DestinationPath ./$ProjectName.zip

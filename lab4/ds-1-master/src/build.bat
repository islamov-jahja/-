@echo off
set version=%1

if defined version (
	cd Frontend
	dotnet publish
	cd ..\BackendApi
	dotnet publish
	cd ..\TextRankCalc
	dotnet publish
	cd ..

	if not exist bouild_versions (
		mkdir bouild_versions
	)

	cd bouild_versions
	if not exist %version% (
		mkdir %version%
		cd %version%
	
		mkdir Frontend
		cd Frontend
		xcopy ..\..\..\Frontend\bin\Debug\netcoreapp2.2\publish /S
		cd ..
		mkdir BackendApi
		cd BackendApi
		xcopy ..\..\..\BackendApi\bin\Debug\netcoreapp2.2\publish /S
		cd ..
		mkdir TextRankCalc
		cd TextRankCalc
		xcopy ..\..\..\TextRankCalc\bin\Debug\netcoreapp2.2\publish /S
		
		cd ..
		echo port_frontend:5001 > config.txt
		echo port_backend:5000 >> config.txt
		echo start dotnet Frontend\Frontend.dll > run.bat
		echo start dotnet BackendApi\BackendApi.dll >> run.bat
		echo start dotnet TextRankCalc\TextRankCalc.dll >> run.bat
		
		echo taskkill /IM dotnet.exe /F > stop.bat
	) else (
		echo This version already exists.
	)
) else ( 
	echo Version are required.
)
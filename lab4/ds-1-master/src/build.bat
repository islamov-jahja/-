@echo off
set version=%1

if defined version (
	cd Frontend
	dotnet publish
	cd ..\BackendApi
	dotnet publish
	cd ..\TextRankCalc
	dotnet publish
	cd ..\VowelConsCounter
	dotnet publish
	cd ..\VowelConsRater
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
		mkdir VowelConsCounter
		cd VowelConsCounter
		xcopy ..\..\..\VowelConsCounter\bin\Debug\netcoreapp2.2\publish /S
		cd ..
		mkdir VowelConsRater
		cd VowelConsRater
		xcopy ..\..\..\VowelConsRater\bin\Debug\netcoreapp2.2\publish /S
		cd ..
        xcopy ..\..\run_vowel_and_rate.bat

		echo VowelConsCounter:3 > config.txt
		echo VowelConsRater:2 >> config.txt

		echo start dotnet Frontend\Frontend.dll > run.bat
		echo start dotnet BackendApi\BackendApi.dll >> run.bat
		echo start dotnet TextRankCalc\TextRankCalc.dll >> run.bat
		echo call run_vowel_and_rate.bat >> run.bat

		echo taskkill /IM dotnet.exe /F > stop.bat
	) else (
		echo This version already exists.
	)
) else ( 
	echo Version are required.
)
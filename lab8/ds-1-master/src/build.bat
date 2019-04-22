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
	cd ..\TextListener
	dotnet publish
	cd ..\TextStatistic
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
		mkdir TextListener
		cd TextListener
		xcopy ..\..\..\TextListener\bin\Debug\netcoreapp2.2\publish /S
		cd ..
		mkdir TextStatistic
		cd TextStatistic
		xcopy ..\..\..\TextStatistic\bin\Debug\netcoreapp2.2\publish /S
		cd ..
        xcopy ..\..\run_vowel_and_rate.bat

		echo VowelConsCounter:3 > config.txt
		echo VowelConsRater:2 >> config.txt

		echo cd Frontend > run.bat
		echo start "Frontend" dotnet Frontend.dll >> run.bat
		echo cd .. >> run.bat
		echo cd BackendApi >> run.bat
		echo start "Backend" dotnet BackendApi.dll >> run.bat
		echo cd .. >> run.bat
		echo cd TextRankCalc >> run.bat
		echo start "TextRankCalc" dotnet TextRankCalc.dll >> run.bat
		echo cd .. >> run.bat
		echo cd TextListener >> run.bat
		echo start "TextListener" dotnet TextListener.dll >> run.bat
		echo cd .. >> run.bat
		echo cd TextStatistic >> run.bat
		echo start "TextStatistic" dotnet TextStatistic.dll >> run.bat
		echo cd .. >> run.bat

		echo call run_vowel_and_rate.bat >> run.bat

		echo taskkill /IM dotnet.exe /F > stop.bat
	) else (
		echo This version already exists.
	)
) else ( 
	echo Version are required.
)
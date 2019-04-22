		cd Frontend > run.bat
		start dotnet Frontend.dll >> run.bat
		cd .. >> run.bat
		d BackendApi >> run.bat
		start dotnet BackendApi.dll >> run.bat
		cd .. >> run.bat
		cd TextRankCalc >> run.bat
		start dotnet TextRankCalc.dll >> run.bat
		cd .. >> run.bat
		cd TextListener >> run.bat
		start dotnet TextListener.dll >> run.bat
		cd ..
		call run_vowel_and_rate.bat
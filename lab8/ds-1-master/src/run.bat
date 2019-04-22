cd Frontend 
start "Frontend" dotnet Frontend.dll 
cd .. 
cd BackendApi 
start "Backend" dotnet BackendApi.dll 
cd .. 
cd TextRankCalc 
start "TextRankCalc" dotnet TextRankCalc.dll 
cd .. 
cd TextListener 
start "TextListener" dotnet TextListener.dll 
cd .. 
cd TextStatistic 
start "TextStatistic" dotnet TextStatistic.dll 
cd .. 
call run_vowel_and_rate.bat 

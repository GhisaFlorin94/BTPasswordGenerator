@ECHO OFF
ECHO Starting Web Api Server...
cd BtPasswordGenerator\bin\Debug\netcoreapp3.1
start BtPasswordGenerator.exe
cd ..\..\..\..
ECHO Starting Angular Web Application...
cd BtWebClient
npm start
cd ..

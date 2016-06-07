call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat"
MSTest /testcontainer:%~dp0\Tests\bin\Debug\tests.dll
timeout /t -1


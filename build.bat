@echo off

SET projectName=SteamLauncher

echo This batch should be run with the Developer Command Line from Visual Studio.
echo Also this batch needs 7z to be in your path.
pause

echo Building %projectName%...
msbuild Build.proj

echo Zipping Folder...
cd bin
7z a -tzip -mx9 -y %projectName%.zip *
cd ..

echo Done!
pause
set CurPath=%~dp0

cd %CurPath%

%CurPath%\scripts\pypy310\pypy %CurPath%\scripts\excel2json_win.py

xcopy %CurPath%\output\config %CurPath%..\..\client\Assets\Resources\Config\ /e /y
xcopy %CurPath%\output\config %CurPath%..\test\Assets\Config\ /e /y

exit
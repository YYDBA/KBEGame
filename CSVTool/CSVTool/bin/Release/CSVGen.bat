@echo off

@for /f "delims=" %%i in ('dir %cd%\CSVDir /a-d /b /s') do (call CSVTool.exe %%i)
pause
@echo off
REM Esse batch chama o seu script PowerShell com as aspas corretas
powershell -NoProfile -ExecutionPolicy Bypass -File "%userprofile%\source\repos\device-control\tests\TelnetTester\helper\telnet-mock.ps1" -Verbose
CD %~dp0
ECHO ON
XCOPY /s ..\DemoBoard\GUI\bin\Release\*.* ..\DemoBoard\DemoBoardDistribution\DemoBoard
REM REM REM COPY ..\DemoBoard\Setup\*.* ..\DemoBoard\DemoBoardDistribution\Setup
PAUSE
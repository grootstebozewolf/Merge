@echo off
ss status -o- -y%1 -r %3 %4 %5 %6 %7 %8 %9
if errorlevel 100 goto BAD_FAILURE
if errorlevel 1 goto CHECKED_OUT

rem Exit code 0, no files were checked out
echo No specified files are checked out: checking all out.
ss get "-cGet files automatically for new installer" -y%1 -w -r %3 %4 %5 %6 %7 %8 %9
goto END

rem Exit code 1, something is checked out
:CHECKED_OUT
echo One or more files is checked out: quitting without checking anything out.
goto END

rem Exit code 100, something went wrong.
:BAD_FAILURE
echo Visual SourceSafe could not run successfully

:END


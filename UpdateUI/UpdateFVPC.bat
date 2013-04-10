rem echo Make a backup of the old dir
rem c:
rem cd C:\Temp\
rem mkdir %4
rem cd C:\Temp\%4
rem xcopy %1\* /s /e /h
echo Copy the old dir to output
xcopy %1 %3 /s /e /h /y
echo Delete the FVI dir and Transport Dir in the output
rmdir "%3\FVI\COPY\Program files\FVDnet\" /s /q
rmdir "%3\FVI\COPY\Program files\Transporter\" /s /q
echo Copy the new dir to output
xcopy %2 %3 /s /e /h /y
echo Copy screen.xml and agenda.xml in the output
copy "%1\FVI\COPY\Program files\fvdnet\screen.xml" "%3\FVI\COPY\Program files\FVDnet\screen.xml"
copy "%1\FVI\COPY\Program files\fvdnet\agenda.xml" "%3\FVI\COPY\Program files\FVDnet\agenda.xml"
copy "%1\FVI\COPY\Program files\fvdnet\logo.jpg" "%3\FVI\COPY\Program files\FVDnet\logo.jpg"
copy "%1\FVI\COPY\Program files\fvdnet\logo.jpg" "%3\FVI\COPY\Program files\FVDnet\laptop.udl"
copy "%1\FVI\FVInstallerDesktop.ini" "%3\FVI\FVInstallerDesktop.ini"

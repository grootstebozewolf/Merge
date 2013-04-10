echo old %1
echo new %2
echo out %3
echo date %4
echo upgradeInstaller %5
echo Copy the old dir to output

xcopy "%1" "%3" /s /e /h /y
echo Delete the FVDnet dir and Transport Dir in the output and the FVResumeCE.dll
rmdir "%3\FVI\COPY\Program files\FVDnet\" /s /q
rmdir "%3\FVI\COPY\Program files\Transporter\" /s /q
del "%3\FVI\COPY\Windows\FVResumeCE.dll"
echo Explicitly copy the FVResumeCE.dll
copy "%2\FVI\COPY\Windows\FVResumeCE.dll" "%3\FVI\COPY\Windows\FVResumeCE.dll"
if not .%5==.True goto else
rem Remove old installer
del "%3\FVI\FVInstallerCE.exe"
del "%3\FVI\FVInstallerCE.ini"
del "%3\FVI\System.Data.Common.dll"
del "%3\FVI\System.Data.Common.xml"
del "%3\2577\autorun.xml"
copy "%2\2577\autorun.xml" "%3\2577\autorun.xml"
goto endif
:else
copy "%1\FVI\FVInstallerCE.ini" "%3\FVI\FVInstallerCE.ini"
:endif
echo Copy the new dir to output
xcopy "%2" "%3" /s /e /h /y

echo Copy screen.xml and agenda.xml in the output
copy "%1\FVI\COPY\Program files\FVDnet\screen.xml" "%3\FVI\COPY\Program files\FVDnet\screen.xml"
copy "%1\FVI\COPY\Program files\FVDnet\agenda.xml" "%3\FVI\COPY\Program files\FVDnet\agenda.xml"
copy "%1\FVI\COPY\Program files\FVDnet\logo.jpg" "%3\FVI\COPY\Program files\FVDnet\logo.jpg"
copy "%1\FVI\COPY\Program files\FVDnet\Tensing_bg.jpg" "%3\FVI\COPY\Program files\FVDnet\Tensing_bg.jpg"
copy "%1\FVI\COPY\Program files\FVDnet\multiLang.xml" "%3\FVI\COPY\Program files\FVDnet\multiLang.xml"
copy "%1\FVI\TensingInstall.xml" "%3\FVI\TensingInstall.xml"
del "%3\FVI\FVUpdateCE.ini"
del "%3\FVI\TensingUpdate.xml"
del "%3\FVI\CAB\sqlce.ppc3.arm.CAB"
del "%3\FVI\COPY\Program files\FVDnet\fieldservice.sdf"
echo output %1
echo createSDF %2
echo Copy the screen to fvdnet

xcopy "%1\FVI\COPY\Program files\FVDnet\Desktop" "%1\FVI\COPY\Program files\FVDnet" /s /e /h /y
echo Delete the Desktop dir
rmdir "%1\FVI\COPY\Program files\FVDnet\Desktop" /S /Q
if not .%2==.True goto endif
echo Move database to Backup
move "%1\FVI\COPY\Program files\FVDnet\FieldService.sdf" "%1\Backup\FieldService.sdf"
:endif



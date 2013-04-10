echo output %1
echo createSDF %2
echo Copy the screen to fvdnet

xcopy "%1\FVI\COPY\Program files\FVDnet\PocketPC" "%1\FVI\COPY\Program files\FVDnet" /s /e /h /y
echo Delete the PocketPC dir
rmdir "%1\FVI\COPY\Program files\FVDnet\PocketPC" /S /Q
if not .%2==.True goto endif
echo Delete import.txt
del "%1\FVI\COPY\Program files\FVDnet\import.txt"
echo Move database to Backup
move "%1\FVI\COPY\Program files\FVDnet\FieldService.sdf" "%1\Backup\FieldService.sdf"
:endif



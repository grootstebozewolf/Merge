echo old %1
echo new %2
echo out %3
echo date %4
echo Copy the screen to fvdnet
xcopy "%3\FVI\COPY\Program files\FVDnet\PocketPC" "%3\FVI\COPY\Program files\FVDnet" /s /e /h /y
echo Delete the PocketPC dir
rmdir "%3\FVI\COPY\Program files\PocketPC\" /s /q

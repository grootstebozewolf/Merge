goto %1
:TomTom5
xcopy "%2\Tree\Mobile\Client PocketPC\Navigation\TomTom\TomTom5" "%3\FVI\COPY\Program files\FVDnet\" /s /e /h /y
goto end
:TomTom6
xcopy "%2\Tree\Mobile\Client PocketPC\Navigation\TomTom\TomTom6" "%3\FVI\COPY\Program files\FVDnet\" /s /e /h /y
goto end
:CoPilot7
xcopy "%2\Tree\Mobile\Client PocketPC\Navigation\CoPilot7" "%3\FVI\COPY\Program files\FVDnet\" /s /e /h /y
goto end
:CoPilot8
xcopy "%2\Tree\Mobile\Client PocketPC\Navigation\CoPilot8" "%3\FVI\COPY\Program files\FVDnet\" /s /e /h /y
goto end
:GPS
xcopy "%2\Tree\Mobile\Client PocketPC\Navigation\GPS" "%3\FVI\COPY\Program files\FVDnet\" /s /e /h /y
copy "%4\FVI\COPY\Program files\FVDnet\Tensing.Navigation.Wrappers.GPS.Config" "%3\FVI\COPY\Program files\FVDnet\"
goto end
:end
::This batch file was writing by OPERATOR_555
::NOT FOR PUBLIC. ONLY FOR PRIVATE PURPOSES

@ECHO OFF
::echo Coping...
::adb push ./"%~n1".apk /data/local/tmp/"%~n1".apk

echo Installing...
adb install -r ./"%~n1".apk

echo END
pause

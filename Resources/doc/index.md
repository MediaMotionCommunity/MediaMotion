Documentation
=============

Compilation
------------

Step 1 "Compilation of Wrapped Device":
Compilation of wrapper device needed.
Mediamotion's wrapper are in folder ./WrapperDevices/

For this guide, we use LeapMotion wrapper in folder ./WrapperDevices/MediaMotion.Motion.LeapMotion/

Windows:
- Open solution with Visual Studio (vs2013+)
- Start compilation

All file are automatically copied in project directory.

Others:
- Open solution with Xamarin Studio
- Start Compilation
- Copy the listed files in project directory

$(ConfigurationName) is Debug or Release in according of compilation mode.

if you are on 32bits plateforme:
#32Bits Start
$(GitDir)/Solution Items/x86/Leapd.dll			-> $(GitDir)/MediaMotion/
$(GitDir)/Solution Items/x86/Leapd.lib			-> $(GitDir)/MediaMotion/
$(GitDir)/Solution Items/x86/Leap.dll			-> $(GitDir)/MediaMotion/
$(GitDir)/Solution Items/x86/Leap.lib			->$(GitDir)/MediaMotion/
$(GitDir)/Solution Items/x86/LeapCSharp.dll		-> $(GitDir)/MediaMotion/

Only if you want run project testing
$(GitDir)/Solution Items/x86/Leapd.dll			-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
$(GitDir)/Solution Items/x86/Leapd.lib			-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
$(GitDir)/Solution Items/x86/Leap.dll			-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
$(GitDir)/Solution Items/x86/Leap.lib			-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
$(GitDir)/Solution Items/x86/LeapCSharp.dll		-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
#32Bits End

if you are on 64bits plateforme:
#64Bits Start
$(GitDir)/Solution Items/x64/Leapd.dll			-> $(GitDir)/MediaMotion/
$(GitDir)/Solution Items/x64/Leapd.lib			-> $(GitDir)/MediaMotion/
$(GitDir)/Solution Items/x64/Leap.dll			-> $(GitDir)/MediaMotion/
$(GitDir)/Solution Items/x64/Leap.lib			-> $(GitDir)/MediaMotion/
$(GitDir)/Solution Items/x64/LeapCSharp.dll		-> $(GitDir)/MediaMotion/ 

Only if you want run project testing
$(GitDir)/Solution Items/x64/Leapd.dll			-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
$(GitDir)/Solution Items/x64/Leapd.lib			-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
$(GitDir)/Solution Items/x64/Leap.dll			-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
$(GitDir)/Solution Items/x64/Leap.lib			-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
$(GitDir)/Solution Items/x64/LeapCSharp.dll		-> $(GitDir)/WrapperDevices/MediaMotion.Motion.LeapMotion/MediaMotion.Motion.LeapMotion.Testing/bin/$(ConfigurationName)
#64Bits Start

For all plateforme:
$(GitDir)/Solution Items/LeapCSharp.NET3.5.dll	-> "$(GitDir)/MediaMotion/WrapperDevicesLibraries/
MediaMotion.Motion.LeapMotion.dll 				-> $(GitDir)/MediaMotion/WrapperDevicesLibraries/

AFT
===

Anti-Forensic Toolkit is a countermeasure application used for encrypted systems.

Anti-Forensic Toolkit is a Windows application used for protecting computers utilizing encryption.
The program is inspired by panic_bcast written by qnrq and is fully compatible with it.
First it was a complete clone, acting only like a Windows version of it, but has now expanded so far off 
that it now can protect against intruders without anyone manually have to set of a panic signal.

It works as a Dead-Mans-Switch with many different features, using (any) USB device as a "key".
This means that if you leave your computer unmonitored (You shouldn't) and still want some sort of 
protection except for locking your computer (Which you should), you can use this.

AFT as well as panic_bcast uses UDP broadcast to send panic signals, but with AFT you can also specify hosts, this means that you can send panic signals to hosts outside of your LAN.
As with panic_bcast it starts a webserver that listens to a specified port, 8080 as default, if accessing 127.0.0.1:8080/panic you will cause a panic.

You can specify what happens when a panic occurs, such as unmount of truecrypt encrypted volumes, shut down the computer and send panic signals to the hosts of course.

It has USB and AC protection, this means that if any unrecognized USB device is plugged or the power chord is unplugged during the DMS, it will cause a panic.

This project is freely distributed and Open source!
So modify it, make it better and redistribute it as you wish!
I will try to perform continous updates to improve it when I have the time :)

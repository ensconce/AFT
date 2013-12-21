Anti-Forensic Toolkit
===

AFT is a countermeasure application used for encrypted systems.

AFT is a simple Windows application suitable for those who have sensitive data to protect.
The program is inspired by panic_bcast written by qnrq and is fully compatible with it.
First it was a complete clone, acting only like a Windows version of it, but has now expanded so far off 
that it now can protect against intruders without anyone manually have to set of a panic signal.

Security functions 
* USB protection - Panics if unrecognized or defined USB devices are plugged in (Rubber ducky anyone?)
* AC protection - Panics if there is a power change, ie someone removes the power chord from your laptop.
* Network protection - Panics if the network becomes unreachable.
* UDP/HTTP listeners - Listens for signals from other computers running panic_bcast or AFT.

Panic events
* Unmount TrueCrypt partitions
* Shutdown system
* Kill processes
* Send panic signals to hosts or Broadcast address

General settings
* Testing allows you to check if everything is working as expected.
* Check if hosts are alive.
* Send configurations to hosts
* Enable remote DMS
* Remote unmount of encrypted partitions
* Authentication for the communication.

This project is freely distributed and Open source!
So modify it, make it better and redistribute it as you wish!
I will try to perform continous updates to improve it when I have the time :)

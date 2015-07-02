Glossary
========

Core
----
> The Core is all the class, scripts and services in the namespace [MediaMotion.Core](http://www.google.com/).
> Services provide by the Core handle the low level operation (Wrapper/Module Loading, Service Resolver, FileSystem...). All of these Services can be access by the static class [MediaMotion.Core.MediaMotionCore](http://www.google.com/).
> The Core is the only part of the program shared by all modules, in order to limit and avoid critical maintainability issue the Coding Convention for this part is really strict.

Input Wrapper
-------------
> The Input Wrapper is the shared library which wrap an user input device (such as keyboard, [LeapMotion Controller](https://www.leapmotion.com/), [Kinect](https://www.microsoft.com/en-us/kinectforwindows/)...) to use it as an user input by the Core.
> The default Input Wrapper is the LeapMotionWrapper which use a [LeapMotion Controller](https://www.leapmotion.com/) as an user input.

Module
------
> A Module a set of assets, scripts and class that handle a specific type of Element and provide an environment adapted to it.
> To summarize a Module is a [Unity](https://unity3d.com/) scene with a specific functionality (such as Video Player, Book Reader, Folder Explorer...)
> A Module can have its own Services or overload existing Services (see [Overload an existing Service](../advancedModule/overloadService.md))

Script
------
> A script is a class which extends (directly or indirectly) UnityEngine.MonoBehaviour.
> This class is used to add functionality to a simple UnityEngine.GameObject.
> An abstract is provide by the Core to handle some functionalities (such as DI (Dependency Injection))

Service
-------
> A Service is a piece of repositionable code that provide a specific functionality.
> The Core provide a set of Services to handle low level operation that can be use by all the Module.

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*

Configuration
=============

The configuration of a module must be done in the module definition class (see [Create a new Module](newModule.md)). The interface IModule and the abstract AModule that implement this interface describe all the configuration that can be made in a module.

The Priority attribute
----------------------
    int Priority { get; }
The Priority attribute contains a value used by the Module Manager to determine the order of all Module. If this value is hight the module is check in the beginning, and vice versa. (see [Overload a Module](overloadModule.md) for more detail)

The Name attribute
------------------
    string Name { get; }
The Name attribute is simply the name of the Module. It is only used for debug or log feature and does not have a real interest but it is recommended to fill it.

The Scene attribute
-------------------
    string Scene { get; }
The Scene attribute contains the name of the Unity Scene that will be used by the module. This attribute is mandatory and should be fill during the creation of the module (see [Create a new Module](newModule.md) for more detail)

The Description attribute
-------------------------
    string Description { get; }
As the Name attribute, the Description attribute does not have a real interest. It is only used for debug feature.

The SupportReload attribute
---------------------------
    bool SupportReload { get; }
The SupportReload attribute is a boolean value that indicate if the module support the Reload method or not. This method is used by the ModuleManager to load a file if the module is already opened (does not reload the unity scene). If the module does not support this method the scene will be reload and all existing elements will be deleted.

The SupportBackground attribute
-------------------------------
    bool SupportBackground { get; }
The SupportBackground attribute is a boolean value that indicate if the module should be run in background if we leave it or should be closed (i.e.: the music viewer module continue to play the playlist even if the module is left).

The SupportedExtensions attribute
---------------------------------
    string[] SupportedExtensions { get; }
The SupportedExtensions attribute is an array of string values that contains all the extension that the module support. This attribute is used by the abstract AModule to perform a basic Support test and also by other script (such as Slideshow) to automatically filter and/or configure the type of file that can be used. This value is not required but should be fill if the module support specific file extension (such as pdf, mp3, avi...).

The SupportedAction attribute
-----------------------------
    ActionType[] SupportedAction { get; }
The SupportedAction attribute define the user input action that the module support. All action not supported by the module will automatically be disable in the input wrapper for optimisation. Due to this optimisation this attribute is mandatory if the module use some user input action.

The Parameters attribute
------------------------
    IElement[] Parameters { get; }
The Parameters attribute contains the elements currently opened in the module. This attribute must be updated by Load and, if supported, Reload method. This attribute is used by script to retrieve all the information they need.

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*

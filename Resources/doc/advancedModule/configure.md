\> [Documentation](../index.md) \> [Advanced Module](index.md) \> Creation and Configuration

----------

Configuration
=============

The configuration of a module must be done in the module definition class (see [Create a new Module](newModule.md)). The interface IModule and the abstract AModule that implement this interface describe all the configuration that can be made in a module.

The Priority property
---------------------
```csharp
int Priority { get; }
```
The Priority property contains a value used by the Module Manager to determine the order of all Modules. If this value is high the module is checked in the beginning, and vice versa. (see [Overload a Module](overloadModule.md) for more details)

The Name property
-----------------
```csharp
string Name { get; }
```
The Name property is simply the name of the Module. It is only used for the debugging or logging feature and does not have a real interest but it is recommended to fill it.

The Scene property
------------------
```csharp
string Scene { get; }
```
The Scene property contains the name of the Unity Scene that will be used by the module. This property is mandatory and should be filled during the creation of the module (see [Create a new Module](newModule.md) for more details)

The Description property
------------------------
```csharp
string Description { get; }
```
As the Name property, the Description property does not have a real interest. It is only used for debugging.

The SupportReload property
--------------------------
```csharp
bool SupportReload { get; }
```
The SupportReload property is a boolean value that indicates if the module supports the Reload method or not. This method is used by the ModuleManager to load a file if the module is already opened (does not reload the unity scene). If the module does not support this method the scene will be reloaded and all existing elements will be deleted.

The SupportBackground property
------------------------------
```csharp
bool SupportBackground { get; }
```
The SupportBackground property is a boolean value that indicates if the module should be run in the background if we leave it or should be closed (i.e.: the music viewer module continues to play the playlist even if the module is left).

The SupportedExtensions property
--------------------------------
```csharp
string[] SupportedExtensions { get; }
```
The SupportedExtensions property is an array of string values that contains all the extensions that the module supports. This property is used by the abstract AModule to perform a basic Support test. Also, other scripts use it (such as Slideshow) to automatically filter and/or configure the type of file that can be used. This value is not required but should be filled if the module supports specific file extensions (such as pdf, mp3, avi...).

The SupportedAction property
----------------------------
```csharp
ActionType[] SupportedAction { get; }
```
The SupportedAction property defines the user input action that the module supports. All actions not supported by the module will automatically be disabled in the input wrapper for optimisation. Due to this optimisation this property is mandatory if the module uses some user input action.

The Parameters property
-----------------------
```csharp
IElement[] Parameters { get; }
```
The Parameters property contains the elements currently opened in the module. This property must be updated by the Load and, if supported, Reload method. This property is used by scripts to retrieve all the information they need.

----------

[:arrow_backward: Enable a Module in the Core](enableModule.md) --- [:arrow_up_small: Advanced Module](index.md) --- [:arrow_forward: Use a global (Core) Service](useGlobalService.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*

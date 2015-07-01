MediaMotion
===========

Development Environment
-----------------------
This part presents the minimal requirements to use and/or develop the project and a simple example of development environment setup.

 1. [Requirements](http://www.google.com/)
 2. [Setup](http://www.google.com/)

*__Notice:__ the setup above is offered as a hint to start quickly - each developer is free to choose their own tools.*

Project Architecture
--------------------
This part is a global view of the project, its architecture, its mains components and services.

 1. [Input and LeapMotion Wrapper](http://www.google.com/)
 2. [Core and Global Services](http://www.google.com/)
 3. [DI (Dependency Injection)](http://www.google.com/)
 4. [Module](http://www.google.com/)

[Input Wrapper API](inputWrapperAPI/index.md)
-----------------
This part details the Input Wrapper API and gave some important informations about the loading process.

 1. [API presentation](inputWrapperAPI/presentation.md)
 2. [Wrapper Loading](inputWrapperAPI/loading.md)
 3. [How to use LeapMotionWrapper](inputWrapperAPI/howToLeapMotion.md)

[Advanced Module](advancedModule/index.md)
--------------------
This part details a list of tasks and functionalities commonly used in the development process of a Module.

[__Creation and Configuration__](advancedModule/CreationAndConfiguration.md)

 1. [Create a new Module](advancedModule/newModule.md)
 2. [Enable a Module in the Core](advancedModule/enableModule.md)
 3. [Configuration](advancedModule/configure.md)

[__Service Creation and Overload__](advancedModule/ServiceCreationAndOverload.md)

 1. [Use a global (Core) Service](advancedModule/useGlobalService.md)
 2. [Configuration of Module's DI (Dependency Injection)](advancedModule/DI.md)
 3. [Define its own parameters](advancedModule/defineParameters.md)
 4. [Create a new Module's Service](advancedModule/newService.md)
 5. [Overload an existing Service](advancedModule/overloadService.md)

[__Core Interaction__](advancedModule/CoreInteraction.md)

 1. [Auto-Loading](advancedModule/autoloading.md)
 2. [Overload a Module](advancedModule/overloadModule.md)
 3. [Customize Element Entity](advancedModule/customizeEntity.md)
 4. [Customize Element GameObject](advancedModule/customizeGameObject.md)
 5. [Element preview (advanced personalisation)](advancedModule/elementPreview.md)

[Coding Convention](codingConvention/index.md)
----------------
This part presents the set of Coding Convention that need to be respected in order to have a browsable and readable source code.

 1. [Global Coding Convention](codingConvention/global.md)
 2. [Core Coding Convention](codingConvention/core.md)
 3. [Lighten Coding Convention (for Pull Request)](codingConvention/lighten.md)

*__Notice:__ some of those rules can be perceived as painful and useless, a quick description explaining why those rules are necessary is provided for each of them. Fortunately the majority of them aren't required by the Lighten Convention use for Pull Request.*

[Glossary](glossary/index.md)
----------------------------------

----------
*__Notice:__ The documentation above is available offline in [PDF format](doc.pdf).*
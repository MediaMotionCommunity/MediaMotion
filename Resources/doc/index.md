MediaMotion
===========

[Development Environment](developmentEnvironment/index.md)
----------------------------------------------------------
This part presents the minimal requirements to use and/or develop the project and a simple example of development environment setup.

 1. [Requirements](developmentEnvironment/requirements.md)
 2. [Setup](developmentEnvironment/setup.md)

*__Notice:__ the setup above is offered as a hint to start quickly - each developer is free to choose their own tools.*

[Project Architecture](projectArchitecture/index.md)
----------------------------------------------------
This part is a global view of the project, its architecture, its mains components and services.

 1. [Input and LeapMotion Wrapper](projectArchitecture/inputWrapper.md)
 2. [Core and Global Services](projectArchitecture/coreServices.md)
 3. [DI (Dependency Injection)](projectArchitecture/DI.md)
 4. [Modules](projectArchitecture/modules.md)

[Input Wrapper API](inputWrapperAPI/index.md)
---------------------------------------------
This part details the Input Wrapper API and gave some important informations about the loading process.

 1. [API presentation](inputWrapperAPI/presentation.md)
 2. [Wrapper Loading](inputWrapperAPI/loading.md)
 3. [How to use LeapMotionWrapper](inputWrapperAPI/howToLeapMotion.md)

[Advanced Module](advancedModule/index.md)
------------------------------------------
This part details a list of tasks and functionalities commonly used in the development process of a Module.

__Creation and Configuration__

 1. [Create a new Module](advancedModule/newModule.md)
 2. [Enable a Module in the Core](advancedModule/enableModule.md)
 3. [Configuration](advancedModule/configure.md)

__Service Creation and Overload__

 1. [Use a global (Core) Service](advancedModule/useGlobalService.md)
 2. [Define/Override parameters](advancedModule/defineParameters.md)
 3. [Create/Overload services](advancedModule/createService.md)

__Core Interaction__

 1. [Auto-Loading](advancedModule/autoloading.md)
 2. [Overload a Module](advancedModule/overloadModule.md)
 3. [Customize Element Entity](advancedModule/customizeEntity.md)
 4. [Customize Element GameObject](advancedModule/customizeGameObject.md)
 5. [Element preview (advanced personalisation)](advancedModule/elementPreview.md)

[Coding Convention](codingConvention/index.md)
----------------------------------------------
This part presents the set of Coding Convention that need to be respected in order to have a browsable and readable source code.

 1. [Global Coding Convention](codingConvention/global.md)
 2. [Core Coding Convention](codingConvention/core.md)
 3. [Lighten Coding Convention (for Pull Request)](codingConvention/lighten.md)

*__Notice:__ some of those rules can be perceived as painful and useless, a quick description explaining why those rules are necessary is provided for each of them. Fortunately the majority of them aren't required by the Lighten Convention use for Pull Request.*

[Glossary](glossary/index.md)
-----------------------------

----------
*__Notice:__ The documentation above is available offline in [PDF format](doc.pdf).*
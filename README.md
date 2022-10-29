# RONIN  

Copyright (C)2004-2022 Michael G. Brehm    
[MIT LICENSE](https://opensource.org/licenses/MIT)   
   
## BUILD ENVIRONMENT
**REQUIRED COMPONENTS**   
* __Visual Studio 2022__ (https://visualstudio.microsoft.com/vs/community/) with:  
   * .NET Framework 4.8 Targeting Pack 
   * Windows 10 SDK (any version)
   * C++/CLI tools
   * C++ 2022 Redistributable MSMs
<!-- -->
* __Wix Toolset Build Tools v3.11.2__ (https://wixtoolset.org/releases/)   
* __Wix Toolset Visual Studio 2022 Extension__ (https://wixtoolset.org/releases/)   
   
## BUILD
**INITIALIZE SOURCE TREE AND DEPENDENCIES**
* Open "Developer Command Prompt for VS2022"   
```
git clone https://github.com/zukisoft/ronin
cd ronin
git submodule update --init
```
   
**BUILD TARGET PACKAGE(S)**   
* Open "Developer Command Prompt for VS2022"   
```
cd ronin
msbuild msbuild.proj
```
   
Output .MSI installer package(s) will be generated in the __out__ folder.   

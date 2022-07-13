# Development

If you want to build your own solution, for specific needs, or check/debug the operations, for certain legal obligations, or quite simply to carry out your evolutions.
We have some recommendations to give you in order to get there quickly.

## Custom development

###### Required software 

 - Visual Studio 2015+

 - Windows Common Platform 

    - We recommend that you do not develop on the ADFS server, to do this copy the DLLs located in C:\Windows\ADFS on your development machine. To debug, install the Remote Debugger on each ADFS server 

 - Tools for windows C++ version 140 

 - .NET framework 3.5 installed for Wix

 - WixToolset: https://wixtoolset.org/?utm_source=getvssdk&utm_medium=referral

 - WixToolset VS extension: https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension

   

## Building solution

* Sign projects with your pfx certificate (required)

- in DataTypes project generate new pfx file in VS with name Neos.IdentityServer (just make sure strong name is checked and password is set)
- copy the same file to all projects which have it added in solution and missing (or browse and reselect the same file from DataTypes project)
  (in case of problems with signing see thread there: https://stackoverflow.com/questions/2815366/cannot-import-the-keyfile-blah-pfx-error-the-keyfile-may-be-password-protec )

* Incorrect friend assemblies - value in "InternalsVisibleTo": 
  https://docs.microsoft.com/en-us/dotnet/standard/assembly/create-signed-friend
  Steps:
  
  * in VS developer command line go to folder with pfx file (in DataTypes project) and execute two commands:
    `sn -p Neos.IdentityServer.pfx Neos.IdentityServer.publickey`
    then
    `sn -tp Neos.IdentityServer.publickey`
  * copy both PublicKey and PublicKeyToken to notepad (these are for generated pfx certificate, important: PublicKey should be in one line!)
  * change in all projects in properties\AssemblyInfo.cs value in PublicKey:
    `InternalsVisibleTo("name, PublicKey=0024000...");`
    to PublicKey generated from pfx certificate
* change in all projects and all files every hardcoded `PublicKeyToken=175aa5ee756d2aa2` to PublicKeyToken generated from pfx certificate


# Installation
## Prerequisites

#### Installation operations

- must be run by a user who have **Local Administration** rights on each Adfs server.

  

#### Configuration operations

- must be run by a user who belongs to the **Local Administrators** or belongs to the **Delegated administration group** , depending of your ADFS configuration. 

- **Local System Administration** must be enabled on your ADFS configuration (can be changed with extended configuration tasks). 

- **ADFS 2012 R2** must be run with a user who is in the **Local Administrators group** of each ADFS server.

 

#### .Net Framework

- The version of the .Net framework installed on the Adfs servers as well as the proxies must be version **4.7.2** or up with the latest updates.
The biometric provider requires this version for the use of **CNG** encryption functionalities.



#### ADDS Configuration for MFA users profiles

- **Adfs service account** used to access ADDS forests, must have **read/write** rights on all users properties (or ensure correct access to the properties described in ADDS configuration) on all forests.
- **Adfs service account** , is a "**Domain Administrator**" (not recommended)
- **Adfs service account** , is a "**Local Administrator**" (not recommended)

+ **Adfs service account** is member of the "**Domain Account Operators**" group in all forests (suitable), this account cannot create or write attributes of privileged accounts like domain administrators 
+ **Delegated Administration Group members** used to access ADDS forests, must have **read/write** rights on all users properties (or ensure correct access to the properties described in ADDS configuration) on all forests.
+ **Local Administrators Group members** (not local accounts) used to access ADDS forests, must have **read/write** rights on all users properties (or ensure correct access to the properties described in ADDS configuration) on all forests.
+ ADDS Schema must be conform to the Adfs prerequisites (FL 2003)

Or

+ **Adfs service account** is a "**Standard User**" (recommended), yous must set privileged account credentials in Security management (ADDS Data Access User).

+ **ADDS Data Access User** used to access ADDS forests, must have **read/write** rights on all users properties (or ensure correct access to the properties described in ADDS configuration) on all forests.

+ **ADDS Data Access User** service account , is a "**Domain Administrator**" (not recommended)

+ **ADDS Data Access User**  is member of the "**Domain Account Operators**" group in all forests (suitable), this account cannot create or write attributes of privileged accounts like domain administrators

+ **ADDS Data Access User**  is required to perform PowerShell Remoting on Cmdlets that support it.

+ **Delegated Administration Group members** used to access ADDS forests, can be "**Standard Users**".

+ **Local Administrators Group members** (not local accounts) used to access ADDS forests, can be "**Standard Users**".

+ ADDS Schema must be conform to the Adfs prerequisites (FL 2003)

  

#### SQL Configuration for MFA users profiles

+ **Adfs service account** is a "**Standard User**" (recommended by Microsoft). must have **read** rights on all users properties (or ensure correct access to the properties described in ADDS configuration) on all forests.
+ **Adfs service account** used to access the MFA Database must be **dbCreator** and **dbSecurityAdmin** (done in database Creation/Upgrade)
+ **Delegated Administration Group members** used to access MFA Database, must be **dbCreator** and **dbSecurityAdmin** (done in database Creation/Upgrade).
+ **Local Administrators Group members** (not local accounts) used to access MFA Database, must be **dbCreator** and **dbSecurityAdmin** (done in database Creation/Upgrade).
+ SQL version must be 2008 and up. some features require SQL Server 2016 (always encrypted columns)

Or

+ **SQL Data Access User** (SQLServer Account) used to access the MFA Database must be **dbCreator** and **dbSecurityAdmin** (done in database Creation/Upgrade).

+ **SQL Data Access User** account , is a "**SQL Server Account**" (user **sa** not recommended)

+ **SQL Data Access User**  is required to perform PowerShell Remoting on Cmdlets that support it.

+ **Delegated Administration Group members** used to access MFA Database, not not need any rights on MFA Database if SQL Data Access Account is set.

+ **Local Administrators Group members** used to access MFA Database, not not need any rights on MFA Database  if SQL Data Access Account is set.

+ **SQL Data Access User**  is required to perform PowerShell Remoting on Cmdlets that support it. this Account must be an SQL Account (not a Windows account) and must be must be **dbCreator** and **dbSecurityAdmin** on your MFA SQL Server Database (done in database Creation/Upgrade), with this account there's no need to give SQL rights to **Adfs service account**, **Adfs Delegated administration group**, **Interactive Account**

+ SQL version must be 2008 and up. some features require SQL Server 2016 (always encrypted columns)

  

#### Summary

| Requirements Summary                                         |    Values    | Comments                             |
| ------------------------------------------------------------ | :----------: | :----------------------------------- |
| **ADFS & Operating System  Configuration**                   |              |                                      |
| - Allow local system account for services administration     | **required** |                                      |
| - Allow local administrators group for services services     | **required** |                                      |
| - Enable delegation for service administration (Delegated Administration Group) | **optional** | **recommended**<br>2016 / 2019 /2022 |
| - Microsoft .Net Framework 4.7.2 or upper                    | **required** |                                      |
|                                                              |              |                                      |
| **ADFS Account** (domain account)                            |              |                                      |
| - Is a domain Administrator (all trusted ADDS forests)       | **optional** | **not recommended**                  |
| - is a member of the Local Administrator group               | **optional** | Windows 2012r2 or for administration |
| - is an account operator member (all trusted ADDS forests)   | **optional** | **recommended**                      |
| - is a standard user account (all trusted ADDS forests with adequate read rights) | **optional** | **recommended**                      |

####  

#### Recommended SQL Server Configurations

| ADFS Account                |                                |                                                              |
| --------------------------- | :----------------------------: | ------------------------------------------------------------ |
|                             |    **ADFS Domain Account**     | **must have  read on all MFA Attributes on all your forests**<br/>default Microsoft configuration requirements<br/>**must be dbcreator, securityadmin and dbowner on the MFA Database** |
|                             |   Local Administrators users   | **must have  read/write access on the MFA Database**<br/>    can use PowerShell to manage MFA<br/>    can use MMC to manage MFA<br/>**must be securityadmin**<br/>     can manage certificates an encrypted columns |
|                             | Delegated Administrators users | **must have  read/write access on the MFA Database**<br/>    can use PowerShell to manage MFA<br/>    can use MMC to manage MFA<br/>**must be securityadmin**<br/>     can manage certificates an encrypted columns |
|                             |                                |                                                              |
| **SQL Data Access Account** |        **Recommended**         |                                                              |
|                             |  **SQL Data Access Account**   | **must be dbcreator, securityadmin and dbowner on the MFA Database** |
|                             |   Local Administrators users   | can use PowerShell to manage MFA<br/>can use MMC to manage MFA |
|                             | Delegated Administrators users | can use PowerShell to manage MFA<br/>can use <u>PowerShell Remoting</u> to manage MFA users<br/>can use MMC to manage MFA<br/>can manage certificates an encrypted columns |
|                             |          ADFS Account          | **must have read on all your forests**<br/>default Microsoft configuration requirements |



#### Recommended ADDS Configurations

| ADFS Account                 |                                |                                                              |
| ---------------------------- | :----------------------------: | ------------------------------------------------------------ |
|                              |    **ADFS Domain Account**     | **must have  read/write on all MFA Attributes on all your forests** |
|                              |   Local Administrators users   | **must have  read/write on all MFA Attributes on all your forests**<br/>    can use PowerShell to manage MFA<br/>    can use MMC to manage MFA |
|                              | Delegated Administrators users | **must have  read/write on all MFA Attributes on all your forests**<br/>    can use PowerShell to manage MFA<br/>    can use MMC to manage MFA |
|                              |                                |                                                              |
| **ADDS Data Access Account** |        **Recommended**         |                                                              |
|                              |  **ADDS Data Access Account**  | **must have  read/write on all MFA Attributes on all your forests** |
|                              |   Local Administrators users   | can use PowerShell to manage MFA<br/>can use MMC to manage MFA |
|                              | Delegated Administrators users | can use PowerShell to manage MFA<br/>can use <u>PowerShell Remoting</u> to manage MFA users<br/>can use MMC to manage MFA |
|                              |          ADFS Account          | **must have  read on all your forests**<br/>default Microsoft configuration requirements |

> For use of PowerShell Remoting, If the Interactive user is not a member of Local Administrators group but member of Delegated Administration Group. You must add the Delegated Administration Group to the "Remote Management Users Group" on the local server.
>



#### Identity Claim

- Identity claim is **by default UPN** (common and recommended in federation projects)  http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn
- The identity claim is stored by Adfs at registration



> In version 3.1, all administrators and operators rights are governed by the ADFS configuration with :
>
> - Local System Accounts (required)
>
> - Local Administrators Accounts
>
> - ADFS Delegated Administrators Accounts (recommended)
>
> For ADFS 2012 R2 only Local Administrators Accounts are suitable



See How to build a **High Trust Configuration** in [Security Management](07-Security)



## Installing Product
+ Download adfsmfa.msi from github : <https://github.com/neos-sdi/adfsmfa/releases>
+ Log on **each Adfs server** (2012r2, 2016, 2019 or 2022) as **Local administrator** user
+ Launch installation of the **adfsmfa.msi** file on each Adfs server.

>msi installation does not configure adfsmfa,
The installation process  deploy the components on the system, binaries in the GAC and in Program Files, and correctly register services, PowerShell Cmdlets and MMC snap-in, and finally create a shortcut on the desktop.
this step don’t require that you reconfigure a prior configuration of adfsmfa.
For example when patching or deploying a new version. You can patch each server at time (disabling the node in NLB). in this case no more actions are required, the system must be “operational”.



## Activating Product

This task registers the adfsmfa product in your Adfs Farm. When completed, adfsmfa became a new MFA provider for your Adfs Farm.
You must execute these task only on one primary Adfs Server 

> By default the component is using **Upn** ("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn") as Identity claim.<br>Alternatively you can use the **WindowsAccountName** ("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname") as Identity claim<br>You, can change this behavior after installing the msi and before registering the component. to do that you must change a registry key on each adfs server : Computer\HKEY_LOCAL_MACHINE\SOFTWARE\MFA\IdentityClaim -> 0: Upn, 1: WindowsAccountName.



+ Using Adfs WID configuration, your actions will be replicated on others servers within 5 minutes or when restarting Adfs services. For the parameters of  ADFSMFA configuration, all parameters are synchronized immediately with all servers (cache file)

+ Using Adfs SQL Configuration, everything is immediately available to all other servers, they share the same Adfs Databases. For the parameters of  ADFSMFA configuration, all parameters are synchronized immediately with all servers (cache file)

  

>Component registration can only be done with a PS Cmdlet **Register-MFASystem**.
The registration process create component Registration with ADFS Farm, and activate the component as an active MFA provider in your Adfs Farm with default configuration values.

>+ Log a **Primary** ADFS server of your farm as **ADFS administrator**
>+ Launch a new PowerShell session as administrator
>+ type **get-help Register-MFASystem –detailed** to get information.
>+ Enter your command
>```powershell
>#register MFA and create a RSA key for encrypting passwords and PassPhrase
>Register-MFASystem
>
>#register MFA and use default AES256 encryption for passwords and PassPhrase
>Register-MFASystem -NoRSAKeyReset 
>```



### Add an Adfs Server

This operation is **required** if you have multiple Adfs servers running **Windows 2012 R2**.
With that version (2012r2) the different servers are not automatically referenced.

For all versions of Adfs (2012r2, 2016, and 2019) you will need to use this command when adding a server to an existing farm.

+ Log on a **Primary** Adfs server as **ADFS administrator**
+ Launch a new PowerShell session as administrator
+ type **get-help Register-MFAComputer –detailed** to get information.
- Enter your command

>```powershell
>#register a new ADFS server and reset the RSA key for encrypting passwords and PassPhrase
>Register-MFAComputer -ServerName "servername_to_add"
>
>#register a new ADFS server
>Register-MFAComputer -ServerName "servername_to_add" -NoRSAKeyReset
>```

> *When you add an Adfs Server to your farm, you must execute Register-MFAComputer to add this computer to the MFA servers list (used by notification system).*
> *This operation is also needed if your Adfs farm servers are 2012 R2.*
>
> For Adfs 2016/2019 Register-MFASystem can do the job without need to register the computer with Register-MFAComputer.



### Get ADFS Farm information

>+ Log on a any Adfs server as **Adfs administrator** or **Delegated administration group** member
>+ Launch a new PowerShell session as administrator
>+ type **get-help Get-MFAFarmInformation –detailed** to get information.
>+ Enter your command
>```powershell
>Get-MFAFarmInformation
>```
>```powershell
>(Get-MFAFarmInformation).Servers
>```
>



#### If MMC not loading
Sometime after an major upgrade, registry key are not well updated by windows installer

>```
>Lauch Regedit as administrator
>Select HKEY_LOCAL_MACHINE
>Search for : FX:{9627f1f3-a6d2-4cf8-90a2-10f85a7a4ee7}
>In the right pane look at the Type attribute and modify the version with 3.0.0.0 if needed.
>```



## Backup and Restore MFA configuration

Once the configuration is set you can Backup your configuration in a xml file and of course you can restore it later
>+ Log on a **Primary** Adfs server as **Adfs administrator**
>+ Launch a new PowerShell session as administrator
>+ type **get-help Export-MFAConfiguration –detailed** to get information.
>+ type **get-help Import-MFAConfiguration –detailed** to get information.
>+ Enter your command>
>```powershell
>Export-MFAConfiguration -ExportFilePath 'c\temp\config.xml'
>```
>```powershell
>Import-MFAConfiguration -ImportFilePath 'c\temp\config.xml'
>```



## Additional configuration tasks

### Configure Windows Firewall Rules

To allow proper informations exchange in real time (Notifications, Anti Replay, etc) between Adfs servers (farm configuration), you must run a PowerShell command on each Adfs Server
>+ Log on each Adfs server as **Adfs administrator** or **Delegated administration group** member
>+ Launch a new PowerShell session as administrator
>+ type **get-help Set-MFAFirewallRules –detailed** to get information.
>+ Enter your command
>```powershell
>Set-MFAFirewallRules    # Using MFA configuration servers list
>
>Set-MFAFirewallRules -ComputersAllowed '172.16.100.1, 172.16.100.2'
>```

This command opens 1 port in your server Firewall (Scope Domain)

1. MFA Notification Hub Service (tcp) : **5987**

   


### Manage certificates private keys

If you have created MFA certificates (RSA) in previous installations, you must update Access Control List, to give the good rights on the certificates private keys for the Adfs service and the Adfs account 
>+ Log on a **Primary** Adfs server as **Adfs administrator** or **Delegated administration group** member
>+ Launch a new PowerShell session as administrator
>+ type **get-help Update-MFACertificatesAcessControlList  –detailed** to get information.
>+ Enter your command
>```powershell
>Update-MFACertificatesAcessControlList
>
>Update-MFACertificatesAcessControlList -CertsKind (AllCerts | MFACerts | ADFSCerts | SSLCerts)
>```

> This Cmdlet only add or update ACL, by default all certificates private keys are updated (AllCerts)
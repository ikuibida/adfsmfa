# UnInstallation
All Uninstallation operations must be run by a user who have **Local Administration** rights on each Adfs server
Starting with Adfs 2016, this user must be in the **Adfs administration group**



## Removing an Adfs Server

Removing an Adfs Server from the MFA farm doesn’t remove the server from Adfs farm :smiley:. 

Server is removed from MFA servers list. So, no notification can occur, and some commands will not operate.
You can use this command if you only want to remove a server from your farm

>+ Log on a **Primary** Adfs server as **Adfs administrator** or **Delegated administration group** member
>+ Launch a new PowerShell session as administrator
>+ type **get-help UnRegister-MFAComputer –detailed** to get information.
>+ Enter your command
>```powershell
>UnRegister-MFAComputer -ServerName yourserver
>```
>*Notifications are used to sync configuration changes without restarting ADFS instances. for example, if you change the password in SMTP configuration, this modification is “live” updated on all servers in the MFA list.*



## UnRegister Product

if you want to completely removes adfsmfa. 

adfsmfa is removed from Adfs’s MFA providers list and configuration is deleted.

Component unregistration can only be done with a PS Cmdlet **UnRegister-MFASystem**.
The unregistration process remove configuration from the ADFS Farm, and optionally backup your actual configuration.

> + Log on a **Primary** Adfs server as **Adfs administrator** or **Delegated administration group** member
> + Launch a new PowerShell session as administrator
> + type **get-help UnRegister-MFASystem –detailed** to get information.
> + Enter your command
> ```powershell
> UnRegister-MFASystem
> ```



## UnInstall Binaries

Log on **each Adfs server** (2012r2, 2016, 2019 or 2022) as Local administrator user, 

you can uninstall adfsmfa from the control panel.

> *All MFA databases, certificates are not removed. you must do it manually*

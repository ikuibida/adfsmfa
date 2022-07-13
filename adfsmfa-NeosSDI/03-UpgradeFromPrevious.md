# Upgrade from previous versions
All Upgrade operations must be run by a user who have **Local Administration** rights on each Adfs server.
Starting with Adfs 2016, this user must be in the **ADFS administration group**

You should only use this procedure when the Major and / or Minor .net (Assemblies) version numbers are different from the one you want to install.

For example your assemblies version is **2.2.0.0** and the version to install is version **3.0.0.0**, yes in this case you can apply the following.
Otherwise , you just need to uninstall your current version and deploy the new MSI.



## Installation Upgrade

Please follow these steps in sequence
### Backup your configuration (PowerShell)
>+ Log on the a primary Adfs server  as administrator
>+ Launch a new PowerShell session as administrator
>+ type **get-help Export-MFASystemConfiguration –detailed** to get information.
>+ Enter your command
>```powershell
>Export-MFASystemConfiguration -ExportFilePath "c:\temp\config 2.5.xml"
>```
>- Make a copy -> config 3.0.xml
>- Edit the config 3.0.xml file and change all assemblies references 2.x.x.x to 3.0.0.0. 
>- Save the changes in config 3.0.xml



### UnRegister ADFSMFA Authentication Provider (PowerShell)

>+ Log on the a primary Adfs server  as administrator
>+ Launch a new PowerShell session as administrator
>+ Enter theses commands
>```powershell
>$prov = (Get-AdfsGlobalAuthenticationPolicy).AdditionalAuthenticationProvider
>
>'Remove all Authentication Providers'
>Set-AdfsGlobalAuthenticationPolicy -AdditionalAuthenticationProvider $null
>'Or'
>'Remove selected MFA Authentication Provider'
>Set-AdfsGlobalAuthenticationPolicy -AdditionalAuthenticationProvider $prov[index]
>
>UnRegister-AdfsAuthenticationProvider -Name "MultifactorAuthenticationProvider" -Confirm:$false
>```



### UnRegister ADFSMFA ThreatDetection Provider - Optional (PowerShell - Adfs 2019/2022 only)

> - Log on the a primary Adfs server  as administrator
> - Launch a new PowerShell session as administrator
> - Enter theses commands
>
> ```powershell
> UnRegister-AdfsThreatDetectionModule -Name MFABlockPlugin
> ```



### UnInstall Binaries (prior msi)

### Install the release 3.0 msi



### Register ADFSMFA Authentication Provider (PowerShell)

>+ Log on the a primary Adfs server  as administrator
>+ Launch a new PowerShell session as administrator
>+ Enter theses commands
>+ Register the Provider with your new modified config file (config 3.0.xml)
>```powershell
>$typeName = "Neos.IdentityServer.MultiFactor.AuthenticationProvider, Neos.IdentityServer.MultiFactor, Version=3.0.0.0, Culture=neutral,PublicKeyToken=175aa5ee756d2aa2"
>
>Register-AdfsAuthenticationProvider -TypeName $typeName -Name "MultiFactorAuthenticationProvider"
>-Verbose -ConfigurationFilePath "c:\temp\config 3.0.xml"
>
>net stop adfssrv
>net start adfssrv
>
>Set-AdfsGlobalAuthenticationPolicy -AdditionalAuthenticationProvider MultiFactorAuthenticationProvider
>```
>You can also import your modified backup file
>+ Log on the a primary Adfs server  as administrator
>+ Launch a new PowerShell session as administrator
>+ type **get-help Import-MFASystemConfiguration –detailed** to get information.
>+ Enter theses commands
>```powershell
>Import-MFASystemConfiguration -ImportFilePath "c:\temp\config2.5.xml"
>```



### Register ADFSMFA ThreatDetection Provider - Optional (PowerShell - Adfs 2019/2022 only)

> - Log on the a primary Adfs server  as administrator
> - Launch a new PowerShell session as administrator
> - Enter theses commands
>
> ```powershell
> $TypeName = "Neos.IdentityServer.MultiFactor.ThreatAnalyzer, Neos.IdentityServer.MultiFactor.ThreatDetection, Version=3.0.0.0, Culture=neutral, PublicKeyToken=175aa5ee756d2aa2"
> 
> Register-AdfsThreatDetectionModule -Name MFABlockPlugin -ConfigurationFilePath "C:\Program Files\MFA\Config\threatconfig.db" -Typename $TypeName
> 
> ```



### If MMC not loading in 3.0

see issue https://github.com/neos-sdi/adfsmfa/issues/20
>```
>Lauch Regedit as administrator
>Select HKEY_LOCAL_MACHINE
>Search for : FX:{9627f1f3-a6d2-4cf8-90a2-10f85a7a4ee7}
>In the right pane look at the Type attribute and modify the version with 3.0.0.0 if needed.
>```



## Configuration for the new Biometric provider 

This procedure is only applicable if your current version is <u>lower than version 3.0</u>.
First update the component as indicated above , then apply the SQL or ADDS procedure

### SQL Configuration

If you are using SQL Configuration, Database tables must be added. You must run a PowerShell Cmdlet to upgrade you database.
Interactive Account and the Adfs service account must be **dbCreator** and **dbSecurityAdmin** on your SQL Server instance 

>+ Log on the a primary Adfs server  as administrator
>+ Launch a new PowerShell session as administrator
>+ type **get-help Upgrade-MFADatabase –detailed** to get information.
>+ Enter this command
>
>```powershell
>Upgrade-MFADatabase -ServerName "yourservername" -DatabaseName "yourexistingMFADB"
>```



### Active Directory Configuration

If you are using Active Directory Configuration, a new attribute for storing WebAuthN credential is required.
This attribute **MUST BE MULTIVALUED**.

Obviously, you can indicate the attributes you want. However we provide three templates, those are accessible in the console and with a dedicated PowerShell command
>+ Log on the a primary Adfs server  as administrator
>+ Launch a new PowerShell session as administrator
>+ type **get-help Set-MFAActiveDirectoryTemplate –detailed** to get information.
>+ Enter this command
>```powershell
>Set-MFAActiveDirectoryTemplate -Kind SchemaAll
>Set-MFAActiveDirectoryTemplate -Kind Schema2016
>Set-MFAActiveDirectoryTemplate -Kind SchemaMFA
>```
>+ **SchemalAll**
>For any version of ADDS schema
>the multivalued attribute is **otherMailbox**
>If not accurate, you can change it with PowerShell or MMC
>
>+ **Schema2016**
>For ADDS schema version 85 and up
>the multivalued attribute is **msDS-KeyCredentialLink**
>If not accurate, you can change it with PowerShell or MMC
>Remark : this attribute is used by Microsoft to store **Windows Hello For Business** or **Device Registration** Informations, (not recommended)
>
>+ **SchemaMFA**
>Custom schema for ADDS
>Two LDF files are provided, you can find them in C:\Program Files\MFA\ADDSTools directory
>   + mfa-schema.hitrust.ldf (using **"confidential 0x80"** flag)
>   + mfa-schema.ldf (standard attributes)
>These schema use auxilliary class and can be disabled later.
>



Remember that a ADDS **schema alteration is not reversible**
with conditional flag active, standard users cannot query MFA attributes, to comply to RGPG, it's the way you must fill
You must change the distinguished names in the file MFA.SCHEMA.LDF (DC=x to DC=mycompany,DC=com)

- Copy modified MFA.SCHEMA.LDF on your domain controller

- Deploy it with **LDIFDE -i -u -f MFA.ALTERED_DN.SCHEMA.LDF** a domain administrator

- repeat operations for each domain
  Attributes are marked as confidential and replicable in global catalog.

  

More informations in [Security Configuration](07-Security)
# 2021

# MFA 3.1 (december 2021) for ADFS (2022/2019/2016/2012R2)
### 11/29/2021 / Build : **3.1.2112.0**

#### Enhancements
- Added 3 properties for WebAuthN support to deal with Apple constraints, see : [Apple's Blog](https://webkit.org/blog/11312/meet-face-id-and-touch-id-for-the-web/)
- ForbiddenBrowsers : browsers that dosen't supports WebAuthN
- InitiatedBrowsers : browsers that require an initiated ceremony (click on a button)
- NoCounterBrowsers : browsers that dosen't support WebAuthN/FIDO's anti-replay feature, UsageCount remains always 0

Only with PowerShell : [Documentation](https://github.com/neos-sdi/adfsmfa/wiki/08B-MFA%20Biometric%20Provider#mfa-biometric-provider-properties)

Sample

```powershell
$c = get-mfaProvider -ProviderType Biometrics
$c.InitiatedBrowsers = "safari; unknown"
$c.NoCounterBrowsers = "safari; unknown"
Set-mfaProvider -ProviderType Biometrics $c
```

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2112.0)

# MFA 3.1 (november 2021) for ADFS (2022/2019/2016/2012R2)
### 11/20/2021 / Build : **3.1.2111.1**

#### Bugs
- Access my options was not available if the current provider do not have Wizard option enabled. So the user cannot register or change anything.

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2111.1)

### 11/15/2021 / Build : **3.1.2111.0**

#### Enhancements
- Added support of ADFS 2022 (behavior remains at 4, So, few changes with 2019, css, js)
- Refactoring of Custom Presentation API
- Access to my options, made visible if there is at least one Wizard available
- Email Provider is no longer required by default
- ADFS 2019 and 2022, automatic download of threat IP list

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2111.0)

# MFA 3.1 (october 2021) for ADFS (2019/2016/2012R2)
### 10/22/2021 / Build : **3.1.2110.1**

#### Enhancements
- Added support of "User Language" for Registered Devices (DRS)
- Added support of "Fast Login" for Registered Devices (DRS)

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2110.1)

### Version 3.1 with Biometric authentication (WebAuthN)
### 10/20/2021 / Build : **3.1.2110.0**

#### Enhancements
- WebAuthN API upgrade from version fido2-net-lib 2.0.2. (more than 90 types of devices supported !!!)
- MDS3 support (download of Metadata payloads automatically from mds.fidoalliance.org)
- MDS3 support for constrained environments (no internet access)
- Support for **FaceID** and **TouchID** (_thanks very much for testing by @rtemelcea_, without the support of Razvan all this would not have been possible)
  Due to Apple restrictions Fastlogin is disabled for Apple products : https://webkit.org/blog/11312/meet-face-id-and-touch-id-for-the-web/
- For developers C# 7.2 is required.

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2110.0)

# MFA 3.1 (september 2021) for ADFS (2019/2016/2012R2)
### Version 3.1 with Biometric authentication (WebAuthN)
### 09/30/2021 / Build : **3.1.2109.1**

#### Enhancements
- API for implenting a custom Adaptater Presentation (UI) has been finalized
- Custom Adapter Presenation Sample (the same as for ADFS 2019, We are waiting for a sample from @apr-un)
  You can use : Install-MFASamples and Set-MFAThemeMode cmdlets to activate these feature.
#### Bugs
- Resolved null reference exception when using Device Registration (UserAgent is null)

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2109.1)

### 09/24/2021 / Build : **3.1.2109.0**

#### Enhancements
- System cache file is now also created at MFA Service startup (like config file). Should solve issue : https://github.com/neos-sdi/adfsmfa/issues/172

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2109.0)

# MFA 3.1 (august 2021) for ADFS (2019/2016/2012R2)
### Version 3.1 with Biometric authentication (WebAuthN)
### 07/26/2021 / Build : **3.1.2108.0**

#### Enhancements
- Biometric authentication disabled for Internet Explorer
- Redirect to choose another option if no biometric device are detected.

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2108.0)

# MFA 3.1 (july 2021) for ADFS (2019/2016/2012R2)
### Version 3.1 with Biometric authentication (WebAuthN)
### 07/02/2021 / Build : **3.1.2107.0**

#### bugs
- Security Issue :  When session is rejected, you can bypass MFA (version 31.2106.*), **you must upgrade with this version**
- Cookie options attributes was not correctly coded.
- Issue : https://github.com/neos-sdi/adfsmfa/issues/189

#### Enhancements
- Some russian translation made by @apr-un

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2107.0)


# MFA 3.1 (june 2021) for ADFS (2019/2016/2012R2)
### Version 3.1 with Biometric authentication (WebAuthN)
### 06/09/2021 / Build : **3.1.2106.2**

#### bugs
- Solved Issue :  https://github.com/neos-sdi/adfsmfa/issues/182 - "Delegated Admin Group cannot be empty"

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2106.2)

### 06/03/2021 / Build : **3.1.2106.1**

#### bugs
- AAD MFA Sample : For ADFS 2019 Changed ADAL AcquireToken to AcquireTokenAsync (works with 2019 and 2016), thanks to @apr-un ! 

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2106.1)

### 06/01/2021 / Build : **3.1.2106.0**

#### Enhancements
- Added the Polish translation made by @apr-un on all UI, PS, MMC modules. A big thank you and above all a lot of work done
#### bugs
- Removed duplicate id for input tag in Html generation

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2106.0)

# MFA 3.1 (may 2021) for ADFS (2019/2016/2012R2)
### Version 3.1 with Biometric authentication (WebAuthN)
### 05/01/2021 / Build : **3.1.2105.0**

#### Enhancements
- Added the ability to provide your own user interface implementation
New property **AdapterPresentationImplementation** in Get-MFAConfig, you must provide an assembly with a class deriving from **BasePresentation** or **BaseMFAPresentation**. To activate it you must change the **UIKind** property to **Custom**.
At this time no sample is provided, But if you feel up to it, you can give it a go.
- some typos in MMC.

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2105.0)


# MFA 3.1 (april 2021) for ADFS (2019/2016/2012R2)
### Version 3.1 with Biometric authentication (WebAuthN)
### 04/15/2021 / Build : **3.1.2104.1**

#### Enhancements
- Encrypt MFA Passwords and PassPhrase in configuration with RSA Key see : **Register-MFASystemMasterKey** (also available in MMC)
- Encrypt AES Keys with ECDH Key see : **Register-MFASystemAESCngKey** (also available in MMC)

About ECDH_P256 encryption (Elliptic-Curve Diffie-Hellman)
>```
>In cryptography, the Diffie-Hellman key exchange, named after its authors Whitfield Diffie and Martin Hellman, 
>is a method1, published in 1976, by which two agents, named by convention Alice and Bob, 
>can agree on a number (which they can use as a key to encrypt the next conversation) without 
>a third agent called Eve being able to discover the number, even after having listened to all their exchanges. 
>This idea won the two authors the Turing Prize in 2015.
>```

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2104.1)


### 04/01/2021 / Build : **3.1.2104.0**

#### Enhancements
- Encrypt MFA Passwords and PassPhrase in configuration with RSA Key see : **Register-MFASystemMasterKey**

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2104.0)

# MFA 3.1 (march 2021) for ADFS (2019/2016/2012R2)
### Version 3.1 with Biometric authentication (WebAuthN)
### 03/22/2021 / Build : **3.1.2103.7**

#### Enhancements
- See issue : https://github.com/neos-sdi/adfsmfa/issues/161
- See issue : https://github.com/neos-sdi/adfsmfa/issues/162

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2103.7)


### 03/19/2021 / Build : **3.1.2103.6**

#### Bugs
- Security Issue : SQL Password was not correctly encrypted in configuration file. However, the configuration file is encrypted correctly, and the password is never displayed in clear. this release solve this issue. 
Only, for those of you who use the SQL Server configuration, this update is necessary, because a simple export of the configuration would allow to see the password.

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2103.6)

### 03/15/2021 / Build : **3.1.2103.5**

#### Enhancements
- Password Policy now checks for Minimum password age and Maximum password age
#### Bugs
- Finalized Biometric Wizards, with latest evolutions for nicknames.

Except for bugs, it should be the last version for march 2021

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2103.5)


### 03/12/2021 / Build : **3.1.2103.4**

#### Enhancements
- Added NickNames for biometric devices, with new encoding for PublicDescriptors (compatible with prior version)

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2103.4)


### 03/10/2021 / Build : **3.1.2103.3**

#### Enhancements
- Added new placeholder for email templates (TOTP, Notification) mail address
- System.db is now deleted at each restart of MFA Service
- Imports now are generating a new Key on new user import.

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2103.3)



### 03/08/2021 / Build : **3.1.2103.2**

#### New Features
- Passwords Policy
Supports ADDS Password Policy settings for each domain
Supports ADDS Fine-Grained Passwords Policy settings for each domain (PSO). See :  https://docs.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2008-r2-and-2008/dd378968(v=ws.10)
Supports adfsmfa Password Policy Settings

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2103.2)



### 03/05/2021 / Build : **3.1.2103.1**

#### Bugs 
- MMC, Cutom Template doesn't dispaly correct values, but values are stored as well. You need to refresh the snapin. 
  issue resolved !
- WebAuthN support for Huawei P40 Lite... see :  https://github.com/abergs/fido2-net-lib/pull/208

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2103.1)


### 03/02/2021 / Build : **3.1.2103.0**

#### New Features
- Passwords Policy in security part (locking and Warnings)

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2103.0)

# MFA 3.1 (february 2021) for ADFS (2019/2016/2012R2)
### Version 3.1 with Biometric authentication (WebAuthN)
### 02/25/2021 / Build : **3.1.2102.2**

#### New Features
- Add more remotable Cmdlets see : https://github.com/neos-sdi/adfsmfa/wiki/10-PowerShell%20Commands
- Some refactor for multi-forests in ADDS configuration, added support for subdomains in trusted forest
- Added more logs (EventLog) for Data access
- Added Authorization Policy for MFA tcp service (Windows Authentication + encrypted streams) 

Uninstall prior msi (3.x.xxxx.x)
install this one (3.1.2102.2)

### 02/20/2021 / Build : **3.1.2102.1**

#### New Features
- Removed WsMan configuration
Now intercommunications between ADFS Farm servers is done with TCP service (port 5987)
Using the TCP service is really faster than WsMan (remote powerShells, more reliable than remote mailslots.
Many actions are now remoted (ex: installing certificates).
- General configuration, you no longer have to give local administration rights to your ADFS operators, we also use the ADFS config options to manage this. 
- Added support for remoting some Cmdlets (*-MFAUsers and more)
- Some bugs correction

#### Bugs 
- Solved Registration Problems see : https://github.com/neos-sdi/adfsmfa/issues/143 

Uninstall prior msi (3.0.xxxx.x)
install this one (3.1.2102.1)

### 02/02/2021 / Build : **3.1.2102.0**

#### New Features
- Removed WsMan configuration
Now intercommunications between ADFS Farm servers is done with TCP service (port 5987)
Using the TCP service is really faster than WsMan (remote powerShells, more reliable than remote mailslots.
Many actions are now remoted (ex: installing certificates).
- General configuration, you no longer have to give local administration rights to your ADFS operators, we also use the ADFS config options to manage this. (The documentation will be quickly updated)
- Some bugs correction

#### Version discarded : Not working with ADFS WID configuration.

Uninstall prior msi (3.0.xxxx.x)
install this one (3.1.2102.0)

# MFA 3.0 (January 2021) for ADFS (2019/2016/2012R2)

### Version 3.0 with Biometric authentication (WebAuthN)
### 01/18/2021 / Build : **3.0.2101.3**

#### Bugs
- Security issue : https://github.com/neos-sdi/adfsmfa/issues/138
 Anti Replay not working for totp since last version
  We recommend to upgrade to this version.

The next version will be 3.1.xxxx.y, with great speed improvements, and security features for configuration and installation.
We recommend to stay connected and watching this projet to be well notified... 

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2101.3)

### 01/08/2021 / Build : **3.0.2101.2**

#### Bugs
- Security issue : The number of retries was not honored since version 3.0.2011.2 when using one-way verification (totp, email, ...)
We recommend to upgrade to this version.

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2101.2)


### 01/06/2021 / Build : **3.0.2101.1**

#### Bugs
- Solved issue 137 : https://github.com/neos-sdi/adfsmfa/issues/137

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2101.1)


### 01/01/2021 / Build : **3.0.2101.0**

#### Bugs
- Solved issue 135 : https://github.com/neos-sdi/adfsmfa/issues/135

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2101.0)





# **2020**



# MFA 3.0 (December 2020) for ADFS (2019/2016/2012R2)

### Version 3.0 with Biometric authentication (WebAuthN)
### 12/28/2020 / Build : **3.0.2012.1**

#### Enhancements
- UI Translations for web UI, Console an Powershell for **Japanese** by **@kouh123** 
- Fix some english typos by **@kouh123**

   See Pull Request : https://github.com/neos-sdi/adfsmfa/pull/134
   Really very happy to have a new contributor, thank you again !!!!

  And... Happy new year !

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2012.1)

### 12/17/2020 / Build : **3.0.2012.0**

#### Enhancements
- WebAuthN : Explicit Replay Protection in WebAuthN Provider
- WebAuthN : CredProtect Extension preview.
- All : Some code refactoring.

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2012.0)

# MFA 3.0 (November 2020) for ADFS (2019/2016/2012R2)
### Version 3.0 with Biometric authentication (WebAuthN)
### 11/20/2020 / Build : **3.0.2011.3**

#### Enhancements
- UI Translations for web UI - **Russian** by **@romqatt**
   Really very happy to have a new contributor, thank you again !!!!

#### Bugs
- Solved issue 132 : https://github.com/neos-sdi/adfsmfa/issues/132 
 it seems never be implemented before ?

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2011.3)

### 11/18/2020 / Build : **3.0.2011.2**

#### Enhancements
- UI - changed "I have no code" to "Sign in another way" see https://github.com/neos-sdi/adfsmfa/issues/130
- Code - some refactoring

11/18/2020 small release : one typo not updated when changing language in MMC console

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2011.2)

### 11/16/2020 / Build : **3.0.2011.1**

#### Enhancements
- UI translations - some typos by **@kouh123** for japanese language
- UI Console - some refactoring - added Digits and duration for TOTP (rfc 6238)
- UI Console - added apple to biometric pin requirements (also for PowerShell)
- UI Console - TOTP Provider required property can be disabled. don't forget to make another Provider as required.
- Code - some refactoring

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2011.1)

### 11/05/2020 / Build : **3.0.2011.0**

#### Enhancements
 - **Config** added PrimaryAuhenticationOptions property. used when MFA is configured as Primary Authentication method.
 - **UI** added support for custom themes at Relying Party level (SAML & WS-FED. Themes are not suppourted by ADFS for Oauth and OpenID-Connect applications) Added Cmdlet **Reset-MFAThemesList** to reset/reload RL themes list

*A strange feeling, Microsoft did not see fit to provide theme support (RL level) for custom components ... a bug? an oversight? or...
Because yes, this feature is well supported for its own components ...*

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2011.0)

# MFA 3.0 (October 2020) for ADFS (2019/2016/2012R2)
### Version 3.0 with Biometric authentication (WebAuthN)
### 10/12/2020 / Build : **3.0.2010.1**

#### Bugs correction
- WebAuthN fix for TPM see : https://github.com/abergs/fido2-net-lib/issues/189 and https://github.com/abergs/fido2-net-lib/pull/187

#### Enhancements
- WebAuthN **experimental support for Apple** see : https://pr-preview.s3.amazonaws.com/alanwaketan/webauthn/pull/1491.html#sctn-apple-anonymous-attestation
- Core : Possibility of using another identification claim. this must be done before registering the component.
You can modify the registry keys for each server: Computer\HKEY_LOCAL_MACHINE\SOFTWARE\MFA\IdentityClaim
( **0** : "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn" - Upn)
( **1** : "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname" - WindowsAccountName)

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2010.1)

### 10/03/2020 / Build : **3.0.2010.0**

#### Security Enhancements (WebAuthN)
- added Pin requirement for unsafe WebAuthN requests (optional see Biometric Provider configuration)
  thanks very much to **@gtbuchanan** for his suggestions.
  see issue https://github.com/neos-sdi/adfsmfa/issues/120

##### Samples
```powershell
$p = Get-MFAProvider -ProviderType Biometrics
$p.PinRequired = $False
$p.PinRequirements = "None, AndroidKey, AndroidSafetyNet, Fido2U2f, Packed, TPM"
Set-MFAProvider -ProviderType Biometrics $p
```
Or for example
```powershell
$p = Get-MFAProvider -ProviderType Biometrics
$p.PinRequired = $False
$p.PinRequirements = "None, Fido2U2f, Packed"
Set-MFAProvider -ProviderType Biometrics $p
```

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2010.0)

# MFA 3.0 (September 2020) for ADFS (2019/2016/2012R2)
### Version 3.0 with Biometric authentication (WebAuthN)
### 09/29/2020 / Build : **3.0.2009.3**

#### Enhancements
 - Pin codes now can have between 4 and 9 digits

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2009.3)

### 09/24/2020 / Build : **3.0.2009.2**

#### Enhancements
 - MMC Console : added management of WebAuthN keys descriptors for each user
 - WebAuthN : added support for latest Yubikeys firmwares (5.2.x)

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2009.2)

### 09/14/2020 / Build : **3.0.2009.1**

#### Enhancements
- WebAuthN : Full update of CBOR extensions (4.2.0) see : https://github.com/peteroupc/CBOR, https://github.com/peteroupc/Numbers
- WebAuthN : TPM and Windows Hello enforced KeyDescriptor validation (Certificates chain) see : https://github.com/abergs/fido2-net-lib
- WebAuthN : Works with [Yubiko Security Key Nfc](https://www.yubico.com/product/security-key-nfc-by-yubico), with Windows Hello as Security Card (double Tap and pin required) and with Samsung Pass and NFC , 

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2009.1)

### 09/09/2020 / Build : **3.0.2009.0**

#### Enhancements
- Some typos in registration process
- optimize flag on all assemblies

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2009.0)


# MFA 3.0 (August 2020) for ADFS (2019/2016/2012R2)
### 08/27/2020 / Build : **3.0.2008.4**

#### Bugs
- Solved Issue see : https://github.com/neos-sdi/adfsmfa/issues/115   "Access my options after authentication" for biometrics doesn't seem to work

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2008.4)

### 08/26/2020 / Build : **3.0.2008.3**

#### Bugs
- Solved Issue see : https://github.com/neos-sdi/adfsmfa/issues/114
- Solved Issue see : https://github.com/neos-sdi/adfsmfa/issues/116

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2008.3)


### 08/25/2020 / Build : **3.0.2008.2**

#### Bugs
- MetadataService must be initialized correction see : https://github.com/neos-sdi/adfsmfa/issues/114
MetadataService is now initialized properly

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2008.2)


### 08/20/2020 / Build : **3.0.2008.1**

#### Enhancements
- Property to lock localization see : https://github.com/neos-sdi/adfsmfa/issues/113 

Uninstall prior msi (3.0.xxxx.x)
install this one (3.0.2008.1)


### 08/03/2020 / Build : **3.0.2008.0**

#### Bug correction
- Solved ADDS schema exception see : https://github.com/neos-sdi/adfsmfa/issues/109 

Uninstall prior msi (3.0.0.x)
install this one (3.0.2008.0)


# MFA 3.0 (July 2020) for ADFS (2019/2016/2012R2)
### 07/10/2020 / Build : **3.0.2007.2**

#### Enhancements
- Complete German translation by @klpo-mt 
- Some typos in mail resx. see https://github.com/neos-sdi/adfsmfa/pull/107

Very happy to be able to benefit from an original German translation by **@klpo-mt**. 
The German language will now be supported by default (with the help of established linguists if necessary ... :)

Uninstall prior msi (3.0.0.x)
install this one (3.0.2007.2)


### 07/09/2020 / Build : **3.0.2007.1**

#### Enhancements
- Some typos in console for english
- Some typos in mail resx. see https://github.com/neos-sdi/adfsmfa/pull/107

Uninstall prior msi (3.0.0.x)
install this one (3.0.2007.1)

### 07/07/2020 / Build : **3.0.2007.0**

#### Enhancements
- Added support for **ldaps** requests when using ADDS configuration (you must deploy adequate certificates on your domain controllers)
- Added support for **AES symetric encryption** for encrypting users keys (**AES128** and **AES256**)
- Added support for logs in WebAuthN (**ShowPII**)
- Added sample for custom encryption (CUSTOM), **Caesar** algorythm...
- Added sample for custom data repository (CUSTOM Storage), In memory demo. and bug correction.
- Added Cmdlet **Install-MFASamples** to install or remove Samples.
- All Samples are now inside one assembly (**Neos.IdentityServer.Multifactor.Samples**)
- Small refactoring of MMC Console
- Users informations (like email addresses or phone number) removed from UI for security reasons

Uninstall prior msi (3.0.0.x)
install this one (3.0.2007.0)

# MFA 3.0 (June 2020) for ADFS (2019/2016/2012R2)
### Version 3.0 with Biometric authentication (WebAuthN)
### 06/16/2020 / Build : **3.0.2006.1**

#### Bugs corrections
- Azure Provider was compiled with debug #define  
- UI Problem corrected. When a user is in choose preferred method and the default mfa provider is a "two way" displayed user interface is inconsistent.

#### Removed samples
- Custom Keys Sample removed, because inconsistent with Storage model. 
- New symetric encryption mecanisms (AES128, AES256,...) will be included in next version, with the ablility to plug your own solution and of course, a basic sample.

Uninstall prior msi (3.0.0.x)
install this one (3.0.2006.1)


### 06/11/2020 / Build : **3.0.2006.0**

#### Enhancements
- Added support for custom storage repository (ADDS, SQL, CUSTOM), 
- Added a sample for custom storage In memory. 
  - works only for one server, it's a sample ! 
  - you must disable others adfs servers from load balancing NLB. 
  - Activation should be very usefull to evaluate ADFSMFA... (no writing on ADDS or SQL)
- some code refactoring
- User Agent feature override no fallback to current LCID context, not 1033. With some devices like Android Phones and when using Device Registration, no User Agent is provided

#### Known problems
- Please verify after upgrade that your storage mode is well defined (ADDS or SQL)

Uninstall prior msi (3.0.0.x)
install this one (3.0.2006.0)



# MFA 3.0 (May 2020) for ADFS (2019/2016/2012R2)

### Version 3.0 with Biometric authentication (WebAuthN)

### 05/28/2020 / Build : **3.0.2005.6**

#### Enhancements
- WebAuthN component update (root certs validation for TPM) see : https://github.com/abergs/fido2-net-lib/pull/166
- WebAuth component upgrade of some nuget's assemblies

Uninstall prior msi (3.0.0.x)
install this one (3.0.2005.6)


### 05/26/2020 / Build : **3.0.2005.5**

#### Enhancements
- WebAuthN component update (new ASN library and support for root certs validation for TPM, Android-SafetyNet, FIDO2-u2f and Packed (Windows Hello).. 
See some of these updates : https://github.com/abergs/fido2-net-lib/pull/166 , https://github.com/abergs/fido2-net-lib/pull/165 , https://github.com/abergs/fido2-net-lib/pull/164, https://github.com/abergs/fido2-net-lib/issues/159, https://github.com/abergs/fido2-net-lib/pull/158

Uninstall prior msi (3.0.0.x)
install this one (3.0.2005.5)

### 05/23/2020 / Build : **3.0.2005.4**

#### Enhancements
- Installer enhancements, .... 
- More detailed logs for encryption (to help for this issue  https://github.com/neos-sdi/adfsmfa/issues/101)

Uninstall prior msi (3.0.0.x)
install this one (3.0.2005.4)

### 05/20/2020 / Build : **3.0.2005.3**

#### Enhancements
- Installer enhancements, .... 
  You should no longer have to worry about installing and uninstalling ...
  Obviously, we put our logo ... ok ! I'm going out !

Uninstall prior msi (3.0.0.x)
install this one (3.0.2005.3)

### 05/18/2020 / Build : **3.0.2005.2**

#### Enhancements
- Installer now always run in **Privilegied Mode**, solving uninstallation problems. 
- PS **Update-MFACredentials** to manage Passwords and Keys, -Clear Credential switch added

Uninstall prior msi (3.0.0.x)
install this one (3.0.2005.2)


### 05/15/2020 / Build : **3.0.2005.1**

#### Bug corrections
- See Issue : https://github.com/neos-sdi/adfsmfa/issues/101

Uninstall prior msi (3.0.0.x)
install this one (3.0.2005.1)


### 05/12/2020 / Build : **3.0.2005.0**

#### Enhancements
- Encryption for configuration (Cache, Passwords, Keys) AES128 -> **AES256**
- PS **Update-MFACredentials** to manage Passwords and Keys 
- PS **Export-MFAMailTemplates** to manage email templates

#### Bug corrections
- WebAutn (TPM and Packed attestation not validating root certs)
see : https://github.com/abergs/fido2-net-lib/pull/158
- PS **Set-MFASecurity** with no params

If you experience errors after upgrading you can run **Update-MFACredentials**

Uninstall prior msi (3.0.0.x)
install this one (3.0.2005.0)



# MFA 3.0 (April 2020) for ADFS (2019/2016/2012R2)

### Version 3.0 with Biometric authentication (WebAuthN)

### 04/24/2020 / Build 3.0.0.10

#### Enhancements
- New languages generated with "Azure Cognitive Services" So, it's not perfect...
-- **Danish**
-- **Japanese**
-- **Quebec** 

If you have better translations, you can make a pull request with your modified resources files

Uninstall prior msi (3.0.0.x)
install this one (3.0.0.10)



### 04/22/2020 / Build 3.0.0.3

#### Enhancements
- administrative emails are send now by default the "default country language", no change for mails initiated by users. see :  https://github.com/neos-sdi/adfsmfa/issues/99

Uninstall prior msi (3.0.0.x)
install this one (3.0.0.3)



### 04/22/2020 / Build 3.0.0.2

#### Enhancements
- ADDS added support for multiple subdomains and multiple forests. see :  https://github.com/neos-sdi/adfsmfa/issues/97

Uninstall prior msi (3.0.0.x)
install this one (3.0.0.2)



### 04/07/2020 / Build 3.0.0.1

#### Bug corrections
- Registry keys problem resolved. see :  https://github.com/neos-sdi/adfsmfa/issues/96

Uninstall prior msi (3.0.0.0)
install this one (3.0.0.1)



### 04/04/2020 / Build 3.0.0.0

#### Enhancements
- All communications between Adfs Server ar now made with __WinRM__ (WsMan). Legacy protocols have been removed.
- You have to run the PS cmdlet __Set-MFAFirewallRules__
- Ports __5985__ (WinRM http), __5986__ (WinRM https), __5987__ (MFA TCP Service) must be open in local firewalls
- You must enable WinRM on each Adfs Server : at command prompt type __WinRM quickconfig__.
- Installation and Configuration Windows Remote Management : https://docs.microsoft.com/en-us/windows/win32/winrm/installation-and-configuration-for-windows-remote-management.
- Added 3 PowerShell cmdlet to manage configuration cache 

__Remember that this version require that your servers have the Microsoft .Net Framework 4.7.2 up to date !!
Biometric Provider require that.__
See : https://github.com/abergs/fido2-net-lib/pull/65

follow steps : https://github.com/neos-sdi/adfsmfa/wiki/03-UpgradeFromPrevious
Uninstall prior msi (2.x.x.x)
install this one (3.0.0.0)



# MFA 2.5 (March 2020) for ADFS (2019/2016/2012R2)

### Version 2.5 with Biometric authentication (WebAuthN)

### 03/05/2020 / Build 2.5.4720.9000

#### Enhancements :  ADFS 2019
- Added support for Adfs 2019 to set adfsmfa as Primary Authenticator.<br>In this case you must disable all secondary methods or choose another than adfsmfa.
- Added cmdlet to set Primary Authentication Enabled. User Registration is disabled, and some providers are not available (email, external and Azure)<br><b>Set-MFAPrimaryAuthenticationStatus $True ou $False</b>
- Added Component for Threat management (first version), with small database of IPs to blackList<br><b>- Register-MFAThreatDetectionSystem</b> to activate threat detection.<br><b>- UnRegister-MFAThreatDetectionSystem</b> to unregister threat detection.<br><b>- Update-MFAThreatDetectionData</b> to update ips from the modified threat config database (\ProgramFiles\MFA\Config\threatconfig.db). You can edit this file as text file, ips must be separated by <b>;</b> 

#### Bug Correction : ALL Versions
- JavaScript correction when detecting biometric device.

__Remember that this version require that your servers have the Microsoft .Net Framework 4.7.2 up to date !!
Biometric Provider require that.__
See : https://github.com/abergs/fido2-net-lib/pull/65

Uninstall prior msi (2.5.0.0)
install this one (2.5.4720.9000)



# MFA 2.5 (February 2020) for ADFS (2019/2016/2012R2)

### Version 2.5 with Biometric authentication (WebAuthN)

### 02/27/2020 / Build 2.5.4720.8000

Small update for Adfs 2019
#### Enhancements : 
- Added support for Adfs 2019 to set adfsmfa as Primary Authenticator.
- In this case you must disable all secondary methods or choose another than adfsmfa

__Remember that this version require that your servers have the Microsoft .Net Framework 4.7.2 up to date !!
Biometric Provider require that.__
See : https://github.com/abergs/fido2-net-lib/pull/65

Uninstall prior msi (2.5.0.0)
install this one (2.5.4720.8000)



### 02/25/2020 / Build 2.5.4720.7000

### Version 2.5 with Biometric authentication (WebAuthN)
Build : 2.5.4720.7000

#### Enhancements : 
- bug correction for Windows 2012r2 and 2016 Console crash when saving MMC
- __Wiki and PDF help files__

__Remember that this version require that your servers have the Microsoft .Net Framework 4.7.2 up to date !!
Biometric Provider require that.__
See : https://github.com/abergs/fido2-net-lib/pull/65

Uninstall prior msi (2.5.0.0)
install this one (2.5.4720.7000)



### 02/20/2020 / Build 2.5.4720.6000

#### Enhancements : 
- bug correction in generation of certificates on Windows 2012R2 (evolution CERTENROLLLib Com Object)
- bug correction for Windows 2012r2 on WebAuthN (the claim is case sensitive in W2012r2)
- added a job in the MFA Hub Service to clean orphaned private keys (you can change this behavior in registry of each server HKLM\Software\MFA) but deactivated by default. 

- Persistance flag removed for certificates
- Library dependency not included in previous build (affect webauthN)

- __Wiki and PDF help files, soon...__

__Remember that this version require that your servers have the Microsoft .Net Framework 4.7.2 up to date !!
Biometric Provider require that.__
See : https://github.com/abergs/fido2-net-lib/pull/65

Uninstall prior msi (2.5.0.0)
install this one (2.5.4720.6000)



### 02/06/2020 / Build 2.5.4720.3001

#### Enhancements : 
- bug correction in cmdlet  __Update-MFACertificatesAccessControlList__

__The future next version (.5000 ??) will include Wiki and PDF help files, more auto-completion in Cmdlets.__

__Remember that this version require that your servers have the Microsoft .Net Framework 4.7.2 up to date !!
Biometric Provider require that.__
See : https://github.com/abergs/fido2-net-lib/pull/65

Uninstall prior msi (2.5.0.0)
install this one (2.5.4720.3001)

_Something strange on my different platforms : When Adfs service is restarted (with or without ADFSMFA), 2 new files are created in __C:\ProgramData\Microsoft\Crypto\RSA\MachineKeys__ for the SSL certificate._

_This do not appen on ADFS2012 R2 with Framework 4.8 (Wid or SQL), but this behavior is reproducible on ADFS 2016 and 2019 (Wid or SQL) with Framework 4.7.2 or 4.8. Have you experienced same behavior ?_





### 02/04/2020 / Build 2.5.4720.3000

#### Enhancements : 
- Cmdlets refactoring for __Register-MFASystem__ and __UnRegister-MFASystem__
- Refactoring and stability for Notifications Service (communications betwwen farm's servers)
- bug correction in MMC

__The future next version (.5000 ??) will include Wiki and PDF help files, more auto-completion in Cmdlets.__

__Remember that this version require that your servers have the Microsoft .Net Framework 4.7.2 up to date !!
Biometric Provider require that.__
See : https://github.com/abergs/fido2-net-lib/pull/65

Uninstall prior msi (2.5.0.0)
install this one (2.5.4720.3000)



# MFA 2.5 (January 2020) for ADFS (2019/2016/2012R2)

### Version 2.5 with Biometric authentication (WebAuthN)

### 01/30/2020 / Build 2.5.4720.2000

#### Enhancements : 
- Cmdlets refactoring for Config, DataStores, Security (eg : __Get-MFAStore -Store SQL__)
- bugs correction in MMC

#### Security
- Password encryption in configuration file

Uninstall prior msi (2.5.0.0)
install this one (2.5.4720.2000)



### 01/22/2020 / Build 2.5.4720.1000

#### Enhacements : 
- Added MMC/PS to update MFA Certificates ACLs fter deployment on other ADFS Servers
- Added cmdlet : __Update-MFACertificatesAcessControlList -CleanOrphaned__
- bug correction in MMC

Uninstall prior msi (2.5.0.0)
install this one (2.5.4720.1000)



### 01/21/2020 / Build 2.5.4720.0000

#### Enhancements : 
- Added full support of acl on certificates privatekey
- Many bug corrections in MMC
- Fully tested all possible configuration

#### Security
- Ability to configure __High Trust__ configuration, by removing roles to the ADFS Service Account (Domain Admins and/or Buildtin Admin). Will be detailled soon in documentation.

#### To DO :
- Documentation dedicated to version 2.5.

feel free to post your comments and ideas, and __star__ this project if you like it





### 01/14/2020 / Build 2.5.1.0 Beta 1

#### Enhancements : 
- Added native support of __certificates per user for ADDS and SQL configuration__
- Remove MMC node for CUSTOM certificates sample (always available and configurable with PowerShell. Sample is deprecated, in the future perhaps a new sample...)

- bug correction in sql scripts
- bug correction in MMC
- final structures of tables for SQL

#### To DO :
- more testing on different topologies and OS versions.
- Documentation dedicated to version 2.5.

#### Final release soon 

feel free to post your comments and ideas

Uninstall prior msi (2.5.0.0)
install this one (2.5.1.0000)





### 01/13/2020 / Build 2.5.0.6 Preview 6

#### Enhancements : 

- bug correction in sql scripts
- final structures of tables for SQL

Uninstall prior msi (2.5.0.0)
install this one (2.5.0.0006)





### 01/10/2020 / Build 2.5.0.5 Preview 5

#### Enhancements : 
- Bugs correction in MMC.
- Schema Templates for ADDS (MMC/PS)
- LDIF file for custom schema extension for ADDS
  _you must change the distinguished names in the file MFA.SCHEMA.LDF (DC=x to DC=mycompany,DC=com)_
  _Copy modified MFA.SCHEMA.LDF on your domain controller_
  _Deploy it with **LDIFDE -i -u -f MFA.SCHEMA.LDF**_ as domain administrator
  _Repeat operations for each domain_
  _Attributes are marked as confidential and replicable in global catalog_ 

**Modifying ADDS schema is not reversible !!!**
**Under no circumstances should you use this preliminary version in production. 
Please test this one on a lab before**

#### Next : 
- More Tests
- SQL Tables structures to be definitevely fixed.
- Documentation
- Licences in files ( https://github.com/abergs/fido2-net-lib)

Uninstall prior msi (2.5.0.0)
install this one (2.5.0.0005)





### 01/06/2020 / Build 2.5.0.4 Preview 4

#### Enhancements : 
- Fast authentication for biometrics (MMC).
  _if user wants to access options, he must cancel the first solicitation, and after he can go the manage options_
- Select ADDS templates for choosing attributes (MMC)

#### Next : 
- Custom MFA extension for ADDS schema (LDIF alteration of ADDS Schema)

Uninstall prior msi (2.5.0.0)
install this one (2.5.0.0004)





### 01/02/2020 / Build 2.5.0.3 Preview 3

#### Bugs corrections

- previously we used aaguid as the property allowing us to identify each specific record for a device, for those who looked at the wc3c specifications, it was an error, so the last update corrects this problem, now we use the public property Id of the security descriptor which guarantees uniqueness.

- We have also changed the structure of the SQL tables, to allow direct access to the Id of the descriptor, and possibly in the future the possibility (if the next version of ADFS allows it) to operate in "UserName Less" with WebAuthN. If this is the case, you must delete KEYDESCS and KEYS tables to rerun Upgrade-MFADatabase. however you will lose the different registrations made by your users.

Remember that this is a "Preview" version

__Happy New Year !__

Uninstall prior msi (2.5.0.0)
install this one (2.5.0.0003)





# **2019**



# MFA 2.5 (December 2019) for ADFS (2019/2016/2012R2)

### Version 2.5 with Biometric authentication (WebAuthN)

### 12/23/20190 / Build 2.5.0.2 Preview 2



#### Enhancements : 

- UI and Wizards

#### Next : 
- ADDS schemas customization and according Console and PowerShell

Some bugs finded, so, please wait for a quickly release 2.5.0.0003.

Uninstall prior msi (2.5.0.0)
install this one (2.5.0.0002)



### 12/17/2019 / Build 2.5.0.1 Preview 1

#### Enhancements : 
- __Biometric WebAuthN Provider__
- Console and Cmdlets

Based on https://github.com/abergs/fido2-net-lib project 
Anders Åberg and Alex Seigler (@abergs and @aseigler) and many others

Here I would like to thank everyone who has invested in this project. 
Their work is enormous and I think that a heavy personal investment had to be provided. 
Congratulations again !

### Warning

**Under no circumstances should you use this preliminary version in production. 
Please test this one on a lab, and try to validate as many devices as possible**

Uninstall prior msi (2.4.0.0)
install this one (2.5.0.0001)

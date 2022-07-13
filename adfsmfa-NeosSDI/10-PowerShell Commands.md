# PowerShell Commands

List of PowerShell commands by themes.

You can type  PS>get-help command -full view syntax.

##### Rights description

**A** - ADFS Administration rights required (System, Local Administrators, ADFS Delegated Administrators)

**S**- System Administration rights required  (System, Local Administrators)

**N** - Not Remotable

**P** - Primary Server Only

**19** - ADFS 2019/2022 Only 

### Managing Users (A)

- Get-MFAUsers (**A**)
- Set-MFAUsers (**A**)
- Add-MFAUsers (**A**)
- Remove-MFAUsers (**A**)
- Enable-MFAUsers (**A**)
- Disable-MFAUsers (**A**)
- Import-MFAUsersCSV (**A**)
- Import-MFAUsersXML (**A**)
- Import-MFAUsersADDS (**A**)
- CleanUp-MFAUsersADDS(**A**)

### Managing Configuration
- Register-MFASystem (**S, P, N**)
- Unregister-MFASystem (**S, P, N**)
- Enable-MFASystem (**S, P, N**)
- Disable-MFASystem (**S, P, N**)
- Import-MFASystemConfiguration (**S, P, N**)
- Export-MFASystemConfiguration (**S, P, N**)
- Register-MFAComputer (**S, P, N**)
- Unregister-MFAComputer (**S, P, N**)
- Register-MFASystemMasterKey (**S, P, N**)
- Register-MFASystemAESCngKey (**S, P, N**)

### Managing Computers

- Get-MFAComputers (**A**)
- Get-MFAFarmInformation (**A**)
- Restart-MFAComputerServices (**A**)
- Set-MFAFirewallRules (**A**)
- Reset-MFAThemesList (**A, P**)

### Managing Config Properties
- Get-MFAConfig (**A, P**)
- Set-MFAConfig (**A, P, N**)
- Set-MFAThemeMode (**A, P, N**)
- Set-MFAPolicyTemplate (**A, P, N**)

### Managing Providers
- Get-MFAProvider (**A, P**)
- Set-MFAProvider (**A, P, N**)
- Export-MFAMailTemplates (email provider) (**A, P, N**)
- Install-MFASamples (samples for developers) (**A, P, N**)

### Managing Storage
- Get-MFAStore (**A, P**)
- Set-MFAStore (**A, P, N**)
- Set-MFAActiveDirectoryTemplate (**A, P, N**)

- New-MFADatabase (**A, P, N**)
- Upgrade-MFADatabase (**A, P, N**)

### Managing Security
- Get-MFASecurity (**A, P**)
- Set-MFASecurity (**A, P, N**)
- Set-MFAEncryptionVersion (**A, P, N**)
- Update-MFACredentials (**A, P, N**)
- Install-MFACertificate (**A, P, N**)
- Install-MFACertificateForADFS (**A, P, N**)
- Update-MFACertificatesAccessControlList (**A, N**)
- Clear-MFAOrphanedRSAKeyPairs (**A, N**)

### Configuration Cache Management

- Refresh-MFAConfigurationCache (**A, P, N**)
- Update-MFAConfigurationCache (**A, P, N**)
- Clear-MFAConfigurationCache (**A, P, N**)

### MFA as Primary authentication method (Adfs 2019/2022)

- Set-MFAPrimaryAuthenticationStatus (adfsmfa must be removed from second factors) (**A, P, N, 19**)

### Managing Threat Detection (Adfs 2019/2022)

- Register-MFAThreatDetectionSystem (**A, P, N, 19**)
- Unregister-MFAThreatDetectionSystem (**A, P, N, 19**)
- Update-MFAThreatDetectionData (**A, P, N, 19**)
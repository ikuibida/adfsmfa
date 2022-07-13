# Localizing adfsmfa

Currently, adfsfma supports 11 languages, English (default), French, Spanish, Italian, German, Dutch, Polish, Portuguese, Swedish, Romanian, Russian, Danish and Japanese for the Web part, management console and PowerShell.

ADFS is an identification platform, so code must be as reliable as possible.
Rely on external resources not mastered is not recommended.

```
We want to be able to integrate the interface elements into resources
compiled into signed statellite assemblies,
in order to avoid any custom code insertion in the pages. changing the labels is not a problem,
but injecting javascript into resources is also possible.
```

We therefore encourage you to collaborate and send us your translations, which we will integrate without problems.



## BuiltIn Languages

List of Languages Integration in adfsmfa.

Not supported languages where translated with "Microsoft Cognitives Services" translator (so, it's not perfect ! )

Country|Culture|LCID|Support
:--------|:------:|:------:|:--------:
**English**|en|9|__Supported__
**English - US**|en-US|1033|**Supported**
**English - GB**|en-GB|2057|**Supported**
**French**|fr|12|__Supported__
**French - France**|fr-FR|1036|**Supported**
**French - Canada**|fr-CA|3084|**Supported**
**Italian**|it|1040|Pull Requests/**Supported**
**Spanish**|es|10|Pull Requests only
**Spanish - Spain**|es-ES|3082|Pull Requests only
**Spanish - Traditional**|es-ES|1034|Pull Requests only
**Spanish - Mexico**|es-MX|2058|Pull Requests only
**German**|de|7|Pull Requests/**Supported**
**German - Germany**|de-DE|1031|Pull Requests/**Supported**
**German - Austria**|de-AT|3079|Pull Requests/**Supported**
**German - Switzerland**|de-CH|2055|Pull Requests/**Supported**
**Dutch**|nl|1043|Pull Requests only
**Portuguese**|pt|22|Pull Requests only
**Portuguese - Portugal**|pt-PT|2070|Pull Requests only
**Portuguese - Brazil**|pt-BR|1046|Pull Requests only
**Swedish**|sv|1053|Pull Requests only
**Romanian**|ro|1048|Pull Requests only
**Polish**|pl|1045|Pull Requests/**Supported**
**Ukrainian**|uk|1058|Pull Requests/**Supported**
**Russian**|ru|1049|Pull Requests/**Supported**
**Danish**|da|6|Pull Requests only
**Danish - Danemark**|da-DK|1030|Pull Requests only
**Japanese**|ja|1041|Pull Requests/**Supported**
**All Others ADFS languages**|||Not Provided at this time, Pull Request only



## ADFS Supported LCID

List of Languages supported by ADFS

LCID|Country|Status|LCID|Country|Status
:----:|:--:|:---:|:----:|:--:|:---:
**1025**|ar||**1026**|bg|
**5146**|bs||**1029**|cs|
**1030**|**da**|*|**1031**|**de**| ** 
**1032**|el||**2057**|**en-GB**|**
**1033**|**en-US**| **|**1034**|**es**| *
**2058**|**es-MX**|*|**1061**|et|
**1035**|fi||**3084**|**fr-CA**|**
**1036**|**fr-FR**| **|**1037**|he|
**1050**|hr||**1038**|hu|
**1040**|**it**| **|**1041**|**ja**|**
**1042**|ko||**1063**|lt|
**1062**|lv||**1043**|**nl**| * 
**1044**|no||**1045**|**pl**| ** 
**2070**|**pt**| *|**1046**|**pt-BR**|*
**1048**|**ro**| *|**1049**|**ru**| ** 
**1051**|sk||**1060**|sl|
**2074**|sr-Latn-RS||**1053**|**sv**| *
**1054**|th||**1055**|tr|
**1058**|**uk**|*|**4100**|zh-HANS|
**1028**|zh-HANT||||

(**) Supported languages

(*) Pull Requests only



## Making your own translations

However, if you want to test changes or new languages without having to recompile the project, you can do it by providing translated resources (interface elements and mail templates).
Please follow the following.

Localizing MFA Web parts
For security reasons, and to avoid unsafe code execution.
The translation of the resources must be done by recompiling the code of the adfsmfa solution with Visual Studio and signing the code with your own key.

Remember that this is an open-source project.
If you think you have a better translation and that it can be shared with everyone. you must post a pull-request at https://github.com/neos-sdi/adfsmfa/pulls.
If these proposals are integrated, they will be maintained in the future.

You can also offer translations for unsupported languages by sending a Pull Request with modified resources files.

If you do not want or can not recompile the solution, we'll give you a simpler alternative that does not require software development skills.
We think that this is not the best solution, but it can allow you to precisely test the interface you want to offer.
Later, you will need to be able to upgrade your translations or to propose them to the community in order to integrate your work into the project.

Editing resource-based translations is only available for the Web Interface part.
The console and PowerShell do not allow resource-based translation.

How to do ?

- 1- Retrieve the complete code of the solution (version 2.3 or upper)

- 2- unpack the source code on your workstation (no development tool required).

- 3- On your adfs server move to __C:\Program Files\MFA\ResourceSet__

- 4- Copy __ResGen.exe__ on your Workstation

- 5- On Workstation

- 6- Move to the directory ..\Manage_Resources

- 7- Paste the Resgen.exe

- 8- Execute: __0-import_resources.cmd__

- 9- Execute: __1-edit_resources.cmd__

- 10- Move to the directory __.. \Manage_Resources\Sources__

- 11- With a text editor of your choice (__Notepad__, __TextPad__, __VS__, but not MSWord) modify the labels present in the different .resx files.

- 12- Execute: __2-Build_resources.cmd (ES or FR or Other)__ you can add more languages if you want

- 13- Get the __.resources__ files corresponding to the .resx files you have modified

- 14- Copy the __.resources__ files on the different ADFS servers in __C:\Program Files\MFA\ResourceSet__

- 15- Restart the ADFS services (net stop adfssrv and net start adfssrv)

- 16- Done!

  

## Removing unwanted and experimental translations

As mentioned above, many translations are performed automatically by Azure Cognitive Service. This level of translation is far from perfect.
If you want to remove a language for the UI and PS part, and switch to English by default, you can delete the satellite resource assemblies of the language concerned on your servers.

In C:\Windows\Microsoft.NET\assembly\GAC_MSIL
Neos.IdentityServer.MultiFactor.resources\ **
Neos.IdentityServer.MultiFactor.Common.resources\ **
Neos.IdentityServer.MultiFactor.Administration.resources\ **
Neos.IdentityServer.MultiFactor.Samples.resources\ **
Neos.IdentityServer.MultiFactor.WebAuthN.Provider.resources\ **

In C:\Program Files\MFA the concerned sub-directory.
In C:\Program Files\WindowsPowerShell\Modules\Neos.IdentityServer.MultiFactor.Cmdlets the concerned sub-directory.

**We still insist on having courageous contributors who can provide us with PRs containing the best translations.**

## Replacing default emails templates

You must provide an html file as an email template

**For mail templates you must provide some properties**

 - LCID for localization (1033, 1034, 1036, 3082, and more, watch for supported LCIDs)
 - FileName path to html file
 - Enabled Is the template active

>```powershell
>$c = Get-MFAProvider -ProviderType Email
>$c.MailOTP.AddTemplate(1033, "c:\yourpath\yourtemplate.html", $True)
>$c.MailOTP.RemoveTemplate(1036)
>$c.MailOTP.Templates
>Set-MFAProvider -ProviderType Email $c
>```



## Cloning default emails templates

You can clone all email templates in **Program Files\MFA\MailTemplates\LCID** for a specific Language/LCID.

> ```powershell
> Export-MFAMailTemplates -LCID 1033
> Export-MFAMailTemplates -LCID 7  # All German languages
> ```

And modify these files according your needs.



### Your html files must contains placeholders

#### TOTP Mail

>- {0} Company (string)
>- {1} User Name (string)
>- {2} Mail address (string)
>- {3} Phone number (string)
>- {4} Preferred MFA method (code, mail, sms) (string)

#### Secure Key Mail

>- {0} Company (string)
>
>- {1} User Name (string)
>
>- {2} Security Key (string)
>
>- {3} QR Code (graphic)  // Something like that in your HTML file 
>
> ```html
> <img id="qr" src="cid:{3}"/>
> ```
>
> 

#### Inscription Mail

>- {0} Company (string)
>- {1} User Name (string)
>- {2} Mail address (string)
>- {3} Phone number (string)
>- {4} Preferred MFA method (code, mail, sms) (string)

#### Notification Mail

>- {0} User Name (string)
>- {1} Company (string)


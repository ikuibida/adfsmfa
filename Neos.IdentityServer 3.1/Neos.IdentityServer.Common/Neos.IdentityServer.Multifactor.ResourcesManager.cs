﻿//******************************************************************************************************************************************************************************************//
// Copyright (c) 2022 @redhook62 (adfsmfa@gmail.com)                                                                                                                                    //                        
//                                                                                                                                                                                          //
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),                                       //
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,   //
// and to permit persons to whom the Software is furnished to do so, subject to the following conditions:                                                                                   //
//                                                                                                                                                                                          //
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.                                                           //
//                                                                                                                                                                                          //
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,                                      //
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,                            //
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                               //
//                                                                                                                                                                                          //
//                                                                                                                                                             //
// https://github.com/neos-sdi/adfsmfa                                                                                                                                                      //
//                                                                                                                                                                                          //
//******************************************************************************************************************************************************************************************//
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Neos.IdentityServer.MultiFactor.Common
{
    public class ResourcesLocale: ResourceLocaleBase
    {
        /// <summary>
        /// ResourceManager constructor
        /// </summary>
        public ResourcesLocale(int lcid): base(lcid)
        {
        }

        /// <summary>
        /// LoadResources method override
        /// </summary>
        public override void LoadResources()
        {
            ResourcesList.Add(ResourcesLocaleKind.CommonHtml, GetResourceManager(typeof(ResourcesLocale).Assembly, "Neos.IdentityServer.MultiFactor.Common.Resources.CSHtml"));
            ResourcesList.Add(ResourcesLocaleKind.CommonErrors, GetResourceManager(typeof(ResourcesLocale).Assembly, "Neos.IdentityServer.MultiFactor.Common.Resources.CSErrors"));
            ResourcesList.Add(ResourcesLocaleKind.CommonMail, GetResourceManager(typeof(ResourcesLocale).Assembly, "Neos.IdentityServer.MultiFactor.Common.Resources.CSMail"));
        }
    }
}

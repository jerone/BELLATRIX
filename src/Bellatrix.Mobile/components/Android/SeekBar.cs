﻿// <copyright file="SeekBar.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Diagnostics;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.Android;
using Bellatrix.Mobile.Events;
using Bellatrix.Mobile.Services;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android
{
    public class SeekBar : Element, IElementDisabled
    {
        public static event EventHandler<ElementActionEventArgs<AndroidElement>> SettingPercentage;
        public static event EventHandler<ElementActionEventArgs<AndroidElement>> PercentageSet;

        public virtual void Set(double percentage)
        {
            SettingPercentage?.Invoke(this, new ElementActionEventArgs<AndroidElement>(this, percentage.ToString()));
            int end = WrappedElement.Size.Width;
            int y = WrappedElement.Location.Y;
            var touchActionsService = ServicesCollection.Current.Resolve<TouchActionsService<AndroidDriver<AndroidElement>, AndroidElement>>();
            int moveTo = (int)((percentage / 100) * end);
            touchActionsService.Press(moveTo, y, 0).Release().Perform();
            PercentageSet?.Invoke(this, new ElementActionEventArgs<AndroidElement>(this, percentage.ToString()));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual bool IsDisabled => GetIsDisabled();
    }
}
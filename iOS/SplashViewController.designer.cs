// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FanReact.iOS
{
    [Register ("SplashViewController")]
    partial class SplashViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GoToFormsButton { get; set; }

        [Action ("GoToFormsButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GoToFormsButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (GoToFormsButton != null) {
                GoToFormsButton.Dispose ();
                GoToFormsButton = null;
            }
        }
    }
}
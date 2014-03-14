// Copyright 2013 The Chromium Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Design;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using VsChromiumPackage.Commands;
using VsChromiumPackage.Package;
using VsChromiumPackage.Package.CommandHandler;

namespace VsChromiumPackage.Features.ChromiumExplorer {
  [Export(typeof(IPackageCommandHandler))]
  public class ShowChromiumExplorerCommandHandler : IPackageCommandHandler {
    private readonly IVisualStudioPackageProvider _visualStudioPackageProvider;

    [ImportingConstructor]
    public ShowChromiumExplorerCommandHandler(IVisualStudioPackageProvider visualStudioPackageProvider) {
      _visualStudioPackageProvider = visualStudioPackageProvider;
    }

    public CommandID CommandId { get { return new CommandID(GuidList.GuidVsChromiumCmdSet, (int)PkgCmdIdList.CmdidChromiumExplorerToolWindow); } }

    public void Execute(object sender, EventArgs e) {
      var window = _visualStudioPackageProvider.Package.FindToolWindow(typeof(ChromiumExplorerToolWindow), 0 /*instance id*/, true /*create*/);
      if (window == null || window.Frame == null) {
        throw new NotSupportedException("Can not create \"Chromium Explorer\" tool window.");
      }
      var windowFrame = (IVsWindowFrame)window.Frame;
      ErrorHandler.ThrowOnFailure(windowFrame.Show());
    }
  }
}

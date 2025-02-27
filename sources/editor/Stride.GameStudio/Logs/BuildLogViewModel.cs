// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using Stride.Core.BuildEngine;
using Stride.Core.Diagnostics;
using Stride.Core.Presentation.ViewModel;

namespace Stride.GameStudio.Logs
{
    public sealed class BuildLogViewModel : LoggerViewModel, IForwardSerializableLogRemote
    {
        public BuildLogViewModel(IViewModelServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
        
        public void ForwardSerializableLog(SerializableLogMessage message)
        {
            foreach (var logger in Loggers.Keys)
            {
                logger.Log(message);
            }
        }

        /// <inheritdoc/>
        public override void Destroy()
        {
            base.Destroy();
        }
    }
}

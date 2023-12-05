// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.Runtime.CompilerServices;
using Stride.Core;
using Stride.Core.Diagnostics;
using Stride.Core.Mathematics;
using Stride.Core.Threading;
using Stride.Rendering.Materials;

namespace Stride.Rendering
{
    /// <summary>
    /// Computes and uploads skinning info.
    /// </summary>
    public class BlendShapeRenderFeature : SubRenderFeature
    {
        private StaticObjectPropertyKey<RenderEffect> renderEffectKey;



        /// <inheritdoc/>
        protected override void InitializeCore()
        {
          //  renderEffectKey = ((RootEffectRenderFeature)RootRenderFeature).RenderEffectKey;
        }

        /// <inheritdoc/>
        public override void PrepareEffectPermutations(RenderDrawContext context)
        {
   //         var renderEffects = RootRenderFeature.RenderData.GetData(renderEffectKey);
            int effectSlotCount = ((RootEffectRenderFeature)RootRenderFeature).EffectPermutationSlotCount;

            Dispatcher.ForEach(((RootEffectRenderFeature)RootRenderFeature).ObjectNodeReferences, objectNodeReference =>
            {
                var objectNode = RootRenderFeature.GetObjectNode(objectNodeReference);
                var renderMesh = (RenderMesh)objectNode.RenderObject;
                var modelData = ((RenderMesh)objectNode.RenderObject).RenderModel?.Model;
                var staticObjectNode = renderMesh.StaticObjectNode;
                if (modelData.blendShapes != null)
                {
                }
            });
        }

        /// <inheritdoc/>
        public override void Extract()
        {
        }

        /// <inheritdoc/>
        public override unsafe void Prepare(RenderDrawContext context)
        {
           
        }
    }
}

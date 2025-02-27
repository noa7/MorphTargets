// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Stride.Core;
using Stride.Core.Annotations;
using Stride.Core.Collections;
using Stride.Core.Mathematics;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Contents;
using Stride.Graphics;
using Stride.Graphics.Data;
using Buffer = Stride.Graphics.Buffer;

namespace Stride.Rendering
{
    /// <summary>
    /// Collection of <see cref="Mesh"/>, each one usually being a different LOD of the same Model.
    /// The effect system will select the appropriate LOD depending on distance, current pass, and other effect-specific requirements.
    /// </summary>
    [ReferenceSerializer, DataSerializerGlobal(typeof(ReferenceSerializer<Model>), Profile = "Content")]
    [ContentSerializer(typeof(DataContentSerializer<Model>))]
    [DataContract]
    public class Model : IEnumerable
    {
        private Model parent;

        /// <summary>
        /// Gets the views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        [MemberCollection(NotNullItems = true)]
        public IList<Model> Children { get; set; }

        /// <summary>
        /// Gets the materials.
        /// </summary>
        /// <value>
        /// The materials.
        /// </value>
        [MemberCollection(NotNullItems = true)]
        public List<MaterialInstance> Materials { get; } = new();

        /// <summary>
        /// Gets the meshes.
        /// </summary>
        /// <value>
        /// The meshes.
        /// </value>
        [MemberCollection(NotNullItems = true)]
        public List<Mesh> Meshes { get; set; } = new();

        /// <summary>
        /// Gets or sets the hierarchy definition, which describes nodes name, default transformation and hierarchical parent.
        /// </summary>
        /// <value>
        /// The hierarchy, which describes nodes name, default transformation and hierarchical parent.
        /// </value>
        public Skeleton Skeleton { get; set; }

        /// <summary>
        /// Gets or sets the bounding box encompassing all the <see cref="Meshes"/> (not including animation).
        /// </summary>
        /// <value>
        /// The bounding box.
        /// </value>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the bounding sphere encompassing all the <see cref="Meshes"/> (not including animation).
        /// </summary>
        /// <value>The bounding sphere.</value>
        public BoundingSphere BoundingSphere { get; set; }

        // Temporarily removed
        //[DataMemberConvert]
        //public ParameterCollection Parameters
        //{
        //    get { return parameters; }
        //}

        /// <summary>
        /// Adds the specified model view (for collection Initializers).
        /// </summary>
        /// <param name="model">The model view.</param>
        public void Add(Model model)
        {
            Children.Add(model);
        }

        /// <summary>
        /// Adds the specified mesh (for collection Initializers).
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        public void Add(Mesh mesh)
        {
            Meshes.Add(mesh);
        }

        /// <summary>
        /// Adds the specified material (for collection Initializers).
        /// </summary>
        /// <param name="material">The mesh.</param>
        public void Add(MaterialInstance material)
        {
            Materials.Add(material);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Meshes.Cast<object>().Concat(Materials).GetEnumerator();
        }

        /// <summary>
        /// Create a clone with its own ParameterCollection.
        /// It allows reuse of a single Model for multiple ModelComponent.
        /// </summary>
        public Model Instantiate()
        {
            var result = new Model();
            if (Children != null)
            {
                result.Children = new List<Model>();
                foreach (var child in Children)
                {
                    result.Children.Add(child.Instantiate());
                }
            }

            foreach (var mesh in Meshes)
            {
                var meshCopy = new Mesh(mesh);
                result.Meshes.Add(meshCopy);
            }

            result.Skeleton = Skeleton;
            result.BoundingBox = BoundingBox;

            return result;
        }

        

        private void Children_CollectionChanged(object sender, TrackingCollectionChangedEventArgs e)
        {
            var child = (Model)e.Item;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (child.parent != null)
                        throw new InvalidOperationException("Model already have a parent.");
                    child.parent = this;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (child.parent != this)
                        throw new InvalidOperationException("Model doesn't have expected parent.");
                    child.parent = null;
                    break;
            }
        }

       public List<BlendShape> blendShapes { get; set; }

        public void AddBlendShape(BlendShape blendShape)
        {
            (blendShapes??=new()).Add(blendShape);
        }
    }
}

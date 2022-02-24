using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umbraco.Cms.Core.Composing
{
    public abstract class InstancesOrderedCollectionBuilderBase<TBuilder, TCollection, TItem> : OrderedCollectionBuilderBase<TBuilder, TCollection, TItem>
        where TBuilder : OrderedCollectionBuilderBase<TBuilder, TCollection, TItem>
        where TCollection : class, IBuilderCollection<TItem>
    {
        private readonly List<TItem> _instances = new List<TItem>();
        private readonly object _locker = new object();
        private TItem[] _registeredInstances;

        public virtual IList<TItem> Instances { get; }

        protected override IEnumerable<TItem> CreateItems(IServiceProvider factory)
        {
            IEnumerable<TItem> factoryInstances = base.CreateItems(factory);
            return factoryInstances.Union(Instances);
        }

        /// <summary>
        /// Configures the internal list of instances.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <remarks>Throws if the instances have already been registered.</remarks>
        protected void ConfigureInstance(Action<List<TItem>> action)
        {
            lock (_locker)
            {
                if (_registeredInstances != null)
                    throw new InvalidOperationException("Cannot configure a collection builder after it has been registered.");
                action(_instances);
            }
        }

        /// <summary>
        /// Clears all instances in the collection.
        /// </summary>
        /// <returns>The builder.</returns>
        public TBuilder ClearInstances()
        {
            ConfigureInstance(instances => instances.Clear());
            return This;
        }

        /// <summary>
        /// Appends a instance to the collection.
        /// </summary>
        /// <typeparam name="instance">The instance to append.</typeparam>
        /// <returns>The builder.</returns>
        public TBuilder AppendInstance(TItem instance)
        {
            ConfigureInstance(instances =>
            {
                if (instances.Contains(instance))
                    instances.Remove(instance);
                instances.Add(instance);
            });
            return This;
        }


        /// <summary>
        ///  Appends instances to the collections.
        /// </summary>
        /// <param name="instances">The instances to append.</param>
        /// <returns>The builder.</returns>
        public TBuilder AppendInstance(IEnumerable<TItem> instances)
        {
            ConfigureInstance(list =>
            {
                foreach (var instance in instances)
                {
                    // would be detected by CollectionBuilderBase when registering, anyways, but let's fail fast
                    if (list.Contains(instance))
                        list.Remove(instance);
                    list.Add(instance);
                }
            });
            return This;
        }

        /// <summary>
        /// Inserts a instance into the collection.
        /// </summary>
        /// <typeparam name="TItem">The instance to insert.</typeparam>
        /// <param name="index">The optional index.</param>
        /// <returns>The builder.</returns>
        /// <remarks>Throws if the index is out of range.</remarks>
        public TBuilder InsertInstance(TItem instance, int index = 0)
        {
            ConfigureInstance(instances =>
            {
                if (instances.Contains(instance))
                    instances.Remove(instance);
                instances.Insert(index, instance);
            });
            return This;
        }

        /// <summary>
        /// Inserts a instance into the collection.
        /// </summary>
        /// <param name="instance">The instance to insert.</param>
        /// <returns>The builder.</returns>
        /// <remarks>Throws if the index is out of range.</remarks>
        public TBuilder InsertInstance(TItem instance)
        {
            return InsertInstance(0, instance);
        }

        /// <summary>
        /// Inserts a instance into the collection.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="instance">The instance to insert.</param>
        /// <returns>The builder.</returns>
        /// <remarks>Throws if the index is out of range.</remarks>
        public TBuilder InsertInstance(int index, TItem instance)
        {
            ConfigureInstance(instances =>
            {
                if (instances.Contains(instance))
                    instances.Remove(instance);
                instances.Insert(index, instance);
            });
            return This;
        }

        /// <summary>
        /// Inserts a instance before another instance.
        /// </summary>
        /// <typeparam name="TBefore">The other instance.</typeparam>
        /// <typeparam name="TItem">The instance to insert.</typeparam>
        /// <returns>The builder.</returns>
        /// <remarks>Throws if both instances are identical, or if the other instance does not already belong to the collection.</remarks>
        public TBuilder InsertBeforeInstance(TItem instanceBefore, TItem instance)
        {
            ConfigureInstance(instances =>
            {
                if (object.ReferenceEquals(instanceBefore, instance))
                    throw new InvalidOperationException();

                var index = instances.IndexOf(instanceBefore);
                if (index < 0)
                    throw new InvalidOperationException();

                if (instances.Contains(instance))
                    instances.Remove(instance);
                index = instances.IndexOf(instanceBefore); // in case removing instance changed index
                instances.Insert(index, instance);
            });
            return This;
        }


        /// <summary>
        /// Inserts a instance after another instance.
        /// </summary>
        /// <typeparam name="TAfter">The other instance.</typeparam>
        /// <typeparam name="TItem">The instance to append.</typeparam>
        /// <returns>The builder.</returns>
        /// <remarks>Throws if both instances are identical, or if the other instance does not already belong to the collection.</remarks>
        public TBuilder InsertAfterInstance(TItem instanceAfter, TItem instance)
        {
            ConfigureInstance(instances =>
            {
                if (object.ReferenceEquals(instanceAfter, instance))
                    throw new InvalidOperationException();

                var index = instances.IndexOf(instanceAfter);
                if (index < 0)
                    throw new InvalidOperationException();

                if (instances.Contains(instance))
                    instances.Remove(instance);
                index = instances.IndexOf(instanceAfter); // in case removing instance changed index
                index += 1; // insert here

                if (index == instances.Count)
                    instances.Add(instance);
                else
                    instances.Insert(index, instance);
            });
            return This;
        }


        /// <summary>
        /// Removes a instance from the collection.
        /// </summary>
        /// <typeparam name="TItem">The instance to remove.</typeparam>
        /// <returns>The builder.</returns>
        public TBuilder RemoveInstance(TItem instance)
        {
            ConfigureInstance(instances =>
            {
                if (instances.Contains(instance))
                    instances.Remove(instance);
            });
            return This;
        }


        /// <summary>
        /// Replaces a instance in the collection.
        /// </summary>
        /// <typeparam name="TReplaced">The instance to replace.</typeparam>
        /// <typeparam name="TItem">The instance to insert.</typeparam>
        /// <returns>The builder.</returns>
        /// <remarks>Throws if the instance to replace does not already belong to the collection.</remarks>
        public TBuilder ReplaceInstance(TItem instanceReplaced, TItem instance)
        {
            ConfigureInstance(instances =>
            {
                if (object.ReferenceEquals(instanceReplaced, instance))
                    return;

                var index = instances.IndexOf(instanceReplaced);
                if (index < 0)
                    throw new InvalidOperationException();

                if (instances.Contains(instance))
                    instances.Remove(instance);
                index = instances.IndexOf(instanceReplaced); // in case removing instance changed index
                instances.Insert(index, instance);
                instances.Remove(instanceReplaced);
            });
            return This;
        }

    }
}

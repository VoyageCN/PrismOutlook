﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml.Schema;
using Prism.Ioc;
using Prism.Regions;
using Unity;

namespace PrismOutlook.Core.Regions
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        public const string BehaviorKey = "DependentViewRegionBehavior";
        private readonly IContainerProvider _container;

        private Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new Dictionary<object, List<DependentViewInfo>>();

        public DependentViewRegionBehavior(IContainerProvider container)
        {
            _container = container;
        }

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newView in e.NewItems)
                {
                    var dependentViews = new List<DependentViewInfo>();

                    if (_dependentViewCache.ContainsKey(newView))
                    {
                        dependentViews = _dependentViewCache[newView];
                    }
                    else
                    {
                        // get the attributes
                        var attrs = GetCustomAttributes<DependentViewAttribute>(newView.GetType());

                        foreach (var attr in attrs)
                        {
                            var info = CreateDependentViewInfo(attr);

                            if (info.View is ISupportDataContext infoDC && newView is ISupportDataContext viewDC)
                            {
                                infoDC.DataContext = viewDC.DataContext;
                            }

                            if (info.View is ISupportRichText infoRT && newView is ISupportRichText viewRT)
                            {
                                infoRT.RichTextEditor = viewRT.RichTextEditor;
                            }

                            dependentViews.Add(info);
                        }
                        _dependentViewCache.Add(newView, dependentViews);
                    }

                    dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Add(item.View));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove) 
            {
                foreach (var oldView in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldView))
                    {
                        var dependentViews = _dependentViewCache[oldView];
                        dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Remove(item.View));

                        if (!ShouldKeepAlive(oldView))
                            _dependentViewCache.Remove(oldView);
                    }
                }

            }
        }

        private bool ShouldKeepAlive(object oldView)
        {
            var regionLifetime = GetViewOrDataContextLifeTime(oldView);
            if (regionLifetime != null)
                return regionLifetime.KeepAlive;

            return true;
        }

        IRegionMemberLifetime GetViewOrDataContextLifeTime(object view)
        {
            if (view is IRegionMemberLifetime regionLifetime)
                return regionLifetime;

            if (view is FrameworkElement fe)
                return fe.DataContext as IRegionMemberLifetime;

            return null;
        }

        DependentViewInfo CreateDependentViewInfo(DependentViewAttribute attribute)
        {
            var info = new DependentViewInfo();
            info.Region = attribute.Region;

            // create the view instance
            info.View = _container.Resolve(attribute.Type);

            return info; 
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }

}

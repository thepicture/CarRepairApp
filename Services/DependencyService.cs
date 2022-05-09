using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRepairApp
{
    public static class DependencyService
    {
        private static readonly Dictionary<Type, object> dependencies =
            new Dictionary<Type, object>();
        public static void Register<TTarget>()
        {
            dependencies.Add(typeof(TTarget),
                             Activator.CreateInstance<TTarget>());
        }

        public static TTarget Get<TTarget>()
        {
            return (TTarget)dependencies.First(d =>
            {
                return typeof(TTarget).IsAssignableFrom(d.Key);
            }).Value;
        }
    }
}

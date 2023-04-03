using System.Collections.Concurrent;
using WebTerminalsServer.Services;

namespace WebTerminalsServer.Logic
{
    public class LegFactory<T> where T : Leg
    {
        private static readonly ConcurrentDictionary<string, Lazy<T>> instances = new ConcurrentDictionary<string, Lazy<T>>();

        public static T GetInstance(IAirPortRepository service)
        {
            var typeName = typeof(T).Name;
            if (instances.ContainsKey(typeName))
            {
                if (instances[typeName] != null)
                    return instances[typeName].Value;
            }
            return instances.GetOrAdd(typeName, new Lazy<T>(() => (T)Activator.CreateInstance(typeof(T), service), LazyThreadSafetyMode.ExecutionAndPublication)).Value;
        }

        public static T GetInstance()
        {
            var typeName = typeof(T).Name;
            if (instances.ContainsKey(typeName))
            {
                if (instances[typeName] != null)
                    return instances[typeName].Value;
            }
            return instances.GetOrAdd(typeName, new Lazy<T>(() => (T)Activator.CreateInstance(typeof(T)), LazyThreadSafetyMode.ExecutionAndPublication)).Value;
        }
    }
}
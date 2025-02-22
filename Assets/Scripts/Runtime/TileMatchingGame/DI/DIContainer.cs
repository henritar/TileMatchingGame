using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.DI
{
    public class DIContainer
    {
        public static DIContainer Instance { get; } = new DIContainer();

        private readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
        private readonly Dictionary<Type, Func<object>> _transients = new Dictionary<Type, Func<object>>();

        public enum RegistrationType { Singleton, Transient }

        public void Register<TInterface, TImplementation>(RegistrationType registrationType, Func<object> factory = null)
            where TImplementation : TInterface
        {
            if (registrationType == RegistrationType.Singleton)
            {
                _singletons[typeof(TInterface)] = factory != null ? factory() : Activator.CreateInstance<TImplementation>();
            }
            else
            {
                _transients[typeof(TInterface)] = factory ?? (() => Activator.CreateInstance<TImplementation>());
            }
        }

        public void Register<T>(RegistrationType registrationType, T instance)
        {
            if (registrationType == RegistrationType.Singleton)
            {
                _singletons[typeof(T)] = instance;
            }
        }

        public T Resolve<T>()
        {
            Type type = typeof(T);

            if (_singletons.ContainsKey(type))
            {
                return (T)_singletons[type];
            }
            else if (_transients.ContainsKey(type))
            {
                return (T)_transients[type]();
            }
            else
            {
                throw new Exception($"Dependency not registered: {type}");
            }
        }
    }
}
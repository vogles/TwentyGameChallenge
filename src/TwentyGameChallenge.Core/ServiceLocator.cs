using System;
using System.Collections.Generic;

namespace TwentyGameChallenge.Core;

public class ServiceLocator : IServiceLocator
    {
        public static ServiceLocator Instance { get; } = new ServiceLocator();

        private ServiceLocator() { }
        
        private readonly Dictionary<string, object> _instances = new Dictionary<string, object>();
        
        public IServiceLocator RegisterInstance<TInterface, TImplementation>() where TImplementation : TInterface, new()
        {
            var typename = typeof(TInterface).Name;
            return RegisterInstance(typename, new TImplementation());
        }

        public IServiceLocator RegisterInstance<T>(T instance)
        {
            var typename = typeof(T).Name;
            return RegisterInstance(typename, instance);
        }

        public IServiceLocator RegisterInstance<T>() where T : new()
        {
            return RegisterInstance<T, T>();
        }
        
        public ServiceLocator RegisterInstance<TInterface>(string key, TInterface instance)
        {
            if (_instances.ContainsKey(key))
                throw new ArgumentException($"There is already an instance of {key} registered");

            _instances[key] = instance;

            return this;
        }
        
        public object GetService(Type serviceType)
        {
            return GetInstance(serviceType, serviceType.Name);
        }

        public object GetInstance(Type serviceType)
        {
            var typename = serviceType.Name;
            return GetInstance(serviceType, typename);
        }

        public object GetInstance(Type instanceType, string key)
        {
            if (_instances.ContainsKey(key))
                throw new ArgumentException($"{key} has not been registered.");

            var instance = _instances[key];

            if (!instanceType.IsInstanceOfType(instance))
                throw new ArgumentException($"The service with {key} is not of type {instanceType.Name}.");
        
            return instance;
        }

        public T GetInstance<T>()
        {
            return GetInstance<T>(typeof(T).Name);
        }

        public T GetInstance<T>(string key)
        {
            return (T)GetInstance(typeof(T), key);
        }
        
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            throw new NotImplementedException();
        }
    }
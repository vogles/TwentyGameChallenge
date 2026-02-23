using System;
using System.Collections.Generic;

namespace TwentyGameChallenge.Core;

public interface IServiceLocator : IServiceProvider
{
    object GetInstance(Type serviceType);
    object GetInstance(Type instanceType, string key);
    IEnumerable<object> GetAllInstances(Type serviceType);
    T GetInstance<T>();
    T GetInstance<T>(string key);
    IEnumerable<T> GetAllInstances<T>();
}
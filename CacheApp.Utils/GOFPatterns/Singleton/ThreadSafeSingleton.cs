namespace CacheApp.Utils.GOFPatterns.Singleton;

using System;

public sealed class ThreadSafeSingleton
{
    private static ThreadSafeSingleton _instance;
    private static readonly object _lock = new();

    private ThreadSafeSingleton() { }

    public static ThreadSafeSingleton GetInstance(string value)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ThreadSafeSingleton();
                    _instance.Value = value;
                }
            }
        }

        return _instance;
    }

    public string Value { get; private set; }
}

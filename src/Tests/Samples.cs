using Tynamix.ObjectFiller;

namespace Skillz.Tests;

public static class Samples
{
    public static T CreateSample<T>() where T : class
    {
        return CreateSample<T>(_ => { });
    }
    
    public static T CreateSample<T>(Action<T> initializer) where T : class
    {
        var sample = new Filler<T>().Create();
        initializer?.Invoke(sample);
        return sample;
    }
}
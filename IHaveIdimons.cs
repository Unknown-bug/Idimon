using SplashKitSDK;

namespace Idimon
{
    public interface IHaveIdimons
    {
        string Name { get; }
        Idimons Idimon { get; }
    }
}
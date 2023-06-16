using VContainer;
using VContainer.Unity;

public class GameControllerLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(this.GetComponent<GameControllerModel>()).AsImplementedInterfaces();
    }
}

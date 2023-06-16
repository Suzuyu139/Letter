using VContainer;
using VContainer.Unity;

public class SpawnLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(this.GetComponent<SpawnModel>()).AsImplementedInterfaces();
        builder.RegisterComponent(this.GetComponentInChildren<SpawnView>()).AsImplementedInterfaces();
    }
}

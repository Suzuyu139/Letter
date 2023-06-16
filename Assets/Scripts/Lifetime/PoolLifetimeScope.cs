using VContainer;
using VContainer.Unity;

public class PoolLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(this.GetComponent<PoolModel>()).AsImplementedInterfaces();
    }
}

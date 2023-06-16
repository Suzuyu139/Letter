using VContainer;
using VContainer.Unity;

public class ItemLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(this.GetComponent<ItemModel>()).AsImplementedInterfaces();
    }
}

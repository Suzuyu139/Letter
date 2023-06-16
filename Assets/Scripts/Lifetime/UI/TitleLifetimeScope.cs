using VContainer;
using VContainer.Unity;

public class TitleLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(this.GetComponent<TitleModel>()).AsImplementedInterfaces();
        builder.RegisterComponent(this.GetComponent<TitleView>()).AsImplementedInterfaces();
    }
}

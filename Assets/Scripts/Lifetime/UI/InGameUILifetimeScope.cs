using VContainer;
using VContainer.Unity;

public class InGameUILifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(this.GetComponent<InGameUIModel>()).AsImplementedInterfaces();
        builder.RegisterComponent(this.GetComponent<InGameUIView>()).AsImplementedInterfaces();

        builder.RegisterComponent(this.GetComponentInChildren<PlayerUIModel>()).AsImplementedInterfaces();
        builder.RegisterComponent(this.GetComponentInChildren<PlayerUIPresenter>()).AsImplementedInterfaces();
        builder.RegisterComponent(this.GetComponentInChildren<PlayerUIView>()).AsImplementedInterfaces();

        builder.RegisterComponent(this.GetComponentInChildren<PlayerEquipListUIModel>()).AsImplementedInterfaces();
        builder.RegisterComponent(this.GetComponentInChildren<PlayerEquipListUIPresenter>()).AsImplementedInterfaces();
        builder.RegisterComponent(this.GetComponentInChildren<PlayerEquipListUIView>()).AsImplementedInterfaces();

        builder.RegisterComponent(this.GetComponentInChildren<ItemRenderTextureView>()).AsImplementedInterfaces();
    }
}

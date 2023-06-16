using VContainer;
using VContainer.Unity;

public class PlayerLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(this.GetComponent<PlayerModel>()).AsImplementedInterfaces();

        builder.RegisterComponent(this.GetComponentInChildren<PlayerMoveView>()).AsImplementedInterfaces();

        builder.RegisterComponent(this.GetComponentInChildren<PlayerViewpointView>()).AsImplementedInterfaces();

        builder.RegisterComponent(this.GetComponentInChildren<PlayerActionCollisionView>()).AsImplementedInterfaces();

        builder.RegisterComponent(this.GetComponentInChildren<PlayerEquipView>()).AsImplementedInterfaces();
    }
}

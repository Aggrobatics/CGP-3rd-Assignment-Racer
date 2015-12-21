
namespace GDLibrary
{
    public enum ActorType : sbyte
    {
        Camera,
        Player,
        PlayerAnimated,
        Bullet,
        EnemyOne,
        EnemyTwo,
        StaticPickup,
        AnimatedPickup, //we need to discriminate so we can cast controller as AnimatedPickupController to access its Value property
        Platform,
        PlatformMoveable,
        Props,  //tree
        Decorator, //background clouds
        Death
    }
}

namespace Blogs.BL.Abstractions
{
    public interface IAddEntityUoW<Entity> : ISingleEntityUoW<Entity> where Entity : class
    {
        void PerformAction(Entity item);
    }
}

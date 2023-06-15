namespace HW4.Task2
{
    interface IEntity
    {
        string Id { get; }
    }

    abstract class Entity : IEntity
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }
}

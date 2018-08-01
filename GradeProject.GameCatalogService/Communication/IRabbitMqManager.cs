namespace GradeProject.GameCatalogService
{
    public interface IRabbitMqManager
    {
        void Publish(string queueName, string data);
    }
}

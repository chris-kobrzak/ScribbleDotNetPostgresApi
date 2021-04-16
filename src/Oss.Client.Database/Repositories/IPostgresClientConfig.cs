namespace Oss.Client.Database
{
    public interface IPostgresClientConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
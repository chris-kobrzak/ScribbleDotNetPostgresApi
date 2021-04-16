namespace Oss.Client.Database
{
    public class PostgresClientConfig : IPostgresClientConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
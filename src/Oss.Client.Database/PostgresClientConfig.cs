namespace Oss.Client.Database
{
    public class PostgresClientConfig : IPostgresClientConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
namespace Database.EF
{
    public class SqlSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string MigrationInfo { get; set; }
        public bool InMemory { get; set; }
    }
}

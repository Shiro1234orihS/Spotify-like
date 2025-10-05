namespace back.Configuration
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string SongsCollectionName { get; set; } = null!;
        public string PlaylistsCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string ArtistsCollectionName { get; set; } = null!;
    }
}

using MongoDB.Driver;
using back.Models;
using back.Configuration;
using Microsoft.Extensions.Options;

namespace back.Services
{
    public class DatabaseSeeder
    {
        private readonly IMongoCollection<Song> _songsCollection;
        private readonly IMongoCollection<Playlist> _playlistsCollection;
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<Artist> _artistsCollection;

        public DatabaseSeeder(IOptions<MongoDbSettings> settings, MongoDbService mongoDbService)
        {
            _songsCollection = mongoDbService.GetCollection<Song>(settings.Value.SongsCollectionName);
            _playlistsCollection = mongoDbService.GetCollection<Playlist>(settings.Value.PlaylistsCollectionName);
            _usersCollection = mongoDbService.GetCollection<User>(settings.Value.UsersCollectionName);
            _artistsCollection = mongoDbService.GetCollection<Artist>(settings.Value.ArtistsCollectionName);
        }

        public async Task SeedAsync()
        {
            var songsCount = await _songsCollection.CountDocumentsAsync(_ => true);
            if (songsCount > 0)
            {
                Console.WriteLine("⚠️  La base contient déjà des données.");
                return;
            }

            Console.WriteLine("🌱 Remplissage de la base de données...");

            var users = await SeedUsersAsync();
            Console.WriteLine($"✅ {users.Count} utilisateurs créés");

            var artists = await SeedArtistsAsync();
            Console.WriteLine($"✅ {artists.Count} artistes créés");

            var songs = await SeedSongsAsync(artists);
            Console.WriteLine($"✅ {songs.Count} chansons créées");

            var playlists = await SeedPlaylistsAsync(users, songs);
            Console.WriteLine($"✅ {playlists.Count} playlists créées");

            Console.WriteLine("🎉 Base de données remplie avec succès !");
        }

        private async Task<List<User>> SeedUsersAsync()
        {
            var users = new List<User>
            {
                new User
                {
                    Username = "john_doe",
                    Email = "john@example.com",
                    DisplayName = "John Doe",
                    AvatarUrl = "https://i.pravatar.cc/150?img=12",
                    IsPremium = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-6),
                    LastLogin = DateTime.UtcNow
                },
                new User
                {
                    Username = "jane_smith",
                    Email = "jane@example.com",
                    DisplayName = "Jane Smith",
                    AvatarUrl = "https://i.pravatar.cc/150?img=47",
                    IsPremium = false,
                    CreatedAt = DateTime.UtcNow.AddMonths(-3),
                    LastLogin = DateTime.UtcNow
                },
                new User
                {
                    Username = "music_lover",
                    Email = "music@example.com",
                    DisplayName = "Music Lover",
                    AvatarUrl = "https://i.pravatar.cc/150?img=33",
                    IsPremium = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-10),
                    LastLogin = DateTime.UtcNow
                },
                new User
                {
                    Username = "dj_master",
                    Email = "dj@example.com",
                    DisplayName = "DJ Master",
                    AvatarUrl = "https://i.pravatar.cc/150?img=68",
                    IsPremium = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    LastLogin = DateTime.UtcNow
                }
            };

            await _usersCollection.InsertManyAsync(users);
            return users;
        }

        private async Task<List<Artist>> SeedArtistsAsync()
        {
            var artists = new List<Artist>
            {
                new Artist
                {
                    Name = "Queen",
                    Bio = "Groupe de rock britannique légendaire formé en 1970",
                    Country = "United Kingdom",
                    Genre = "Rock",
                    ImageUrl = "https://i.scdn.co/image/b040846ceba13c3e9c125d68389491094e7f2982",
                    Verified = true,
                    MonthlyListeners = 49500000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Artist
                {
                    Name = "The Weeknd",
                    Bio = "Artiste canadien de R&B et pop",
                    Country = "Canada",
                    Genre = "Pop",
                    ImageUrl = "https://i.scdn.co/image/ab6761610000e5eb214f3cf1cbe7139c1e26ffbb",
                    Verified = true,
                    MonthlyListeners = 107000000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Artist
                {
                    Name = "Eminem",
                    Bio = "Rappeur américain emblématique",
                    Country = "United States",
                    Genre = "Hip-Hop",
                    ImageUrl = "https://i.scdn.co/image/ab6761610000e5eba00b11c129b27a88fc72f36b",
                    Verified = true,
                    MonthlyListeners = 77000000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Artist
                {
                    Name = "Daft Punk",
                    Bio = "Duo électronique français révolutionnaire",
                    Country = "France",
                    Genre = "Electronic",
                    ImageUrl = "https://i.scdn.co/image/ab6761610000e5eb0c68f6c95232e716f0abee8d",
                    Verified = true,
                    MonthlyListeners = 31000000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Artist
                {
                    Name = "Ed Sheeran",
                    Bio = "Auteur-compositeur-interprète britannique",
                    Country = "United Kingdom",
                    Genre = "Pop",
                    ImageUrl = "https://i.scdn.co/image/ab6761610000e5eb7a4e8c0f4f3b8c8e3c3e9e3c",
                    Verified = true,
                    MonthlyListeners = 85000000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Artist
                {
                    Name = "Kendrick Lamar",
                    Bio = "Rappeur et producteur américain, lauréat du prix Pulitzer",
                    Country = "United States",
                    Genre = "Hip-Hop",
                    ImageUrl = "https://i.scdn.co/image/ab6761610000e5eb437b9e2a82505b3d93ff1022",
                    Verified = true,
                    MonthlyListeners = 70000000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Artist
                {
                    Name = "Bob Marley",
                    Bio = "Icône du reggae jamaïcain",
                    Country = "Jamaica",
                    Genre = "Reggae",
                    ImageUrl = "https://i.scdn.co/image/ab6761610000e5eb8c1d9e6e8c8e8e8e8e8e8e8e",
                    Verified = true,
                    MonthlyListeners = 42000000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Artist
                {
                    Name = "Radiohead",
                    Bio = "Groupe de rock alternatif britannique",
                    Country = "United Kingdom",
                    Genre = "Alternative",
                    ImageUrl = "https://i.scdn.co/image/ab6761610000e5eba03696716c9ee605006047fd",
                    Verified = true,
                    MonthlyListeners = 18000000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            await _artistsCollection.InsertManyAsync(artists);
            return artists;
        }

        private async Task<List<Song>> SeedSongsAsync(List<Artist> artists)
        {
            var queen = artists.First(a => a.Name == "Queen");
            var weeknd = artists.First(a => a.Name == "The Weeknd");
            var eminem = artists.First(a => a.Name == "Eminem");
            var daftpunk = artists.First(a => a.Name == "Daft Punk");
            var edsheeran = artists.First(a => a.Name == "Ed Sheeran");
            var kendrick = artists.First(a => a.Name == "Kendrick Lamar");

            var songs = new List<Song>
            {
                // Queen
                new Song
                {
                    Title = "Bohemian Rhapsody",
                    Artist = "Queen",
                    ArtistId = queen.Id,
                    Album = "A Night at the Opera",
                    Duration = 354,
                    Genre = "Rock",
                    ReleaseDate = new DateTime(1975, 10, 31),
                    AudioUrl = "https://example.com/audio/bohemian-rhapsody.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/4/4d/Queen_A_Night_At_The_Opera.png",
                    Plays = 1250000000,
                    Likes = 45000000,
                    CreatedAt = DateTime.UtcNow
                },
                new Song
                {
                    Title = "We Will Rock You",
                    Artist = "Queen",
                    ArtistId = queen.Id,
                    Album = "News of the World",
                    Duration = 122,
                    Genre = "Rock",
                    ReleaseDate = new DateTime(1977, 10, 7),
                    AudioUrl = "https://example.com/audio/we-will-rock-you.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/3/3c/Queen_News_Of_The_World.png",
                    Plays = 980000000,
                    Likes = 32000000,
                    CreatedAt = DateTime.UtcNow
                },
                new Song
                {
                    Title = "Don't Stop Me Now",
                    Artist = "Queen",
                    ArtistId = queen.Id,
                    Album = "Jazz",
                    Duration = 209,
                    Genre = "Rock",
                    ReleaseDate = new DateTime(1978, 11, 10),
                    AudioUrl = "https://example.com/audio/dont-stop-me-now.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/0/06/Queen_Jazz.png",
                    Plays = 875000000,
                    Likes = 28000000,
                    CreatedAt = DateTime.UtcNow
                },

                // The Weeknd
                new Song
                {
                    Title = "Blinding Lights",
                    Artist = "The Weeknd",
                    ArtistId = weeknd.Id,
                    Album = "After Hours",
                    Duration = 200,
                    Genre = "Pop",
                    ReleaseDate = new DateTime(2019, 11, 29),
                    AudioUrl = "https://example.com/audio/blinding-lights.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Weeknd_-_After_Hours.png",
                    Plays = 3800000000,
                    Likes = 95000000,
                    CreatedAt = DateTime.UtcNow
                },
                new Song
                {
                    Title = "Starboy",
                    Artist = "The Weeknd",
                    ArtistId = weeknd.Id,
                    Album = "Starboy",
                    Duration = 230,
                    Genre = "Pop",
                    ReleaseDate = new DateTime(2016, 9, 22),
                    AudioUrl = "https://example.com/audio/starboy.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/3/3f/The_Weeknd_-_Starboy.png",
                    Plays = 2100000000,
                    Likes = 52000000,
                    CreatedAt = DateTime.UtcNow
                },

                // Eminem
                new Song
                {
                    Title = "Lose Yourself",
                    Artist = "Eminem",
                    ArtistId = eminem.Id,
                    Album = "8 Mile Soundtrack",
                    Duration = 326,
                    Genre = "Hip-Hop",
                    ReleaseDate = new DateTime(2002, 10, 28),
                    AudioUrl = "https://example.com/audio/lose-yourself.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/1/11/8Mile.jpg",
                    Plays = 1450000000,
                    Likes = 38000000,
                    CreatedAt = DateTime.UtcNow
                },
                new Song
                {
                    Title = "Without Me",
                    Artist = "Eminem",
                    ArtistId = eminem.Id,
                    Album = "The Eminem Show",
                    Duration = 290,
                    Genre = "Hip-Hop",
                    ReleaseDate = new DateTime(2002, 5, 26),
                    AudioUrl = "https://example.com/audio/without-me.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/5/53/EminemShow.jpg",
                    Plays = 1200000000,
                    Likes = 34000000,
                    CreatedAt = DateTime.UtcNow
                },

                // Daft Punk
                new Song
                {
                    Title = "One More Time",
                    Artist = "Daft Punk",
                    ArtistId = daftpunk.Id,
                    Album = "Discovery",
                    Duration = 320,
                    Genre = "Electronic",
                    ReleaseDate = new DateTime(2000, 11, 30),
                    AudioUrl = "https://example.com/audio/one-more-time.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/2/27/Daft_Punk_-_Discovery.png",
                    Plays = 890000000,
                    Likes = 25000000,
                    CreatedAt = DateTime.UtcNow
                },
                new Song
                {
                    Title = "Get Lucky",
                    Artist = "Daft Punk",
                    ArtistId = daftpunk.Id,
                    Album = "Random Access Memories",
                    Duration = 369,
                    Genre = "Electronic",
                    ReleaseDate = new DateTime(2013, 4, 19),
                    AudioUrl = "https://example.com/audio/get-lucky.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/a/a7/Random_Access_Memories.jpg",
                    Plays = 1100000000,
                    Likes = 31000000,
                    CreatedAt = DateTime.UtcNow
                },

                // Ed Sheeran
                new Song
                {
                    Title = "Shape of You",
                    Artist = "Ed Sheeran",
                    ArtistId = edsheeran.Id,
                    Album = "÷ (Divide)",
                    Duration = 233,
                    Genre = "Pop",
                    ReleaseDate = new DateTime(2017, 1, 6),
                    AudioUrl = "https://example.com/audio/shape-of-you.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/4/45/Divide_cover.png",
                    Plays = 3500000000,
                    Likes = 88000000,
                    CreatedAt = DateTime.UtcNow
                },
                new Song
                {
                    Title = "Perfect",
                    Artist = "Ed Sheeran",
                    ArtistId = edsheeran.Id,
                    Album = "÷ (Divide)",
                    Duration = 263,
                    Genre = "Pop",
                    ReleaseDate = new DateTime(2017, 3, 3),
                    AudioUrl = "https://example.com/audio/perfect.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/4/45/Divide_cover.png",
                    Plays = 2800000000,
                    Likes = 72000000,
                    CreatedAt = DateTime.UtcNow
                },

                // Kendrick Lamar
                new Song
                {
                    Title = "HUMBLE.",
                    Artist = "Kendrick Lamar",
                    ArtistId = kendrick.Id,
                    Album = "DAMN.",
                    Duration = 177,
                    Genre = "Hip-Hop",
                    ReleaseDate = new DateTime(2017, 3, 30),
                    AudioUrl = "https://example.com/audio/humble.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/5/51/Kendrick_Lamar_-_Damn.png",
                    Plays = 1400000000,
                    Likes = 37000000,
                    CreatedAt = DateTime.UtcNow
                },
                new Song
                {
                    Title = "DNA.",
                    Artist = "Kendrick Lamar",
                    ArtistId = kendrick.Id,
                    Album = "DAMN.",
                    Duration = 185,
                    Genre = "Hip-Hop",
                    ReleaseDate = new DateTime(2017, 4, 14),
                    AudioUrl = "https://example.com/audio/dna.mp3",
                    CoverUrl = "https://upload.wikimedia.org/wikipedia/en/5/51/Kendrick_Lamar_-_Damn.png",
                    Plays = 920000000,
                    Likes = 24000000,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await _songsCollection.InsertManyAsync(songs);
            return songs;
        }

        private async Task<List<Playlist>> SeedPlaylistsAsync(List<User> users, List<Song> songs)
        {
            var playlists = new List<Playlist>
            {
                new Playlist
                {
                    Name = "Rock Classics",
                    Description = "Les meilleurs morceaux de rock de tous les temps",
                    UserId = users[0].Id!,
                    SongIds = songs.Where(s => s.Genre == "Rock").Select(s => s.Id!).ToList(),
                    IsPublic = true,
                    CoverUrl = "https://i.scdn.co/image/ab67706c0000da84a4f5c91d2f8a5e3f5e3f5e3f",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Playlist
                {
                    Name = "Top Hits 2020s",
                    Description = "Les plus grands hits des années 2020",
                    UserId = users[1].Id!,
                    SongIds = songs.Where(s => s.Plays > 2000000000).Select(s => s.Id!).ToList(),
                    IsPublic = true,
                    CoverUrl = "https://i.scdn.co/image/ab67706c0000da842c2c2c2c2c2c2c2c2c2c2c2c",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Playlist
                {
                    Name = "Hip-Hop Essentials",
                    Description = "Les classiques incontournables du hip-hop",
                    UserId = users[0].Id!,
                    SongIds = songs.Where(s => s.Genre == "Hip-Hop").Select(s => s.Id!).ToList(),
                    IsPublic = true,
                    CoverUrl = "https://i.scdn.co/image/ab67706c0000da843d3d3d3d3d3d3d3d3d3d3d3d",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Playlist
                {
                    Name = "Electronic Vibes",
                    Description = "Musique électronique pour danser",
                    UserId = users[2].Id!,
                    SongIds = songs.Where(s => s.Genre == "Electronic").Select(s => s.Id!).ToList(),
                    IsPublic = true,
                    CoverUrl = "https://i.scdn.co/image/ab67706c0000da844e4e4e4e4e4e4e4e4e4e4e4e",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Playlist
                {
                    Name = "Chill Afternoon",
                    Description = "Ma playlist perso pour me détendre",
                    UserId = users[3].Id!,
                    SongIds = new List<string>
                    {
                        songs.First(s => s.Title == "Perfect").Id!,
                        songs.First(s => s.Title == "Starboy").Id!,
                        songs.First(s => s.Title == "Get Lucky").Id!,
                        songs.First(s => s.Title == "Don't Stop Me Now").Id!
                    },
                    IsPublic = false,
                    CoverUrl = "https://i.scdn.co/image/ab67706c0000da845f5f5f5f5f5f5f5f5f5f5f5f",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Playlist
                {
                    Name = "Workout Motivation",
                    Description = "Musique énergique pour le sport",
                    UserId = users[1].Id!,
                    SongIds = new List<string>
                    {
                        songs.First(s => s.Title == "Lose Yourself").Id!,
                        songs.First(s => s.Title == "We Will Rock You").Id!,
                        songs.First(s => s.Title == "HUMBLE.").Id!,
                        songs.First(s => s.Title == "DNA.").Id!,
                        songs.First(s => s.Title == "Blinding Lights").Id!
                    },
                    IsPublic = true,
                    CoverUrl = "https://i.scdn.co/image/ab67706c0000da846a6a6a6a6a6a6a6a6a6a6a6a",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            await _playlistsCollection.InsertManyAsync(playlists);
            return playlists;
        }

        public async Task ClearDatabaseAsync()
        {
            Console.WriteLine("🗑️  Suppression de toutes les données...");
            await _songsCollection.DeleteManyAsync(_ => true);
            await _playlistsCollection.DeleteManyAsync(_ => true);
            await _usersCollection.DeleteManyAsync(_ => true);
            await _artistsCollection.DeleteManyAsync(_ => true);
            Console.WriteLine("✅ Base de données vidée");
        }
    }
}
namespace Okaimono.src.SaveData
{
    public class DataModels
    {
        public List<Anime> AnimeList { get; set; }
        public List<Manga> MangaList { get; set; }
    }


    public class Anime
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<string>? Tags { get; set; }
        public string? InLive { get; set; }
        public string? NextNewCap { get; set; }
        public int? MaxCaps { get; set; }
        public int? LastViewCap { get; set; }
        public List<string>? Prequels { get; set; }
        public List<string>? Sequels { get; set; }
        public List<string>? Movies { get; set; }
        public List<string>? SpinOffs { get; set; }
        public int? Ovas { get; set; }
    }


    public class Manga
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<string>? Tags { get; set; }
        public string? OnGoing { get; set; }
        public int? MaxCaps { get; set; }
        public int? LastViewCap { get; set; }
        public List<string>? Prequels { get; set; }
        public List<string>? Sequels { get; set; }
        public List<string>? SpinOffs { get; set; }
    }


    public class User
    {
        public string? Name { get; set; } = "Unknown";
        public string? DBPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }


}





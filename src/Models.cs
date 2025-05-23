﻿namespace Okaimono.SaveData;

public sealed class DataLists {
    public List<Anime> AnimeList { get; set; } = new();
    public List<Manga> MangaList { get; set; } = new();
}

public sealed class Anime {
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

public sealed class Manga {
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

public sealed class User {
    public string? Name { get; set; } = "Unknown";
    public string? DbPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
}






﻿

public class DataBase
{
    public List<Anime> AnimeList = new();
    public List<Manga> MangaList = new();
    public string LastUpdate = DateTime.Today.ToString();
}


public class Anime
{
    public string? Name = "Unknown";
    public List<string>? Tags = new(){ "None" };
    public bool? InLive = false;
    public string? NextNewCap = "Never";
    public int? MaxCaps = 0;
    public int? LastViewCap = 0;
    public List<string>? Prequels = new(){"0"};
    public List<string>? Sequels = new(){"0"};
    public List<string>? Movies  = new(){"0"};
    public List<string>? SpinOffs = new(){"0"};
    public int? Ovas = 0;
}


public class Manga
{
    public string? Name = "Unknown";
    public List<string>? Tags= new (){"None"};
    public bool? OnGoing = false;
    public int? MaxCaps = 0;
    public int? LastViewCap = 0;
    public List<string>? Prequels = new(){"0"};
    public List<string>? Sequels = new(){"0"};
    public List<string>? SpinOffs = new(){"0"};
}
using Okaimono.Properties;
using System.Diagnostics;

namespace Okaimono.src
{
    using Okaimono.Logs;
    using Okaimono.src.SaveData;

    public class Backend
    {

        #region Variables

        private Profile _userProfile = new();
        private Database _database = new();
        private string _dataLog = string.Empty;
        public string GetDataLogs { get => _dataLog; }
        
        //Encapsulates
        public Profile UserProfile { get => _userProfile; }
        public Database Db { get => _database; }
        
        #endregion



        #region Public_Methods

        public void Start()
        {
            _userProfile.ReadUser();
            _database.LoadData();
        }
        public bool CloseApplication() => true;
        public void CreateNewItem<T>(T item) => NewItem(item);
        public object SearchItem(bool isAnime, string name) => Search(isAnime, name);
        public void DeleteItem<T>(T item) => DeleteAnItem(item);
        public void EditItem<T>(T item) => Edit(item);
        public string GetDoc() => Resources.Doc;
        public void Donation() => Koffi();

        #endregion



        #region Private_Methods
        
        object Search(bool isAnime, string name)
        {
            //isAnime
            if (isAnime)
            {
                Anime? anime = default;
                if (_database.Data.AnimeList.Exists(x => x.Name == name))
                    anime = _database.Data.AnimeList.Find(x => x.Name == name);
                else 
                    _dataLog = Logs.GetBackendLog(BEL.B01);
                return anime = new();
            }

            //manga
            else
            {
                Manga? manga = default;
                if (_database.Data.MangaList.Exists(x => x.Name == name))
                    manga = _database.Data.MangaList.Find(x => x.Name == name);
                else
                    _dataLog = Logs.GetBackendLog(BEL.B01);
                return manga = new();
            }
        }

        void NewItem<T>(T item)
        {
            if (item.GetType() == typeof(Anime))
                _database.Data.AnimeList.Add(item as Anime ?? new());
            else if (item.GetType() == typeof(Manga))
                _database.Data.MangaList.Add(item as Manga ?? new());
            _database.SaveData();
        }

        void DeleteAnItem<T>(T item)
        {
            if(item.GetType() == typeof(Anime))
                _database.Data.AnimeList.Remove(item as Anime ?? new());
            else if(item.GetType() == typeof(Manga))
                _database.Data.MangaList.Remove(item as Manga ?? new());
            _database.SaveData();
        }

        void Edit<T>(T element)
        {
            if (element.GetType() == typeof(Anime))
            {
                _database.Data.AnimeList.ForEach(anime =>
                {
                    if (anime.Id == (element as Anime).Id)
                    {
                        anime = element as Anime;
                    }
                });
            }
            else if (element.GetType() == typeof(Manga))
            {
                _database.Data.MangaList.ForEach(manga =>
                {
                    if (manga.Id == (element as Manga).Id)
                    {
                        manga = element as Manga;
                    }
                });
            }
            _database.SaveData();
        }

        void Koffi()
        {
            string url = "https://ko-fi.com/dotchoco";
            try {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex) { _dataLog = Logs.GetBackendLog(BEL.B02); }
        }

        #endregion


    }
}


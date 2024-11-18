using Okaimono.Properties;
using System.Diagnostics;

namespace Okaimono.src
{
    using Okaimono.Logs;
    using Okaimono.src.SaveData;

    public class Backend
    {

        #region Variables

        private Profile userProfile = new();
        private Database database = new();
        private string dataLog = string.Empty;
        public string GetDataLogs { get => dataLog; }
        
        //Encapsulates
        public Profile UserProfile { get => userProfile; }
        public Database DB { get => database; }
        
        #endregion



        #region Public_Methods

        public void Start()
        {
            userProfile.ReadUser();
            database.LoadData();
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
                if (database.Data.AnimeList.Exists(x => x.Name == name))
                    anime = database.Data.AnimeList.Find(x => x.Name == name);
                else 
                    dataLog = Logs.GetBackendLog(BEL.B01);
                return anime = new();
            }

            //manga
            else
            {
                Manga? manga = default;
                if (database.Data.MangaList.Exists(x => x.Name == name))
                    manga = database.Data.MangaList.Find(x => x.Name == name);
                else
                    dataLog = Logs.GetBackendLog(BEL.B01);
                return manga = new();
            }
        }

        void NewItem<T>(T item)
        {
            if (item.GetType() == typeof(Anime))
                database.Data.AnimeList.Add(item as Anime ?? new());
            else if (item.GetType() == typeof(Manga))
                database.Data.MangaList.Add(item as Manga ?? new());
            database.SaveData();
        }

        void DeleteAnItem<T>(T item)
        {
            if(item.GetType() == typeof(Anime))
                database.Data.AnimeList.Remove(item as Anime ?? new());
            else if(item.GetType() == typeof(Manga))
                database.Data.MangaList.Remove(item as Manga ?? new());
            database.SaveData();
        }

        void Edit<T>(T Element)
        {
            if (Element.GetType() == typeof(Anime))
            {
                database.Data.AnimeList.ForEach(anime =>
                {
                    if (anime.Id == (Element as Anime).Id)
                    {
                        anime = Element as Anime;
                    }
                });
            }
            else if (Element.GetType() == typeof(Manga))
            {
                database.Data.MangaList.ForEach(manga =>
                {
                    if (manga.Id == (Element as Manga).Id)
                    {
                        manga = Element as Manga;
                    }
                });
            }
            database.SaveData();
        }

        void Koffi()
        {
            string url = "https://ko-fi.com/dotchoco";
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error al abrir la página: " + ex.Message);
                dataLog = Logs.GetBackendLog(BEL.B02);
            }
        }

        #endregion


    }
}


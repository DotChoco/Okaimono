using Models;
using Okaimono.Properties;
using System.Diagnostics;

namespace Okaimono.src
{
    public class Backend
    {

        #region Variables

        private Profile userProfile =new();
        private Database database =new();
        private (byte, string) dataLogs = default;

        public (byte, string) GetDataLogs { get => dataLogs; }
        #endregion



        #region Public_Methods

        public void Start()
        {
            userProfile.ReadUser();
            database.LoadData();
        }

        public bool CloseApplication() => true;
        public void CreateNewItem<T>(T item) => NewItem(item);
        public object SearchItem(byte item, string name) => Search(item, name);
        public void DeleteItem<T>(T item) => DeleteAnItem(item);
        public void EditItem<T>(T item) =>Edit(item);
        public void PrintDoc() => Doc();
        public void Donation() => Koffi();

        #endregion



        #region Private_Methods
        
        object Search(byte item, string name)
        {
            //anime
            if (item == 0)
            {
                Anime anime = default;
                if (database.Data.AnimeList.Exists(x => x.Name == name))
                    anime = database.Data.AnimeList.Find(x => x.Name == name);
                else { 
                    anime = null;
                    dataLogs = (1, GetLog(1));
                }
                return anime;
            }

            //manga
            else if (item == 1)
            {
                Manga manga = default;
                if (database.Data.MangaList.Exists(x => x.Name == name))
                    manga = database.Data.MangaList.Find(x => x.Name == name);
                else { 
                    manga = null;
                    dataLogs = (1, GetLog(1));
                }
                return manga;
            }


            //Log de retorno nulo
            dataLogs = (1, GetLog(1));
            return null;
        }

        void NewItem<T>(T item)
        {
            if (item.GetType() == typeof(Anime))
                database.Data.AnimeList.Add(item as Anime);
            else if (item.GetType() == typeof(Manga))
                database.Data.MangaList.Add(item as Manga);
            database.SaveData();
        }

        void DeleteAnItem<T>(T item)
        {
            if(item.GetType() == typeof(Anime))
                database.Data.AnimeList.Remove(item as Anime);
            else if(item.GetType() == typeof(Manga))
                database.Data.MangaList.Remove(item as Manga);
            database.SaveData();
        }

        void Edit<T>(T Element)
        {
            if (Element.GetType() == typeof(Anime))
            {
                database.Data.AnimeList.ForEach(x => 
                {
                    if (x.Name == (Element as Anime).Name)
                    {
                        x = Element as Anime;
                    }
                });
            }
            else if (Element.GetType() == typeof(Manga))
            {
                database.Data.MangaList.ForEach(x =>
                {
                    if (x.Name == (Element as Manga).Name)
                    {
                        x = Element as Manga;
                    }
                });
            }
        }
        
        void Doc()
        {
            foreach (var Line in Resources.Doc)
            {
                Console.Write(Line);
            }
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
                dataLogs = (2, GetLog(2));
            }
        }

        string GetLog(byte logCode)
        {
            Logs.BackendErrors.TryGetValue(logCode, out string value);
            return value;
        }

        #endregion


    }
}


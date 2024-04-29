using Models;
namespace Okaimono.src
{
    internal class Backend
    {

        #region Variables

        Profile UserProfile =new();
        Database database =new();

        #endregion


        #region Public_Methods


        public void Start()
        {
            UserProfile.ReadUser();
            database.GetData();
        }

        public void CloseApplication() => Close();
        public void CreateNewItem() => NewItem();
        public void SearchItem() => Search();
        public void DeleteItem() => DeleteAnItem();
        public void EditItem() =>EditAnItem();
        public void PrintDoc() => Doc();
        public void BuyMeAKoffi() => Koffi();

        #endregion



        #region Private_Methods


        #region Application

        void Search() => SearchAnItem();
        void Close()
        {
            
        }


        object SearchAnItem()
        {
            string name = default;
            Anime anime = default;
            Manga manga = default;

            //anime
            if (manga.Name != name)
            {
                if (database.Data.AnimeList.Exists(x => x.Name == name))
                {
                    anime = database.Data.AnimeList.Find(x => x.Name == name);
                }
                else
                {

                }
                return anime;
            }
            
            //manga
            else if (true)
            {
                if (database.Data.MangaList.Exists(x => x.Name == name))
                {
                    manga = database.Data.MangaList.Find(x => x.Name == name);
                    database.Data.MangaList.Remove(manga);
                }
                else
                {

                }
                return manga;
            }
        }


        void NewItem()
        {

        }


        void DeleteAnItem()
        {
            Anime anime = SearchAnItem() as Anime;
            database.Data.AnimeList.Remove(anime);
        }


        void EditAnItem()
        {
           
        }


        void Edit<T>(T Element)
        {
            if (Element.GetType() == typeof(Anime))
            {

            }
            else if (Element.GetType() == typeof(Manga))
            {

            }
        }


        #endregion



        #region Extras

        void Doc()
        {

        }


        void Koffi()
        {

        }

        #endregion


        #endregion

    }
}

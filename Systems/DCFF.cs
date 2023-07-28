using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace Okaimono_CMD_Version
{
    public class DCFF
    {
        #region Variables

        PRFM PRFM = new();
        public DataBase database = new();
        private string Today = default;
        private string DOKDBPath = default;

        #endregion



        #region Methods


        public void Main()
        {
            List<string> Lines = default;
            string Line = default;
            PRFM.Main();
            DOKDBPath= PRFM.GetDOKDB + "DOKDB";
            if (File.Exists(DOKDBPath))
            {
                Lines = ReadDB(DOKDBPath);
                Today = Lines[Lines.Count - 1];
                Lines.RemoveAt(Lines.Count - 1);

                for (int i = 0; i < Lines.Count; i++)
                {
                    Line = Lines[i];
                    string Word = default;
                    string AOM = default;
                    for (int j = 0; j < Line.Length; j++)
                    {
                        if (Line[j] == ':')
                        {
                            AOM = Word;
                            Word = default;
                        }
                        else if (Line[j] == ')')
                        {
                            Word = Word.Replace("(", "");
                            TypeSelector(AOM , Word);
                            AOM = default;
                            Word = default;
                        }
                        else
                        {
                            Word += Line[j];
                        }
                    }
                }

                database.LastUpdate = Lines[Lines.Count - 1];
            }

            Console.Clear();
        }



        /// <summary>
        ///Return the DOKDB data in a string list
        ///</summary>
        ///<param name="DOKDB">DataBase Path</param>
        ///<returns>String List</returns>
        List<string> ReadDB(string DOKDB)
        {
            StreamReader DataReader;
            string CharList = default;
            List<string> Result = new List<string>();
            DataReader = new StreamReader(DOKDBPath);

            foreach (var Line in DataReader.ReadToEnd())
            {
                if (Line.ToString() != "\n")
                {
                    CharList += Line;
                }
                else
                {
                    CharList = CharList.Replace("\r", "");
                    Result.Add(CharList);
                    CharList = default;
                }
            }

            DataReader.Close();

            return Result;
        }




        ///<summary>
        ///Receive the type of object and
        ///the data to be processed in the 
        ///respective method
        ///</summary>
        ///<param name="Type">Type of Objetc</param>
        ///<param name="Data">String with the data</param>
        void TypeSelector(string Type, string Data)
        {
            if(Type == "Anime")
                database.AnimeList.Add(FillAnimeList(Data));
            else
                database.MangaList.Add(FillMangaList(Data));
        }



        ///<summary>
        ///Return a new Class type of Anime
        ///</summary>
        ///<param name="Data">The string whith the list of items</param>
        ///<returns>Anime Class</returns>
        Anime FillAnimeList(string Data)
        {
            Anime anime = new();
            List<string> Items = new();
            string Item = default;
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] == ',' || i == Data.Length-1)
                {
                    if (i == Data.Length - 1)
                        Item += Data[i];
                    Items.Add(Item);
                    Item = default;
                }
                else
                    Item += Data[i];
            }


            anime.Name = Items[0].Trim('[', ']');
            anime.Tags = ReturnListItems(Items[1]);
            anime.InLive = bool.Parse(Items[2].Trim('[', ']'));
            anime.NextNewCap = Items[3].Trim('[', ']');
            anime.MaxCaps = int.Parse(Items[4].Trim('[', ']'));
            anime.LastViewCap = int.Parse(Items[5].Trim('[', ']'));
            anime.Prequels = ReturnListItems(Items[6]);
            anime.Sequels= ReturnListItems(Items[7]);
            anime.Movies = ReturnListItems(Items[8]);
            anime.SpinOffs = ReturnListItems(Items[9]);
            anime.Ovas = int.Parse(Items[10].Trim('[', ']'));

            return anime;
        }



        ///<summary>
        ///Separete the string in a list 
        ///of strings to return
        ///</summary>
        ///<param name="StringList">String with the item list</param>
        ///<returns>List of strings</returns>
        List<string> ReturnListItems(string StringList)
        {
            List<string> Result = new();
            string Item = default;
            for (int i = 1; i <= StringList.Length-1; i++)
            {
                if (StringList[i] == ';' || i == StringList.Length -1)
                {
                    Result.Add(Item);
                    Item = default;
                }
                else
                    Item += StringList[i];
            }


            return Result;
        }



        ///<summary>
        ///Return a new Class type of Manga
        ///</summary>
        ///<param name="DB">The string whith the list of items</param>
        ///<returns>Manga Class</returns>
        Manga FillMangaList(string Data)
        {
            Manga manga = new();
            List<string> Items = new();
            string Item = default;
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] == ',' || i == Data.Length - 1)
                {
                    if (i == Data.Length - 1)
                        Item += Data[i];
                    Items.Add(Item);
                    Item = default;
                }
                else
                    Item += Data[i];
            }

            manga.Name = Items[0].Trim('[', ']');
            manga.Tags = ReturnListItems(Items[1]);
            manga.OnGoing= bool.Parse(Items[2].Trim('[', ']'));
            manga.MaxCaps = int.Parse(Items[3].Trim('[', ']'));
            manga.LastViewCap = int.Parse(Items[4].Trim('[', ']'));
            manga.Prequels = ReturnListItems(Items[5]);
            manga.Sequels = ReturnListItems(Items[6]);
            manga.SpinOffs = ReturnListItems(Items[7]);


            return manga;
        }



        ///<summary>
        /// Save the DataBase in DOKDB
        ///</summary>
        ///<param name="DB">The Database</param>
        void UpdateDOKDB(DataBase DB)
        {   
            string MainString = default;
            StreamWriter DataWriter;
            DataWriter = new StreamWriter(PRFM.GetDOKDB + "DOKDB");


            foreach (var Anime in DB.AnimeList)
            {
                MainString = "Anime:(";
                MainString += "[" + Anime.Name + "],";
                MainString += "[" + SaveListFormater(Anime.Tags) + "],";
                MainString += "[" + Anime.InLive + "],";
                MainString += "[" + Anime.NextNewCap + "],";
                MainString += "[" + Anime.MaxCaps + "],";
                MainString += "[" + Anime.LastViewCap + "],";
                MainString += "[" + SaveListFormater(Anime.Prequels) + "],";
                MainString += "[" + SaveListFormater(Anime.Sequels) + "],";
                MainString += "[" + SaveListFormater(Anime.Movies) + "],";
                MainString += "[" + SaveListFormater(Anime.SpinOffs) + "],";
                MainString += "[" + Anime.Ovas + "])";
                DataWriter.WriteLine(MainString);
                MainString = default;
            }

            foreach (var Manga in DB.MangaList)
            {
                MainString = "Manga:(";
                MainString += "[" + Manga.Name + "],";
                MainString += "[" + SaveListFormater(Manga.Tags) + "],";
                MainString += "[" + Manga.OnGoing + "],";
                MainString += "[" + Manga.MaxCaps + "],";
                MainString += "[" + Manga.LastViewCap + "],";
                MainString += "[" + SaveListFormater(Manga.Prequels) + "],";
                MainString += "[" + SaveListFormater(Manga.Sequels) + "],";
                MainString += "[" + SaveListFormater(Manga.SpinOffs) + "])";
                DataWriter.WriteLine(MainString);
                MainString = default;
            }
            
            DataWriter.WriteLine(DateToString());

            DataWriter.Close();
        }



        ///<summary>
        /// Create the separator for the list elements
        ///</summary>
        ///<param name="List"></param>
        string SaveListFormater<T>(List<T> List, string Separator = ";")
        {
            string Result = default;

            for (int i = 0; i < List.Count; i++)
            {
                if (i < List.Count-1)
                    Result += List[i].ToString() + Separator;

                else if (i == List.Count - 1)
                    Result += List[i].ToString();
            }

            return Result;
        }



        /// <summary>
        /// Obtain the date of today
        /// </summary>
        /// <returns>A string with the date parsed to MMM/dd/yyyy</returns>
        string DateToString()
        {
            string Result = DateTime.Today.ToString();
            string formatoFecha = "M/d/yyyy h:mm:ss tt";
            DateTime fecha = DateTime.ParseExact(Result, formatoFecha, CultureInfo.InvariantCulture);
            Result = fecha.ToString("MMM/dd/yyyy");
            return Result;
        }



        /// <summary>
        /// Obtain the day of the week of today
        /// </summary>
        /// <returns>A string with the day of week</returns>
        public string GetWeekDay()
        {
            string fechaString = DateToString();
            DateTime fechaDateTime;
            DayOfWeek diaSemana = new();

            // Convertir la cadena de fecha a DateTime utilizando ParseExact
            if (DateTime.TryParseExact(fechaString, "MMM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out fechaDateTime))
                // Obtener el día de la semana
                diaSemana = fechaDateTime.DayOfWeek;

            else
                Console.WriteLine("La cadena de fecha no tiene un formato válido.");

            return diaSemana.ToString();
        }



        /// <summary>
        /// Delete an item of any class list
        /// </summary>
        /// <param name="Name">Name of the element</param>
        /// <param name="ItemType">Type of the element</param>
        public void DeleteItem()
        {
            string Name = Console.ReadLine();
            string ItemType = Console.ReadLine();
            while (Name == "" || ItemType == "")
            {
                PRFM.PlaySound("error");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("\nERROR: Nombre de Usuario invalido");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Vuelve a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\nDime, Como te gustaria que te llame?");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNota: Recuerda no usar caracteres especiales como espacios");
                Console.WriteLine("ya que no solo sera tu nombre de usuario si no que tambien " +
                                "\nuna de las llaves para poder acceder a la base de datos\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Name = Console.ReadLine();
                ItemType = Console.ReadLine();
            }


            if (ItemType == "Anime")
            {
                if (database.AnimeList.Exists(x => x.Name == Name))
                    database.AnimeList.Remove(database.AnimeList.Find
                                              (x => x.Name == Name));
                else
                {
                    PRFM.PlaySound("error");
                    Console.WriteLine("No existe el anime");
                }
            }
            else if(ItemType == "Manga")
            {
                if (database.MangaList.Exists(x => x.Name == Name))
                database.MangaList.Remove(database.MangaList.Find
                                      (x => x.Name == Name));
                else
                {
                    PRFM.PlaySound("error");
                    Console.WriteLine("No existe el manga");
                }
            }

            PRFM.PlaySound("delete");
        }




        public void CreateNewItem()
        {
            Console.WriteLine("\nQue tipo de elemento quieres crear???\n");
            Console.WriteLine("\nAnime(A/a), Manga(M/m), Salir(E/e)");
            char Answer = default;
            Answer = char.Parse(Console.ReadLine());

            Console.Clear();
            while (Answer != 'A' || Answer != 'a' ||
                   Answer != 'M' || Answer != 'm' ||
                   Answer == 'E' || Answer != 'e')
            {
                PRFM.PlaySound("error");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("\nERROR: La respuesta ingresada no es valida");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Vuelve a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\nQue tipo de elemento quieres crear???\n");
                Console.WriteLine("\nAnime(A/a), Manga(M/m), Salir(E/e)");
                Answer = char.Parse(Console.ReadLine());
            }
            if (Answer == 'A' || Answer == 'a')
            {
                database.AnimeList.Add(AddNewAnime());
            }
            else if(Answer == 'M' || Answer == 'm')
            {
                database.MangaList.Add(AddNewManga());
            }
            UpdateDOKDB(database);

            PRFM.PlaySound("create");
        }




        Anime AddNewAnime()
        {
            Anime anime = new();



            return anime;
        }




        Manga AddNewManga()
        {
            Manga manga = new();


            return manga;
        }



        public void CloseProgram()
        {
            double delay = default;
            int timeOut;
            Console.Clear();
            Console.WriteLine("\nGracias por usar el programa");
            Console.WriteLine("Presiona cualquier tecla para salir");
            PRFM.PlaySound("close");
            delay = PRFM.GetTimeOut;
            timeOut = (int)delay;
            Task.Delay(timeOut).Wait();
        }



        void PrintAnyItem(Anime? anime, Manga? manga)
        {
            string? Answer = default;

            if (anime != null)
            {
                while (Answer != "E" || Answer != "e" || Answer == null)
                {
                    Console.Clear();
                    Console.WriteLine("\n----------------Anime----------------");
                    Console.WriteLine("\nName: " + anime.Name);

                    Console.Write("Tags: ");
                    Console.Write(SaveListFormater(anime.Tags, ", "));

                    Console.WriteLine("\nInLive: " + anime.InLive);
                    Console.WriteLine("NextNewCap: " + anime.NextNewCap);
                    Console.WriteLine("MaxCaps: " + anime.MaxCaps);
                    Console.WriteLine("LastViewCap: " + anime.LastViewCap);

                    Console.Write("Prequels: ");
                    Console.Write(SaveListFormater(anime.Prequels, ", "));

                    Console.Write("\nSequels: ");
                    Console.Write(SaveListFormater(anime.Sequels, ", "));

                    Console.Write("\nMovies: ");
                    Console.Write(SaveListFormater(anime.Movies, ", "));

                    Console.Write("\nSpinOffs: ");
                    Console.Write(SaveListFormater(anime.SpinOffs, ", "));

                    Console.WriteLine("\nOvas: ");


                    Console.WriteLine("\n\nEscribe (E/e) para salir al menu principal");
                    Answer = Console.ReadLine();
                    if (Answer == "E" || Answer == "e")
                    {
                        Console.Clear();
                        break;
                    }
                }
            }

            else if (manga != null)
            {
                while (Answer != "E" || Answer != "e")
                {
                    Console.Clear();
                    Console.WriteLine("\n----------------Manga----------------");
                    Console.WriteLine("\nName: " + manga.Name);

                    Console.Write("Tags: ");
                    Console.Write(SaveListFormater(manga.Tags, ", "));

                    Console.WriteLine("\nInLive: " + manga.OnGoing);
                    Console.WriteLine("MaxCaps: " + manga.MaxCaps);
                    Console.WriteLine("LastViewCap: " + manga.LastViewCap);

                    Console.Write("Prequels: ");
                    Console.Write(SaveListFormater(manga.Prequels, ", "));

                    Console.Write("\nSequels: ");
                    Console.Write(SaveListFormater(manga.Sequels, ", "));

                    Console.Write("\nSpinOffs: ");
                    Console.Write(SaveListFormater(manga.SpinOffs, ", "));


                    Console.WriteLine("\n\n\nEscribe (E/e) para salir al menu principal");
                    Answer = Console.ReadLine();
                    if (Answer == "E" || Answer == "e")
                    {
                        Console.Clear();
                        break;
                    }

                }
            }
        }



        public void SearchItem()
        {
            string Name = Console.ReadLine();
            string ItemType = Console.ReadLine();
            while (Name == "" || ItemType == "")
            {
                PRFM.PlaySound("error");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("\nERROR: Nombre de Usuario invalido");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Vuelve a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\nDime, Como te gustaria que te llame?");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNota: Recuerda no usar caracteres especiales como espacios");
                Console.WriteLine("ya que no solo sera tu nombre de usuario si no que tambien " +
                                "\nuna de las llaves para poder acceder a la base de datos\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Name = Console.ReadLine();
                ItemType = Console.ReadLine();
            }

            if (ItemType == "Anime")
            {
                if (database.AnimeList.Exists(x => x.Name == Name))
                    PrintAnyItem(database.AnimeList.Find(x => x.Name == Name), null);
                else
                    Console.WriteLine("No existe el anime");
            }
            else if (ItemType == "Manga")
            {
                if (database.MangaList.Exists(x => x.Name == Name))
                    PrintAnyItem(null, database.MangaList.Find(x => x.Name == Name));
                else
                    Console.WriteLine("No existe el manga");
            }
            PRFM.PlaySound("find");
        }



        public void KoffiForMe()
        {
            string url = "https://ko-fi.com/dotchoco"; // Reemplaza esta URL con la que deseas abrir

            PRFM.PlaySound("link");
            try
            {
                // Abrir la URL en el navegador web predeterminado a través de la consola de comandos
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la página: " + ex.Message);
            }
        }



        public void TransformItem()
        {

        }




        #endregion

    }



}

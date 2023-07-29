using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Okaimono_CMD_Version.Properties;
using Windows.Graphics.Printing3D;

namespace Okaimono_CMD_Version
{
    public class DCFF
    {

        #region Variables

        PRFM PRFM = new();
        public DataBase database = new();
        private string Today = default;
        private string DOKDBPath = default;
        private bool PDOC = false;
        #endregion



        #region Encapsulation


        public bool GetPDOC
        {
              get => PDOC; 
        }


        #endregion



        #region Methods


        public void Main()
        {
            List<string> Lines = default;
            string Line = default;
            PRFM.Main();
            PDOC = PRFM.GetPDOC;
            DOKDBPath = PRFM.GetDOKDB + "DOKDB";
            if (File.Exists(DOKDBPath))
            {
                Lines = ReadDB(DOKDBPath);
                if (Lines.Count > 0)
                {
                    Today = Lines[Lines.Count - 1];
                    Lines.RemoveAt(Lines.Count - 1);
                }

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
                if (Lines.Count > 0)
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
            anime.InLive = Items[2].Trim('[', ']');
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
        List<string> ReturnListItems(string StringList, char Separator = ';')
        {
            List<string> Result = new();
            string Item = default;
            int StringListLength = StringList.Length;
            int Start = 0;
            if (StringList[0] == '[')
            {
                StringListLength = StringList.Length - 1;
                Start = 1;
                for (int i = Start; i <= StringListLength; i++)
                {
                    if (StringList[i] == Separator || i == StringList.Length - 1)
                    {
                        if (i != StringList.Length - 1)
                            i++;
                        Result.Add(Item);
                        Item = default;
                    }
                    Item += StringList[i];
                }
            }
            else
            {
                for (int i = Start; i < StringListLength; i++)
                {
                    if (StringList[i] == Separator || i == StringList.Length-1)
                    {
                        if (i != StringList.Length - 1)
                            i++;
                        else
                            Item += StringList[i];
                        Result.Add(Item);
                        Item = default;
                    }
                    Item += StringList[i];
                }
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
            Console.Clear();
            Console.WriteLine("\nEscribe el nombre del Anime:\n");
            string Name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("\nEscribe el tipo de Elemento Anime[A/a] o Manga[M/m]):\n");
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


            if (ItemType == "A" || ItemType == "a")
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
            else if(ItemType == "M" || ItemType == "m")
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



        /// <summary>
        /// 
        /// </summary>
        public void CreateNewItem()
        {
            Console.Clear();
            Console.WriteLine("\nQue tipo de elemento quieres crear???\n");
            Console.WriteLine("\nAnime(A/a), Manga(M/m), Salir(E/e)");
            string? Answer = default;
            Answer = Console.ReadLine();

            Console.Clear();
            while (Answer != "A" && Answer != "a" &&
                   Answer != "M" && Answer != "m" &&
                   Answer != "E" && Answer != "e")
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
                Answer = Console.ReadLine();
            }
            if (Answer == "A" || Answer == "a")
            {
                database.AnimeList.Add(AddNewAnime());
            }
            else if(Answer == "M" || Answer == "m")
            {
                database.MangaList.Add(AddNewManga());
            }
            UpdateDOKDB(database);

            PRFM.PlaySound("create");
            Task.Delay(500).Wait();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Anime AddNewAnime()
        {
            string? Answer = default;
            string DefaultAnswer = default;
            Anime anime = new();

            Console.Clear();
            Console.WriteLine("\nEscribe el nombre del Anime:\n");
            anime.Name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("\nEscribe los generos del Anime(escribelos como en el sig. ejemplo: Romance,Escolares,Ecchi):\n");
            Console.WriteLine("En caso de que no contar con ellos solo presiona Enter\n");
            Answer = Console.ReadLine();
            if (Answer == "")Answer = "Unknown";
            anime.Tags = ReturnListItems(Answer, ',');

            Console.Clear();
            Console.WriteLine("\nEscribe 'Si' o 'No' si esta en Emision:\n");
            anime.InLive = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("\nEscribe el dia de la semana que se emite:\n");
            anime.NextNewCap = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("\nEscribe el numero maximo de caps que tiene el Anime:\n");
            anime.MaxCaps = int.TryParse(Console.ReadLine(), out int maxcaps) ? maxcaps : 0;


            Console.Clear();
            Console.WriteLine("\nEscribe el numero del ultimo cap que has visto:\n");
            anime.LastViewCap = int.TryParse(Console.ReadLine(), out int lastcap) ? lastcap : 0;

            Console.Clear();
            Console.WriteLine("\nEscribe los nombres o numero de Precuelas del Anime\n" +
                "(solo en caso de tener escribelos como en la seccion de Generos, caso contrario solo presiona Enter\n");
            Answer = Console.ReadLine();
            if (Answer == "") Answer = "0";
            anime.Prequels = ReturnListItems(Answer, ',');

            Console.Clear();
            Console.WriteLine("\nEscribe los nombres o numero de Secuelas del Anime\n" +
                "(solo en caso de tener escribelos como en la seccion de Generos, caso contrario solo presiona Enter\n");
            Answer = Console.ReadLine();
            if (Answer == "") Answer = "0";
            anime.Sequels = ReturnListItems(Answer, ',');

            Console.Clear();
            Console.WriteLine("\nEscribe los nombres o numero de Peliculas del Anime\n" +
                "(solo en caso de tener escribelos como en la seccion de Generos, caso contrario solo presiona Enter\n");
            Answer = Console.ReadLine();
            if (Answer == "") Answer = "0";
            anime.Movies = ReturnListItems(Answer, ',');

            Console.Clear();
            Console.WriteLine("\nEscribe los nombres o numero de SpinOffs del Anime\n" +
                "(solo en caso de tener escribelos como en la seccion de Genero, caso contrario solo presiona Enter\n");
            Answer = Console.ReadLine();
            if (Answer == "") Answer = "0";
            anime.SpinOffs = ReturnListItems(Answer, ',');

            Console.Clear();
            Console.WriteLine("\nNombre o Numero de Ovas:\n");
            anime.Ovas = int.TryParse(Console.ReadLine(), out int ovas) ? ovas : 0;

            Console.Clear();            
            Console.WriteLine("\nAnime Agregado Correctamente\n");
            return anime;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Manga AddNewManga()
        {
            Manga manga = new();


            return manga;
        }



        /// <summary>
        /// 
        /// </summary>
        public void CloseProgram()
        {
            Console.Clear();
            Console.WriteLine("\nGracias por usar el programa");
            Console.WriteLine("Presiona cualquier tecla para salir");
            PRFM.PlaySound("close");
            Task.Delay(PRFM.GetTimeOut).Wait();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="anime"></param>
        /// <param name="manga"></param>
        void PrintAnyItem(Anime? anime, Manga? manga)
        {
            string? Answer = default;

            if (anime != null)
            {
                while (Answer != "E" || Answer != "e" || Answer == null)
                {
                    Console.Clear();
                    Console.WriteLine("\n----------------Anime----------------");
                    Console.WriteLine("\nNombre: " + anime.Name);

                    Console.Write("Tags: ");
                    Console.Write(SaveListFormater(anime.Tags, ", "));

                    Console.WriteLine("\nEn Emision: " + anime.InLive);
                    Console.WriteLine("Siguiente Capitulo: " + anime.NextNewCap);
                    Console.WriteLine("No. Capitulos: " + anime.MaxCaps);
                    Console.WriteLine("Ultimo Capitulo Visto: " + anime.LastViewCap);

                    Console.Write("Precuelas: ");
                    Console.Write(SaveListFormater(anime.Prequels, ", "));

                    Console.Write("\nSecuela: ");
                    Console.Write(SaveListFormater(anime.Sequels, ", "));

                    Console.Write("\nPeliculas: ");
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



        /// <summary>
        /// 
        /// </summary>
        public void SearchItem()
        {
            Console.Clear();
            Console.WriteLine("\nEscribe el nombre del Anime:\n");
            string Name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("\nEscribe el tipo de Elemento Anime[A/a] o Manga[M/m]):\n");
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
            Name = Name.ToLower();
            if (ItemType == "A" || ItemType == "a")
            {
                if (database.AnimeList.Exists(x => x.Name.ToLower() == Name))
                {
                    PRFM.PlaySound("find");
                    PrintAnyItem(database.AnimeList.Find(x => x.Name.ToLower() == Name), null);
                }
                else
                {
                    PRFM.PlaySound("error");
                    Console.WriteLine("No existe el anime");
                    Task.Delay(1000);
                }
                    
            }
            else if (ItemType == "M" || ItemType == "m")
            {
                if (database.MangaList.Exists(x => x.Name.ToLower() == Name))
                {
                    PRFM.PlaySound("find");
                    PrintAnyItem(null, database.MangaList.Find(x => x.Name.ToLower() == Name));
                }
                else
                {
                    PRFM.PlaySound("error");
                    Console.WriteLine("No existe el manga");
                    Task.Delay(1000);
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
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



        /// <summary>
        /// 
        /// </summary>
        public void TransformItem()
        {
            
        }



        /// <summary>
        /// 
        /// </summary>
        public void PrintAnimesForToday()
        {
            int counter = default;
            Console.WriteLine("\n----------------Animes Emitidos Hoy----------------");
            foreach (var Anime in database.AnimeList)
            {
                if (Anime.NextNewCap == GetWeekDay())
                {
                    counter++;
                    Console.WriteLine("\nNombre: " + Anime.Name);
                    Console.WriteLine("En Emision: " + Anime.InLive);
                    Console.WriteLine("Siguiente Capitulo: " + Anime.NextNewCap);
                    Console.WriteLine("No. Capitulos: " + Anime.MaxCaps);
                    Console.WriteLine("Ultimo Capitulo Visto: " + Anime.LastViewCap + "\n\n");
                }

            }
            if (counter == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo hay animes emitidos el dia de hoy\n\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public void PrintDoc()
        {
            string CharList = default;
            string Doc = Resources.Doc;
            foreach (var Line in Doc)
            {
                if (Line.ToString() != "\n")
                {
                    CharList += Line;
                }
                else
                {
                    Console.WriteLine(CharList);
                    CharList = default;
                }
            }


        }



        #endregion


    }



}

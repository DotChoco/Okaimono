using NAudio.Wave;
using Okaimono_CMD_Version.Properties;
using System.Media;


namespace Okaimono_CMD_Version
{
	public class PRFM
	{

        #region Variables

        private string User = "Tenkosei";
        private string DefaultPath = @"C:/Okaimono/";
        private string DOKDB = @"C:/Okaimono/";
        private string DOKSF = @"C:/Okaimono/DOKSF";
        private string DBGS = "Default";
        SoundPlayer soundPlayer = new();
        private double TimeOut = default;

        #endregion



        #region Methods


        public double GetTimeOut
        {
            get { return TimeOut; }
        }


        public string GetDOKDB
        {
            get { return DOKDB; }
        }



        public void Main()
        {
            PlaySound("open");
            ReadSettings();
            if (!Directory.Exists(DefaultPath) || !File.Exists(DOKSF))
            {
                if (!Directory.Exists(DefaultPath))
                    Directory.CreateDirectory(DefaultPath);
                Console.Clear();
                Console.WriteLine("\nDirectorio y Base de datos Creados");
                Console.WriteLine("Presiona una tecla para continuar....");
                Console.ReadKey();
                ResetSettigs();
                NewUser(true);
            }
            else
            {
                if (!Directory.Exists(DOKDB))
                    COUPBD();
                //else
                //{
                //    Console.WriteLine("\nTodo existe");
                //    Console.WriteLine("Presiona una tecla para continuar....");
                //    Console.ReadKey();
                //}
                NewUser(false);
            }
        }



        /// <summary>
        ///Set the default UserName or Change it
        /// </summary>
        /// <param name="NewUser">If the user is new or not</param>
        void NewUser(bool NewUser)
		{
			Console.Clear();
			if(!NewUser)
			{
				Console.Write("\nBienvenido ");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(User);
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("!!!");
				Console.WriteLine("\nPresiona una tecla para continuar....");
				Console.ReadKey();
			}
			else
			{
				ChangeUserName();
			}
        }



        /// <summary>
        ///Allow the User change his UserName
        /// </summary>
        public void ChangeUserName()
		{
            Console.Clear();
            Console.WriteLine("\nDime, Como te gustaria que te llame?");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNota: Recuerda no usar caracteres especiales como espacios");
            Console.WriteLine("ya que no solo sera tu nombre de usuario si no que tambien " +
                            "\nuna de las llaves para poder acceder a la base de datos\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            User = Console.ReadLine();
            while (User == "")
            {
                PlaySound("error");
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
                User = Console.ReadLine();
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nUsuario registrado con exito");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nBienvenido ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(User);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("!!!");
            Console.WriteLine("\n\nPresiona una tecla para continuar....");
            UpdateSettings();
            Console.ReadKey();
        }



        /// <summary>
        ///Allow the User change the current path of the DataBase
        ///by the default path or a new one
        /// </summary>
		public void MoveDefaultPath()
        {
            Console.Clear();
            char Answer = default;
			string OldDefaultPath = DefaultPath;
			string NewDefaultPath = DefaultPath;
			string OldDOKDBPath = DefaultPath + "DOKDB";
			string NewDOKDBPath = DefaultPath;
			Console.WriteLine("\nLa ruta de la base de datos actual es" +
				"\n " + DOKDB);
			Console.WriteLine("\nDeseas cambiarla? Y/N");

			string CharId = Console.ReadLine();
			if (CharId == "")
				Answer = 'y';
			else
				Answer = CharId[0];


            Console.Clear();
			if (Answer == 'Y' || Answer == 'y')
			{
				Console.WriteLine("\nEscribe toda la ruta completa desde el disco" +
					"\nEjemplo: D:/Descargas/Backups/Okaimono/\n");
                NewDefaultPath = Console.ReadLine();
                while (!Directory.Exists(NewDefaultPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("\nERROR: La ruta porporcionada no existente");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Vuelve a intentarlo");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("\nEscribe toda la ruta completa desde el disco" +
                    "\nEjemplo: D:/Descargas/Backups/Okaimono/\n");
                    NewDefaultPath = Console.ReadLine();
                }

                if (NewDefaultPath[NewDefaultPath.Length-1] != '/')
				{
                    NewDefaultPath += '/';
				}
                NewDOKDBPath = NewDefaultPath + "DOKDB";
                if (!File.Exists(NewDOKDBPath))
				{
					File.Copy(OldDOKDBPath, NewDOKDBPath, false);
				}
				DOKDB = NewDefaultPath;
                UpdateSettings();
				Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nRuta Actualizada Correctamente");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Presiona una tecla para continuar....");
                Console.ReadKey();
				Console.Clear();
			}
        }



        /// <summary>
        ///Open the DOKSF and read the settings
        /// </summary>
		void ReadSettings()
		{
            StreamReader DataReader;
            DataReader = new StreamReader(DOKSF);
			string CharList = default;
			List<string> Settings = new List<string>();
            Settings.ForEach(delegate (string Line)
            {
                Line = default;
            });
            foreach (var Line in DataReader.ReadToEnd())
            {
				if(Line.ToString() != "\n")
				{
					CharList += Line;
				}
				else
				{
					Settings.Add(CharList);
					CharList = default;
				}
			}
			string Data = default;
			string Lines = default;
			int Counter = 0;

			//Recorre cada elemento de la lista
			for (int i = 0; i < Settings.Count; i++)
            {
				//asigna cada Linea a la variable para despues manejar la
				//lista y poder llenarla
				Lines = Settings[i];

				//Recorre cada letra de la frase que en este caso es
				//una linea de texto
				for (int j = 0; j < Lines.Length-1; j++)
				{
					//En caso de que ya haya pasado los primero dos puntos ':'
					//agregara cada elemento de la frase a la data resultante
                    if (Counter >= 1)
					{
						Data += Lines[j];
					}
                    if (Lines[j] == ':')
                    {
                        Counter++;
                    }
				}
                //Con esto borro el primer espacio al inicio de la data
                Settings[i] = Data.Remove(0,1);
				
				Data = default;
				Counter = 0;
            }
			User = Settings[0];
			DOKDB = Settings[1];
            DBGS = Settings[2];
			DataReader.Close();

            //Console.WriteLine(User);
            //Console.WriteLine(DOKDB);
            //Console.WriteLine(DBGS);
            //Console.ReadKey();
        }



        /// <summary>
        ///Do the update of the settings in the DOKSF
        /// </summary>
		void UpdateSettings()
		{
            StreamWriter FileSettings;
            FileSettings = new StreamWriter(DOKSF);
            FileSettings.WriteLine("User: " + User);
            FileSettings.WriteLine("DOKDB: " + DOKDB);
            FileSettings.WriteLine("DBGS: " + DBGS);
            FileSettings.Close();
        }



        /// <summary>
        ///Set the default settings in the DOKSF
        /// </summary>
		public void ResetSettigs()
		{
            Console.Clear();
			User = "Tenkosei";
			DOKDB = DefaultPath;
            DBGS = "Default";
            UpdateSettings();
			Console.WriteLine("Configuracion restablecia");
        }



        /// <summary>
        ///Create or Update the Path of DataBase
        /// </summary>
		void COUPBD()
		{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNo se ha podido encontrar la ruta de la base de datos");
            Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Deseas Actualizar la ruta o usar la ruta por Defecto? (A/D)");

            string Option = Console.ReadLine();
            while (Option != "A" && Option != "a" && Option != "D" && Option != "d" && Option != "")
            {
                PlaySound("error");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("\nERROR: Por favor introduce la letra correspondiente a tu peticion");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Presiona una tecla para continuar....");
                Console.ReadKey();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nDeseas Actualizar la ruta o usar la ruta por Defecto? (A/D)");
                Option = Console.ReadLine();
            }
            if (Option == "A" || Option == "a")
			{
                MoveDefaultPath();
            }
			else if (Option == "D" || Option == "d" || Option == "")
			{
				DOKDB = DefaultPath;
                UpdateSettings();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nRuta Actualizada Correctamente");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Presiona una tecla para continuar....");
                Console.ReadKey();
                Console.Clear();
            }

			ReadSettings();
        }



        /// <summary>
        /// Change the path of the DBGS
        /// </summary>
        public void ChangeDBGS()
        {
            Console.Clear();
            char Answer = default;
            string NewDBGSPath = DefaultPath;
            if(DBGS == "Default")
                Console.WriteLine("\nLa ruta de la DBGS actual es" +
                    "la predeterminada\n ");
            else
                Console.WriteLine("\nLa ruta de la DBGS actual es" +
                    "\n " + DBGS);
            Console.WriteLine("\nDeseas cambiarla? Y/N");

            string CharId = Console.ReadLine();
            if (CharId == "")
                Answer = 'y';
            else
                Answer = CharId[0];



            Console.Clear();
            if (Answer == 'Y' || Answer == 'y')
            {
                Console.WriteLine("\nEscribe toda la ruta completa desde el disco" +
                    "\nEjemplo: D:/Descargas/Backups/Okaimono/\n");
                NewDBGSPath = Console.ReadLine();
                while (!Directory.Exists(NewDBGSPath))
                {
                    PlaySound("error");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("\nERROR: La ruta porporcionada no existente");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Vuelve a intentarlo");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("\nEscribe toda la ruta completa desde el disco" +
                    "\nEjemplo: D:/Descargas/Backups/Okaimono/\n");
                    NewDBGSPath = Console.ReadLine();
                }

                if (NewDBGSPath[NewDBGSPath.Length - 1] != '/')
                {
                    NewDBGSPath += '/';
                }
                DBGS = NewDBGSPath;
                UpdateSettings();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nRuta Actualizada Correctamente");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Presiona una tecla para continuar....");
                Console.ReadKey();
                Console.Clear();
            }

        }



        bool DBGS_Is_Wav(string Path)
        {
            bool IsWav = false;
            for (int i = Path.Length-1; i >= 0; i--)
            {
                if (Path[i] == '.')
                {
                    if (Path[i + 1] == 'w' && Path[i + 2] == 'a' && Path[i + 3] == 'v')
                    {
                        IsWav = true;
                        break;
                    }
                    else
                    {
                        IsWav = false;
                        break;
                    }
                }

            }   

            return IsWav;
        }



        /// <summary>
        /// Play a sound from the DBGS
        /// </summary>
        /// <param name="Sound">Name of the sound</param>
        public void PlaySound(string Sound)
        {
            WaveFileReader wf = new(Resources.delete); 
            string[] SoundsList = {"create","delete","update","error","find","link","open","close"};
            string SoundCheck = default;
            if (DBGS != "Default")
            {
                for (int i = 0; i < SoundsList.Length; i++)
                {
                    SoundCheck = DBGS + SoundsList[i] + ".wav";
                    if (SoundsList[i] == Sound)
                    {
                        if (File.Exists(SoundCheck))
                        {
                            soundPlayer = new(DBGS + SoundsList[i] + ".wav");
                            wf = new(DBGS + SoundsList[i] + ".wav");
                            break;
                        }
                        else
                        {
                            SoundCheck = "Default";
                            break;
                        }
                    }
                }
            }

            if (DBGS == "Default" || SoundCheck == "Default")
            {
                switch (Sound)
                {
                    case "create":
                        soundPlayer = new(Properties.Resources.create);
                        wf = new(Properties.Resources.create);
                        break;
                    case "delete":
                        soundPlayer = new(Properties.Resources.delete);
                        wf = new(Properties.Resources.delete);
                        break;
                    case "update":
                        soundPlayer = new(Properties.Resources.update);
                        wf = new(Properties.Resources.update);
                        break;
                    case "error":
                        soundPlayer = new(Properties.Resources.error);
                        wf = new(Properties.Resources.error);
                        break;
                    case "find":
                        soundPlayer = new(Properties.Resources.find);
                        wf = new(Properties.Resources.find);
                        break;
                    case "link":
                        soundPlayer = new(Properties.Resources.link);
                        wf = new(Properties.Resources.link);
                        break;
                    case "open":
                        soundPlayer = new(Properties.Resources.open);
                        wf = new(Properties.Resources.open);
                        break;
                    case "close":
                        soundPlayer = new(Properties.Resources.close);
                        wf = new(Properties.Resources.close);
                        break;

                    default:
                        Console.WriteLine("No se encontro ningun sonido para usar \n" +
                            $"para el caso de {Sound}");
                        break;
                }
            }
            TimeOut = wf.TotalTime.TotalMilliseconds;
            
            soundPlayer.Play();
        }



        /// <summary>
        /// Stops the sound
        /// </summary>
        bool StopSound(SoundPlayer soundPlayer)
        {
            return soundPlayer.IsLoadCompleted;
        }


        #endregion

    }
}
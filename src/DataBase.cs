using Models;

namespace Okaimono.src
{
    public class Database
    {

        #region Variables

        readonly string? dbPath = Profile.user.DBPath;
        const string dbFileName = "Data.dcf"; //.dcf = dot choco file

        public DataModels Data = new();
        
        #endregion



        #region Public_Methods


        public void SaveData() => Save();


        public void LoadData() => Load();


        #endregion



        #region Private_Methods

        void Save()
        {
            if (Logs.DBSaveErrors.TryGetValue(TryGetOrSaveProfile(), out string log)
                && log != "Successful Process")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(log);
                Console.ForegroundColor = ConsoleColor.White;
                Create();
            }
            else
            {
                StreamWriter writer = new(dbPath + dbFileName);
                writer.Write(JsonSerializer.Serialize(Data));
                writer.Close();
            }
        }


        void Load()
        {
            if (Logs.DBLoadErrors.TryGetValue(TryGetOrSaveProfile(), out string log)
                && log != "Successful Process")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(log);
                Console.ForegroundColor = ConsoleColor.White;
                Create();
            }
            else
            {
                StreamReader data = new(dbPath+dbFileName);
                Data = JsonSerializer.Deserialize<DataModels>(data.ReadToEnd());
                data.Close();
            }
        }


        void Create()
        {
            if (!Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);

            StreamWriter streamWriter = new(dbPath + dbFileName);
            streamWriter.Write(JsonSerializer.Serialize(Data));
            streamWriter.Close();
        }


        byte TryGetOrSaveProfile()
        {
            if (!Directory.Exists(dbPath)) return 2;
            if (!File.Exists(dbPath + dbFileName)) return 1;
            return 0;
        }
        
        #endregion

    }
}

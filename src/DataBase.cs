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

        public string SaveData() => Save();

        public string LoadData() => Load();

        public void CreateData() => Create();

        #endregion



        #region Private_Methods

        string Save()
        {
            string log = Logs.GetDBLog(TryGetOrSaveProfile());
            if (log == "Successful Process") {
                StreamWriter writer = new(dbPath + dbFileName);
                writer.Write(JsonSerializer.Serialize(Data));
                writer.Close();
            }
            return log;
        }

        string Load()
        {
            string log = Logs.GetDBLog(TryGetOrSaveProfile());
            if (log == "Successful Process")
            {
                StreamReader data = new(dbPath+dbFileName);
                Data = JsonSerializer.Deserialize<DataModels>(data.ReadToEnd());
                data.Close();
            }
            return log;
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

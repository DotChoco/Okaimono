using Models;

namespace Okaimono.src
{
    using Okaimono.Logs;
    using Windows.System;

    public class Database
    {

        #region Variables

        readonly string? dbPath = Profile.user.DBPath;
        const string dbFileName = "Data.dcf"; //.dcf = dot choco file
        string log = string.Empty;
        public DataModels? Data = new();
        
        #endregion



        #region Public_Methods

        public string SaveData() => Save();

        public string LoadData() => Load();

        public string CreateData() => Create();

        #endregion



        #region Private_Methods

        string Save()
        {
            log = Logs.GetSaveDBLog(DiretoriesSaveLogs());

            if (Data == null) return Logs.GetSaveDBLog(DBSL.S01);
            if (log != Logs.SUCCESSFUL_LOG) return log;
            else
            {
                try
                {
                    StreamWriter writer = new(dbPath + dbFileName);
                    writer.Write(JsonSerializer.Serialize(Data));
                    writer.Close();
                }
                catch { return Logs.GetSaveDBLog(DBSL.S02); }
                
            }
            return Logs.SUCCESSFUL_LOG;
        }

        string Load()
        {
            log = Logs.GetLoadDBLog(DiretoriesLoadLogs());

            if (log != Logs.SUCCESSFUL_LOG) return log;
            else
            {
                try
                {
                    StreamReader data = new(dbPath + dbFileName);
                    Data = JsonSerializer.Deserialize<DataModels>(data.ReadToEnd());
                    data.Close();
                    if (Data == null) return Logs.GetLoadDBLog(DBLL.L02);
                }
                catch { return Logs.GetLoadDBLog(DBLL.L03); }
            }
            return Logs.SUCCESSFUL_LOG;
        }

        string Create()
        {
            if (!Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);

            return Save();
        }

        DBLL DiretoriesLoadLogs()
        {
            if (!Directory.Exists(dbPath)) return DBLL.LDBP;
            if (!File.Exists(dbPath + dbFileName)) return DBLL.LDB;
            return DBLL.SC;
        }
        
        DBSL DiretoriesSaveLogs()
        {
            if (!Directory.Exists(dbPath)) return DBSL.SPP;
            if (!File.Exists(dbPath + dbFileName)) return DBSL.SP;
            return DBSL.SC;
        }
       
        
        #endregion

    }
}

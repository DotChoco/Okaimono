namespace Okaimono.src
{
    public static class ErrorMessages{
        public static Dictionary<byte,string> LoadErrors = new Dictionary<byte, string>() {
            {1, "" },
            {2, "" },
            {3, "" },
            {4, "" },
            {5, "" },
        };

        public static Dictionary<byte, string> SaveErrors = new Dictionary<byte, string>() {
            {1, "" },
            {2, "" },
            {3, "" },
            {4, "" },
            {5, "" },
        };

    }
    public class Database
    {

        #region SaveAndLoad


        public void SetData(DataModels database, string path)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.Write(JsonSerializer.Serialize(database));
            writer.Close();
        }


        public DataModels GetData(string path)
        {
            StreamReader data = new(path);
            DataModels dataLoaded = JsonSerializer.Deserialize<DataModels>(data.ReadToEnd());
            data.Close();
            return dataLoaded;
        }


        #endregion


        #region GetBadCases

        public byte TrySaveDB(string path)
        {
            if (!File.Exists(path))
            {
                return 1;
            }
            return 0;
        }
        

        public byte TryGetDB(string path)
        {
            if (!File.Exists(path))
            {
                return 1;
            }
            return 0;
        }

        #endregion

    }
}

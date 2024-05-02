using Models;

namespace Okaimono.src
{
    public class Profile
    {

        #region Variables

        const string profilePath = @"D:/Okaimono/";
        const string profileFileName= "Profile.dcf"; //.dcf = dot choco file
        private (byte, string) dataLogs = default;

        public (byte, string) GetDataLogs { get => dataLogs; }
        public static User user = new();
        #endregion



        #region Public_Methods

        public void CreateUser(string userName) => Create(userName);


        public string ReadUser() => Read(profilePath);


        public string UpdateUser() => UpdateUserData();


        public string DeleteUser() => UpdateUserData();

        #endregion



        #region Private_Methods

        void Create(string userName = "Unknown")
        {
            user = new() { Name = userName, DBPath = profilePath };
            if(!Directory.Exists(profilePath)) 
                Directory.CreateDirectory(profilePath);
            
            StreamWriter streamWriter = new(profilePath + profileFileName);
            streamWriter.Write(JsonSerializer.Serialize(user));
            streamWriter.Close();
        }

        string Read(string profilePath)
        {
            string log = Logs.GetProfileLog(TryGetOrSaveProfile());
            if (log == "Successful Process")
            {
                StreamReader sr = new(profilePath + profileFileName);
                user = JsonSerializer.Deserialize<User>(sr.ReadToEnd());
                sr.Close();
            }
            return log;
        }

        string UpdateUserData()
        {
            string log = Logs.GetProfileLog(TryGetOrSaveProfile());
            if (log == "Successful Process")
            {
                StreamWriter sw = new(profilePath + profileFileName);
                sw.Write(JsonSerializer.Serialize(user));
                sw.Close();
            }
            return log;
        }

        byte TryGetOrSaveProfile()
        {
            if (!Directory.Exists(profilePath)) return 2;
            if (!File.Exists(profilePath + profileFileName)) return 1;
            return 0;
        }

        #endregion

    }
}

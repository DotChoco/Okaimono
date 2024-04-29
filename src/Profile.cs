
using Models;

namespace Okaimono.src
{
    public class Profile
    {

        #region Variables

        const string profilePath = @"D:/Okaimono/";
        const string profileFileName= "Profile.dcf"; //.dcf = dot choco file
        public static User user = new();

        #endregion



        #region Public_Methods

        public void CreateUser(string userName) => Create(userName);


        public void ReadUser() => Read(profilePath);


        public void UpdateUser(string userName, string dbPath) 
            => UpdateUserData(userName, dbPath);


        public void DeleteUser() => UpdateUserData("Unknown", profilePath);

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


        void Read(string profilePath)
        {
            if (Logs.ProfileLoadErrors.TryGetValue(TryGetOrSaveProfile(), out string log)
                && log != "Successful Process")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(log);
                Console.ForegroundColor = ConsoleColor.White;
                Create("Unknown");
            }
            else
            {
                StreamReader sr = new(profilePath +profileFileName);
                user = JsonSerializer.Deserialize<User>(sr.ReadToEnd());
                sr.Close();
            }
        }


        void UpdateUserData(string username = "Unknown", string dbPath = profilePath)
        {

            if (Logs.ProfileSaveErrors.TryGetValue(TryGetOrSaveProfile(), out string log)
                && log != "Successful Process")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(log);
                Console.ForegroundColor = ConsoleColor.White;
                Create(username);
            }
            else
            {
                StreamWriter sw = new(dbPath + profileFileName);
                sw.Write(JsonSerializer.Serialize(user));
                sw.Close();
            }
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

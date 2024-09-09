using Models;
using NKDot;

namespace Okaimono.src
{
    using Okaimono.Logs;
    public class Profile
    {

        #region Variables

        const string profilePath = @"D:/Okaimono/";
        const string profileFileName= "Profile.dcf"; //.dcf = dot choco file
        string log = string.Empty;

        public static User? user = new();

        #endregion



        #region Public_Methods

        public string CreateUser(string userName) => Create(userName);


        public string ReadUser() => Read(profilePath);


        public string UpdateUser() => Update(user);


        public string DeleteUser() => Update(new(), true);

        #endregion



        #region Private_Methods

        string Create(string userName = "Unknown")
        {
            user = new() { Name = userName, DBPath = profilePath };
            if(!Directory.Exists(profilePath)) 
                Directory.CreateDirectory(profilePath);
            
            return Update(user, true);
        }


        string Read(string profilePath)
        {
            log = Logs.GetLoadProfileLog(DiretoriesLoadLogs());

            if (log != Logs.SUCCESSFUL_LOG) return log;
            else
            {
                try
                {
                    StreamReader sr = new(profilePath + profileFileName);
                    string dataDecrypt = NKObj.DKRPT(sr.ReadToEnd());
                    sr.Close();

                    user = JsonSerializer.Deserialize<User>(dataDecrypt);
                }
                catch { return Logs.GetLoadProfileLog(PLL.L03); }
            }
            return VoidUserData(user) ? Logs.GetLoadProfileLog(PLL.L04) : Logs.SUCCESSFUL_LOG;
        }


        string Update(User dataUser, bool reset = false)
        {
            log = Logs.GetSaveProfileLog(DiretoriesSaveLogs());

            if (log != Logs.SUCCESSFUL_LOG) return log;
            else
            {
                try{
                    StreamWriter sw = new(profilePath + profileFileName);
                    sw.Write(NKObj.NKRPT(JsonSerializer.Serialize(dataUser)));
                    sw.Close();
                }
                catch { return Logs.GetLoadProfileLog(PLL.L03); }
            }

            return (VoidUserData(dataUser) && !reset || !VoidUserData(dataUser) && reset) 
                ? Logs.GetLoadProfileLog(PLL.L04) : Logs.SUCCESSFUL_LOG;
        }


        bool VoidUserData(User user)
        {
            if (user == null) return true;
            if (user.Name == string.Empty) return true;
            if (!string.IsNullOrEmpty(user.DBPath)) return true;
            return false;
        }

        PLL DiretoriesLoadLogs()
        {
            if (!Directory.Exists(profilePath)) return PLL.LPP;
            if (!File.Exists(profilePath + profileFileName)) return PLL.LP;
            return PLL.SC;
        }
        PSL DiretoriesSaveLogs()
        {
            if (!Directory.Exists(profilePath)) return PSL.SPP;
            if (!File.Exists(profilePath + profileFileName)) return PSL.SP;
            return PSL.SC;
        }

        #endregion

    }
}

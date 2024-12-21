using System.Text;

namespace Okaimono.src.SaveData
{
    using Okaimono.Logs;

    public class Database
    {

        #region Variables

        readonly string? _dbPath = Profile.user.DbPath;
        const string DbFileName = "Data.dcf"; //.dcf = dot choco file
        const string DbAnimeFileName = "Animes.dcf"; //.dcf = dot choco file
        const string DbMangaFileName = "Manga.dcf"; //.dcf = dot choco file
        string _log = string.Empty;
        public DataLists? Data = new();

        #endregion



        #region Public_Methods

        public string SaveData() => Save();

        public string LoadData() => Load();

        public string CreateData() => Create();

        #endregion



        #region Private_Methods

        string Save()
        {
            _log = Logs.GetSaveDBLog(DiretoriesSaveLogs());

            if (Data == null) return Logs.GetSaveDBLog(DBSL.S01);
            if (_log != Logs.SUCCESSFUL_LOG) return _log;
            else {
                try {
                    StreamWriter writer = new(_dbPath + DbAnimeFileName);
                    writer.Write(SaveOwnFormat(Data.AnimeList));
                    writer.Close();
                }
                catch { return Logs.GetSaveDBLog(DBSL.S02); }
            }
            return Logs.SUCCESSFUL_LOG;
        }

        string Load()
        {
            _log = Logs.GetLoadDBLog(DiretoriesLoadLogs());

            if (_log != Logs.SUCCESSFUL_LOG) return _log;
            else {
                try {
                    StreamReader sr = new(_dbPath + DbAnimeFileName);
                    Data.AnimeList = ReadOwnFormat(sr.ReadToEnd());
                    sr.Close();
                    if (Data == null) return Logs.GetLoadDBLog(DBLL.L02);
                }
                catch { return Logs.GetLoadDBLog(DBLL.L03); }
            }
            return Logs.SUCCESSFUL_LOG;
        }

        string Create()
        {
            if (!Directory.Exists(_dbPath))
                Directory.CreateDirectory(_dbPath);
            return Save();
        }

        List<Anime> ReadOwnFormat(string format)
        {
            List<Anime> animes = new();
            foreach (var anime in format.Split('\n'))
            {
                Anime newAnime = new();
                var data = anime.Split('/');
                
                newAnime.Name = data[0];
                newAnime.Tags = data[1].Split("\\").ToList();
                newAnime.InLive = data[2];
                newAnime.NextNewCap = data[3];
                newAnime.MaxCaps = int.Parse(data[4]);
                newAnime.LastViewCap = int.Parse(data[5]);
                newAnime.Prequels = data[6].Split("\\").ToList();
                newAnime.Sequels = data[7].Split("\\").ToList();
                newAnime.Movies = data[8].Split("\\").ToList();
                newAnime.SpinOffs = data[9].Split("\\").ToList();
                newAnime.Ovas = int.Parse(data[10]);
                
                
                animes.Add(newAnime);
            }
            return animes;
        }

        string SaveOwnFormat(List<Anime> animes)
        {
            StringBuilder sb = new();
            foreach (var anime in animes)
            {
                sb.AppendFormat($@"{anime.Name}/");
                foreach (var tag in anime.Tags)
                {
                    sb.AppendFormat($@"{tag}\");
                }
                sb.AppendFormat($@"{anime.InLive}/");
                sb.AppendFormat($@"{anime.NextNewCap}/");
                sb.AppendFormat($@"{anime.MaxCaps}/");
                sb.AppendFormat($@"{anime.LastViewCap}/");
                foreach (var prequel in anime.Prequels)
                {
                    sb.AppendFormat($@"{prequel}\");
                }
                sb.AppendFormat($@"/");
                foreach (var sequel in anime.Sequels)
                {
                    sb.AppendFormat($@"{sequel}\");
                }
                sb.AppendFormat($@"/");
                foreach (var movie in anime.Movies)
                {
                    sb.AppendFormat($@"{movie}\");
                }
                sb.AppendFormat($@"/");
                foreach (var spinOff in anime.SpinOffs)
                {
                    sb.AppendFormat($@"{spinOff}\");
                }
                sb.AppendFormat($@"/{anime.Ovas}/");
            }
            return sb.ToString();
        }
        
        
        DBLL DiretoriesLoadLogs()
        {
            if (!Directory.Exists(_dbPath)) return DBLL.LDBP;
            if (!File.Exists(_dbPath + DbFileName)) return DBLL.LDB;
            return DBLL.SC;
        }

        DBSL DiretoriesSaveLogs()
        {
            if (!Directory.Exists(_dbPath)) return DBSL.SPP;
            if (!File.Exists(_dbPath + DbFileName)) return DBSL.SP;
            return DBSL.SC;
        }


        #endregion

    }
}

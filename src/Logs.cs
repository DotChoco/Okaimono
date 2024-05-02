
namespace Okaimono.src
{
    public static class Logs
    {
        #region Logs

        public static Dictionary<byte, string> DBLoadErrors = new Dictionary<byte, string>() {
        {0, "Successful Process" },
        {1, "Failed Process, error while loading the database because it doesn't exist" },
        {2, "Failed Process, error while loading the database because the path doesn't exist" },
        {3, "Failed Process, error while loading the database L01" }, //L01 = El modelo de carga es diferente
                                                                      //al usado cuando se guardo la data
        {4, "Failed Process, error while loading the database L02" },//L02 = No se pudo cargar un dato en el
                                                                     //modelo ya que no coincidia con el tipo(ej. "hola" => bool yes;)
        {5, "Failed Process, error while loading the database L03" },//L03 = No se pudo cargar la data porque el archivo
                                                                     //de la DB ha sido modificado y quedo corrupto o con otro
                                                                     //tipo de estructura
        };

        public static Dictionary<byte, string> DBSaveErrors = new Dictionary<byte, string>() {
        {0, "Successful Process" },
        {1, "Failed Process, error while saving the database because it doesn't exist" },
        {2, "Failed Process, error while saving the database because the path doesn't exist" },
        {3, "Failed Process, error while loading the database S01" },//S01 = No se pudo guardar un dato porque contenia
                                                                     //un valor nulo 
        {4, "Failed Process, error while loading the database S02" },//S02 = No se pudo guardar porque el serializador
                                                                     //presento un error al convertir la informacion
        {5, "Failed Process, error while loading the database S03" },//S03 = 
        };



        public static Dictionary<byte, string> ProfileLoadErrors = new Dictionary<byte, string>() {
        {0, "Successful Process" },
        {1, "Failed Process, error while loading the profile because it doesn't exist" },
        {2, "Failed Process, error while loading the profile because the path doesn't exist" },
        {3, "Failed Process, error while loading the profile L01" }, //L01 = El modelo de carga es diferente
                                                                      //al usado cuando se guardo la data
        {4, "Failed Process, error while loading the profile L02" },//L02 = No se pudo cargar un dato en el
                                                                     //modelo ya que no coincidia con el tipo(ej. "hola" => bool yes;)
        {5, "Failed Process, error while loading the profile L03" },//L03 = No se pudo cargar la data porque el archivo
                                                                     //del perfil ha sido modificado y quedo corrupto o con otro
                                                                     //tipo de estructura
        };

        public static Dictionary<byte, string> ProfileSaveErrors = new Dictionary<byte, string>() {
        {0, "Successful Process" },
        {1, "Failed Process, error while saving the profile because it doesn't exist" },
        {2, "Failed Process, error while saving the profile because the path doesn't exist" },
        {3, "Failed Process, error while loading the profile S01" },//S01 = No se pudo guardar un dato porque contenia
                                                                     //un valor nulo 
        {4, "Failed Process, error while loading the profile S02" },//S02 = No se pudo guardar porque el serializador
                                                                     //presento un error al convertir la informacion
        {5, "Failed Process, error while loading the profile S03" },//S03 = 
        };



        public static Dictionary<byte, string> BackendErrors = new Dictionary<byte, string>() {
        {1, "Failed Search, the element that you want don't exist B01" },//B01 = Retorna un dato nulo ya que no se pudo encontrar el
                                                                         //elemento que se estaba buscando
        {2, "Failed Redirection, the link doesn't exist or it's incorrect B02" },//B02 = No existe el link al que se hace referencia o
                                                                                 //esta mal escrito
        
        };

        #endregion


        #region GetLogs

        public static string GetBackendLog(byte logCode)
        {
            BackendErrors.TryGetValue(logCode, out string value);
            return value;
        }

        public static string GetProfileLog(byte logCode)
        {
            BackendErrors.TryGetValue(logCode, out string value);
            return value;
        }

        public static string GetDBLog(byte logCode)
        {
            BackendErrors.TryGetValue(logCode, out string value);
            return value;
        }

        #endregion

    }
}


namespace Okaimono.Logs
{
    public enum DBLL //DataBaseLoadLogs
    {
        SC,
        LDB,
        LDBP,
        L01,
        L02,
        L03,
    }
    public enum DBSL //DataBaseSaveLogs
    {
        SC,
        SP,
        SPP,
        S01,
        S02,
        S03,
    }

    public enum PLL //ProfileLoadLogs
    {
        SC,
        LP,
        LPP,
        L01,
        L02,
        L03,
        L04,
    }
    public enum PSL //ProfileSaveLogs
    {
        SC,
        SP,
        SPP,
        S01,
        S02,
        S03,
    }

    public enum BEL //BackendErrorLogs
    {
        SC,
        B01,
        B02,
    }


    public static class Logs
    {

        #region Constants

        const string GENERIC_ERROR = "Unknown error, " +
                                    "please make your report and post it(error code: UEGM01)";
        public const string SUCCESSFUL_LOG = "Successful Process";

        #endregion
        
        
        #region Logs

        public static Dictionary<DBLL, string> DBLoadErrors = new Dictionary<DBLL, string>() {
        {DBLL.SC, SUCCESSFUL_LOG },
        {DBLL.LDB, "Failed Process, error while loading the database because it doesn't exists" },
        {DBLL.LDBP, "Failed Process, error while loading the database because the path doesn't exists" },
        {DBLL.L01, "Failed Process, error while loading the database S01" }, //S01 = El modelo de carga es diferente
                                                                      //al usado cuando se guardo la data
        {DBLL.L02, "Failed Process, error while loading the database S02" },//S02 = No se pudo cargar la data porque durante el proceso de
                                                                    //carga los datos debido a que el serializador no pudo convertir la data
        {DBLL.L03, "Failed Process, error while loading the database S03" },//S03 = No se pudo cargar la data porque el archivo
                                                                     //de la DB ha sido modificado y quedo corrupto o con otro
                                                                     //tipo de estructura
        };

        public static Dictionary<DBSL, string> DBSaveErrors = new Dictionary<DBSL, string>() {
        {DBSL.SC, SUCCESSFUL_LOG },
        {DBSL.SP, "Failed Process, error while saving the database because it doesn't exists" },
        {DBSL.SPP, "Failed Process, error while saving the database because the path doesn't exists" },
        {DBSL.S01, "Failed Process, error while saving the database S01" },//S01 = No se pudo guardar un dato porque contenia
                                                                     //un valor nulo 
        {DBSL.S02, "Failed Process, error while saving the database S02" },//S02 = No se pudo guardar porque el serializador
                                                                     //presento un error al convertir la informacion
        {DBSL.S03, "Failed Process, error while saving the database S03" },//S03 = 
        };


        
        public static Dictionary<PLL, string> ProfileLoadErrors = new Dictionary<PLL, string>() {
        {PLL.SC, SUCCESSFUL_LOG },
        {PLL.LP, "Failed Process, error while loading the profile because it doesn't exists" },
        {PLL.LPP, "Failed Process, error while loading the profile because the path doesn't exists" },
        {PLL.L01, "Failed Process, error while loading the profile S01" }, //S01 = El modelo de carga es diferente
                                                                      //al usado cuando se guardo la data
        {PLL.L02, "Failed Process, error while loading the profile S02" },//S02 = No se pudo cargar la data porque el archivo
                                                                     //del perfil ha sido modificado y quedo corrupto o con otro
                                                                     //tipo de estructura
        {PLL.L03, "Failed Process, error while loading the profile S03" },//S03 = No se pudo cargar la data porque durante el proceso de
                                                                    //carga los datos se perdieron o hubo una alteracion en tiempo
                                                                    //de ejecucion que corrompio los datos
        {PLL.L04, "Failed Process, error while loading the profile L04" },//L04 = Se realizo el proceso, pero durante la carga se devolvio
                                                                    //un objeto vacio
        };

        public static Dictionary<PSL, string> ProfileSaveErrors = new Dictionary<PSL, string>() {
        {PSL.SC, SUCCESSFUL_LOG },
        {PSL.SP, "Failed Process, error while saving the profile because it doesn't exists" },
        {PSL.SPP, "Failed Process, error while saving the profile because the path doesn't exists" },
        {PSL.S01, "Failed Process, error while saving the profile S01" },//S01 = No se pudo guardar un dato porque contenia
                                                                     //un valor nulo 
        {PSL.S02, "Failed Process, error while saving the profile S02" },//S02 = No se pudo guardar porque el serializador
                                                                     //presento un error al convertir la informacion
        {PSL.S03, "Failed Process, error while saving the profile S03" },//S03 = Se realizo el proceso pero el contenido guardado
                                                                   //esta alterado o no coincidia con el tipo de dato necesario
        };



        public static Dictionary<BEL, string> BackendErrors = new Dictionary<BEL, string>() {
        {BEL.B01, "Failed Search, the element that you want doesn't exists B01" },//B01 = Retorna un dato nulo ya que no se pudo encontrar el
                                                                         //elemento que se estaba buscando
        {BEL.B02, "Failed Redirection, the link doesn't exist or it's incorrect B02" },//B02 = No existe el link al que se hace referencia o
                                                                                 //esta mal escrito
        
        };

        #endregion


        #region GetLogs

        public static string GetBackendLog(BEL logCode)
        {
            BackendErrors.TryGetValue(logCode, out string? value);
            return value != null ? value : GENERIC_ERROR;
        }

        public static string GetSaveProfileLog(PSL logCode)
        {
            ProfileSaveErrors.TryGetValue(logCode, out string? value);
            return value != null ? value : GENERIC_ERROR;
        }
        public static string GetLoadProfileLog(PLL logCode)
        {
            ProfileLoadErrors.TryGetValue(logCode, out string? value);
            return value != null ? value : GENERIC_ERROR;
        }

        public static string GetLoadDBLog(DBLL logCode)
        {
            DBLoadErrors.TryGetValue(logCode, out string? value);
            return value != null ? value: GENERIC_ERROR;
        }

        public static string GetSaveDBLog(DBSL logCode)
        {
            DBSaveErrors.TryGetValue(logCode, out string? value);
            return value != null ? value : GENERIC_ERROR;
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okaimono.src
{
    public static class Logs
    {

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

    }
}

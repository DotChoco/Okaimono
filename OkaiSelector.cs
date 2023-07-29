

namespace Okaimono_CMD_Version
{
    public class OkaiSelector
    {
        DCFF DCFF = new DCFF();

        public OkaiSelector()
        {

            DCFF.Main();

            string? Answer = default;

            while (Answer != "E" || Answer != "e" || Answer == null)
            {
                Console.Clear();
                Presentation();
                Console.WriteLine("\n\n\n\n");
                Console.WriteLine("Salir [E/e]  Agregar[C/c]  Buscar[S/s]  Eliminar[D/d]  Editar[T/t]");

                Console.WriteLine("\n\n\n");
                Answer = Console.ReadLine();
                if (Answer == "E" || Answer == "e")
                {
                    DCFF.CloseProgram();
                    break;
                }
                else if (Answer == "C" || Answer == "c")
                {
                    DCFF.CreateNewItem();
                }
                else if (Answer == "S" || Answer == "s")
                {
                    DCFF.SearchItem();
                }
                else if (Answer == "D" || Answer == "d")
                {
                    DCFF.DeleteItem();
                }
                else if (Answer == "T" || Answer == "t")
                {
                    DCFF.TransformItem();
                }
                else if (Answer == "K" || Answer == "k")
                {
                    DCFF.KoffiForMe();
                }

            }

        }


        void Presentation()
        {
            Console.WriteLine(@"  ____  __        _                    " + "\n" +
                              @" / __ \/ /_____ _(_)_ _  ___  ___  ___" + "\n" +
                              @"/ /_/ /  '_/ _ `/ /  ' \/ _ \/ _ \/ _ \" + "\n" +
                              @"\____/_/\_\\_,_/_/_/_/_/\___/_//_/\___/");
            Console.WriteLine("\n\n\n");
            if (DCFF.GetPDOC)
            {
                DCFF.PrintDoc();
            }
            else
            {
                DCFF.PrintAnimesForToday();
            }
            Console.WriteLine("\n\n");

        }







    }
}

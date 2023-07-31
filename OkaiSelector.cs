namespace Okaimono_Desktop
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
                Console.WriteLine("Salir [E/e]  Agregar[C/c]  Buscar[S/s]  Eliminar[D/d]  Editar[T/t]  Info[I/i]");

                Console.WriteLine("\n\n");
                Answer = Console.ReadLine();
                Answer = Answer.ToLower();

                if (Answer == "e")
                {
                    DCFF.CloseProgram();
                    break;
                }
                else if (Answer == "c")
                {
                    DCFF.CreateNewItem();
                }
                else if (Answer == "s")
                {
                    DCFF.SearchItem();
                }
                else if (Answer == "d")
                {
                    DCFF.DeleteItem();
                }
                else if (Answer == "t")
                {
                    DCFF.TransformItem();
                }
                else if (Answer == "k")
                {
                    DCFF.BuyMeAKoffi();
                }
                else if (Answer == "i")
                {

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

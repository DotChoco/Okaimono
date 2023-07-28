

namespace Okaimono_CMD_Version
{
    public class OkaiSelector
    {
        DCFF DCFF = new DCFF();

        public OkaiSelector()
        {

            //Presentation();
            DCFF.Main();

            string? Answer = default;

            while (Answer != "E" || Answer != "e" || Answer == null)
            {
                Console.WriteLine("\n\nEscribe (E/e) para salir al menu principal");
                Answer = Console.ReadLine();
                if (Answer == "E" || Answer == "e")
                {
                    DCFF.CloseProgram();
                    break;
                }
                else if (Answer == "C" || Answer == "e")
                {
                    DCFF.CreateNewItem();
                }
                else if (Answer == "S" || Answer == "e")
                {
                    DCFF.SearchItem();
                }
                else if (Answer == "D" || Answer == "e")
                {
                    DCFF.DeleteItem();
                }
                else if (Answer == "T" || Answer == "e")
                {
                    DCFF.TransformItem();
                }
                else if (Answer == "K" || Answer == "e")
                {
                    DCFF.KoffiForMe                     ();

                }

            }

        }




        void Presentation(){
            
        }


        


    }
}

using DCTI.Components;

namespace Okaimono
{
    // public enum RenderObjs{
    //     None, 
    //     Table, 
    //     InputField, 
    //     Text, 
    //     CheckBox,
    //     Tree,
    //     Region
    // } 
    public class Render {
        public Render() { 

            // Console.Clear();
            // InputField inputField = new(new()
            //     {text = "Placeholder", color = "879EA6" },"FE94FF");
            // inputField.SetPosition(new(10,3));
            // inputField.SetScale(new(25,3));
            // inputField.Render();
            // inputField.ReadInput();


            string[,] content0 = { { "hola", "estas" } };
            string[,] content = { 
                { "hola", "como", "estas" }, 
                { "yo", "muy", "bien" }, 
                { "gracias", "por", "preguntar" }
            };
            // string[,] content = { 
            //     { "hola", "como", "estas"}, 
            //     {"yo", "cansado\npero", "bien"}, 
            //     {"gracias\nasdad", "por", "preguntar"}
            // };
            Console.CursorVisible = false;
            Table tabless = new(new(content0), new(10,3));
            tabless.Render();
            
            Table table = new(new(content), new(10,5));
            table.Render();
            Console.ReadKey();
        }

        



    }



}
using DCTI;

namespace Okaimono
{
    // public enum RenderObjs{
    //     None, 
    //     Table, 
    //     InputField, 
    //     Text, 
    //     Selector, 
    //     Border 
    // } 
    public class Render {
        int consoleHeight = default;
        int consoleWidth = default;
        
        public Render() { 
            Console.Clear();
            // consoleHeight = Console.WindowHeight;
            // consoleWidth = Console.WindowWidth;
            // InputField inputField = new(new()
            //     {text = "Placeholder", color = "879EA6" },"FE94FF");
            // inputField.SetPosition(new(10,3));
            // inputField.SetScale(new(25,3));
            // inputField.Render();
            // inputField.ReadInput();
            string[,] content = { 
                { "hola", "como", "estas"}, 
                {"yo", "muy", "bien"}, 
                {"gracias", "por", "preguntar"}
            };
            
            Table table = new(new(content));
            table.Render();
        }


        public T Draw<T>(T obj, sbyte height, sbyte width) {
            
            return obj;
        }

    }



}
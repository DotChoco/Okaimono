using System.Net.Http.Headers;
using DCTI.Components;
using DCTI.Structs;

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
    public sealed class Render {
        public Render() { 

            Console.Clear();
            // InputField inputField = new(new()
            //     {text = "Placeholder", color = "879EA6" },"FE94FF");
            // inputField.SetPosition(new(10,3));
            // inputField.SetScale(new(25,3));
            // inputField.Render();
            // inputField.ReadInput();


            // string[,] content0 = { { "hola", "estas" } };
            string[,] content = { 
                { "hola", "como", "estas" }, 
                { "yo", "muy", "bien" }, 
                { "gracias", "por", "preguntar" }
            };
            string[,] content0 = { 
                { "hola", "como", "estas"}, 
                {"yo", "cansado\npero", "bien"}, 
                {"gracias\nasdad", "por", "preguntar"}
            };
            Console.CursorVisible = false;
            Table tabless = new(new(content), new(10,3));
            tabless.Render();
            
            // Tree nw = new();
            // var data = new List<TItem>()
            // {
            //     new(){Content = "Holis"},
            //     new(){Content = "Como"},
            //     new()
            //     {
            //         Content = "Tas", 
            //         Children = new(){ 
            //             new(){ Content = "Bien" },
            //             new(){ Content = "Y" },
            //             new(){ Content = "Tu?" }
            //         }
            //     }
            // };
            // nw.Content = data;
            // nw.Transform.position = new(10,2);
            // nw.Render();
        }

        


    }



}
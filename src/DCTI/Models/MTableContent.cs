using DCTI.Structs.Enums;

namespace DCTI.Structs
{
    public class MTableContent {

        //Public
        public Vector2 ItemsMaxLenght; 
        
        public string[,] Content;
        public string TbColor = string.Empty;
        public string TextColor = string.Empty;

        
        public MTableContent(string[,] content) => Content = content;

    }

}
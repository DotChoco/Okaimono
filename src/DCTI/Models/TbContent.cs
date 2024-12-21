using DCTI.Structs;

namespace DCTI.Models;
public sealed class TbContent {
    
    public Vector2 ItemsMaxLenght; 
    public string[,] Content;
    public string TbColor = string.Empty;
    public string TextColor = string.Empty;

    public TbContent(string[,] content) => Content = content;

}
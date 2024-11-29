namespace DCTI.Intefaces
{
    public interface IText {
        const string DEFAULT_TEXT_COLOR = "AC90D8";
        const int HexMask = 0xFF;
        
        
        void SetTextColor(string hex = DEFAULT_TEXT_COLOR){
            if (hex == string.Empty)
                hex = DEFAULT_TEXT_COLOR;

            // Convert hex string to 24 bits integer
            int color = Convert.ToInt32(hex, 16);

            // Extract RGB components using bitwise operations
            int r = (color >> 16) & HexMask; // Bits 16-23
            int g = (color >> 8) & HexMask;  // Bits 8-15
            int b = color & HexMask;         // Bits 0-7

            // Use ANSI escape to change the color text
            Console.Write($"\u001b[38;2;{r};{g};{b}m");
        }
        string GetDefaultColor() => DEFAULT_TEXT_COLOR;
        void ResetTextColor() => Console.Write("\u001b[0m");
        
        
    }
}
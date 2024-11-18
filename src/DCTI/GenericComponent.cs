using DCTI.Models;

namespace DCTI.Components
{
    public abstract class GenericComponent{

        //Cons
        protected const char TL_CORNNER = (char)9484;
        protected const char TR_CORNNER = (char)9488;
        protected const char INNER_LINE = (char)9472;
        protected const char VERTICA_BAR = (char)9474;
        protected const char BL_CORNNER = (char)9492;
        protected const char BR_CORNNER = (char)9496;
        protected const string DEFAULT_TEXT_COLOR = "AC90D8";

        //Public 
        public Transform transform = new();
        public Align align = Align.Left;

        //Protected
        protected Vector2 cursorPos = new Vector2();
        protected const int HexMask = 0xFF;
        
        
        public void SetScale(Vector2 scale) => transform.scale = scale;

        public void SetPosition(Vector2 position) => transform.position = position;

        public void SetTextColor(string hex = DEFAULT_TEXT_COLOR){
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
    

        public void ResetTextColor() => Console.Write("\u001b[0m");

        public Vector2 GetCursorPosition() => cursorPos;
        

        protected void SetCursorPosition(float x = 0, float y = 0){
            //Set Horizontal Axis
            if(x == 0)
                cursorPos = new(transform.position.x, cursorPos.y); 
            else
                cursorPos = new(x, cursorPos.y); 
            //Set Vertical Axis
            if(y == 0)
                cursorPos = new(cursorPos.x, transform.position.y); 
            else
                cursorPos = new(cursorPos.x, y); 
            
            Console.SetCursorPosition((int)cursorPos.x, (int)cursorPos.y);
        }


    }
}
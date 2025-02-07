using DCTI.Structs;

namespace DCTI.Models
{
    public abstract class Component {
        //Public 
        public Transform Transform = new() ;
        
        //Protected
        protected Vector2 CursorPosition = new();

        //Methods
        public void SetScale(Vector2 scale) => Transform.scale = scale;
        public Vector2 GetCursorPosition() => CursorPosition;

        protected void SetCursorPosition(int x, int y)
            => SetCursorPosition(new Vector2(x, y)); 
        protected void SetCursorPosition(Vector2 pos = default)
        {
            //If dont have space in the terminal, it make more
            ExpandTerminalSize(pos);
            
            //Set Horizontal Axis
            if(pos.x == 0)
                CursorPosition = new(Transform.position.x, CursorPosition.y); 
            else
                CursorPosition = new(pos.x, CursorPosition.y); 
            
            //Set Vertical Axis
            if(pos.y == 0)
                CursorPosition = new(CursorPosition.x, Transform.position.y); 
            else
                CursorPosition = new(CursorPosition.x, pos.y); 
            
            //Set New Position
            Console.SetCursorPosition(CursorPosition.x, CursorPosition.y);
        }

        protected void ExpandTerminalSize(Vector2 size)
        {
            //Make a new vertical space in the console
            if (size.y >= Console.WindowHeight) { Console.WindowHeight++; }
            //Make a new horizontal space in the console
            if (size.x >= Console.WindowWidth) { Console.WindowWidth++; }
        }
        
    }
}
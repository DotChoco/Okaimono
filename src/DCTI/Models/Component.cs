using DCTI.Structs;

namespace DCTI.Models
{
    public abstract class Component {
        //Public 
        public Transform Transform = new();
        
        //Methods
        public void SetScale(Vector2 scale) => Transform.scale = scale;
        public void SetPosition(Vector2 position) => Transform.position = position;
        
        //Protected
        protected Vector2 CursorPos = new();

        //Methods
        public Vector2 GetCursorPosition() => CursorPos;

        protected void SetCursorPosition(int x, int y)
        { SetCursorPosition(new Vector2(x, y)); }
        protected void SetCursorPosition(Vector2 pos = default){
            
            //Make a new vertical space in the console
            if (pos.y >= Console.WindowHeight) { Console.WindowHeight++; }
            //Make a new horizontal space in the console
            if (pos.x >= Console.WindowWidth) { Console.WindowWidth++; }
            
            //Set Horizontal Axis
            if(pos.x == 0)
                CursorPos = new(Transform.position.x, CursorPos.y); 
            else
                CursorPos = new(pos.x, CursorPos.y); 
            
            //Set Vertical Axis
            if(pos.y == 0)
                CursorPos = new(CursorPos.x, Transform.position.y); 
            else
                CursorPos = new(CursorPos.x, pos.y); 
            
            //Set New Position
            Console.SetCursorPosition(CursorPos.x, CursorPos.y);
        }

        protected void ExpandTerminalSize(Vector2 size)
        {
            
        }
        
    }
}
using System.Text;
using DCTI.Components;

namespace DCTI
{
    public class InputField: FieldComponent{

        #region Variables
        
        //CONS
        const string DEFAULT_PH_COLOR = "A2B9C1";

        //Private
        string placeHolderColor = DEFAULT_PH_COLOR;
        string textColor = DEFAULT_TEXT_COLOR;
        string fieldContent = string.Empty;
        StringBuilder inputBuilder = new();

        //public
        public string PlaceHolder = string.Empty;

        //Encapsulation
        public string Text { get => fieldContent; }
        
        #endregion
        

        public InputField(Models.Text ph,
            string textColor = DEFAULT_TEXT_COLOR
        ){
            //Set Colors for the Placeholder
            if (ph.color != DEFAULT_PH_COLOR || ph.color !=string.Empty)
                placeHolderColor = ph.color;
            
            //Set Colors that applied to Borders and Text
            if (textColor != DEFAULT_TEXT_COLOR || textColor != string.Empty)
                this.textColor = textColor;

            PlaceHolder = ph.text;

            SetPosition(new(0,0));
            SetScale(new(5,3));
            SetCursorPosition();
        }


        public override void Render()
        {
            RenderBorders();
            RenderPlaceholder();
        }


        private void RenderPlaceholder()
        {
            SetTextColor(placeHolderColor);
            SetCursorPosition(cursorPos.x + 1, cursorPos.y + 1);
            Console.Write(PlaceHolder);
            SetCursorPosition();
            SetCursorPosition(cursorPos.x + 1, cursorPos.y + 1);
        }


        protected override void RenderBorders(){
            SetTextColor(textColor);
            SetCursorPosition();
            float curposY = cursorPos.y;
            for (int y = 0; y < (int)transform.scale.y; y++){
                for (int x = 0; x < (int)transform.scale.x; x++)
                {
                    //Connect the cornners with Lines
                    if(x >= 1 && x < (int)transform.scale.x - 2 &&
                    (y == 0 || y == (int)transform.scale.y - 1))
                        Console.Write(INNER_LINE);
                    //Fill the rest of field with spaces 
                    else if(x >= 1 && x < (int)transform.scale.x - 2 &&
                    (y >= 1 || y < (int)transform.scale.y - 1))
                        Console.Write(" ");

                    //Top Left Cornnner
                    if (y == 0 && x == 0)
                        Console.Write(TL_CORNNER);
                    //Top Right Cornnner
                    else if (y == 0 && x == (int)transform.scale.x - 1)
                        Console.Write(TR_CORNNER);
                    
                    //Botton Left Cornnner
                    else if (x == 0 && y == (int)transform.scale.y - 1)
                        Console.Write(BL_CORNNER);
                    //Botton Right Cornnner
                    else  if (x == (int)transform.scale.x - 1 && 
                        y == (int)transform.scale.y - 1)
                        Console.Write(BR_CORNNER);

                    else if(x == 0 || x == (int)transform.scale.x - 1)
                            Console.Write(VERTICA_BAR);
                    
                }
        
                curposY++;
                SetCursorPosition(cursorPos.x, curposY);
            }

            SetCursorPosition();

        }
        

        public void ReadInput(){
            SetTextColor(textColor);
            // Use the StringBuilder to make the input result
            inputBuilder = new();
            ConsoleKeyInfo keyPress;
            RenderBorders();
            SetCursorPosition(cursorPos.x + 1, cursorPos.y + 1);
            while (true)
            {
                
                // Read key pressed
                keyPress = Console.ReadKey(intercept: true); 

                // Keep data in the field and escape
                if (keyPress.Key == ConsoleKey.Enter)
                {
                    break;
                }
                // Delete data from the input and escape
                else if (keyPress.Key == ConsoleKey.Escape)
                {
                    inputBuilder.Clear();
                    Console.WriteLine(); // Nueva línea
                    break;
                }
                // Delete char per char in the input
                else if (keyPress.Key == ConsoleKey.Backspace && inputBuilder.Length >= 0)
                {
                    if(Console.GetCursorPosition().Left >= cursorPos.x + 1){
                        inputBuilder.Length--; 
                        Console.Write("\b \b"); 
                    }
                }
                
                //Save and print the character 
                else
                {
                    if(Console.GetCursorPosition().Left <= (cursorPos.x + transform.scale.x) - 4){
                        inputBuilder.Append(keyPress.KeyChar); 
                        Console.Write(keyPress.KeyChar);     
                    }
                    
                }
            }


            ResetTextColor();
            
            // if(inputBuilder != null && inputBuilder.Length > 0){
            //     Console.WriteLine($"\nEl input field contiene: {inputBuilder}");
            // }
            // else {
            //     Console.WriteLine($"\nEl input field esta vacio");
            // }
        }
        

        
    }

}
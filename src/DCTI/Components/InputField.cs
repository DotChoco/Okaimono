using System.Text;
using DCTI.Structs;
using DCTI.Intefaces;
using DCTI.Models;

namespace DCTI.Components;

public sealed class InputField: MRenderable, IText{

    #region Variables
    
    //CONS
    const string DEFAULT_PH_COLOR = "A2B9C1";

    //Private
    MText content = new();
    StringBuilder inputBuilder = new();

    //public
    public MText PlaceHolder = new();

    //Encapsulation
    public string Text { get => content.text; }

    #endregion


    public InputField(MText ph,
        string textColor = IText.DEFAULT_TEXT_COLOR,
        string componentColor = IText.DEFAULT_TEXT_COLOR
    ){
        //Set Colors for the Placeholder
        if (ph.color != DEFAULT_PH_COLOR && ph.color !=string.Empty)
            PlaceHolder.color = ph.color;
        
        //Set Colors that applied to Borders
        if (textColor != IText.DEFAULT_TEXT_COLOR && textColor != string.Empty)
            content.color = textColor;
        else 
            content.color = IText.DEFAULT_TEXT_COLOR;
        
        //Set Colors that applied to Text
        if (componentColor != IText.DEFAULT_TEXT_COLOR && componentColor != string.Empty)
            BorderColor = textColor;
        
        
        PlaceHolder.text = ph.text;

        SetPosition(new(0,0));
        SetScale(new(5,3));
        SetCursorPosition();
    }


    public sealed override void Render()
    {
        RenderBorders();
        RenderPlaceholder();
    }


    protected sealed override void RenderBorders(){
        ((IText)this).SetTextColor(BorderColor);

        SetCursorPosition();
        int curposY = CursorPos.y;
        for (int y = 0; y < transform.scale.y; y++){
            for (int x = 0; x < transform.scale.x; x++)
            {
                //Connect the cornners with Lines
                if(x >= 1 && x < transform.scale.x - 2 &&
                (y == 0 || y == transform.scale.y - 1))
                    Console.Write(INNER_LINE);
                //Fill the rest of field with spaces 
                else if(x >= 1 && x < transform.scale.x - 2 &&
                (y >= 1 || y < transform.scale.y - 1))
                    Console.Write(" ");

                //Top Left Cornnner
                if (y == 0 && x == 0)
                    Console.Write(TL_CORNNER);
                //Top Right Cornnner
                else if (y == 0 && x == transform.scale.x - 1)
                    Console.Write(TR_CORNNER);
                
                //Botton Left Cornnner
                else if (x == 0 && y == transform.scale.y - 1)
                    Console.Write(BL_CORNNER);
                //Botton Right Cornnner
                else  if (x == transform.scale.x - 1 && 
                    y == transform.scale.y - 1)
                    Console.Write(BR_CORNNER);

                else if(x == 0 || x == transform.scale.x - 1)
                        Console.Write(VERTICAL_BAR);
                
            }
    
            curposY++;
            SetCursorPosition(new(CursorPos.x, curposY));
        }

        SetCursorPosition();

    }
    

    private void RenderPlaceholder()
    {
        ((IText)this).SetTextColor(PlaceHolder.color);
        SetCursorPosition(new(CursorPos.x + 1, CursorPos.y + 1));
        Console.Write(PlaceHolder);
        SetCursorPosition();
        SetCursorPosition(new(CursorPos.x + 1, CursorPos.y + 1));
    }


    public void ReadInput(){
        ((IText)this).SetTextColor(content.color);
        // Use the StringBuilder to make the input result
        inputBuilder = new();
        ConsoleKeyInfo keyPress;
        RenderBorders();
        SetCursorPosition(new(CursorPos.x + 1, CursorPos.y + 1));
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
                if(Console.GetCursorPosition().Left >= CursorPos.x + 1){
                    inputBuilder.Length--; 
                    Console.Write("\b \b"); 
                }
            }
            //Save and print the character 
            else
            {
                if(Console.GetCursorPosition().Left <= (CursorPos.x + transform.scale.x) - 4){
                    inputBuilder.Append(keyPress.KeyChar); 
                    Console.Write(keyPress.KeyChar);     
                }
                
            }
        }

        ((IText)this).ResetTextColor();
        content.text = inputBuilder.ToString();
    }

}

    
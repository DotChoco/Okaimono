using DCTI.Components;
using DCTI.Models;

namespace DCTI
{
    public class Table: FieldComponent{
        
        #region Variables
        
        //CONS
        const string DEFAULT_PH_COLOR = "A2B9C1";
        
        //Private
        string placeHolderColor = DEFAULT_PH_COLOR;
        string textColor = DEFAULT_TEXT_COLOR;
        string fieldContent = string.Empty;
        TableContent tb = new();

        //public
        public string PlaceHolder = string.Empty;

        //Encapsulation
        
        #endregion
        


        public Table(TableContent tb){ 
            this.tb = tb;
            if (tb.color != string.Empty)
                textColor = tb.color;
            
            GetMaxRowLenghts();


        }

        private void GetMaxRowLenghts(){
            int dataLength = 0;
            for (int row = 0; row < tb.content.GetLength(0); row++){
                for (int col = 0; col < tb.content.GetLength(1); col++)
                {
                    dataLength = tb.content[row,col].Length;

                    if (row == 0){
                        if (dataLength > tb.GetMaxColumLenght())
                            tb.SetMaxColumLenght(dataLength);
                    }
                    else{
                        if (dataLength > tb.GetMaxRowLenght())
                            tb.SetMaxRowLenght(dataLength);
                    }
                }
            }
        
        }


        public override void Render()
        {
            BorderMapping();
            RenderBorders();
            Console.ReadLine();
        }

        protected override void RenderBorders()
        {
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

        private void BorderMapping(){
            SetScale(new(10,10));
        }


    }


}



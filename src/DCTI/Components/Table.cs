using System.Text;
using Windows.Networking.Sockets;
using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Components
{
    public sealed class Table: Renderable {
        
        #region Variables
        
        //Private
        private StringBuilder _sb = new();
        private TbContent _tb = new(new string[,] { { "Example" }});
        private string _textColor = MColor.DEFAULT_COLOR;
        private string[] _tbRender = Array.Empty<string>();
        
        //public
        public string PlaceHolder = string.Empty;

        #endregion



        public Table(TbContent tb, Vector2 position){ 
            _tb = tb;
            if (tb.TbColor != string.Empty)
                BorderColor = tb.TbColor;
                
            if (tb.TextColor != string.Empty)
                _textColor = tb.TextColor;
            
            if(position.x != default && position.y != default)
                Transform.position = position;
            _tb.ItemsMaxLenght = new(10, 1);
            BorderMapping();
        }


        private void BorderMapping(){
            
            string data = string.Empty;
            StringBuilder sbRow = new();
            int extraRows = 0, rowSize = _tb.Content.GetLength(0);
            int colSize = _tb.Content.GetLength(1);
            int baseSeparators = 2, colSeparators = colSize - 1;
            int tableLength = (colSize * _tb.ItemsMaxLenght.x) 
                              + baseSeparators + colSeparators;
            
            colSeparators = _tb.Content.GetLength(1) - 1;

            for (int i = 0; i < rowSize; i++)
                { extraRows += ItHasJumps(_tb.Content, i);}
            
            _tbRender = new string[rowSize + baseSeparators + extraRows];
            
            for (int row = 0; row < rowSize; row++) {
                if (row == 0) { RenderMiddleLines(tableLength, 0); }
                
                int multLines = ItHasJumps(_tb.Content, row);
                _sb.Append(VERTICAL_BAR);
                
                for (int colum = 0; colum < colSize; colum++) {
                    data = _tb.Content[row, colum];
                    if (multLines > 0) {
                        sbRow.Append(RowItem(data));
                        _sb.Append(sbRow.ToString());
                    }
                    else {
                        _sb.Append(data);
                        _sb.Append(new string(' ', 
                            _tb.ItemsMaxLenght.x - data.Length));
                        _sb.Append(VERTICAL_BAR);
                    }
                }

                _tbRender[row + 1] = _sb.ToString();
                _sb.Clear();
                
                RenderMiddleLines(tableLength, row + 2);
            }
        }


        public sealed override void Render() => RenderBorders();


        protected override void RenderBorders()
        {
            MColor.SetTextColor(BorderColor);
            for (int i = 0; i < _tbRender.Length; i++)
            {
                SetCursorPosition(Transform.position.x, Transform.position.y + i);
                Console.Write(_tbRender[i]);
            }
        }


        string RowItem(string data)
        {
            StringBuilder sbRow = new();
            for (int i = 0; i < data.Length; i++) {
                if (i == data.Length - 1)
                {
                    int rest = data.Length / _tb.ItemsMaxLenght.x + 1;
                    sbRow.Append(new string(' ', 
                        _tb.ItemsMaxLenght.x - rest));
                    sbRow.Append(VERTICAL_BAR);
                }
                if (i % _tb.ItemsMaxLenght.x == 0)
                    sbRow.Append($"{data[i]}\n{VERTICAL_BAR}");
                else
                    sbRow.Append($"{data[i]}");
            }
            return sbRow.ToString();
        }

        void RenderMiddleLines(int tableLength, int index)
        {
            for (int i = 0; i < tableLength; i++) {
                if (i % (_tb.ItemsMaxLenght.x + 1) == 0)
                    _sb.Append(VERTICAL_BAR);
                else
                    _sb.Append(INNER_LINE);
            }
            _tbRender[index] = _sb.ToString();
            _sb.Clear();
        }

        int ItHasJumps(string[,] data, int rowIndex)
        {
            int lines = 0;
            for (int i = 0; i < data.GetLength(0); i++) {
                if(data[rowIndex,i].Contains('\n') || data[rowIndex,i].Contains('\r'))
                    lines++;
            }

            return lines;
        }

        
    }


}



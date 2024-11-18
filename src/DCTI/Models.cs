
namespace DCTI.Models
{
    
    public struct Vector2 {
        public float x { get;}
        public float y { get; }

        public Vector2(float x = 0, float y = 0) { 
            this.x = x; 
            this.y = y;
        }
    }

    public struct Text {
        public Align align { get; set; }
        public string text { get; set; }
        public string color { get; set; }
    }


    public struct Transform {
        public Vector2 position { get; set; }
        public Vector2 scale { get; set; }

    }

    public struct TableContent {

        //Cons


        //Private
        int rowMaxLength;
        int columMaxLength;
        
        //Public
        public string[,] content;
        public Align contentAlign;
        public string color = string.Empty;

        //Encapsulation
        public int GetMaxRowLenght() => rowMaxLength;
        public int SetMaxRowLenght(int value) => rowMaxLength = value;
        public int GetMaxColumLenght() => columMaxLength;
        public int SetMaxColumLenght(int value) => columMaxLength = value;


        public TableContent(string[,] content) => this.content = content;

    }


}



namespace DCTI.Models
{
    public abstract class MRegion: Component {
        //Cons
        protected const char TL_CORNNER = (char)9484;
        protected const char TR_CORNNER = (char)9488;
        protected const char INNER_LINE = (char)9472;
        protected const char VERTICAL_BAR = (char)9474;
        protected const char BL_CORNNER = (char)9492;
        protected const char BR_CORNNER = (char)9496;
        
        //Protected
        protected string BorderColor = "AC90D8";

        //Methods
        protected abstract void RenderBorders();
        

        
    }
}
using DCTI.Components;

namespace DCTI
{
    public class Text: GenericComponent{
        
        public Text(Models.Text text){
            SetTextColor(text.color);
            SetPosition(transform.position);
        }
    }
}
using DCTI.Models;
using DCTI.Intefaces;
using DCTI.Structs;

namespace DCTI.Components;

public sealed class Text: Component, IText{
    
    public Text(MText text, Transform transform){
        this.transform = transform;

        ((IText)this).SetTextColor(text.color.ToString());
        SetPosition(base.transform.position);
    }

    public override void Render()
    {

        
    }
}

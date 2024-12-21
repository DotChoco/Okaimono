using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Components;

public sealed class Text: Renderable {
    private MColor Color { get; set; } = new();
    private MText _text = new();
    public Text(MText text, Transform transform){
        _text = text;
        Transform = transform;
        
        MColor.SetTextColor(text.color.ToString());
    }

    protected override void RenderBorders(){}

    public override void Render()
    {
        SetCursorPosition(Transform.position);
        Console.Write(_text.value);
    }
    
}

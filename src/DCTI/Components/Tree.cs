using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Components;


/// <summary>
/// Tree Item
/// </summary>
public sealed class TItem {
    public string HexColor { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<TItem> Children { get; set; } = new();
}


public sealed class Tree: Renderable {
    
    public List<TItem> Content { get => _content; set => _content = value; }
    private List<TItem> _content = new();

    public Tree(List<TItem> items, Vector2 position) {
        _content = items;
        Transform.position = position;
    }

    public Tree() { }

    public override void Render() { RenderChildren(_content); }

    void RenderChildren(List<TItem> items, int spaces = 1) {
        int posy = Transform.position.y;
        SetPosition(Transform.position);
        foreach (var item in items) {
            MColor.SetTextColor(item.HexColor);

            if (spaces > 1) {
                var tabs = new string(' ', spaces/2);
                var lines = new string('-', (spaces / 2));
                Console.Write($"{tabs}|{lines}{item.Content}\n");
            }
            else
                Console.Write($"{item.Content}\n");
            
            posy+=1;
            SetPosition(new(Transform.position.x, posy));
            
            if (item.Children != null && item.Children.Count > 0) {
                RenderChildren(item.Children, spaces+=1);
            }
        }
    }
    
    protected override void RenderBorders() { }
}
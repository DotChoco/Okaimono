namespace DCTI.Intefaces
{
    public interface IFieldComponent {
        string Color { get; set; }
        string DefaultColor() => "AC90D8";



        //Methods
        void RenderBorders();
        void Render();

    }
}
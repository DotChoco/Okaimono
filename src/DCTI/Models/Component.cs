using DCTI.Structs;

namespace DCTI.Models
{
    public abstract class Component {
        //Public 
        public Transform transform = new();

        //Methods
        public void SetScale(Vector2 scale) => transform.scale = scale;
        public void SetPosition(Vector2 position) => transform.position = position;

        public abstract void Render();



    }
}
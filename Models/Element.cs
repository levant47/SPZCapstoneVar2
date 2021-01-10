namespace SPZCapstoneVar2.Models
{
    public class Element
    {
        public int Id { get; set; }

        public ElementType Type { get; set; }

        public double PositionX { get; set; }

        public double PositionY { get; set; }

        public bool InputElementValue { get; set; } = false;
    }
}

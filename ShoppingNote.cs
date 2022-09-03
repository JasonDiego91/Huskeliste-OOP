namespace Huskeliste
{
    internal class ShoppingNote:SharedData
    {
        public string GetLength()
        {
            return Length.ToString("hh:mm");
        }

    }
}

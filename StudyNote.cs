namespace Huskeliste
{
    internal class StudyNote:SharedData
    {
        public string GetLength()
        {
            return Length.ToString("hh:mm");
        }

    }
}

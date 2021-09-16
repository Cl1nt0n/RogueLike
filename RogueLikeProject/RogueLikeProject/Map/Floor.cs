namespace RogueLikeProject.GameWorld
{
    public class Floor : MapElement
    {
        public Floor(int x, int y) : base(Resource.floor)
        {
            X = x;
            Y = y;
        }
    }
}

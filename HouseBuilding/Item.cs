namespace HouseBuilding
{
    public class Item
    {
        public string MainImage { get; set; }

        public override string ToString()
            => string.Format($"MainImage: {MainImage}\n");
    }
}

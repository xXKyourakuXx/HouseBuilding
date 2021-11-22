namespace HouseBuilding
{
    public class MainCategoryItem
    {
        public string Name { get; set; }
        public System.Collections.Generic.IList<string> Images { get; set; }
        public string MainImage { get; set; }

        public override string ToString()
        {
            System.Text.StringBuilder b = new System.Text.StringBuilder($"Name: {this.Name}\nImages:\n");
            
            foreach (var i in Images)
                b.Append($"\t{i}\n");
            b.Append("\n");
            
            return b.ToString();
        }
    }
}

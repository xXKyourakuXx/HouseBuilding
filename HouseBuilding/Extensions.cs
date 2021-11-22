namespace HouseBuilding
{
    static public class Extensions
    {
        public static string ExtractName(this string path)
        {
            var tmp = path.Split('\\');
            return tmp[tmp.Length - 1];
        }
    }
}

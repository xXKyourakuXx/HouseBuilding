namespace HouseBuilding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class DataController
	{
		public static IList<MainCategoryItem> LoadData()
		{
			IList<MainCategoryItem> res = new List<MainCategoryItem>();

            string dataDir = Path.Combine(Environment.CurrentDirectory, "Data");
            
            foreach (var dir in Directory.GetDirectories(dataDir))
            {
                var tmp = new MainCategoryItem { Name = dir.ExtractName(), Images = ReadPNGs(dir) };
                tmp.MainImage = tmp.Images[0];
                res.Add(tmp);
            }
                
            return res;
		}

        private static IList<string> ReadPNGs(string path)
            => Directory.GetFiles(path).Where(file => file.EndsWith(".png")).ToList();
	}
}
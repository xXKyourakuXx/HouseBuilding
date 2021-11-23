namespace HouseBuilding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class DataController
	{
        static private Dictionary<string, List<string>> pboxMapped;

		public static IList<MainCategoryItem> LoadData()
		{
			IList<MainCategoryItem> res = new List<MainCategoryItem>();

            InitializePboxMapped();

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

        public static (string,string) GetLocation(SubCategoryItem item)
        {
            var res = (string.Empty, string.Empty);
            string value = item.Item.MainImage.ToLower();

            var filtered = pboxMapped.Where(x => value.Contains(x.Key)).FirstOrDefault().Value;
            
            res.Item1 = filtered[0];
            
            if(filtered.Count > 1)
                res.Item2 = filtered[1];

            return res;
        }

        static private void InitializePboxMapped()
        {
            pboxMapped = new Dictionary<string, List<string>>();
            pboxMapped.Add("ablak", new List<string>(){ "pictureBoxWindowLeft", "pictureBoxWindowRight" });
            pboxMapped.Add("ajto", new List<string>() { "pictureBoxDoor" });
            pboxMapped.Add("dísz", new List<string>() { "pictureBoxDecoration" });
            pboxMapped.Add("csengo", new List<string>() { "pictureBoxBell" });
            pboxMapped.Add("csillag", new List<string>() { "pictureBoxCloudStars" });
            pboxMapped.Add("felho", new List<string>() { "pictureBoxCloudStars" });
            pboxMapped.Add("hold", new List<string>() { "pictureBoxPlanet" });
            pboxMapped.Add("nap", new List<string>() { "pictureBoxPlanet" });
            pboxMapped.Add("egosor", new List<string>() { "pictureBoxChristmasLights" });
            pboxMapped.Add("fal", new List<string>() { "pictureBoxWall" });
            pboxMapped.Add("fa", new List<string>() { "pictureBoxTree" });
            pboxMapped.Add("kemeny", new List<string>() { "pictureBoxChimney" });
            pboxMapped.Add("kerites", new List<string>() { "pictureBoxFenceLeft" });
            pboxMapped.Add("labtorlo", new List<string>() { "pictureBoxMat" });
            pboxMapped.Add("mano", new List<string>() { "pictureBoxElf" });
            pboxMapped.Add("postalada", new List<string>() { "pictureBoxLetterBox" });
            pboxMapped.Add("renszarvas", new List<string>() { "pictureBoxReindeer" });
            pboxMapped.Add("teto", new List<string>() { "pictureBoxRoof" });
        }
	}
}
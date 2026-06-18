using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using WpfApp1.Models;
using Newtonsoft.Json;

namespace WpfApp1.Services
{
    internal class TreeStorageService
    {
        private const string FilePath = "tree.json";

        public void Save(TreeInfo tree)
        {
            string json = JsonConvert.SerializeObject(tree);
            File.WriteAllText(FilePath, json);
        }

        public TreeInfo Load()
        {
            if (!File.Exists(FilePath))
            {
                return new TreeInfo
                {
                    Exp = 0
                };
            }

            string json = File.ReadAllText(FilePath);

            return JsonConvert.DeserializeObject<TreeInfo>(json)
                   ?? new TreeInfo
                   {
                       Exp = 0
                   };
        }
    }
}

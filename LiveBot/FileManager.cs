using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LiveBot
{
    public class FileManager
    {
        private string _fileName;

        public FileManager(string fileName)
        {
            _fileName = fileName;
        }

        public List<string> GetRawFlats()
        {
            return File.ReadAllText(_fileName).Split("\n").ToList();
        }

        public void AddNewFlat(string rawFlatData)
        {
            File.AppendAllText(_fileName,$"\n{rawFlatData}");
        }
    }
}
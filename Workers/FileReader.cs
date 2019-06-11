using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scrambler.Workers
{
    class FileReader
    {
        public string[] Read(string fileName)
        {
            string[] fileContext;
            try
            {
                fileContext = File.ReadAllLines(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           

            return fileContext;
        }
    }
}

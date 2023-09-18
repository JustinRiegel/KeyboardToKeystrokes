using KeyboardToKeystrokes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardToKeystrokes.Interfaces
{
    internal interface IMappingsFileService
    {
        public KeyMappingsList LoadMappingFile();

        public void SaveMappingFile(KeyMappingsList mappingsList);
    }
}

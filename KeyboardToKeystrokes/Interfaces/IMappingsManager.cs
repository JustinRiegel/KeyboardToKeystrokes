using KeyboardToKeystrokes.Models;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardToKeystrokes.Interfaces
{
    public interface IMappingsManager
    {
        public Dictionary<int, char> LoadMappingsFromFile();

        public void SaveMappingsToFile(Dictionary<int, char> mappingsDictionary);

        public bool AddMappingToDictionary(SevenBitNumber note, char key);

        public bool RemoveMappingFromDictionary(int midiNote);
    }
}

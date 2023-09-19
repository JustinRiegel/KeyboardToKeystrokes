using Melanchall.DryWetMidi.Common;

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

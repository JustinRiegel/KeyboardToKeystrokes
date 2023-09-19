using KeyboardToKeystrokes.Interfaces;
using Melanchall.DryWetMidi.Common;

namespace KeyboardToKeystrokes.Models
{
    internal class MappingsManager : IMappingsManager
    {
        private IMappingsFileService _mappingsFileService = new MappingsFileService();
        private Dictionary<int, char> _keyMappingsDictionary = new Dictionary<int, char>();

        public Dictionary<int, char> KeyMappingsDictionary
        {
            get { return _keyMappingsDictionary; }
            private set { _keyMappingsDictionary = value; }
        }

        public MappingsManager()
        {

        }

        public Dictionary<int, char> LoadMappingsFromFile()
        {
            KeyMappingsDictionary = GetDictionaryFromMappings(_mappingsFileService.LoadMappingFile());
            return KeyMappingsDictionary;
        }

        public void SaveMappingsToFile(Dictionary<int, char> mappingsDictionary)
        {
            KeyMappingsDictionary = mappingsDictionary;
            _mappingsFileService.SaveMappingFile(GetMappingsFromDictionary(KeyMappingsDictionary));
        }

        public bool AddMappingToDictionary(SevenBitNumber note, char key)
        {
            if (KeyMappingsDictionary.ContainsKey((int)note))
            {
                MessageBox.Show($"You attempted to assign {note} to {key} but {note} is already assigned to {_keyMappingsDictionary[note]}.");
                return false;
            }
            if (KeyMappingsDictionary.ContainsValue(key))
            {
                MessageBox.Show($"You attempted to assign {note} to {key} but {key} is already assigned to {_keyMappingsDictionary.FirstOrDefault(m => m.Value == key).Key}.");
                return false;
            }

            KeyMappingsDictionary.Add(note, key);
            SaveMappingsToFile(KeyMappingsDictionary);
            return true;
        }

        public bool RemoveMappingFromDictionary(int midiNote)
        {
            KeyMappingsDictionary.Remove(midiNote);
            _mappingsFileService.SaveMappingFile(GetMappingsFromDictionary(KeyMappingsDictionary));
            return !KeyMappingsDictionary.ContainsKey(midiNote);
        }

        private Dictionary<int, char> GetDictionaryFromMappings(KeyMappingsList mappingsJsonList)
        {
            Dictionary<int, char> newDictionary = new Dictionary<int, char>();

            foreach (KeyMapping mapping in mappingsJsonList.KeyMappings)
            {
                newDictionary.Add(mapping.MidiNote, char.ToUpper(mapping.KeyboardKey));
            }

            return newDictionary;
        }

        private KeyMappingsList GetMappingsFromDictionary(Dictionary<int, char> mappingsDictionary)
        {
            var newMappingsList = new KeyMappingsList { KeyMappings = new KeyMapping[mappingsDictionary.Count] };
            int i = 0;

            foreach (KeyValuePair<int, char> item in mappingsDictionary)
            {
                newMappingsList.KeyMappings[i] = new KeyMapping { MidiNote = item.Key, KeyboardKey = char.ToUpper(item.Value) };
                i++;
            }

            return newMappingsList;
        }
    }
}

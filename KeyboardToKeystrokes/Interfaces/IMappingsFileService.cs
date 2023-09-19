using KeyboardToKeystrokes.Models;

namespace KeyboardToKeystrokes.Interfaces
{
    internal interface IMappingsFileService
    {
        public KeyMappingsList LoadMappingFile();

        public void SaveMappingFile(KeyMappingsList mappingsList);
    }
}

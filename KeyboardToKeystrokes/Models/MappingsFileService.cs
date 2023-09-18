using KeyboardToKeystrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KeyboardToKeystrokes.Models
{
    internal class MappingsFileService : IMappingsFileService
    {
        public KeyMappingsList LoadMappingFile()
        {
            if (!File.Exists(Constants.MAPPINGS_FILE_NAME))
            {
                using (FileStream fs = File.Create(Constants.MAPPINGS_FILE_NAME))
                {
                    //left intentionally blank so the file creation handler gets cleaned up
                }

                return new KeyMappingsList();
            }
            else
            {
                using (FileStream fs = File.OpenRead(Constants.MAPPINGS_FILE_NAME))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string incomingJsonMappings = sr.ReadToEnd();
                        try
                        {
                            return JsonSerializer.Deserialize<KeyMappingsList>(incomingJsonMappings)!;
                        }
                        catch (JsonException)
                        {
                            MessageBox.Show("There was a problem deserializing the mappings file. Please attempt to use the backup file.");
                            return new KeyMappingsList();
                        }
                    }
                }
            }
        }

        public void SaveMappingFile(KeyMappingsList mappingsList)
        {
            string mappingsJson = JsonSerializer.Serialize(mappingsList);

            if (File.Exists(Constants.MAPPINGS_FILE_NAME))
            {
                if (File.Exists(Constants.MAPPINGS_BACKUP_FILE_NAME))
                {
                    File.Delete(Constants.MAPPINGS_BACKUP_FILE_NAME);
                }

                File.Move(Constants.MAPPINGS_FILE_NAME, Constants.MAPPINGS_BACKUP_FILE_NAME);
            }

            using (FileStream fs = File.Open(Constants.MAPPINGS_FILE_NAME, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    sw.Write(mappingsJson);
                }
            }
        }
    }
}

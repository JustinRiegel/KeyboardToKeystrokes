using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardToKeystrokes.Models
{
    internal static class Constants
    {
        public const string MAPPINGS_FILE_NAME = $"{nameof(KeyboardToKeystrokes)}Mappings.json";
        public const string MAPPINGS_BACKUP_FILE_NAME = $"{nameof(KeyboardToKeystrokes)}Mappings.json.bak";

        public const string CLICK_TO_LISTEN_MIDI_NOTE = "Listen for MIDI note";
        public const string CLICK_TO_LISTEN_KEYSTROKE = "Listen for keystroke";
        public const string LISTENING_FOR_MIDI_NOTE = "Listening for MIDI note...";
        public const string LISTENING_FOR_KEYSTROKE = "Listening for keystroke...";
        public const string MAPPING_CANCELED_PREVIOUS_MAPPING = "Mapping canceled.{Environment.NewLine}Previous mapping, if exists: {0}";
        public const string MAPPING_FAILED_TRY_AGAIN = "Mapping failed, try again.";
    }
}

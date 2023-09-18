using KeyboardToKeystrokes.Interfaces;
using KeyboardToKeystrokes.Models;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace KeyboardToKeystrokes
{
    public partial class KeyboardToKeystrokes : Form
    {
        //private const string MAPPINGS_FILE_NAME = $"{nameof(KeyboardToKeystrokes)}Mappings.json";
        //private const string MAPPINGS_BACKUP_FILE_NAME = $"{nameof(KeyboardToKeystrokes)}Mappings.json.bak";

        private IInputDevice? _inputDevice;
        private List<string> _inputLogMessages = new List<string>();
        //private KeyMappingsList _keyMappingsJsonList = new KeyMappingsList();
        private Dictionary<int, char> _keyMappingsDictionary = new Dictionary<int, char>();
        private IMappingsManager _mappingsManager = new MappingsManager();

        public KeyboardToKeystrokes()
        {
            InitializeComponent();

            if (InputDevice.GetAll().Count > 0)
            {
                //Piaggero-1
                _inputDevice = InputDevice.GetByName("Piaggero-1");
                _inputDevice.EventReceived += OnMidiEventReceived;
            }

            _keyMappingsDictionary = _mappingsManager.LoadMappingsFromFile();

            this.Disposed += DisposeInputDevice;
            this.FormClosing += KeyboardToKeystrokes_FormClosing;
        }

        #region Listening

        private void startListeningButton_Click(object sender, EventArgs e)
        {
            _inputDevice?.StartEventsListening();
            startListeningButton.Enabled = false;
            stopListeningButton.Enabled = true;
        }

        private void stopListeningButton_Click(object sender, EventArgs e)
        {
            _inputDevice?.StopEventsListening();
            startListeningButton.Enabled = true;
            stopListeningButton.Enabled = false;
        }

        private void OnMidiEventReceived(object? sender, MidiEventReceivedEventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var midiDevice = (MidiDevice)sender;

            if (e.Event.EventType == MidiEventType.NoteOn)
            {
                NoteOnEvent noteEvent = ((NoteOnEvent)e.Event);
                int noteNumberInt = (int)noteEvent.NoteNumber;

                if (noteEvent.Velocity > 0)
                {
                    if (_keyMappingsDictionary.ContainsKey(noteNumberInt))
                    {
                        //Console.WriteLine($"Event received: {midiDevice.Name}, {e.Event}");
                        //Console.WriteLine($"Note number: {((NoteOnEvent)e.Event).NoteNumber}");
                        AddInputLogMessages($"Note number: {noteEvent.NoteNumber}, key: {_keyMappingsDictionary[noteNumberInt]}");

                        inputLogTextBox.Invoke(() => inputLogTextBox.Lines = _inputLogMessages.ToArray());
                    }
                }
            }

            //if (e.Event is NoteOnEvent && ((NoteOnEvent)e.Event).NoteNumber == SevenBitNumber.Parse("60"))
            //{
            //    //Console.WriteLine("middle c");
            //    //inputLogTextBox.Text += "mid-C ";
            //    //SendKeys.SendWait("C");
            //}
        }

        private void AddInputLogMessages(string message)
        {
            while (_inputLogMessages.Count >= 100)
            {
                _inputLogMessages.RemoveAt(99);
            }

            _inputLogMessages.Insert(0, message);
        }

        #endregion

        #region Mapping

        private void openMappingManagerButton_Click(object sender, EventArgs e)
        {
            if (_inputDevice == null)
            {
                MessageBox.Show("No input device is currently available, so mappings cannot be created.");
                return;
            }

            _inputDevice.EventReceived -= OnMidiEventReceived;

            MappingManagerForm listenForm = new MappingManagerForm(_inputDevice, _mappingsManager, _keyMappingsDictionary);
            if (listenForm.ShowDialog() == DialogResult.OK)
            {
                _keyMappingsDictionary = listenForm.KeyMappingsDictionary;
            }

            _inputDevice.EventReceived += OnMidiEventReceived;
        }


        #endregion

        #region Closing

        private void KeyboardToKeystrokes_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _inputDevice?.Dispose();
        }

        private void DisposeInputDevice(object? sender, EventArgs e)
        {
            _inputDevice?.Dispose();
        }

        #endregion
    }
}
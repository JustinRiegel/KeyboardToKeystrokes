using KeyboardToKeystrokes.Interfaces;
using KeyboardToKeystrokes.Models;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

namespace KeyboardToKeystrokes
{
    public partial class KeyboardToKeystrokes : Form
    {
        private IInputDevice? _inputDevice;
        private List<string> _inputLogMessages = new List<string>();
        private Dictionary<int, char> _keyMappingsDictionary = new Dictionary<int, char>();
        private IMappingsManager _mappingsManager = new MappingsManager();
        private bool _inputDeviceIsSelected = false;
        private bool _chordTimerIsRunning = false;

        private string _chordCollection = string.Empty;

        public KeyboardToKeystrokes()
        {
            InitializeComponent();

            _keyMappingsDictionary = _mappingsManager.LoadMappingsFromFile();

            this.HandleCreated += KeyboardToKeystrokes_HandleCreated;
            this.Disposed += DisposeInputDevice;
            this.FormClosing += KeyboardToKeystrokes_FormClosing;
        }

        private void KeyboardToKeystrokes_HandleCreated(object? sender, EventArgs e)
        {
            PopulateInputDeviceCombobox();
        }

        #region Device selection

        private void PopulateInputDeviceCombobox()
        {
            inputDeviceCombobox.Items.Clear();

            if (InputDevice.GetAll().Count > 0)
            {
                var inputDevices = InputDevice.GetAll().Select(i => i.Name).ToArray();

                inputDeviceCombobox.Items.AddRange(inputDevices);
                inputDeviceCombobox.SelectedIndex = 0;
                selectInputDeviceButton.Enabled = true;
            }
            else
            {
                selectInputDeviceButton.Enabled = false;
            }
        }

        private void selectInputDeviceButton_Click(object sender, EventArgs e)
        {
            //if input device has not been selected yet, then it is currently being selected
            if (!_inputDeviceIsSelected)
            {
                try
                {
                    string deviceName = inputDeviceCombobox.SelectedItem.ToString()!;
                    _inputDevice = InputDevice.GetByName(deviceName);
                    _inputDevice.EventReceived += OnMidiEventReceived;
                }
                catch(ArgumentException)
                {
                    MessageBox.Show("The device you are attempting to use is no longer available. Please select a different one.");
                    PopulateInputDeviceCombobox();
                    return;
                }
            }
            else
            {
                if (_inputDevice != null)
                {
                    _inputDevice.EventReceived -= OnMidiEventReceived;
                }
            }

            _inputDeviceIsSelected = !_inputDeviceIsSelected;

            ToggleEnableOfDeviceSelectionControls(_inputDeviceIsSelected);
        }

        private void ToggleEnableOfDeviceSelectionControls(bool deviceSelected)
        {
            if (deviceSelected)
            {
                inputDeviceCombobox.Enabled = false;
                selectInputDeviceButton.Text = "Select new device";
                listeningGroupBox.Enabled = true;
                startListeningButton.Enabled = true;
                stopListeningButton.Enabled = false;
            }
            else
            {
                inputDeviceCombobox.Enabled = true;
                selectInputDeviceButton.Text = "Use this device";
                listeningGroupBox.Enabled = false;
                startListeningButton.Enabled = false;
                stopListeningButton.Enabled = true;
            }
        }

        private void refreshInputDeviceListButton_Click(object sender, EventArgs e)
        {
            PopulateInputDeviceCombobox();
        }

        #endregion

        #region Listening

        private void startListeningButton_Click(object sender, EventArgs e)
        {
            if (_inputDevice != null)
            {
                _inputDevice.StartEventsListening();
                startListeningButton.Enabled = false;
                stopListeningButton.Enabled = true;
            }
        }

        private void stopListeningButton_Click(object sender, EventArgs e)
        {
            if (_inputDevice != null)
            {
                _inputDevice.StopEventsListening();
                startListeningButton.Enabled = true;
                stopListeningButton.Enabled = false;
            }
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
                        AddInputLogMessages($"Note number: {noteEvent.NoteNumber}, key: {_keyMappingsDictionary[noteNumberInt]}, Time: {DateTime.Now.Millisecond}");
                        
                        if (!_chordTimerIsRunning)
                        {
                            _chordTimerIsRunning = true;
                            _chordCollection += Char.ToLower(_keyMappingsDictionary[noteNumberInt]);
                            var t = Task.Run(async delegate
                            {
                                await Task.Delay(TimeSpan.FromMilliseconds(20));

                                _chordTimerIsRunning = false;

                                foreach(var key in _chordCollection)
                                {
                                    SendKeys.SendWait(key.ToString());
                                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                                }
                                
                                _chordCollection = string.Empty;
                            });
                        }
                        else
                        {
                            _chordCollection += Char.ToLower(_keyMappingsDictionary[noteNumberInt]);
                        }
                    }
                }
            }
        }

        private void AddInputLogMessages(string message)
        {
            while (_inputLogMessages.Count >= 100)
            {
                _inputLogMessages.RemoveAt(99);
            }

            _inputLogMessages.Insert(0, message);

            inputLogTextBox.Invoke(() => inputLogTextBox.Lines = _inputLogMessages.ToArray());
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
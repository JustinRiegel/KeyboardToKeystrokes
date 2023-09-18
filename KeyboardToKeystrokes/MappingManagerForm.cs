using KeyboardToKeystrokes.Interfaces;
using KeyboardToKeystrokes.Models;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardToKeystrokes
{
    public partial class MappingManagerForm : Form
    {
        public Dictionary<int, char> KeyMappingsDictionary = new Dictionary<int, char>();

        private IInputDevice _inputDevice;
        private IMappingsManager _mappingsManager;

        private SevenBitNumber _midiNoteNumber;
        private char _keyboardKey;
        private bool _deviceWasListeningOnEntry;

        private bool _midiNoteRecorded = false;
        private bool _keyboardKeyRecorded = false;

        public MappingManagerForm(IInputDevice inputDevice, IMappingsManager mappingsManager, Dictionary<int, char> incomingDictonary)
        {
            InitializeComponent();
            _inputDevice = inputDevice;
            _mappingsManager = mappingsManager;

            if (incomingDictonary.Count > 0)
            {
                KeyMappingsDictionary = incomingDictonary;
            }
            else
            {
                KeyMappingsDictionary = _mappingsManager.LoadMappingsFromFile();
            }

            //keyboardMappingAssignmentButton.KeyPress -= keyboardMappingAssignmentButton_KeyPress;

            _deviceWasListeningOnEntry = _inputDevice.IsListeningForEvents;

            this.HandleCreated += MappingManagerForm_HandleCreated;
            this.FormClosing += ListeningPopupForm_FormClosing;

            this.DialogResult = DialogResult.Cancel;
        }

        private void PopulateMappingsListBoxWithExistingMappings()
        {
            if (KeyMappingsDictionary.Count > 0)
            {
                foreach (KeyValuePair<int, char> mapping in KeyMappingsDictionary)
                {
                    AddMappingToListBox(mapping.Key, mapping.Value);
                }
            }
        }

        private void ResetMappingButtonStates()
        {
            SetMidiNoteMappingAssignmentButtonText(Constants.CLICK_TO_LISTEN_MIDI_NOTE);
            SetKeyboardKeyMappingAssignmentButtonText(Constants.CLICK_TO_LISTEN_KEYSTROKE);

            _midiNoteRecorded = false;
            _keyboardKeyRecorded = false;
        }

        #region Midi mapping methods

        private void midiNoteMappingAssignmentButton_Click(object? sender, EventArgs e)
        {
            SetMidiNoteMappingAssignmentButtonText(Constants.LISTENING_FOR_MIDI_NOTE);

            SetMidiEventsOnOff(true);

            if (!_inputDevice.IsListeningForEvents)
            {
                _inputDevice.StartEventsListening();
            }
        }

        private void midiNoteMappingAssignmentButton_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SetMidiEventsOnOff(false);

                SetMidiNoteMappingAssignmentButtonText(Constants.CLICK_TO_LISTEN_MIDI_NOTE);
            }
            else if (e.KeyCode == Keys.Enter && _midiNoteRecorded && _keyboardKeyRecorded)
            {
                SetMidiEventsOnOff(false);

                SetAddMappingButtonEnabled(true);
                addMappingButton_Click(sender, e);
            }
        }

        private void midiNoteMappingAssignmentButton_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!_keyboardKeyRecorded)
            {
                keyboardMappingAssignmentButton_KeyPress(sender, e);
            }
        }

        private void MidiMappingAssignmentEventReceived(object? sender, MidiEventReceivedEventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var midiDevice = (MidiDevice)sender;

            if (e.Event.EventType == MidiEventType.NoteOn)
            {
                _inputDevice.EventReceived -= MidiMappingAssignmentEventReceived;
                _inputDevice.StopEventsListening();

                _midiNoteNumber = ((NoteOnEvent)e.Event).NoteNumber;
                SetMidiNoteMappingAssignmentButtonText($"MIDI note: {_midiNoteNumber}");

                _midiNoteRecorded = true;
                if (!_keyboardKeyRecorded)
                {
                    keyboardMappingAssignmentButton_Click(sender, e);
                }
            }
            else if (e.Event.EventType != MidiEventType.TimingClock && e.Event.EventType != MidiEventType.ActiveSensing)
            {
                SetMidiNoteMappingAssignmentButtonText(Constants.MAPPING_FAILED_TRY_AGAIN);
                _inputDevice.EventReceived -= MidiMappingAssignmentEventReceived;
                _inputDevice.StopEventsListening();
            }

            //TODO refactor these buttons into 1 button that just kicks off the midi listening event and has a keydown/keypress listener?
        }

        private void SetMidiEventsOnOff(bool enable)
        {
            if (enable)
            {
                _inputDevice.EventReceived += MidiMappingAssignmentEventReceived;
                midiNoteMappingAssignmentButton.KeyPress += midiNoteMappingAssignmentButton_KeyPress;
                midiNoteMappingAssignmentButton.KeyDown += midiNoteMappingAssignmentButton_KeyDown;
            }
            else
            {
                _inputDevice.EventReceived -= MidiMappingAssignmentEventReceived;
                midiNoteMappingAssignmentButton.KeyPress -= midiNoteMappingAssignmentButton_KeyPress;
                midiNoteMappingAssignmentButton.KeyDown -= midiNoteMappingAssignmentButton_KeyDown;
            }
        }

        #endregion

        #region Keyboard mapping methods

        private void keyboardMappingAssignmentButton_Click(object sender, EventArgs e)
        {
            SetKeyboardEventsOnOff(true);

            SetKeyboardKeyMappingAssignmentButtonText(Constants.LISTENING_FOR_KEYSTROKE);
        }

        private void keyboardMappingAssignmentButton_KeyPress(object? sender, KeyPressEventArgs e)
        {
            SetKeyboardEventsOnOff(false);

            _keyboardKey = char.ToUpper(e.KeyChar);
            SetKeyboardKeyMappingAssignmentButtonText($"Keyboard key: {_keyboardKey}");

            _keyboardKeyRecorded = true;
            if (!_midiNoteRecorded)
            {
                midiNoteMappingAssignmentButton_Click(sender, e);
            }
            else if (_midiNoteRecorded && _keyboardKeyRecorded)
            {
                SetAddMappingButtonEnabled(true);
            }

        }

        private void keyboardMappingAssignmentButton_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SetKeyboardEventsOnOff(false);

                SetKeyboardKeyMappingAssignmentButtonText(Constants.CLICK_TO_LISTEN_KEYSTROKE);
            }
            else if (e.KeyCode == Keys.Enter && _midiNoteRecorded && _keyboardKeyRecorded)
            {
                SetKeyboardEventsOnOff(false);

                SetAddMappingButtonEnabled(true);
                addMappingButton_Click(sender, e);
            }
        }

        private void SetKeyboardEventsOnOff(bool enable)
        {
            if (enable)
            {
                keyboardMappingAssignmentButton.KeyPress += keyboardMappingAssignmentButton_KeyPress;
                keyboardMappingAssignmentButton.KeyDown += keyboardMappingAssignmentButton_KeyDown;
            }
            else
            {
                keyboardMappingAssignmentButton.KeyPress -= keyboardMappingAssignmentButton_KeyPress;
                keyboardMappingAssignmentButton.KeyDown -= keyboardMappingAssignmentButton_KeyDown;
            }
        }

        #endregion

        #region Set form object properties

        private void SetMidiNoteMappingAssignmentButtonText(string text)
        {
            midiNoteMappingAssignmentButton.Invoke(() => midiNoteMappingAssignmentButton.Text = text);
        }

        private void SetKeyboardKeyMappingAssignmentButtonText(string text)
        {
            keyboardMappingAssignmentButton.Invoke(() => keyboardMappingAssignmentButton.Text = text);
        }

        private void SetAddMappingButtonEnabled(bool enabled)
        {
            addMappingButton.Invoke(() => addMappingButton.Enabled = enabled);
        }

        private void AddMappingToListBox(int midiNote, char key)
        {
            string mappingString = $"Note: {midiNote}, Key: {key}";
            mappingsListBox.Invoke(() => mappingsListBox.Items.Add(mappingString));
        }

        private void RemoveMappingFromListbox(int index)
        {
            mappingsListBox.Invoke(() => mappingsListBox.Items.RemoveAt(index));
        }

        #endregion

        #region Workflow control buttons

        private void finishedMappingButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelMappingButton_Click(object sender, EventArgs e)
        {
            keyboardMappingAssignmentButton.KeyPress -= keyboardMappingAssignmentButton_KeyPress;
            _inputDevice.EventReceived -= MidiMappingAssignmentEventReceived;
            ResetMappingButtonStates();
        }

        private void addMappingButton_Click(object? sender, EventArgs e)
        {
            SetAddMappingButtonEnabled(false);

            if (_mappingsManager.AddMappingToDictionary(_midiNoteNumber, _keyboardKey))
            {
                AddMappingToListBox((int)_midiNoteNumber, _keyboardKey);
            }

            ResetMappingButtonStates();
        }

        private void deleteMappingButton_Click(object sender, EventArgs e)
        {
            if (mappingsListBox.SelectedItem != null)
            {
                string noteString = mappingsListBox.SelectedItem.ToString()!.Split(',')[0];//split on the comma between the note and the key

                if (int.TryParse(noteString.Split(' ')[1], out int noteNumber))//split on the space between note: and the number, int parse the second string
                {
                    if (_mappingsManager.RemoveMappingFromDictionary(noteNumber))
                    {
                        RemoveMappingFromListbox(mappingsListBox.SelectedIndex);
                    }
                }
            }
        }

        #endregion

        #region Form events

        private void ListeningPopupForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            //if listening was on for the device when this window was opened, and it is not currently listening,
            //then turn listening on again
            if (_deviceWasListeningOnEntry && !_inputDevice.IsListeningForEvents)
            {
                _inputDevice.StartEventsListening();
            }
            //or if listening was off for the device when this window was opened, and it is currently listening,
            //then turn listening off again
            else if (!_deviceWasListeningOnEntry && _inputDevice.IsListeningForEvents)
            {
                _inputDevice.StopEventsListening();
            }
            //otherwise, the listening state when this window was opened matches whether or not it is currently listening, so no change needed

            _inputDevice.EventReceived -= MidiMappingAssignmentEventReceived;
        }

        private void MappingManagerForm_HandleCreated(object? sender, EventArgs e)
        {
            PopulateMappingsListBoxWithExistingMappings();
        }

        #endregion
    }
}

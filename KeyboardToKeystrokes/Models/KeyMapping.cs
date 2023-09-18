using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardToKeystrokes.Models
{
    public class KeyMapping
    {
        public int MidiNote { get; set; }

        public char KeyboardKey { get; set; }
    }
}

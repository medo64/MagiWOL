using System;
using System.Collections.Generic;
using System.Text;

namespace MagiWol {
    internal class ImportProgressState {

        public ImportProgressState(string text, int? secondsRemaining) {
            this.Text = text;
            this.SecondsRemaining = secondsRemaining;
        }

        public ImportProgressState(string text)
            : this(text, null) {
        }

        public ImportProgressState(int secondsRemaining)
            : this(null, secondsRemaining) {
        }


        public string Text { get; private set; }
        public int? SecondsRemaining { get; private set; }

    }
}

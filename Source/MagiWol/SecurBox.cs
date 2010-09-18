using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MagiWol {

    internal class SecurBox : System.Windows.Forms.TextBox {

        public SecurBox() {
            this.MaxLength = 2 * 6 + 5;
        }


        protected override void OnEnter(EventArgs e) {
            this.SelectAll();
            base.OnEnter(e);
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.KeyData == (Keys.Control | Keys.A)) {
                base.SelectAll();
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e) {
            if (System.Convert.ToInt32(e.KeyChar) >= 32) {
                switch (e.KeyChar) {
                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                        break;

                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                        e.KeyChar = char.ToUpper(e.KeyChar);
                        break;

                    default:
                        e.Handled = true;
                        return;
                }
            }
            base.OnKeyPress(e);
        }

        private bool isIn = false;
        protected override void OnTextChanged(EventArgs e) {
            if (isIn) { return; }
            isIn = true;

            int iStart = this.SelectionStart;

            this.Text = GetProperSecureOn(this.Text);

            this.SelectionStart = this.Text.Length;
            this.SelectionLength = 0;

            isIn = false;

            base.OnTextChanged(e);
        }


        private string GetProperSecureOn(string text) {
            string addressText = System.Text.RegularExpressions.Regex.Replace(text.ToUpper(), "[^0-9A-F]", "-") + "-";
            Medo.Text.StringAdder newText = new Medo.Text.StringAdder("-");

            string lastPart = string.Empty;
            for (int i = 0; i < addressText.Length; i++) {
                switch (addressText[i]) {

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                        lastPart += addressText.Substring(i, 1);
                        break;

                    case '-':
                        while (lastPart.Length > 0) {
                            string tmpPart = string.Empty;
                            if (lastPart.Length > 2) {
                                tmpPart = lastPart.Substring(0, 2);
                                lastPart = lastPart.Substring(2, lastPart.Length - 2);
                            } else {
                                tmpPart = lastPart;
                                if ((tmpPart.Length < 2) && (i < (addressText.Length - 1))) { tmpPart = tmpPart.PadLeft(2, '0'); }
                                lastPart = string.Empty;
                            }
                            newText.Append(tmpPart);
                        }
                        break;

                }
            }//for

            if (newText.ToString().Length > 2 * 6 + 5) {
                return newText.ToString().Substring(0, 2 * 6 + 5);
            } else {
                return newText.ToString();
            }

        }


    }

}

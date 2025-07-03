using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;


namespace Calculator
{
    public partial class MainForm : Form
    {
        #region Consts
        private const string LOG_FILE = "ic-history.log";
        #endregion

        #region Fields
        private dynamic       _Calculate;
        private RadioButton[] _SvSwitch;
        private RadioButton[] _WidthSwitch;
        private int           _PrevBase  = 10;
        private ulong         _Mask      = 0xFFFFFFFF;
        private ulong         _Operand   = 0;
        private string        _Operation = "";
        private List<string>  _History;
        private int           _HistoryIndex;
        #endregion

        #region Ctors
        public MainForm()
        {
            InitializeComponent();

            TopLine.ContextMenuStrip = new ContextMenuStrip();
            TopLine.Cursor           = Cursors.Default;
            TopLine.GotFocus        += TopLineGotFocus;

            _SvSwitch    = new RadioButton[4]{ SvBin, SvOct, SvDec, SvHex };
            _WidthSwitch = new RadioButton[4]{ WidthByte, WidthWord, WidthDword, WidthQword };

            if (!File.Exists("calc.py"))
            {
                File.WriteAllBytes("calc.py", Properties.Resources.calc);
            }
            
            if (File.Exists("IronPython.dll") && File.Exists("IronPython.Modules.dll") && 
                File.Exists("Microsoft.Dynamic.dll") && File.Exists("Microsoft.Scripting.dll"))
            {
                ScriptEngine Script = Python.CreateEngine();
                ScriptScope  Scope  = Script.CreateScope();
                Script.ExecuteFile("calc.py", Scope);
                _Calculate          = Scope.GetVariable("calculate");
            }
            else
            {
                MessageBox.Show
                (
                    "There are no libraries:\r\n" +
                    "IronPython.dll\r\n" +
                    "IronPython.Modules.dll\r\n" +
                    "Microsoft.Dynamic.dll\r\n" +
                    "Microsoft.Scripting.dll", "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );

                Application.Exit();
            }

            if (File.Exists(LOG_FILE))
            {
                string[] lines = File.ReadAllLines(LOG_FILE);
                _History       = new List<string>(lines);
                _HistoryIndex  = _History.Count;
            }
            else
            {
                _History      = new List<string>();
                _HistoryIndex = -1;
            }
        }
        #endregion

        #region Override
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            e.Graphics.DrawRectangle(new Pen(Color.Gray), 11, 11, TopLine.Width+1, 33);
            e.Graphics.FillRectangle(Brushes.White, 12, 25, TopLine.Width, 3);
        }
        #endregion

        #region Utils
        private void DoCalculate()
        {
            string expression = BottomLine.Text;

            expression = expression.Replace("×", "*");
            expression = expression.Replace("^", "**");
            expression = expression.Replace("²", "**2");
            expression = expression.Replace("π", "pi");
            expression = expression.Replace("℮", "e");
            expression = expression.Replace("_", "h");
            expression = expression.Replace("j", "J");

            string result = _Calculate(expression);

            if (result == "Error")
            {
                TopLine.ForeColor    = Color.LightGray;
                BottomLine.ForeColor = Color.Red;
            }
            else
            {
                _History.Add(BottomLine.Text);
                File.AppendAllText(LOG_FILE, BottomLine.Text+"\r\n");
                _HistoryIndex = _History.Count;

                TopLine.ForeColor         = Color.DarkGray;
                TopLine.Text              = BottomLine.Text + " =";
                BottomLine.Text           = result;
                BottomLine.SelectionStart = BottomLine.TextLength;
                BottomLine.Select();
            }
        }

        private ulong FromBase(string image, int basis)
        {
            ulong result = 0;
            ulong pterm  = 1;
            for (int i = image.Length-1; i >= 0; i--)
            {
                result += Convert.ToUInt64(image.Substring(i, 1), 16) * pterm;
                pterm  *= (ulong)basis;
            }
            return result;
        }

        private string ToBase(ulong value, int basis)
        {
            string result = "";
            if (value == 0)
            {
                return "0";
            }
            while (value > 0)
            {
                result = Convert.ToString((int)(value % (ulong)basis), 16) + result;
                value /= (ulong)basis;
            }
            return result.ToUpper();
        }
        
        private string SubScript(int x)
        {
            string result = "";
            while (x > 0)
            {
                result = "₀₁₂₃₄₅₆₇₈₉"[x % 10].ToString() + result;
                x /= 10;
            }
            return result;
        }
        
        private bool CheckValue(string image)
        {
            string alphabet = "0123456789ABCDEF".Substring(0, _PrevBase);

            for (int i = 0; i < image.Length; i++)
            {
                if (alphabet.IndexOf(image[i]) == -1)
                {
                    BottomLine.ForeColor       = Color.Red;
                    BottomLine.SelectionStart  = i;
                    BottomLine.SelectionLength = 1;
                    BottomLine.Select();
					
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Handlers
        private void TopLineGotFocus(object sender, EventArgs e)
        {
            Focus();
        }

        private void CopyClick(object sender, EventArgs e)
        {
            BottomLine.Copy();
        }
        
        private void BottomLineTextChanged(object sender, EventArgs e)
        {
            if (BottomLine.ForeColor == Color.Red)
            {
                BottomLine.ForeColor = Color.Black;
            }
        }

        private void BottomLineKeyDown(object sender, KeyEventArgs e)
        {
            if (_HistoryIndex != -1)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (_HistoryIndex < 1)
                    {
                        _HistoryIndex = _History.Count;
                    }

                    BottomLine.Text = _History[--_HistoryIndex];
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (++_HistoryIndex >= _History.Count)
                    {
                        _HistoryIndex = 0;
                    }

                    BottomLine.Text = _History[_HistoryIndex];
                }
            }
        }

        private void BottomLineKeyPress(object sender, KeyPressEventArgs e)
        {
            if (FunctionsBox.SelectedIndex != 3 && e.KeyChar == '\r')
            {
                DoCalculate();
            }
        }

        private void ClearClick(object sender, EventArgs e)
        {
            BottomLine.Text = "";
            TopLine.Text    = "";
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            string text   = BottomLine.Text;
            int    length = text.Length;

            if (length > 0)
            {
                int sel = BottomLine.SelectionStart;

                if (sel > 0)
                {
                    BottomLine.Text = text.Remove(sel - 1, 1);
                    BottomLine.SelectionStart = sel - 1;
                    BottomLine.Select();
                }
            }
        }

        private void CalculateClick(object sender, EventArgs e)
        {
            DoCalculate();
        }

        private void ButtonsClick(object sender, EventArgs e)
        {
            int    sel = BottomLine.SelectionStart;
            string exp = ((Button)sender).Tag.ToString();
            
            BottomLine.Paste(exp);
            BottomLine.SelectionStart = sel + exp.Length;
            BottomLine.Select();
        }

        private void SvBaseChanged(object sender, EventArgs e)
        {
            int NewBase = -1;
            if (sender is CheckBox)
            {
                ArbitraryBaseValue.Enabled = ArbitraryBaseSelect.Checked;
                Array.ForEach(_SvSwitch, rb => rb.Enabled = !ArbitraryBaseSelect.Checked);
                NewBase = ArbitraryBaseSelect.Checked ? (int)ArbitraryBaseValue.Value : Convert.ToInt32(Array.Find(_SvSwitch, rb => rb.Checked).Tag);
            }
            else if (sender is RadioButton)
            {
                NewBase = Convert.ToInt32(((RadioButton)sender).Tag);
            }
            else if (sender is NumericUpDown)
            {
                NewBase = (int)ArbitraryBaseValue.Value;
            }

            if (NewBase != _PrevBase)
            {
                string value = BottomLine.Text;

                if (_Operation == "")
                {
                    if (value.Length == 0)
                    {
                        value = "0";
                    }
                    else
                    {
                        if (!CheckValue(value))
                        {
                            if (ArbitraryBaseSelect.Checked)
                            {
                                ArbitraryBaseValue.Value = _PrevBase;
                            }
                            else
                            {
                                Array.Find(_SvSwitch, rb => Convert.ToInt32(rb.Tag) == _PrevBase).Checked = true;
                            }

                            return;
                        }
                    }

                    string result = ToBase(FromBase(value, _PrevBase), NewBase);

                    TopLine.ForeColor         = Color.DarkGray;
                    TopLine.Text              = value + SubScript(_PrevBase) + " = " + result + SubScript(NewBase);
                    BottomLine.Text           = result;
                    BottomLine.SelectionStart = BottomLine.TextLength;
                    BottomLine.Select();
                }

                _PrevBase = NewBase;
            }
        }

        private void SvWidthChanged(object sender, EventArgs e)
        {
            int width = Convert.ToInt32(((RadioButton)sender).Tag);
            _Mask = (0xFFFFFFFFFFFFFFFF >> (64 - width));
        }

        private void SvInvClick(object sender, EventArgs e)
        {
            string value = BottomLine.Text;

            if (value.Length == 0)
            {
                value = "0";
            }

            if (CheckValue(value))
            {
                string op = Convert.ToString(((Button)sender).Tag);

                ulong  invert = (~FromBase(value, _PrevBase)) + (ulong)(op == "-" ? 1 : 0);
                string result = ToBase(invert & _Mask, _PrevBase);

                TopLine.ForeColor         = Color.DarkGray;
                TopLine.Text              = op + value + SubScript(_PrevBase) + " = ";
                BottomLine.Text           = result;
                BottomLine.SelectionStart = BottomLine.TextLength;
                BottomLine.Select();
            }

            _Operation = "";
        }
        
        private void SvBitwiseOperationClick(object sender, EventArgs e)
        {
            if (_Operation == "")
            {
                string value = BottomLine.Text;

                if (value.Length == 0)
                {
                    value = "0";
                }

                if (CheckValue(value))
                {
                    _Operation = Convert.ToString(((Button)sender).Tag);
                    _Operand   = FromBase(value, _PrevBase);

                    TopLine.ForeColor = Color.DarkGray;
                    TopLine.Text      = value + SubScript(_PrevBase) + " " + _Operation;

                    BottomLine.Text = "";
                }
            }
            else
            {
                _Operation   = Convert.ToString(((Button)sender).Tag);
                TopLine.Text = TopLine.Text.Remove(TopLine.Text.Length - 1) + _Operation;
            }
        }

        private void SvCalculateClick(object sender, EventArgs e)
        {
            if (_Operation == "")
            {
                return;
            }

            string value = BottomLine.Text;

            if (value.Length == 0)
            {
                value = "0";
            }

            if (CheckValue(value))
            {
                ulong result = FromBase(value, _PrevBase);

                TopLine.ForeColor = Color.DarkGray;
                TopLine.Text     += " " + value + SubScript(_PrevBase) + " =";

                switch (_Operation)
                {
                    case "|": result |= _Operand; break;
                    case "^": result ^= _Operand; break;
                    case "&": result &= _Operand; break;
                }

                BottomLine.Text           = ToBase(result & _Mask, _PrevBase);
                BottomLine.SelectionStart = BottomLine.TextLength;
                BottomLine.Select();

                _Operation = "";
            }
        }
        #endregion
    }
}
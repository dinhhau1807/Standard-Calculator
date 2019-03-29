using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1760056
{
    public partial class CalcForm : Form
    {
        // ============================= Private fields
        double value = 0;
        string operation = "";
        bool operation_pressed = false;
        bool btnEqual_pressed = false;
        bool number_pressed = false;
        double memory = 0;


        // ============================= Load
        public CalcForm()
        {
            this.KeyPreview = true;
            InitializeComponent();
        }

        private void CalcForm_Load(object sender, EventArgs e)
        {
            result.Text = "0";
            memoryLabel.Text = "";
        }


        // ============================= Number / operator
        private void btn_Click(object sender, EventArgs e)
        {
            if (result.Text == "0" || operation_pressed)
            {
                result.Clear();
            }

            operation_pressed = false;
            number_pressed = true;

            var btn = (Button)sender;
            if (btnEqual_pressed)
            {
                result.Text = btn.Text;
                btnEqual_pressed = false;
            }
            else
            {
                result.Text += btn.Text;
            }
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (result.Text.IndexOf(',') == -1)
            {
                result.Text += ",";
            }
            else if (btnEqual_pressed)
            {
                result.Text = "0,";
                btnEqual_pressed = false;
            }
        }

        private void operator_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            if (value != 0 && !operation_pressed)
            {
                equation.Text += result.Text + " " + btn.Text + " ";

                switch (operation)
                {
                    case "+":
                        value += double.Parse(result.Text);
                        break;
                    case "−":
                        value -= double.Parse(result.Text);
                        break;
                    case "×":
                        value *= double.Parse(result.Text);
                        break;
                    case "÷":
                        value /= double.Parse(result.Text);
                        break;
                }

                result.Text = value.ToString();

                number_pressed = false;
                operation_pressed = true;
                operation = btn.Text;
            }
            else if (value != 0 && operation_pressed)
            {
                equation.Text = equation.Text.Substring(0, equation.Text.Length - 2) + btn.Text + " ";
                operation = btn.Text;
            }
            else
            {
                operation = btn.Text;
                value = double.Parse(result.Text);
                number_pressed = false;
                operation_pressed = true;
                equation.Text = value + " " + operation + " ";
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            equation.ResetText();

            if (number_pressed)
            {
                switch (operation)
                {
                    case "+":
                        result.Text = (value + double.Parse(result.Text)).ToString();
                        break;
                    case "−":
                        result.Text = (value - double.Parse(result.Text)).ToString();
                        break;
                    case "×":
                        result.Text = (value * double.Parse(result.Text)).ToString();
                        break;
                    case "÷":
                        result.Text = (value / double.Parse(result.Text)).ToString();
                        break;
                    default:
                        result.Text = (double.Parse(result.Text)).ToString();
                        break;
                }
            }
            else
            {
                result.Text = (double.Parse(result.Text)).ToString();
            }

            value = 0;
            operation = "";

            operation_pressed = false;
            btnEqual_pressed = true;
            number_pressed = false;
        }


        // ============================= Special Operator
        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            if (equation.Text == "")
            {
                if (number_pressed && result.Text != "0")
                {
                    result.Text = (-double.Parse(result.Text)).ToString();
                }
            }
            else
            {
                result.Text = (-double.Parse(result.Text)).ToString();
            }
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            if (value == 0)
            {
                result.Text = "0";
                equation.Text = "0";
            }
            else
            {
                result.Text = (value * (double.Parse(result.Text)) / 100).ToString();
            }
        }

        private void btnSquareRootOf2_Click(object sender, EventArgs e)
        {
            if (double.Parse(result.Text) >= 0)
            {
                result.Text = Math.Sqrt(double.Parse(result.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Khong the can bac 2 cho so am!");
            }
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            result.Text = Math.Pow(double.Parse(result.Text), 2).ToString();
        }

        private void btn1DivideX_Click(object sender, EventArgs e)
        {
            if (result.Text == "0")
            {
                MessageBox.Show("Khong the chia cho 0!");
            }
            else
            {
                result.Text = (1 / double.Parse(result.Text)).ToString();
            }
        }


        // ============================= Tools
        private void btnCE_Click(object sender, EventArgs e)
        {
            result.Text = "0";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            result.Text = "0";
            value = 0;
            equation.ResetText();
            btnEqual_pressed = false;
            operation_pressed = false;
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (result.Text.Length != 0)
            {
                result.Text = result.Text.Substring(0, result.Text.Length - 1);
            }

            if (result.Text.Length == 0 || result.Text == "-")
            {
                result.Text = "0";
            }
        }


        // ============================= Memory Process
        private void btnMC_Click(object sender, EventArgs e)
        {
            memory = 0;
            memoryLabel.Text = "";
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            result.Text = memory.ToString();
        }

        private void btnMS_Click(object sender, EventArgs e)
        {
            memory = double.Parse(result.Text);
            if (memory != 0)
            {
                memoryLabel.Text = "M";
            }
            else
            {
                memoryLabel.Text = "";
            }
        }

        private void btnMPlus_Click(object sender, EventArgs e)
        {
            memory += double.Parse(result.Text);
        }

        private void btnMMinus_Click(object sender, EventArgs e)
        {
            memory -= double.Parse(result.Text);
        }


        // ============================= KeyDown event
        private void btn_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnEqual.PerformClick();
                    break;
                case Keys.Escape:
                    btnC.PerformClick();
                    break;
                case Keys.Delete:
                    btnCE.PerformClick();
                    break;
                case Keys.Back:
                    btnBackspace.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    btn0.PerformClick();
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    btn1.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    btn2.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    btn3.PerformClick();
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    btn4.PerformClick();
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    btn5.PerformClick();
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    btn6.PerformClick();
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    btn7.PerformClick();
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    btn8.PerformClick();
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    btn9.PerformClick();
                    break;
                case Keys.Multiply:
                    btnMultiply.PerformClick();
                    break;
                case Keys.Add:
                case Keys.Oemplus:
                    btnPlus.PerformClick();
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    btnMinus.PerformClick();
                    break;
                case Keys.Decimal:
                case Keys.Oemcomma:
                case Keys.OemPeriod:
                    btnPoint.PerformClick();
                    break;
                case Keys.Divide:
                case Keys.OemQuestion:
                    btnDivide.PerformClick();
                    break;
                default:
                    break;
            }
        }


        // ============================= Fix size text
        private void result_TextChanged(object sender, EventArgs e)
        {
            if (result.Text.Length <= 12)
            {
                result.Font = new Font(result.Font.OriginalFontName, 30);
                result.Location = new Point(2, 71);
            }
            else if (result.Text.Length <= 18)
            {
                result.Font = new Font(result.Font.OriginalFontName, 20);
                result.Location = new Point(2, 80);
            }
            else
            {
                result.Font = new Font(result.Font.OriginalFontName, 15);
                result.Location = new Point(2, 85);
            }
        }
    }
}

using System;
using System.Windows.Forms;
using System.Globalization;

namespace pertemuan3
{
    public partial class Form1 : Form
    {
        // Menyiapkan variabel pengendali kalkulator
        double firstNumber = 0;
        double secondNumber = 0;
        double result = 0;
        string operation = "";

        public Form1()
        {
            InitializeComponent();
        }

        // Event handler untuk semua tombol angka (0-9)
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (txtDisplay.Text == "0")
                txtDisplay.Text = button.Text;
            else
                txtDisplay.Text += button.Text;
        }

        // Event handler untuk tombol operator (+, -, ×, ÷)
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            firstNumber = double.Parse(txtDisplay.Text, CultureInfo.InvariantCulture);
            operation = button.Text;
            txtDisplay.Clear();
        }

        // Event handler untuk tombol sama dengan (=)
        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                secondNumber = double.Parse(txtDisplay.Text, CultureInfo.InvariantCulture   );

                switch (operation)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "x":
                    case "×":
                        result = firstNumber * secondNumber;
                        break;
                    case "÷":
                    case "/":
                        if (secondNumber == 0)
                            throw new DivideByZeroException("Tidak boleh membahagi dengan kosong (zero).");
                        result = firstNumber / secondNumber;
                        break;
                }

                txtDisplay.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler untuk tombol Clear (C)
        private void btnClear_Click(object sender, EventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            result = 0;
            operation = "";
            txtDisplay.Text = "0";
        }

        // Event handler untuk tombol Perpuluhan (.)
        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (!txtDisplay.Text.Contains("."))
            {
                txtDisplay.Text += ".";
            }
        }
    }
}
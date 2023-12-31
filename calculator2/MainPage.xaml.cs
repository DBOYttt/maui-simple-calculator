
using Microsoft.Maui.Graphics;

namespace calculator2
{
    public partial class MainPage : ContentPage
    {
        public string CurrentInput { get; private set; }  = string.Empty;
        public string RuningTotal { get; private set; } = string.Empty;

        private string selectedOperator;
        string[] operators = { "+", "-", "/", "x", "=" };
        string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        bool resetOnNextInput = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private Color originalBtnColor;
        private Color originalTextColor;

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                originalBtnColor = btn.BackgroundColor;
                originalTextColor = btn.TextColor;

                btn.Background = Colors.Black;
                btn.TextColor = Colors.White;

                await btn.ScaleTo(0.9, 50);
                await btn.ScaleTo(1, 50);
            }
            var thisInput = btn.Text;

            if (numbers.Contains(thisInput))
            {
                if (resetOnNextInput)
                {
                    CurrentInput = btn.Text;
                    resetOnNextInput = false;
                }
                else
                {
                    CurrentInput += btn.Text;
                }
                result.Text = CurrentInput;
            }
            else if(operators.Contains(thisInput))
            {
                var ans = PerformCalculator();
                if (thisInput == "=")
                {
                    CurrentInput = ans.ToString();
                    result.Text = CurrentInput;
                    RuningTotal = String.Empty;
                    selectedOperator = String.Empty;

                    resetOnNextInput = true;
                }
                else
                {
                    RuningTotal = ans.ToString();
                    selectedOperator = thisInput;
                    CurrentInput = String.Empty;
                    result.Text = CurrentInput;
                }
            }


        }

        private double PerformCalculator()
        {
            double currentVal;
            double.TryParse(CurrentInput, out currentVal);

            double runningVal;
            double.TryParse(CurrentInput, out runningVal);

            double res;

            switch(selectedOperator)
            {
                case "+":
                    res = runningVal + currentVal;
                    break;
                case "-":
                    res = runningVal - currentVal;
                    break;
                case "x":
                    res = runningVal * currentVal;
                    break;
                case "/":
                    res = runningVal / currentVal;
                    break;
                default: res = currentVal;
                    break;
            }
            return res;
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                btn.BackgroundColor = originalBtnColor;
                btn.TextColor = originalTextColor;
            }
        }

    }
}

using NCalc;
using NCalcAsync;

namespace Secant_Method_Approximation_Tool
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();

            Ruleslbl.Text =
                "Input Guidelines:\n" +
                "• The function must be in terms of 'x' (e.g., exp(-x) - x).\n" +
                "• Use only supported syntax: exp(x), log(x), sin(x), etc.\n" +
                "• Do not include '=' in the equation.\n" +
                "• Use decimal points (e.g., 0.05).\n" +
                "• Enter both guesses (x(i-1) and x(i)) as numeric values.\n" +
                "• Error tolerance must be in percent (e.g., 0.05).";

            Formulaslbl.Text =
                "Secant Method Formula:\n" +
                "x(i+1) = x(i) - f(x(i)) * (x(i-1) - x(i)) / [f(x(i-1)) - f(x(i))]\n\n" +
                "Termination Criterion:\n" +
                "εa = |(x(i+1) - x(i)) / x(i+1)| * 100%";
        }

        private async Task<double> EvaluateFunctionAsync(string function, double x)
        {
            var expr = new Expression(function);
            expr.Parameters["x"] = x;
            var result = await expr.EvaluateAsync();
            return Convert.ToDouble(result);
        }


        private async void Calcbtn_Click(object sender, EventArgs e)
        {
            Iterationsdgv.Rows.Clear();

            try
            {
                string func = Functiontxtbx.Text.Trim();
                double xi_minus1 = double.Parse(XMinustxtbx.Text);
                double xi = double.Parse(Xtxtbx.Text);
                double es = double.Parse(AppxErrortxtbx.Text);

                int iteration = 1;
                double xi_plus1 = 0, ea = 100;

                if (Iterationsdgv.Columns.Count == 0)
                {
                    Iterationsdgv.Columns.Add("Iteration", "Iteration");
                    Iterationsdgv.Columns.Add("x(i-1)", "x(i-1)");
                    Iterationsdgv.Columns.Add("x(i)", "x(i)");
                    Iterationsdgv.Columns.Add("f(x(i-1))", "f(x(i-1))");
                    Iterationsdgv.Columns.Add("f(x(i))", "f(x(i))");
                    Iterationsdgv.Columns.Add("x(i+1)", "x(i+1)");
                    Iterationsdgv.Columns.Add("ea", "ea (%)");
                }

                double f_xi_minus1 = await EvaluateFunctionAsync(func, xi_minus1);
                double f_xi = await EvaluateFunctionAsync(func, xi);

                Iterationsdgv.Rows.Add("0",
                    xi_minus1.ToString("F6"),
                    xi.ToString("F6"),
                    f_xi_minus1.ToString("F6"),
                    f_xi.ToString("F6"),
                    "", "");

                while (ea > es)
                {
                    f_xi_minus1 = await EvaluateFunctionAsync(func, xi_minus1);
                    f_xi = await EvaluateFunctionAsync(func, xi);

                    xi_plus1 = xi - f_xi * (xi_minus1 - xi) / (f_xi_minus1 - f_xi);
                    ea = Math.Abs((xi_plus1 - xi) / xi_plus1) * 100;

                    Iterationsdgv.Rows.Add(iteration.ToString(),
                        xi_minus1.ToString("F6"),
                        xi.ToString("F6"),
                        f_xi_minus1.ToString("F6"),
                        f_xi.ToString("F6"),
                        xi_plus1.ToString("F6"),
                        ea.ToString("F2"));

                    xi_minus1 = xi;
                    xi = xi_plus1;
                    iteration++;
                }

                MessageBox.Show($"Estimated Root: {xi.ToString("F6")}", "Result");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Error");
            }
        }
    }
}

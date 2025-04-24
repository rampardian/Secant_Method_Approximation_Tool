using NCalc;

namespace Secant_Method_Approximation_Tool
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();

            Ruleslbl.Text =
                "Input Guidelines:\n" +
                "• Use 'e^(-x)' for exponentials (Euler’s number).\n" +
                "• Use '^' for powers, e.g., 'x^2' for x squared.\n" +
                "• Do not include '=' or 'f(x) ='.\n" +
                "• Use decimal points for numbers (e.g., 0.05).\n" +
                "• Input variables in lowercase 'x'.\n" +
                "• Enter numeric values for x(i-1), x(i), and error.";

            Formulaslbl.Text =
                "Secant Method Formula:\n" +
                "Secant Method Formula:\n" +
                "x(i+1) = x(i) - f(x(i)) * (x(i-1) - x(i)) / [f(x(i-1)) - f(x(i))]\n\n" +
                "Termination Criterion:\n" +
                "εa = |(x(i+1) - x(i)) / x(i+1)| * 100%";
        }

        private Task<double> EvaluateFunctionAsync(string function, double x)
        {
            var expr = new Expression(function);

            // Define variables
            expr.Parameters["x"] = x;
            expr.Parameters["e"] = Math.E; // Treat 'e' as Euler’s number

            try
            {
                var result = expr.Evaluate();
                return Task.FromResult(Convert.ToDouble(result));
            }
            catch
            {
                throw new Exception("Error parsing or evaluating the expression.");
            }
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

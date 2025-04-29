using Mathos.Parser;
using NCalc;
using ScottPlot;
using ScottPlot.WinForms;


namespace Secant_Method_Approximation_Tool
{
    public partial class Menu : Form
    {
        private MathParser parser; // Declare parser globally
        private string processedFunction; // Store processed function

        public Menu()
        {
            InitializeComponent();

            Ruleslbl.Text =
                "Input Guidelines:\n" +
                "• Use '^' for exponents: x^2 for x squared, e^(-x) for exponentials.\n" +
                "• Supported functions: sin(x), cos(x), log(x), sqrt(x), etc.\n" +
                "• Do not include '=' symbol in the function.\n" +
                "• Use decimal points (e.g., 0.05).\n" +
                "• Enter guesses (x(i-1) and x(i)) as numbers.\n" +
                "• Enter approximate error as a percent (e.g., 0.05).";

            Formulaslbl.Text =
                "Secant Method Formula:\n" +
                "x(i+1) = x(i) - f(x(i)) * (x(i-1) - x(i)) / [f(x(i-1)) - f(x(i))]\n\n" +
                "Termination Criterion:\n" +
                "εa = |(x(i+1) - x(i)) / x(i+1)| * 100%";

            parser = new MathParser(); // Initialize the parser
        }

        private Task<double> EvaluateFunctionAsync(string function, double x)
        {
            try
            {
                // Update variables every time you evaluate
                parser.LocalVariables["x"] = x;
                parser.LocalVariables["e"] = Math.E; // Euler's number

                double result = parser.Parse(function);
                return Task.FromResult(result);
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
                string rawFunc = Functiontxtbx.Text.Trim();
                processedFunction = rawFunc; // No processing needed with Mathos.Parser

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

                double f_xi_minus1 = await EvaluateFunctionAsync(processedFunction, xi_minus1);
                double f_xi = await EvaluateFunctionAsync(processedFunction, xi);

                Iterationsdgv.Rows.Add("0",
                    xi_minus1.ToString("F6"),
                    xi.ToString("F6"),
                    f_xi_minus1.ToString("F6"),
                    f_xi.ToString("F6"),
                    "", "");

                while (ea > es)
                {
                    f_xi_minus1 = await EvaluateFunctionAsync(processedFunction, xi_minus1);
                    f_xi = await EvaluateFunctionAsync(processedFunction, xi);

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

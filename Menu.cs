using NCalc;
using ScottPlot;
using ScottPlot.WinForms;

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
                formsPlot1.Plot.Clear();

                // Generate values for the function curve (f(x)) for a smooth curve
                double minX = xi_minus1 - 1;
                double maxX = xi + 1;
                double step = 0.01;  // Finer step for a smooth curve
                List<double> xValues = new List<double>();
                List<double> yValues = new List<double>();

                for (double x = minX; x <= maxX; x += step)
                {
                    try
                    {
                        double y = await EvaluateFunctionAsync(func, x);
                        xValues.Add(x);
                        yValues.Add(y);
                    }
                    catch { }
                }

                // Plot the function curve (smooth line)
                var funcPlot = formsPlot1.Plot.Add.Scatter(xValues.ToArray(), yValues.ToArray());
                funcPlot.Label = "f(x)";
                funcPlot.Color = Colors.Blue;
                funcPlot.LineWidth = 2;  // Make the line a little thicker for visibility

                // Plot secant points
                List<double> secantX = new List<double>();
                List<double> secantY = new List<double>();

                foreach (DataGridViewRow row in Iterationsdgv.Rows)
                {
                    if (row.IsNewRow || row.Cells["x(i)"].Value == null) continue;

                    double x_i = double.Parse(row.Cells["x(i)"].Value.ToString());
                    double y_i = await EvaluateFunctionAsync(func, x_i);

                    secantX.Add(x_i);
                    secantY.Add(y_i);
                }

                // Plot secant points as red dots
                var secantPlot = formsPlot1.Plot.Add.Scatter(secantX.ToArray(), secantY.ToArray());
                secantPlot.Color = Colors.Red;  // Red for secant points
                secantPlot.MarkerSize = 7;
                secantPlot.Label = "Secant Points";

                // Add title and labels
                formsPlot1.Plot.Title("Secant Method Approximation");
                formsPlot1.Plot.XLabel("x");
                formsPlot1.Plot.YLabel("f(x)");
                formsPlot1.Refresh();
                MessageBox.Show($"Estimated Root: {xi.ToString("F6")}", "Result");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Error");
            }
        }
    }
}

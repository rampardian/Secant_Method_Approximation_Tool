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
            formsPlot1.Plot.Clear(); // Clear previous plots

            try
            {
                // Read input
                string rawFunc = Functiontxtbx.Text.Trim();
                processedFunction = rawFunc;

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

                List<double> secantX = new List<double> { xi_minus1, xi };
                List<double> secantY = new List<double> { f_xi_minus1, f_xi };

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

                    secantX.Add(xi);
                    double f_xi_current = await EvaluateFunctionAsync(processedFunction, xi);
                    secantY.Add(f_xi_current);
                }

                MessageBox.Show($"Estimated Root: {xi.ToString("F6")}", "Result");

                // Plotting after solving
                List<double> xData = new List<double>();
                List<double> yData = new List<double>();

                double minX = xi - 2;
                double maxX = xi + 2;
                double step = 0.01;

                for (double x = minX; x <= maxX; x += step)
                {
                    try
                    {
                        double y = await EvaluateFunctionAsync(processedFunction, x);
                        xData.Add(x);
                        yData.Add(y);
                    }
                    catch
                    {
                        // Skip bad points
                    }
                }

                // Plot the function curve
                var curve = formsPlot1.Plot.Add.Scatter(xData.ToArray(), yData.ToArray());
                
                // Plot secant points
                var secantPoints = formsPlot1.Plot.Add.Scatter(secantX.ToArray(), secantY.ToArray());
                curve.Color = Colors.Blue;
                curve.LineWidth = 2;
                curve.MarkerSize = 0;
                curve.Label = "f(x)";

                secantPoints.Color = Colors.Red;
                secantPoints.MarkerSize = 7;
                secantPoints.MarkerShape = MarkerShape.FilledCircle;
                secantPoints.LineWidth = 0;
                secantPoints.Label = "Secant Points";

                formsPlot1.Plot.Title("Secant Method Approximation");
                formsPlot1.Plot.Axes.Left.Label.Text = "f(x)";
                formsPlot1.Plot.Axes.Bottom.Label.Text = "x";
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Plot.ShowLegend();

                formsPlot1.Refresh();

                formsPlot1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Error");
            }
        }

    }
}

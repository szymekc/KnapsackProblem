using System.Collections.Generic;

namespace GeneticAlgorithm {
    class GraphGenerator {
        static public void Run(List<double[]> data, string label, double[] values) {
            var plt = new ScottPlot.Plot(1280, 720);
            int pointCount = data[0].Length;
            for (int i = 0; i < data.Count; i++)
                plt.PlotSignal(data[i], label: label + " = " + values[i].ToString());

            plt.Title("Scores of each generation");
            plt.YLabel("Score");
            plt.XLabel("Iterations");
            plt.Ticks(useExponentialNotation: false, useMultiplierNotation: false);
            plt.Legend();
            plt.SaveFig(label + ".png");
        }

    }
}
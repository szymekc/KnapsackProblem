using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeneticAlgorithm {
    public class Program {
        static void Main(string[] args) {
            int pop_size = 100;
            int iterations = 1000;
            int tournament_size = 10;
            double crossover_rate = 0.9;
            double mutation_rate = 0.001;
            string file = "C:\\Users\\szyme\\GeneticAlgorithm\\tasks.csv";
            Stopwatch stopwatch = new Stopwatch();
            Random rnd = new Random();
            Task task = TaskLoader.Read(file);

            var data = new List<double[]>();
            for (crossover_rate = 0.8; crossover_rate < 0.95; crossover_rate += 0.05) {
                var scores = new List<double[]>();
                for (int i = 0; i < 5; i++) {
                    scores.Add(RunAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate));
                }
                data.Add(AverageScores(scores));
            }
            GraphGenerator.Run(data,"Crossover rate", new double[] { 0.8, 0.85, 0.9 });

            crossover_rate = 0.9;
            data = new List<double[]>();
            for (mutation_rate = 0.001; mutation_rate < 0.0025; mutation_rate += 0.0005) {
                var scores = new List<double[]>();
                for (int i = 0; i < 5; i++) {
                    scores.Add(RunAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate));
                }
                data.Add(AverageScores(scores));
            }
            GraphGenerator.Run(data, "Mutation rate", new double[] { 0.001, 0.0015, 0.002 });

            mutation_rate = 0.001;
            data = new List<double[]>();
            for (tournament_size = 5; tournament_size < 25; tournament_size += 5) {
                var scores = new List<double[]>();
                for (int i = 0; i < 5; i++) {
                    scores.Add(RunAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate));
                }
                data.Add(AverageScores(scores));
            }
            GraphGenerator.Run(data, "Tournament size", new double[] { 5, 10, 15, 20 });

            tournament_size = 10;
            data = new List<double[]>();
            for (pop_size = 100; pop_size < 350; pop_size += 50) {
                var scores = new List<double[]>();
                for (int i = 0; i < 5; i++) {
                    scores.Add(RunAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate));
                }
                data.Add(AverageScores(scores));
            }
            GraphGenerator.Run(data, "Population size", new double[] { 100, 150, 200, 250, 300 });
        }
        static double[] RunAlgorithm(Task task, int pop_size, int iterations, int tournament_size, double crossover_rate, double mutation_rate) {
            Population population = new Population(task.n, pop_size);
            var bestScores = new double[iterations];
            for (int i = 0; i < iterations; i++) {
                population.Evaluate(task);
                Population new_population = new Population(pop_size);
                for (int j = 0; j < pop_size; j++) {
                    Individual parent1 = population.Tournament(tournament_size, task);
                    Individual parent2 = population.Tournament(tournament_size, task);
                    Individual child = parent1.Crossover(parent2, crossover_rate);
                    child.Mutate(mutation_rate);
                    new_population.individuals[j] = child;
                }
                population = new_population;
                List<int> scores = new List<int>();
                foreach (Individual el in population.individuals) {
                    scores.Add(el.score);
                }
                bestScores[i] = (double)scores.Max();
            }
            return bestScores;
        }
        public static double[] AverageScores(List<double[]> allScores) {
            double[] avgScore = new double[allScores[0].Length];
            for (int i = 0; i < allScores[0].Length; i++) {
                double sum = 0;
                for (int j = 0; j < allScores.Count; j++) {
                    sum += allScores[j][i];
                }
                avgScore[i] = (sum / allScores.Count);
            }
            return avgScore;
        }
    }
}

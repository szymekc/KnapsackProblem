using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeneticAlgorithm {
    public class Program {
        static Stopwatch geneticTime;
        static Stopwatch greedyTime;
        static TimeSpan ts;
        static void Main(string[] args) {
            geneticTime = new Stopwatch();
            greedyTime = new Stopwatch();
            int pop_size = 300;
            int iterations = 1500;
            int tournament_size = 10;
            double crossover_rate = 0.9;
            double mutation_rate = 0.001;
            string file = "C:\\Users\\szyme\\GeneticAlgorithm\\tasks.csv";
            Stopwatch stopwatch = new Stopwatch();
            Random rnd = new Random();
            Task task = TaskLoader.Read(file);
            GenerateGraphs(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate);
            Console.WriteLine("Greedy algorithm score {0}, Execution time {1}ms", RunGreedyAlgorithm(task), ts.Milliseconds);
            Console.WriteLine("Genetic algorithm score {0}, Execution time {1}ms", RunGeneticForComparison(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate), ts.Milliseconds);
        }
        static double[] RunGeneticAlgorithm(Task task, int pop_size, int iterations, int tournament_size, double crossover_rate, double mutation_rate) {
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
        static int RunGreedyAlgorithm(Task task) {
            greedyTime.Start();
            var items = new List<(int, int, int)>();
            for (int j = 0; j < task.n; j++) {
                items.Add((task.w_i[j], task.s_i[j], task.c_i[j]));
            }
            items.Sort((b, a) =>
                (a.Item3 / (a.Item2 + a.Item1)).CompareTo(b.Item3 / (b.Item2 + b.Item1))
            );
            int score = 0;
            int w_sum = 0;
            int s_sum = 0;
            int i = 0;
            while (w_sum < task.w && s_sum < task.s) {
                score += items[i].Item3;
                w_sum += items[i].Item1;
                s_sum += items[i].Item2;
                i++;
            }
            greedyTime.Stop();
            ts = greedyTime.Elapsed;
            return score;
        }
        static int RunGeneticForComparison(Task task, int pop_size, int iterations, int tournament_size, double crossover_rate, double mutation_rate) {
            geneticTime.Start();
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
            }
            geneticTime.Stop();
            ts = geneticTime.Elapsed;
            return population.individuals.Max((a) => a.score);
        }
        static void GenerateGraphs(Task task, int pop_size, int iterations, int tournament_size, double crossover_rate, double mutation_rate) {

            var data = new List<double[]>();
            var labels = new List<double>();
            for (crossover_rate = 0.1; crossover_rate < 1; crossover_rate += 0.2) {
                var scores = new List<double[]>();
                for (int i = 0; i < 5; i++) {
                    scores.Add(RunGeneticAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate));
                }
                data.Add(AverageScores(scores));
                labels.Add(crossover_rate);
            }
            GraphGenerator.Run(data, "Crossover rate", labels.ToArray());

            crossover_rate = 0.9;
            data = new List<double[]>();
            labels = new List<double>();
            for (mutation_rate = 0.001; mutation_rate < 0.005; mutation_rate += 0.001) {
                var scores = new List<double[]>();
                for (int i = 0; i < 5; i++) {
                    scores.Add(RunGeneticAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate));
                }
                data.Add(AverageScores(scores));
                labels.Add(mutation_rate);
            }
            GraphGenerator.Run(data, "Mutation rate", labels.ToArray());

            mutation_rate = 0.001;
            data = new List<double[]>();
            labels = new List<double>();
            foreach (int tour_size in new int[] { 2, 5, 10, 50, 100, pop_size }) {
                var scores = new List<double[]>();
                for (int i = 0; i < 5; i++) {
                    scores.Add(RunGeneticAlgorithm(task, pop_size, iterations, tour_size, crossover_rate, mutation_rate));
                }
                data.Add(AverageScores(scores));
                labels.Add(tour_size);
            }
            GraphGenerator.Run(data, "Tournament size", labels.ToArray());

            tournament_size = 10;
            data = new List<double[]>();
            labels = new List<double>();
            for (pop_size = 50; pop_size < 500; pop_size += 50) {
                var scores = new List<double[]>();
                for (int i = 0; i < 5; i++) {
                    scores.Add(RunGeneticAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate));
                }
                data.Add(AverageScores(scores));
                labels.Add(pop_size);
            }
            GraphGenerator.Run(data, "Population size", labels.ToArray());
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

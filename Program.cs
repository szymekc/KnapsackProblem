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
            stopwatch.Start();
            Console.WriteLine("Best score in last gen {0}", GeneticAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate)[iterations-1]);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("{0}ms per iteration", ts.TotalMilliseconds / iterations);

        }
        static List<int> GeneticAlgorithm(Task task, int pop_size, int iterations, int tournament_size, double crossover_rate, double mutation_rate) {
            Population population = new Population(task.n, pop_size);
            List<int> bestScores = new List<int>();
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
                bestScores.Add(scores.Max());
            }
            return bestScores;
        }
        public static List<int> AverageScores(List<List<int>> allScores) {
            List<int> avgScore = new List<int>();
            for (int i = 0; i < allScores[0].Count; i++) {
                int sum = 0;
                for (int j = 0; j < allScores.Count; j++) {
                    sum += allScores[j][i];
                }
                avgScore.Add(sum / allScores.Count);
            }
            return avgScore;
        }
    }
}

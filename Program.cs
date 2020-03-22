using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeneticAlgorithm {
    class Program {
        static void Main(string[] args) {
            int pop_size = 100;
            int iterations = 1000;
            int tournament_size = 20;
            double crossover_rate = 0.05;
            double mutation_rate = 0.001;
            string file = "C:\\Users\\szyme\\GeneticAlgorithm\\tasks.csv";
            Random rnd = new Random();
            Mission task = TaskLoader.Read(file);
            GeneticAlgorithm(task, pop_size, iterations, tournament_size, crossover_rate, mutation_rate);
        }
        static List<int> GeneticAlgorithm(Mission task, int pop_size, int iterations, int tournament_size, double crossover_rate, double mutation_rate) {
            Population population = new Population(task.n, pop_size);
            List<int> bestScores = new List<int>();
            for (int i = 0; i < iterations; i++) {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Population new_population = new Population(pop_size);
                for (int j = 0; j < pop_size; j++) {
                    Individual parent1 = population.Tournament(tournament_size, task);
                    Individual parent2 = population.Tournament(tournament_size, task);
                    Individual child = parent1.Crossover(parent2, crossover_rate);
                    child.Mutate(mutation_rate);
                    new_population.individuals[j] = child;
                }
                population = new_population;
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                List<int> scores = new List<int>();
                foreach (Individual el in population.individuals) {
                    scores.Add(el.Evaluate(task));
                }
                bestScores.Add(scores.Max());
                Console.WriteLine("Generation {0} Score {1} Time Elapsed {2}ms", i, scores.Max(), ts.TotalMilliseconds);
            }
            return bestScores;
        }
    }
}

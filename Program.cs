using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeneticAlgorithm {
    class Program {
        static void Main(string[] args) {
            int pop_size = 100;
            int iterations = 5000;
            int tournament_size = 20;
            double crossover_rate = 0.05;
            double mutation_rate = 0.001;
            string file = "C:\\Users\\szyme\\GeneticAlgorithm\\tasks.csv";
            Random rnd = new Random();
            Task task = TaskLoader.Read(file);
            Population population = new Population(task.n, pop_size);
            for (int i = 0; i < iterations; i++) {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                // Get the elapsed time as a TimeSpan value.

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
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours, ts.Minutes, ts.Seconds,
    ts.Milliseconds / 10);
                List<int> scores = new List<int>();
                foreach (Individual el in population.individuals) {
                    scores.Add(el.Evaluate(task));
                }
                Individual best = population.individuals.ElementAt<Individual>(scores.IndexOf(scores.Max()));
                Console.WriteLine("Iteration number {0}", i);
                Console.WriteLine("RunTime " + elapsedTime);
                Console.WriteLine("Best score {0}", scores.Max());
            }

        }
    }
}

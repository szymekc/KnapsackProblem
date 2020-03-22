using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {
    public class Population {
        public Individual[] individuals;
        static Random rnd = new Random();
        public Population(int size) {
            individuals = new Individual[size];
        }
        public Population(int n_items, int size) {
            individuals = new Individual[size];
            for (int i = 0; i < size; i++) {
                individuals[i] = new Individual(n_items);
            }
        }
        public Individual Tournament(int size, Task task) {
            HashSet<int> indices = new HashSet<int>();
            List<int> scores = new List<int>();
            while (indices.Count < size) {
                indices.Add(rnd.Next(individuals.Length));
            }
            foreach (int idx in indices) {
                scores.Add(individuals[idx].score);
            }
            return individuals.ElementAt<Individual>(indices.ElementAt(scores.IndexOf(scores.Max())));
        }
        public void Evaluate(Task task) {
            foreach (Individual ind in individuals) {
                ind.Evaluate(task);
            }
        }
    }
}

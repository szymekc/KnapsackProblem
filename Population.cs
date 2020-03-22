using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {
    class Population {
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
        public Individual Tournament(int size, Mission task) {
            HashSet<int> indices = new HashSet<int>();
            List<int> scores = new List<int>();
            while (indices.Count < size) {
                indices.Add(rnd.Next(individuals.Length));
            }
            foreach (int idx in indices) {
                scores.Add(individuals[idx].Evaluate(task));
            }
            return individuals.ElementAt<Individual>(indices.ElementAt(scores.IndexOf(scores.Max())));
        }
    }
}

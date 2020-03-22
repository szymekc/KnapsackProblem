using System;
using System.Collections.Generic;
namespace GeneticAlgorithm {
    public class Individual {
        public bool[] dna;
        public Random rnd = new Random();
        public int score;
        public Individual(int n_items) {
            dna = new bool[n_items];
            for (int i = 0; i < n_items; i++) {
                dna[i] = rnd.Next(5) == 0;
            }
        }
        public Individual(bool[] dna) {
            this.dna = dna;
        }
        public void Evaluate(Task task) {
            if (Dot(task.w_i, dna) > task.w || Dot(task.s_i, dna) > task.s) {
                score = 0;
            } else {
                score = Dot(task.c_i, dna);
            }
        }
        public Individual Crossover(Individual partner, Double rate) {
            int crossoverPoint = (int)(rnd.NextDouble() * dna.Length);
            if (rnd.NextDouble() < rate) {
                bool[] ret = new bool[dna.Length];
                for (int i = 0; i < crossoverPoint; i++) {
                    ret[i] = dna[i];
                }
                for (int i = crossoverPoint; i < dna.Length; i++) {
                    ret[i] = partner.dna[i];
                }
                return new Individual(dna: ret);
            }
            return (Individual)this.MemberwiseClone();
        }
        public void Mutate(Double rate) {
            HashSet<int> indices = new HashSet<int>();
            while (indices.Count < (int)(this.dna.Length * rate)) {
                indices.Add(rnd.Next(dna.Length));
            }
            foreach (int idx in indices) {
                this.dna[idx] ^= true;
            }
        }
        public static int Dot(int[] a, bool[] b) {
            int sum = 0;
            for (int i = 0; i < a.Length; i++) {
                if (b[i]) sum += a[i];
            }
            return sum;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GeneticAlgorithm {
    class Individual {
        bool[] dna;
        Random rnd = new Random();
        public Individual(int n_items) {
            dna = new bool[n_items];
            for (int i = 0; i < n_items; i++) {
                dna[i] = rnd.Next(10) == 0;
            }
        }
        public Individual(bool[] dna) {
            this.dna = dna;
        }
        public int Evaluate(Mission task) {
            if (Dot(task.w_i, dna) > task.w || Dot(task.s_i, dna) > task.s) {
                return 0;
            } else {
                return Dot(task.c_i, dna);
            }
        }
        public Individual Crossover(Individual partner, Double rate) {
            if (rnd.NextDouble() < rate) {
                return this;
            }
            bool[] ret = new bool[dna.Length];
            dna[0..(dna.Length / 2)].CopyTo(ret, 0);
            partner.dna[(dna.Length / 2)..^0].CopyTo(ret, dna.Length / 2);
            return new Individual(dna: ret);
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
                sum += a[i] * (b[i] ? 1 : 0);
            }
            return sum;
        }
        //public async System.Threading.Tasks.Task Evaluate(Mission task) {

        //}
        //public async System.Threading.Tasks.Task<int> Dot(int[] a, bool[] b) {

        //}
    }
}

using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
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
        public int Evaluate(Task task) {
            int[] w_arr = new int[task.n];
            int[] s_arr = new int[task.n];
            int[] c_arr = new int[task.n];
            int i = 0;
            foreach (Item it in task.itemList) {
                w_arr[i] = it.w;
                s_arr[i] = it.s;
                c_arr[i] = it.c;
                i++;
            }
            if (Dot(w_arr, dna) > task.w || Dot(s_arr, dna) > task.s) {
                return 0;
            } else {
                return Dot(c_arr, dna);
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
            //return a.Zip(b, (x, y) => x * (y ? 1 : 0)).Sum();
            int sum = 0;
            for (int i = 0; i < a.Length; i++) {
                sum += a[i] * (b[i] ? 1 : 0);
            }
            return sum;
        }
    }
}

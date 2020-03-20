using System;
using System.IO;

namespace GeneticAlgorithm {
    class TaskGenerator {
        static Random rnd = new Random();
        static public void Generate(int n, int w, int s, string output_file) {
            int w_sum = 0;
            int s_sum = 0;
            using (StreamWriter sw = File.CreateText(output_file)) {
                sw.WriteLine("{0},{1},{2}", n, w, s);
            }
            for (int i = 0; i < n; i++) {
                int w_i = rnd.Next(1, 10 * w / n);
                int s_i = rnd.Next(1, 10 * s / n);
                int c_i = rnd.Next(1, n);
                using (StreamWriter sw = File.AppendText(output_file)) {
                    sw.WriteLine("{0},{1},{2}", w_i, s_i, c_i);
                }
                w_sum += w_i;
                s_sum += s_i;
            }
            if (w_sum <= 2 * w || s_sum <= 2 * s) {
                Console.WriteLine("Criteria not met. Retrying generation.");
                Generate(n, w, s, output_file);
            } else {
                return;
            }
        }
    }
}

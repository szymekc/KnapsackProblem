using Microsoft.VisualBasic.FileIO;

namespace GeneticAlgorithm {
    class TaskLoader {
        static Mission task;
        static public Mission Read(string filename) {
            using (TextFieldParser parser = new TextFieldParser(filename)) {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string[] fields = parser.ReadFields();
                int n, w, s;
                int i = 0;
                int.TryParse(fields[0], out n);
                int.TryParse(fields[1], out w);
                int.TryParse(fields[2], out s);
                task = new Mission(n, w, s);
                int[] w_i = new int[n];
                int[] s_i = new int[n];
                int[] c_i = new int[n];
                while (!parser.EndOfData) {
                    fields = parser.ReadFields();
                    int.TryParse(fields[0], out w_i[i]);
                    int.TryParse(fields[1], out s_i[i]);
                    int.TryParse(fields[2], out c_i[i]);
                    i++;
                }
                task.w_i = w_i;
                task.s_i = s_i;
                task.c_i = c_i;
                return task;
            }
        }
    }
}

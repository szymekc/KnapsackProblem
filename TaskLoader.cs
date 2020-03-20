using Microsoft.VisualBasic.FileIO;

namespace GeneticAlgorithm {
    class TaskLoader {
        static Task task;
        static public Task Read(string filename) {
            using (TextFieldParser parser = new TextFieldParser(filename)) {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string[] fields = parser.ReadFields();
                int n, w, s;
                int i = 0;
                int.TryParse(fields[0], out n);
                int.TryParse(fields[1], out w);
                int.TryParse(fields[2], out s);
                Item[] itemList = new Item[n];
                task = new Task(n, w, s);
                while (!parser.EndOfData) {
                    fields = parser.ReadFields();
                    int w_i, s_i, c_i;
                    int.TryParse(fields[0], out w_i);
                    int.TryParse(fields[1], out s_i);
                    int.TryParse(fields[2], out c_i);
                    Item it = new Item(w_i, s_i, c_i);
                    itemList[i] = it;
                    i++;
                }
                task.itemList = itemList;
                return task;
            }
        }
    }
}

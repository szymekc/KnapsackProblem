using GeneticAlgorithm;
using System.Collections.Generic;

namespace Tests {
    [TestClass]
    public class AverageValueTests {
        [TestMethod]
        public void TestMethod1() {
            List<int> list1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> list2 = new List<int>() { 2, 3, 4, 5, 6 };
            List<int> list3 = new List<int>() { 3, 4, 5, 6, 7 };
            List<List<int>> listlist = new List<List<int>>() { list1, list2, list3 };
            List<int> expectedList = new List<int>() { 2, 3, 4, 5, 6 };
            CollectionAssert.AreEqual(expectedList, Program.AverageScores(listlist));
        }
        [TestMethod]
        public void TestMethod2() {
            List<int> list1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> list2 = new List<int>() { 2, 3, 4, 5, 6 };
            List<int> list3 = new List<int>() { 3, 4, 5, 6, 7 };
            List<int> list4 = new List<int>() { 5, 6, 7, 8, 9 };
            List<List<int>> listlist = new List<List<int>>() { list1, list2, list3, list4 };
            List<int> expectedList = new List<int>() { 2, 3, 4, 5, 6 };
            CollectionAssert.AreEqual(expectedList, Program.AverageScores(listlist));
        }
        [TestMethod]
        public void TestMethod3() {
            List<int> list1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> list2 = new List<int>() { 2, 3, 4, 5, 6 };
            List<int> list3 = new List<int>() { 3, 4, 5, 6, 7 };
            List<int> list4 = new List<int>() { 10, 11, 12, 13, 14 };
            List<List<int>> listlist = new List<List<int>>() { list1, list2, list3, list4 };
            List<int> expectedList = new List<int>() { 4, 5, 6, 7, 8 };
            CollectionAssert.AreEqual(expectedList, Program.AverageScores(listlist));
        }
        [TestMethod]
        public void TestMethod4() {
            List<int> list1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> list2 = new List<int>() { 2, 3, 4, 5, 6 };
            List<int> list3 = new List<int>() { 3, 4, 5, 6, 7 };
            List<int> list4 = new List<int>() { 10, 11, 12, 13, 14 };
            List<List<int>> listlist = new List<List<int>>() { list1, list2, list3, list4 };
            List<int> unexpectedList = new List<int>() { 4, 5765, 6, 7, 8 };
            CollectionAssert.AreNotEqual(unexpectedList, Program.AverageScores(listlist));
        }
    }
}

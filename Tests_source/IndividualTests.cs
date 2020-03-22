using GeneticAlgorithm;
using System;

namespace Tests {
    [TestClass]
    public class IndividualTests {

        [TestMethod]
        public void EvaluateTest1() {
            Task task = new Task(5, 5, 5);
            task.w_i = new int[] { 1, 2, 3, 2, 2 };
            task.s_i = new int[] { 1, 3, 3, 2, 1 };
            task.c_i = new int[] { 10, 60, 90, 40, 20 };
            bool[] dna = new bool[] { true, true, false, false, true };
            Individual ind = new Individual(dna);
            int expectedScore = 90;
            ind.Evaluate(task);
            Assert.AreEqual(expectedScore, ind.score);
        }
        [TestMethod]
        public void EvaluateTest2() {
            Task task = new Task(5, 5, 5);
            task.w_i = new int[] { 1, 2, 3, 2, 2 };
            task.s_i = new int[] { 1, 3, 3, 2, 1 };
            task.c_i = new int[] { 10, 60, 90, 40, 20 };
            bool[] dna = new bool[] { true, true, false, true, true };
            Individual ind = new Individual(dna);
            int expectedScore = 0;
            ind.Evaluate(task);
            Assert.AreEqual(expectedScore, ind.score);
        }
        [TestMethod]
        public void MutateTest1() {
            bool[] dna = new bool[] { true, true, false, true, true };
            Individual ind = new Individual((bool[])dna.Clone());
            double rate = 0.2;
            ind.Mutate(rate);
            CollectionAssert.AreNotEqual(dna, ind.dna);
        }
        [TestMethod]
        public void CrossoverTest1() {
            bool[] dna1 = new bool[] { true, true, false, false, false };
            Individual ind = new Individual(dna1);
            bool[] dna2 = new bool[] { false, true, false, true, false };
            Individual partner = new Individual(dna2);
            ind.rnd = new Random(1); //NextDouble() forced 0.24
            Individual child = ind.Crossover(partner, 1);
            bool[] expectedDna = new bool[] { true, true, false, true, false };
            CollectionAssert.AreEqual(expectedDna, child.dna);
        }
        [TestMethod]
        public void DotTest() {
            int[] num = new int[] { 1, 2, 3, 4, 5 };
            bool[] dna = new bool[] { true, true, false, false, true };
            int expectedDot = 8;
            Assert.AreEqual(expectedDot, Individual.Dot(num, dna));
        }
        [TestMethod]
        public void TournamentTest() {
            Population pop = new Population(5, 3);
            pop.individuals[0].score = 5;
            pop.individuals[1].score = 30;
            pop.individuals[2].score = 15;
            Assert.AreEqual(pop.individuals[1], pop.Tournament(3, new Task(5, 1, 1)));
        }
    }
}
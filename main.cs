using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Gather Population, Chromosome gene count, and mutation rate from user */
            int population_count, gene_count;
            double mutation_rate;
            do
            {
                Console.WriteLine("Select a valid population count.");
            } while (!Int32.TryParse(Console.ReadLine(), out population_count));
            Console.Clear();

            do
            {
                Console.WriteLine("Select a valid amount of genes per member in population.");
            } while (!Int32.TryParse(Console.ReadLine(), out gene_count));
            Console.Clear();

            do
            {
                Console.WriteLine("Select a valid mutation percentage (double).");
            } while (!Double.TryParse(Console.ReadLine(), out mutation_rate));
            Console.Clear();

            /* Create world based on inputs and simulate */
            World my_world = new World(population_count, gene_count, mutation_rate);
            my_world.world_simulate();
        }


    }
}

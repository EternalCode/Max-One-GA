using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class Chromosome
    {
        private List<int> genes = new List<int>();

        public Chromosome(Random prng, int gene_size)
        {
            for (int i = 0; i <  gene_size; i++)
            {
                this.genes.Add(prng.Next(0, 2));
            }
        }

        public string str_genes()
        {
            string output = "";
            foreach (int gene in this.genes)
            {
                output += gene.ToString();
            }
            return output;
        }

        public List<int> get_genes()
        {
            return this.genes;
        }

        public double get_fitness()
        {
            /* Fitness is exponential.
             * Higher fit members are exponentially more likely to be used */
            int fitness = 0;
            foreach (int gene in this.genes)
            {
                fitness += gene;
            }
            return Math.Pow(2, fitness);
        }

        public void set_genes(List<int> new_genes)
        {
            // this is used when creating a child,the parent's crossover genes are passed
            this.genes = new_genes;
        }

        public void apply_mutation(Random prng, double mutation)
        {
            for (int i = 0; i < this.genes.Count; i++)
            {
                if ((prng.NextDouble() * 100) <= mutation)
                {
                    // flip value
                    this.genes[i] = (this.genes[i] > 0) ? 0 : 1;
                }
            }
        }
    }
}

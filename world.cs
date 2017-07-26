using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class World
    {
        public Random prng = new Random();
        private List<Chromosome> population = new List<Chromosome>();
        private int population_count;
        private int gene_count;
        private double fitness_sum = 0;
        private int generation_count = 1;
        private double mutation_rate = 0;

        public World(int population_count, int gene_count, double mutation_rate)
        {
            this.population_count = population_count;
            this.gene_count = gene_count;
            this.mutation_rate = mutation_rate;

        }

        public void world_simulate()
        {
            /* World simulation loop until perfect member produced */
            population_initialize();
            while (!check_perfect_member())
            {
                this.generation_count++;
                Console.WriteLine("\n\nGeneration {0}\n", this.generation_count);
                new_generation();
            }
            Console.WriteLine("\n\n\tPerfect member created in {0} Generations", this.generation_count);
        }

        private void population_initialize()
        {
            Console.WriteLine("Population: {0}\nGene Count: {1}\nMutation Rate: {2}", this.population_count, this.gene_count, this.mutation_rate);
            Console.WriteLine("\n\nGeneration {0}\n", this.generation_count);

            /* Add initial population to list */
            for (int i = 0; i < this.population_count; i++)
            {
                this.population.Add(new Chromosome(this.prng, this.gene_count));
                this.fitness_sum += this.population[i].get_fitness(); // update fitness sum
                Console.WriteLine("{0}\t{1}", this.population[i].str_genes(), this.population[i].get_fitness());
            }
            
        }

        private void new_generation()
        {
            List<Chromosome> new_population = new List<Chromosome>();
            double new_fitness_sum = 0;
            int elite_count = Math.Max((this.population.Count / 100), 1);
            while (new_population.Count < this.population.Count - elite_count)
            {
                // select two random parent's genes based on fitness
                List<int> parent1_genes = population[pick_member()].get_genes();
                List<int> parent2_genes = population[pick_member()].get_genes();

                // perform cross over on parents
                parent1_genes = parent1_genes.GetRange(0, (parent1_genes.Count / 2));
                parent2_genes = parent2_genes.GetRange((parent2_genes.Count / 2), (parent2_genes.Count / 2));
                List<int> cross_over_genes = (parent1_genes.Concat(parent2_genes).ToList());

                // Create child
                Chromosome child = new Chromosome(prng, gene_count);
                child.set_genes(cross_over_genes);

                // Apply mutation to child
                child.apply_mutation(this.prng, this.mutation_rate);

                new_population.Add(child);
                new_fitness_sum += child.get_fitness();
                Console.WriteLine("{0}\t{1}", child.str_genes(), child.get_fitness());
            }

            while (new_population.Count < this.population.Count)
            {
                // GA Elitism
                // Get most fit member
                if (this.prng.Next(0, 2) > 0)
                {
                    new_population.Add(population[get_elite()]);
                } else
                {
                    int index = get_elite();
                    population[index].apply_mutation(this.prng, this.mutation_rate);
                    new_population.Add(population[index]);
                }
            }
            this.population = new_population;
            this.fitness_sum = new_fitness_sum;
        }

        private int pick_member()
        {
            /* Weighted member selection, based on fitness */
            int index = 0;
            double rand_chance = prng.NextDouble();
            while ((rand_chance -= population[index].get_fitness() / fitness_sum) > 0)
                index++;
            return index;
        }

        private bool check_perfect_member()
        {
            foreach (Chromosome member in this.population)
            {
                /* A perfect member's fitness will be the highest possible fitness */
                if (member.get_fitness() == Math.Pow(2, member.get_genes().Count))
                {
                    return true;
                }
            }
            return false;
        }

        private int get_elite()
        {
            /* Find most fit member's index */
            int elite_index = 0;
            double highest_fitness = 0;
            for (int i = 0; i < this.population.Count; i++)
            {
                double fitness = this.population[i].get_fitness();
                if (fitness > highest_fitness)
                {
                    highest_fitness = fitness;
                    elite_index = i;
                }
            }
            return elite_index;
        }
    }
}

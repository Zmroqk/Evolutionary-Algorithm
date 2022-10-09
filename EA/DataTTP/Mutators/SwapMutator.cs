﻿using EA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.DataTTP.Mutators
{
    public class SwapMutator : IMutator<Specimen>
    {
        public Data Config { get; set; }
        public double MutateRatio { get; set; }

        public int NodeSwappingCount { get; set; }
        public int ItemSwappingCount { get; set; }
        public double Probability
        {
            get
            {
                return this.MutateRatio;
            }
            set
            {
                this.MutateRatio = value;
            }
        }

        public SwapMutator(Data config, double mutateRatio, int nodeSwappingCount, int itemSwappingCount)
        {
            this.Config = config;
            this.MutateRatio = mutateRatio;
            this.NodeSwappingCount = nodeSwappingCount;
            this.ItemSwappingCount = itemSwappingCount;
        }

        public IList<Specimen> Mutate(IList<Specimen> currentPopulation)
        {
            Random random = new Random();
            foreach(Specimen specimen in currentPopulation)
            {
                if(random.Next() <= this.MutateRatio)
                {
                    for(int i = 0; i < this.NodeSwappingCount; i++)
                    {
                        var index1 = random.Next(specimen.Nodes.Count);
                        var index2 = random.Next(specimen.Nodes.Count);
                        var swappedNode = specimen.Nodes[index1];
                        specimen.Nodes[index1] = specimen.Nodes[index2];
                        specimen.Nodes[index2] = swappedNode;
                    }
                    //TODO Should be moved to own MutateRatio if?
                    for (int i = 0; i < this.ItemSwappingCount; i++)
                    {
                        var items = specimen.GetKnapsackItems();
                        var index1 = random.Next(items.Length);
                        var index2 = random.Next(this.Config.Items.Count);
                        specimen.RemoveItemFromKnapsack(items[index1]);
                        specimen.AddItemToKnapsack(this.Config.Items[index2]);
                    }
                }
            }
            return currentPopulation;
        }
    }
}

namespace T3___influence_score
{
    class InfluenceScore()
    {
        public static void Main(string[] args)
        {

            Console.Write("Example graph 1: (unweighted)\n\nResults:\n");

            UnweightedGraph ug1 = new UnweightedGraph(8);
            ug1.AddEdge(0, 1);
            ug1.AddEdge(1, 2);
            ug1.AddEdge(2, 3);
            ug1.AddEdge(3, 4);
            ug1.AddEdge(3, 7);
            ug1.AddEdge(4, 5);
            ug1.AddEdge(5, 6);
            ug1.AddEdge(5, 7);
            ug1.AddEdge(6, 7);

            for (int i = 0; i < ug1.getNumberOfNodes(); i++)
            {
                Console.WriteLine($"Node {i} : score {ug1.InfluenceScore(i)} ");
            }

            Console.Write("\n\nExample graph 2: (weighted)\n\nResults:\n");

            var wg1 = new WeightedGraph(10);
            wg1.AddEdge(0, 1, 1);//a
            wg1.AddEdge(0, 2, 1);
            wg1.AddEdge(0, 4, 5);

            wg1.AddEdge(1, 2, 4);//b
            wg1.AddEdge(1, 4, 1);
            wg1.AddEdge(1, 6, 1);
            wg1.AddEdge(1, 7, 1);

            wg1.AddEdge(2, 3, 3);//c
            wg1.AddEdge(2, 4, 1);

            wg1.AddEdge(3, 4, 2);//d
            wg1.AddEdge(3, 5, 1);
            wg1.AddEdge(3, 6, 5);

            wg1.AddEdge(4, 6, 2);//e

            wg1.AddEdge(5, 6, 1);//f

            wg1.AddEdge(6, 7, 2);//g

            wg1.AddEdge(7, 8, 3);//h

            wg1.AddEdge(8, 9, 3);//i

            for (int i = 0; i < wg1.getNumberOfNodes(); i++)
            {
                Console.WriteLine($"Node {i} : score {wg1.InfluenceScore(i)} ");
            }
        }
    }
}
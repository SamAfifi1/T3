namespace T3___influence_score
{
    class UnweightedGraph
    {
        private int numberOfNodes;
        private List<int>[] adjacencyList; //an array of nodes, with each node having a list of adjacencies

        public UnweightedGraph(int vertices) //constructor to create a graph
        {
            numberOfNodes = vertices;
            adjacencyList = new List<int>[numberOfNodes]; //makes an array of size = number of nodes

            //for each node in the array, makes a list to store adjacencies  
            for (int i = 0; i < numberOfNodes; i++)
            {
                adjacencyList[i] = new List<int>();
            }
        }

        //Function to add an edge between 2 nodes
        public void AddEdge(int u, int v)
        {
            adjacencyList[u].Add(v); //adds node v to the adjacency list for node u
            adjacencyList[v].Add(u); //adds node u to the adjacency list for node v
        }
        public int getNumberOfNodes() { return numberOfNodes; }

        public double InfluenceScore(int node) //calculates the influence score for a given node
        {
            //following the fomula score(u) = (n-1)/ Σd(u,v) where d(u,v) is the shortest distance between u and v

            double distanceSum = (double)SumAllShortestPaths(node); //calculate Σd(u,v)
            return (numberOfNodes - 1) / distanceSum; //calc + return score
        }

        // Function to find the shortest paths from a start node to all other nodes using BFS
        private int SumAllShortestPaths(int start)
        {
            bool[] visited = new bool[numberOfNodes];
            int[] distance = new int[numberOfNodes];

            //creates a queue and adds the start node as the first item

            visited[start] = true;
            distance[start] = 0;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            //runs until the queue is empty (all nodes have been visited)
            while (queue.Count > 0)
            {
                //removes the first item from the queue and sets it as the current node
                int current = queue.Dequeue();


                foreach (int neighbor in adjacencyList[current])
                {
                    if (!visited[neighbor]) //if the neighborhas not been visited yet
                    {
                        visited[neighbor] = true; //mark it as visited
                        distance[neighbor] = distance[current] + 1; //add a distance
                        queue.Enqueue(neighbor);//adds it to the queue so its neighbors can be visted
                    }
                }
            }

            //calculate the sum of all distances
            int sumOfDistances = 0;
            foreach (var d in distance)
            {
                sumOfDistances += d;
            }

            return sumOfDistances; ;
        }
    }
}

namespace T3___influence_score
{
    //define a datatype to simplify later code
    using adjacencyPair = Tuple<int, int>;  //tuple<node,weight>
    class WeightedGraph
    {
        private int numberOfNodes;
        //an array of nodes, with each node having a list of adjacency pairs
        // each adjacency pair has a node and a weight
        private List<adjacencyPair>[] adjacencyList;

        public WeightedGraph(int vertices) //constructor to create a graph
        {
            numberOfNodes = vertices;
            adjacencyList = new List<adjacencyPair>[numberOfNodes]; //makes an array of size = number of nodes

            //for each node in the array, makes a list to store adjacency pairs 
            for (int i = 0; i < numberOfNodes; i++)
            {
                adjacencyList[i] = new List<adjacencyPair>();
            }
        }

        //Function to add an edge between 2 nodes
        public void AddEdge(int u, int v, int weight)
        {
            adjacencyList[u].Add(new adjacencyPair(v, weight)); //adds node v and weight to the adjacency list for node u
            adjacencyList[v].Add(new adjacencyPair(u, weight)); //adds node u and weight to the adjacency list for node v
        }
        public int getNumberOfNodes() { return numberOfNodes; }

        public double InfluenceScore(int node) //calculates the influence score for a given node
        {
            //following the fomula score(u) = (n-1)/ Σd(u,v) where d(u,v) is the shortest distance between u and v
            double distanceSum = (double)SumAllShortestPaths(node); //calculate Σd(u,v)
            return (numberOfNodes - 1) / distanceSum; //calc + return score
        }


        // Function to find the shortest paths from a start node to all other nodes using dijkstras algorithm (a type of BFS)
        public int SumAllShortestPaths(int start)
        {
            //create 2 arrays to store if we have visited that node and the distance to that node
            //list index = node number
            bool[] visited = new bool[numberOfNodes];
            int[] distance = new int[numberOfNodes];

            //initialize visited = false and distance = infinity to the arrays
            for (int i = 0; i < numberOfNodes; i++)
            {
                distance[i] = int.MaxValue;
                visited[i] = false;
            }
            //set the distance to the start node to 0 as we are already at the start
            distance[start] = 0;

            //create a priority queue to store which node to visit next (distance, node)
            var priorityQueue = new PriorityQueue<int, int>();

            //enqueue the start node with a distance of 0
            priorityQueue.Enqueue(start, 0);

            while (priorityQueue.Count > 0) //continue to loop until we have visited each node in the queue
            {
                //dequeue the node with the smallest distance
                int currentNode = priorityQueue.Dequeue();

                //skip this node if it has already been visited
                if (visited[currentNode]) continue;

                //set the current node to visited
                visited[currentNode] = true;

                //itterates through each node with an adjacency to the current node
                foreach (var neighbor in adjacencyList[currentNode])
                {
                    int neighborNode = neighbor.Item1; //get the node number
                    int weight = neighbor.Item2; //get the weight to visit that node


                    //if it is easier to visit that node from the current node: (distance to current node + weight) than the current lowest distance for that node
                    if (distance[currentNode] + weight < distance[neighborNode])
                    {
                        //then update distance to the new lower distance
                        distance[neighborNode] = distance[currentNode] + weight;

                        //enqueue the neighbor with the updated distance
                        priorityQueue.Enqueue(neighborNode, distance[neighborNode]);
                    }//else: do not change that nodes distance and try the next neighbour
                }//when all neighbors are checked we have done with this node and evaluate the next item in the queue
            }//once this loop terminates we have a completed list of the lowest distances to each node from the start

            // Calculate the sum of all distances
            int sumOfDistances = 0;
            foreach (var d in distance)
            {
                if (d != int.MaxValue) // Ignore unreachable nodes
                {
                    sumOfDistances += d;
                }
            }

            return sumOfDistances;
        }
    }
}

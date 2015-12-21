using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    /// <summary>
    /// Implementation of Dijkstra's Shortest Path Algorithm - works in forward (e.g. a->e) and reverse (e.g. e->a) modes
    /// NMCG - 6/12/15
    /// </summary>
    public class PathFindingEngine
    {
        private string id;

        private List<Node> nodeList;  
        private List<Edge> edgeList;
        private List<Node> originalNodeList;

        //a dictionary to associate a friendly symbolic name (e.g. "a") with the node data
        private Dictionary<string, Node> nodeDictionary = new Dictionary<string, Node>();
        //records shortest path to each node in the network from a given start node
        //the previous node is if im at "e" then the previous and shortest path back from "e" is "d" or "c" or whatever it may be
        private Dictionary<string, Node> previousNodeDictionary = new Dictionary<string, Node>();
        //records lowest cost to each node in the network from a given start node
        private Dictionary<string, double> costDictionary = new Dictionary<string, double>();

        public PathFindingEngine(string id)
        {
            //unique id for the engine in case we make more than one for separate node networks (e.g. air and ground networks)
            this.id = id;

            //set up the network of vertices (nodes) and edges (paths)
            this.nodeList = new List<Node>();
            this.edgeList = new List<Edge>();
            this.originalNodeList = new List<Node>();

            this.costDictionary = new Dictionary<string, double>();
            this.previousNodeDictionary = new Dictionary<string, Node>();

        }



        /// <summary>
        /// Used to set or reset the process of pathfinding
        /// </summary>
        public void Initialise()
        {
            //if there was something in the nodeList this means that we've already run the code - which means we need to reset the state of the engine
            if (originalNodeList.Count == 0)
                originalNodeList.Clear();

            //set up the node list with all nodes by populating from the dictionary
            if (nodeList.Count == 0)
            {
                foreach (Node node in nodeDictionary.Values)
                {
                    nodeList.Add(node);
                }
            }
            foreach (Node node in nodeList)
            {
                costDictionary[node.ID] =  double.MaxValue;
                previousNodeDictionary[node.ID] = null;
                originalNodeList.Add(node);
            }
        }

        public string findNearestNode(Vector2 position)
        {
            Node node = null, nearestNode = null;
            float minDistance = float.MaxValue;
            int count = nodeList.Count;
            for (int i = 0; i < count; i++)
            {
                node = nodeList[i];
                float distance = Vector2.Distance(node.Position, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestNode = node;
                }
            }
            return nearestNode.ID;
        }


        /// <summary>
        /// Add nodes that will be connected by edges - give each node a name and position
        /// </summary>
        /// <param name="node"></param>
        public void addNode(Node node)
        {
            nodeDictionary.Add(node.ID, node);
        }

        /// <summary>
        /// Removes a node based on a name - you must call reset() after you remove a node
        /// </summary>
        /// <param name="nodeName"></param>
        public void removeNode(string nodeName)
        {
            nodeDictionary.Remove(nodeName);
        }

        /// <summary>
        /// Add edges (or paths) to connect nodes
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <param name="cost"></param>
        public void addEdge(string startNodeID, string endNodeID, float cost)
        {
            edgeList.Add(new Edge(startNodeID + endNodeID, nodeDictionary[startNodeID], nodeDictionary[endNodeID], cost));
        }

        /// <summary>
        /// Removes a specified edge from the list by edge handle (e.g. when a path is no longer available)
        /// </summary>
        /// <param name="edge"></param>
        public void removeEdge(Edge edge)
        {
            edgeList.Remove(edge);
        }

        /// <summary>
        /// Removes a specified edge from the list by edge id (e.g. when a path is no longer available)
        /// </summary>
        /// <param name="ID"></param>
        public void removeEdge(string ID)
        {
            foreach(Edge edge in edgeList)
            {
                if(edge.ID.Equals(ID))
                {
                    edgeList.Remove(edge);
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the position of a node by passing the node name
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public Vector2 getNodePosition(string nodeName)
        {
            return nodeDictionary[nodeName].Position;
        }

        /// <summary>
        /// Specify start node for the algorithm
        /// </summary>
        /// <param name="nodeName"></param>
        public void setStartNode(string nodeName)
        {
            costDictionary[nodeName] = 0;
            double totalCost = 0;

            while (originalNodeList.Count > 0)
            {
                Node node = getNodeSmallestCost();
                if (node != null)
                {
                    foreach (Node connectedNode in getConnectedNodes(node))
                    {
                        //what is the cost to arrive at this node
                        totalCost = costDictionary[node.ID] + getCostBetweenNodes(node, connectedNode);
                        //is it smaller than any cost already assigned to the connected node?
                        if (totalCost < costDictionary[connectedNode.ID])
                        {
                            //if so then replace this lower cost to travel to the connected node
                            costDictionary[connectedNode.ID] = totalCost;
                            //and new connection as node -> connectedNode (e.g. b->e is replaced by c->e because distance travelled from start node through b->e is greater than  from start node through c->e)
                            previousNodeDictionary[connectedNode.ID] = node;
                        }
                    }
                    originalNodeList.Remove(node);
                }
                else
                {
                    //if no node data then end
                    originalNodeList.Clear(); 
                }
            }
        }

        /// <summary>
        /// Retrieves the path from the nodeName specified to the node specified by setStartNode()
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public List<Node> getPath(string nodeName)
        {
            List<Node> path = new List<Node>();
            //nodes to record path
            Node node = nodeDictionary[nodeName];
            //add this node to the top of the list
            path.Insert(0, node);

            //until we reach the end of the chain
            while (previousNodeDictionary[node.ID] != null)
            {
                //iterate backwards through the dictionary of connections
                node = previousNodeDictionary[node.ID];
                //add each new back-traced node to the list
                path.Insert(0, node);
            }
            //return the path from start to finish
            return path;
        }

        /// <summary>
        /// Gets the node with the shortest distance in the cost dictionary
        /// </summary>
        /// <returns></returns>
        public Node getNodeSmallestCost()
        {
            double distance = double.MaxValue;
            Node smallest = null;

            foreach (Node node in originalNodeList)
            {
                if (costDictionary[node.ID] < distance)
                {
                    distance = costDictionary[node.ID];
                    smallest = node;
                }
            }
            return smallest;
        }

        /// <summary>
        /// Gets the distance * cost (usually 1) value to move to from start to end node (assumes they are connected by an edge)
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public double getCostBetweenNodes(Node startNode, Node endNode)
        {
            foreach (Edge edge in edgeList)
            {
                if (edge.Start.Equals(startNode) && edge.End.Equals(endNode))
                {
                    return edge.COST;
                }
            }
            return 0;
        }

        /// <summary>
        /// Gets a list of all nodes connected to a specfied node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<Node> getConnectedNodes(Node node)
        {
            List<Node> connectedNodeList = new List<Node>();
            foreach (Edge edge in edgeList)
            {
                if (edge.Start.Equals(node) && originalNodeList.Contains(node))
                {
                    connectedNodeList.Add(edge.End);
                }
            }
            return connectedNodeList;
        }

        
        /// <summary>
        /// Prints list of path nodes
        /// </summary>
        /// <param name="nodeList"></param>
        public static void printPath(List<Node> nodeList)
        {
            System.Diagnostics.Debug.WriteLine("\n----- PATH -----\n");
            foreach (Node node in nodeList)
            {
                System.Diagnostics.Debug.Write(node.ID + "->");
            }
            System.Diagnostics.Debug.Write("\n\n");
        }

        /// <summary>
        /// Called to calculate a path between two nodes
        /// </summary>
        /// <param name="nodeStart"></param>
        /// <param name="nodeEnd"></param>
        /// <returns></returns>
        public List<Node> ExecutePathFind(string nodeStart, string nodeEnd)
        {
            Initialise();
            setStartNode(nodeStart);
            List<Node> nodeList = getPath(nodeEnd);

            //if user looks for path in the reverse mode (e.g. e->a instead of a->e we swap the start and end nodes, run the code, then reverse the result)
            if (nodeList.Count == 1)
            {
                Initialise();
                //set search in forward mode from a->e
                setStartNode(nodeEnd);
                nodeList = getPath(nodeStart);
                //then reverse result to give path from e->a
                nodeList.Reverse();
            }

            return nodeList;
        }
 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject field;
    public GameObject road;
    public GameObject spawner;
    public GameObject despawner;
    public GameObject uI;
    public GameObject towerButton;

    public AvailableBuildings[] availableBuildings;

    [System.Serializable]
    public struct AvailableBuildings
    {
        public GameObject building;
        public string hotkey;
    }

    public List<List<Transform>> routes;

    public struct Cell
    {
        public Transform transform;
        public bool isRoad;
        public bool isSpawn;
        public bool isDespawn;
    }

    public struct Node
    {
        public Cell cell;
        public List<Cell> neighbors;
        public int distance;
    }

    void Start()
    {
        string[] gridPattern = LoadGridPattern(1);
        Cell[,] cellMatrix = BuildLevel(gridPattern);
        List<Node> graph = BuildGraph(cellMatrix);
        routes = FindRoutes(graph);
        InitializeUi();
    }

    // Import level from resources. Input parameter requires level number
    string[] LoadGridPattern(int i)
    {
        TextAsset tmpText = Resources.Load<TextAsset>("Level" + i); //Import level matrix from .txt file
        string tmpGrid = tmpText.text.Replace(Environment.NewLine, ";"); //Replace all line break characters with ';' character
        if (tmpGrid != null)
            return tmpGrid.Split(';'); //Return array with level matrix
        else
        {
            Debug.Log("Level not found");
            return null;
        }
    }

    // Instantiate field and road cells on scene. Return result as a matrix & amount of despawners
    Cell[,] BuildLevel(string[] gridPattern)
    {
        float xPosition = 0f;
        float zPosition = 0f;
        float xStartPosition = xPosition; //Set up start position
        int stringLength = gridPattern[0].Length;
        int stringAmount = gridPattern.Length;
        Cell[,] matrix = new Cell[stringLength, stringAmount];
        Vector3 position;

        for (int i = 0; i < stringLength; i++)
        {
            for (int j = 0; j < stringAmount; j++)
            {
                position = new Vector3(xPosition, 0, zPosition);
                switch (gridPattern[i][j])
                {
                    case '0':
                        Instantiate(field, position, Quaternion.identity, gameObject.transform);
                        break;
                    case '1':
                        matrix[i, j].transform = Instantiate(road, position, Quaternion.identity, gameObject.transform).transform;
                        matrix[i, j].isRoad = true;
                        break;
                    case 'I':
                        matrix[i, j].transform = Instantiate(spawner, position, Quaternion.identity, gameObject.transform).transform;
                        matrix[i, j].isSpawn = true;
                        matrix[i, j].isRoad = true;
                        break;
                    case 'O':
                        matrix[i, j].transform = Instantiate(despawner, position, Quaternion.identity, gameObject.transform).transform;
                        matrix[i, j].isDespawn = true;
                        matrix[i, j].isRoad = true;
                        break;
                }
                xPosition += 5f;
            }
            zPosition -= 5f;
            xPosition = xStartPosition;
        }
        return matrix;
    }

    //Use cell matrix to build road graph
    List<Node> BuildGraph(Cell[,] matrix)
    {
        List<Node> nodes = new List<Node>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j].isRoad)
                {
                    //Look for cells adjacent to the selected
                    List<Cell> cellNeighbors = new List<Cell>();
                    if (i > 0 && matrix[i - 1, j].isRoad)
                        cellNeighbors.Add(matrix[i - 1, j]);
                    if (j < matrix.GetLength(1) - 1 && matrix[i, j + 1].isRoad)
                        cellNeighbors.Add(matrix[i, j + 1]);
                    if (i < matrix.GetLength(0) - 1 && matrix[i + 1, j].isRoad)
                        cellNeighbors.Add(matrix[i + 1, j]);
                    if (j > 0 && matrix[i, j - 1].isRoad)
                        cellNeighbors.Add(matrix[i, j - 1]);
                    nodes.Add(new Node() { cell = matrix[i, j], neighbors = cellNeighbors });
                }
            }
        }
        return nodes;
    }

    //Return List with routes for enemy
    List<List<Transform>> FindRoutes(List<Node> graph) //not ready
    {
        List<List<Transform>> routes = new List<List<Transform>>();

        //Search despawners
        for (int i = 0; i < graph.Count; i++)
            if (graph[i].cell.isDespawn)
            {
                List<Node> distancesGraph = buildDistancesGraph(i); // Return graph with nodes that have a distances
                List<Transform> startRoute = new List<Transform>();
                fillingRoutes(distancesGraph, startRoute, distancesGraph[0]);
            }
        return routes;

        //Build distances graph
        List<Node> buildDistancesGraph(int despawnIndex)
        {
            List<Node> dGraph = new List<Node>(); // new distances graph 
            int[] distances = new int[graph.Count];
            Queue<int> queue = new Queue<int>();
            distances[despawnIndex] = 1; //1 despawner start distance
            queue.Enqueue(despawnIndex);
            dGraph.Add(new Node { cell = graph[despawnIndex].cell, neighbors = graph[despawnIndex].neighbors, distance = distances[despawnIndex] });
            while (queue.Count > 0)
            {
                int firstIndex = queue.Dequeue();
                //Search neighbors of cell with first index in queue
                for (int i = 0; i < graph[firstIndex].neighbors.Count; i++)
                {
                    for (int j = 0; j < graph.Count; j++)
                    {
                        if (graph[firstIndex].neighbors[i].transform == graph[j].cell.transform && distances[j] == 0)
                        {
                            queue.Enqueue(j);
                            distances[j] = distances[firstIndex] + 1;
                            dGraph.Add(new Node { cell = graph[j].cell, neighbors = graph[j].neighbors, distance = distances[j] });
                        }
                    }
                }
            }
            return dGraph;
        }

        //Fill posible routes in Routes List
        void fillingRoutes(List<Node> dGraph, List<Transform> prevRoute, Node startNode)
        {
            Node currentNode = startNode;
            List<Transform> route = new List<Transform>();
            bool hasNeighbors = true;
            route.AddRange(prevRoute);
            while (hasNeighbors)
            {
                bool oneBrunch = true;
                Node nextNode = currentNode;
                for (int i = 0; i < currentNode.neighbors.Count; i++) //Finding neighbors
                {
                    for (int j = 0; j < dGraph.Count; j++) //Finding neighbor node
                    {
                        //Add Transform to route if his distance more then current node
                        if (currentNode.neighbors[i].transform == dGraph[j].cell.transform && currentNode.distance < dGraph[j].distance)
                        {
                            if (oneBrunch)
                            {
                                route.Add(currentNode.cell.transform);
                                nextNode = dGraph[j];
                                oneBrunch = false;
                            }
                            else
                            {
                                fillingRoutes(dGraph, route, dGraph[j]); //Creates a route that will lead the other way
                            }

                        }
                    }
                }
                if (currentNode.cell.transform == nextNode.cell.transform)
                    hasNeighbors = false;
                currentNode = nextNode;
            }

            if (currentNode.cell.isSpawn)
            {
                route.Add(currentNode.cell.transform);
                routes.Add(route);
            }
                
        }
    }

    private void InitializeUi()
    {
        Transform towerSelector = uI.transform.Find("Canvas/TowerSelector");

        for (int i = 0; i < availableBuildings.Length; i++)
        {
            if (availableBuildings[i].building != null && availableBuildings[i].hotkey != null)
            {
                GameObject button = Instantiate(towerButton);
                button.transform.SetParent(towerSelector, false);
                button.transform.localPosition = new Vector3(0, 0, 0);

                Text name = button.transform.Find("Button/Name").GetComponent(typeof(Text)) as Text;
                name.text = availableBuildings[i].building.name;

                Text hotkey = button.transform.Find("Button/Hotkey").GetComponent(typeof(Text)) as Text;
                hotkey.text = availableBuildings[i].hotkey;

                GameObject building = Instantiate(availableBuildings[i].building);
                building.transform.SetParent(button.transform.Find("TowerSpot").transform, false);
                building.transform.localScale = Vector3.one;
                ChangeChildrenLayers(building.transform, "UI");
            }
            else
                Debug.LogError("Property in Available Buildings is empty. Index: " + i);
        }

        void ChangeChildrenLayers(Transform current, string layerName)
        {
            current.gameObject.layer = LayerMask.NameToLayer(layerName);

            foreach(Transform child in current)
            {
                ChangeChildrenLayers(child, layerName);
            }
        }

    }
}
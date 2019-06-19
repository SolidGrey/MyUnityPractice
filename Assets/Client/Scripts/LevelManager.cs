using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public GameObject map;
    public GameObject field;
    public GameObject road;
    public GameObject spawner;
    public GameObject despawner;

    public struct Cell
    {
        public Transform transform;
        public bool isRoad;
        public bool isSpawn;
        public bool isDespawn;
    }

    public struct NNode
    {
        public Cell cell;
        public List<Cell> neighbors;
    }

    public struct DNode
    {
        public Cell cell;
        public int distance;
    }

    void Start()
    {
        string[] gridPattern = LoadGridPattern(1);
        Cell[,] cellMatrix = BuildLevel(gridPattern);
        List<NNode> graph = BuildGraph(cellMatrix);
        FindRoutes(graph);
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
                        Instantiate(field, position, Quaternion.identity, map.transform);
                        break;
                    case '1':
                        matrix[i,j].transform = Instantiate(road, position, Quaternion.identity, map.transform).transform;
                        matrix[i,j].isRoad = true;
                        break;
                    case 'I':
                        matrix[i,j].transform = Instantiate(spawner, position, Quaternion.identity, map.transform).transform;
                        matrix[i,j].isSpawn = true;
                        matrix[i,j].isRoad = true;
                        break;
                    case 'O':
                        matrix[i,j].transform = Instantiate(despawner, position, Quaternion.identity, map.transform).transform;
                        matrix[i,j].isDespawn = true;
                        matrix[i,j].isRoad = true;
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
    List<NNode> BuildGraph (Cell[,] matrix)
    {
        List<NNode> nodes = new List<NNode>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if(matrix[i,j].isRoad)
                {
                    //Look for cells adjacent to the selected
                    List<Cell> cellNeighbors = new List<Cell>();
                    if (i > 0 && matrix[i-1,j].isRoad)
                        cellNeighbors.Add(matrix[i-1,j]);
                    if (j < matrix.GetLength(1)-1 && matrix[i,j+1].isRoad)
                        cellNeighbors.Add(matrix[i,j+1]);
                    if (i < matrix.GetLength(0)-1 && matrix[i+1,j].isRoad)
                        cellNeighbors.Add(matrix[i+1,j]);
                    if (j > 0 && matrix[i,j-1].isRoad)
                        cellNeighbors.Add(matrix[i,j-1]);
                    nodes.Add(new NNode() { cell = matrix[i,j], neighbors = cellNeighbors });
                }
            }
        }
        return nodes;
    }

    //Return List with routes for enemy
    List<List<Cell>> FindRoutes(List<NNode> nGraph) //not ready
    {
        List<List<Cell>> routes = new List<List<Cell>>();

        //Search despawners and build distances graphs
        List<List<DNode>> dGraphs = new List<List<DNode>>();
        for (int i = 0; i < nGraph.Count; i++)
            if (nGraph[i].cell.isDespawn)
                dGraphs.Add(buildDistancesGraph(i));
        return null;

        //Build distances graph. Use nodes graph
        List<DNode> buildDistancesGraph(int despawnIndex)
        {
            List<DNode> dGraph = new List<DNode>();
            int[] distances = new int[nGraph.Count];
            Queue<int> queue = new Queue<int>();
            distances[despawnIndex] = 1; //1 despawner start distance
            queue.Enqueue(despawnIndex);
            dGraph.Add(new DNode { cell = nGraph[despawnIndex].cell, distance = distances[despawnIndex] });
            while (queue.Count > 0)
            {
                int firstIndex = queue.Dequeue();
                //Search neighbors of cell with first index in queue
                for (int i = 0; i < nGraph[firstIndex].neighbors.Count; i++)
                {
                    for (int j = 0; j < nGraph.Count; j++)
                    {
                        if (nGraph[firstIndex].neighbors[i].transform == nGraph[j].cell.transform && distances[j] == 0)
                        {
                            queue.Enqueue(j);
                            distances[j] = distances[firstIndex] + 1;
                            dGraph.Add(new DNode { cell = nGraph[j].cell, distance = distances[j] });
                        }
                    }
                }
            }
            return dGraph;
        }

    }
    
}
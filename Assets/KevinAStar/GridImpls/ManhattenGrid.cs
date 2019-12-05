﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhattenGrid : MonoBehaviour, IGrid {

    public GameObject tilePrefab;
    public Vector2 gridWorldSize;
    public Node[,] grid;
    public int gridSizeX, gridSizeY;

    void Start(){
        grid = new Node[gridSizeX, gridSizeY];
        Node[] nodes = transform.GetComponentsInChildren<Node>();
        Debug.Log("Grid size: " + grid.Length +" - Nodes size: " + nodes.Length);
        for(int i = 0; i < nodes.Length; i++){
            grid[nodes[i].gridX, nodes[i].gridY] = nodes[i];
        }
    }

    public void Create(int xSize, int ySize){
        Reset();
        gridSizeX = Mathf.RoundToInt(xSize);
        gridSizeY = Mathf.RoundToInt(ySize);
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++){
            for (int y = 0; y < gridSizeY; y++){
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0, y), tilePrefab.transform.rotation);
                Node node = tile.AddComponent<Node>();
                node.walkable = true;
                node.gridX = x;
                node.gridY = y;
                grid[x, y] = node;
                tile.gameObject.transform.parent = this.gameObject.transform;
                tile.gameObject.name = "Tile " + x + "," + y;
            }
        }
    }

    public void Clear(){
        for (int x = 0; x < gridSizeX; x++){
            for (int y = 0; y < gridSizeY; y++){
                if (grid[x, y].walkable){
					//zero all calculated values
					grid[x,y].gCost = 0;
					grid[x,y].hCost = 0;
                    grid[x,y].parent = null;
                }
            }
        }
    }

    public Node[,] GetGrid(){
        return grid;
    }

    public Node GetNodeFromGrid(int x, int y){
        return grid[x,y];
    }

    void Reset(){
        for (int x = 0; x < gridSizeX; x++){
            for (int y = 0; y < gridSizeY; y++){
                Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }
        gridSizeX = 0;
        gridSizeY = 0;
        grid = null;
    }

    public List<Node> GetNeighbors(Node node) {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if ((x == 0 && y == 0) /*self*/ || (Mathf.Abs(x) == Mathf.Abs(y)) /*diagonal*/ ) { //Self or obstacle
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if ((checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) && grid[checkX,checkY].walkable ){
					Node neighbor = grid[checkX,checkY];
                    if(neighbor.name == "Tile 0,0"){
                            Debug.Log("-------ARG!!!!!!!!!!!-------");
                        }
					// neighbor.parent = node;
					neighbor.gCost = node.gCost + GetDistanceBetween(node, neighbor);
					neighbors.Add(neighbor);
                }
            }
        }
        return neighbors;
    }

	public int GetHCost(Node node, Node target){
		return ManhattenDistance(node,target);
	}
	public int GetDistanceBetween(Node current, Node neighbor){
		return 10;
	}

    private int ManhattenDistance(Node node, Node target) {
        int D = 10;
        int dX = Mathf.Abs(node.gridX - target.gridX);
        int dY = Mathf.Abs(node.gridY - target.gridY);
        return D * (dX + dY);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGrid : MonoBehaviour, IGrid {

	public GameObject tilePrefab;
    public Vector2 gridWorldSize;
    Node[,] grid;
    Node[] gridList;
    public int gridSizeX, gridSizeY;
	//Remove above once ready ^^^^^
	Dictionary<string, Node> graphMap;

    void Start(){
        gridList = GameObject.FindObjectsOfType<Node>();
        
    }
    // void Start(){
    //     grid = new Node[gridSizeX, gridSizeY];
    //     Node[] nodes = transform.GetComponentsInChildren<Node>();
    //     Debug.Log("Grid size: " + grid.Length +" - Nodes size: " + nodes.Length);
    //     for(int i = 0; i < nodes.Length; i++){
    //         grid[nodes[i].gridX, nodes[i].gridY] = nodes[i];
    //     }
    // }


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

    public void Clear()
    {
        for(int i = 0; i < gridList.Length; i++){
            gridList[i].parent = null;
            gridList[i].gCost = 0;
            gridList[i].hCost = 0;
        }
    }

    public Node[,] GetGrid(){
        return grid;
    }

    public Node GetNodeFromGrid(int x, int y){
        return grid[x,y];
    }

    void Reset()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }
        gridSizeX = 0;
        gridSizeY = 0;
        grid = null;
    }
    public List<Node> GetNeighbors(Node node) {
        return node.GetComponent<GraphNeighbors>().GetNeighborNodes();
    }

	public int GetHCost(Node node, Node target){
		return EuclidieanDistance(node,target);
	}
	public int GetDistanceBetween(Node current, Node neighbor){
		return Mathf.RoundToInt(Vector3.Distance(current.transform.position, neighbor.transform.position)); 
	}

    private int EuclidieanDistance(Node node, Node target){
        int dx = (target.gridX - node.gridX)^2;
        int dy = (target.gridY - node.gridY)^2;
        return Mathf.RoundToInt(Mathf.Sqrt((dx+dy)));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {

    public Map[] maps;
    public int mapIndex;

    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Transform navmeshMaskPrefab;
    public Transform navmeshFloor;
    public Vector3 maxMapSize;
    [Range(0,1)]
    public float outlinePercent;
    public float tileSize = 1;
    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;

    Map currentMap;

    private void Start() {
        GenerateMap();
        }

    public void GenerateMap() {

        currentMap = maps[mapIndex];
        System.Random prng = new System.Random(currentMap.seed);

        allTileCoords = new List<Coord>(); // Makes a list of the Coords for each tile
        for(int x = 0; x < currentMap.mapSize.x; x++) {
            for(int y = 0; y < currentMap.mapSize.y; y++) {
                allTileCoords.Add(new Coord(x, y)); // Adds all the Coords for the tiles to the list
                }
            }
        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray (allTileCoords.ToArray(), currentMap.seed)); // Shuffle, probably not necessary

        string holderName = "Generated Map"; // Creates parent object
        if(transform.Find(holderName)) {
            DestroyImmediate(transform.Find(holderName).gameObject); // Destroys all objects if updated
            }
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for(int x = 0; x < currentMap.mapSize.x; x++) {
            for(int y = 0; y < currentMap.mapSize.y; y++) {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform; // Instantiates tile on position tilePosition
                newTile.localScale = Vector3.one * (1 - outlinePercent) *  tileSize; // Makes tiles scale properly
                newTile.parent = mapHolder;
                }
            }
        // Generating obstacles, modify later for your own needs

        bool[,] obstacleMap = new bool[(int)currentMap.mapSize.x,(int)currentMap.mapSize.y];
        int currentObstacleCount = 0;

        int obstacleCount = (int)(currentMap.mapSize.x*currentMap.mapSize.y * currentMap.obstaclePercent); 
        for(int i = 0; i< obstacleCount; i++) {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;
            if(randomCoord != currentMap.MapCenter && MapIsFullyAccessible(obstacleMap, currentObstacleCount)) {
                float obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, (float)prng.NextDouble());
                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);

                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * (obstacleHeight/2 + 0.72f), Quaternion.identity) as Transform;
                newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight, (1 - outlinePercent) * tileSize) ;
                newObstacle.parent = mapHolder;
                }
            else {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
                }
            }

        Transform maskLeft = Instantiate(navmeshMaskPrefab, Vector3.left * (currentMap.mapSize.x + maxMapSize.x) / 4 * tileSize, Quaternion.identity) as Transform;
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2, 1, currentMap.mapSize.y) * tileSize;

        Transform maskRight = Instantiate(navmeshMaskPrefab, Vector3.right * (currentMap.mapSize.x + maxMapSize.x) / 4 * tileSize, Quaternion.identity) as Transform;
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2, 1, currentMap.mapSize.y) * tileSize;

        Transform maskTop = Instantiate(navmeshMaskPrefab, Vector3.forward * (currentMap.mapSize.y + maxMapSize.y) / 4 * tileSize, Quaternion.identity) as Transform;
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y)/2) * tileSize;

        Transform maskBottom = Instantiate(navmeshMaskPrefab, Vector3.back * (currentMap.mapSize.y + maxMapSize.y) / 4 * tileSize, Quaternion.identity) as Transform;
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2) * tileSize;

        navmeshFloor.localScale = new Vector3(maxMapSize.x, maxMapSize.y) * tileSize;
        }


    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount) { // Makes sure that all tiles that aren't obstacles are connected
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)]; 
        Queue<Coord> queue = new Queue<Coord>(); // Temporary queue to check neighbours with
        queue.Enqueue(currentMap.MapCenter); // adds origin point to queue
        mapFlags[currentMap.MapCenter.x, currentMap.MapCenter.y] = true; // Makes sure nothing spawns on the origin point

        int accessibleTileCount = 1; // Counter

        while(queue.Count > 0) {
            Coord tile = queue.Dequeue(); // Removes whatever is in the queue
            for(int x = -1; x <= 1; x++) { //Checks the tile behind and in front of itself on the x axis
                for(int y = -1; y <= 1; y++) { //Checks the tile behind and in front of itself on the y axis
                    int neighbourX = tile.x + x; //Sets neighbour x to the value it has relative to itself
                    int neighbourY = tile.y + y;//Sets neighbour y to the value it has relative to itself
                    if(x == 0 || y == 0) { // Makes sure it doesn't go outside of the tile grid
                        if(neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1)) { //Checks it's inside of the obstaclemap
                            if(!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY]) { //If we haven't checked this tile && isn't an obstacletile
                                mapFlags[neighbourX, neighbourY] = true; // We've checked this tile
                                queue.Enqueue(new Coord(neighbourX, neighbourY)); // Adds tile to queue
                                accessibleTileCount++;
                                }
                            }
                        }
                    }
                }
            }
        int targetAccesssibleTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - currentObstacleCount);
        return targetAccesssibleTileCount == accessibleTileCount;
        }
    //** Generating obstacles
    Vector3 CoordToPosition(int x, int y) { 
        return new Vector3(-currentMap.mapSize.x / 2 + 0.5f + x, 0, -currentMap.mapSize.y / 2 + 0.5f + y) * tileSize  /* (1-outlinePercent)*/;
        }

    public Coord GetRandomCoord() {
        Coord randomCoord = shuffledTileCoords.Dequeue(); //Gets value of the last Coord in the queue
        shuffledTileCoords.Enqueue(randomCoord); // Adds Coord back to queue
        return randomCoord; 
        }
    [System.Serializable]
    public struct Coord {
        public int x;
        public int y;

        public Coord(int _x, int _y) {
            x = _x;
            y = _y;
            }
        public static bool operator == (Coord c1, Coord c2) {
            return c1.x == c2.x && c1.y == c2.y;
            }
        public static bool operator !=(Coord c1, Coord c2) {
            return !(c1 == c2);
            }
        }
    [System.Serializable]
    public class Map {

        public Coord mapSize;
        [Range(0,1)]
        public float obstaclePercent;
        public int seed;
        public float minObstacleHeight;
        public float maxObstacleHeight;
        public Color foregroundColor;
        public Color backgroundColor;

        public Coord MapCenter {
            get { return new Coord(mapSize.x / 2, mapSize.y / 2); }
            }
        }
    }


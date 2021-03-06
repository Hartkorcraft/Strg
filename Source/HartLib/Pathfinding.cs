using static HartLib.Utils;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HartLib
{

    public class PathFinding<TBlock>
    {
        Vector2i gridSize;
        PathFindingCell[,] grid;
        Func<Vector2i, HashSet<TBlock>, bool> checkBlocking;


        public List<PathFindingCell> FindPath(Vector2i startPos, Vector2i endPos, HashSet<TBlock> blockingTiles, bool include_checking_last = true, bool diagonals = false, bool big = false)
        {
            int safeCheck = 700;

            if (startPos.x < 0 || startPos.x > gridSize.x - 1) return null;
            if (startPos.y < 0 || startPos.y > gridSize.y - 1) return null;
            if (endPos.x < 0 || endPos.x > gridSize.x - 1) return null;
            if (endPos.y < 0 || endPos.y > gridSize.y - 1) return null;

            //if (blockingTiles is null) return null;
            if (big)
            {
                var adjacentEndPositions = new HashSet<Vector2i>() { new Vector2i(endPos + Vector2i.Right), new Vector2i(endPos + Vector2i.Up), new Vector2i(endPos + Vector2i.Up + Vector2i.Right) };

                foreach (var pos in adjacentEndPositions)
                {
                    if (CheckIfInRange(pos, gridSize) is false || grid[pos.x, pos.y] is null || grid[pos.x, pos.y].CheckForTiles(blockingTiles, checkBlocking))
                    {
                        return null;
                    }
                }
            }


            PathFindingCell startCell = grid[startPos.x, startPos.y];
            PathFindingCell endCell = grid[endPos.x, endPos.y];

            List<PathFindingCell> openSet = new List<PathFindingCell>();
            HashSet<PathFindingCell> closedSet = new HashSet<PathFindingCell>();
            openSet.Add(startCell);

            while (openSet.Count > 0 && safeCheck > 0)
            {
                safeCheck--;

                if (safeCheck <= 0) GD.Print("SafetyCheck");

                PathFindingCell currentCell = openSet[0];

                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < currentCell.FCost || openSet[i].FCost == currentCell.FCost && openSet[i].HCost < currentCell.HCost)
                    {
                        currentCell = openSet[i];
                    }
                }
                openSet.Remove(currentCell);
                closedSet.Add(currentCell);

                if (currentCell == endCell)
                {
                    if (include_checking_last && currentCell.CheckForTiles(blockingTiles, checkBlocking))
                    {
                        return null;
                    }
                    List<PathFindingCell> path = RetracePath(startCell, endCell);
                    return path;
                }

                List<PathFindingCell> neigbours;

                if (diagonals && big is false) { neigbours = GetNeigboursDiagonal(currentCell); }
                else { neigbours = GetNeigbours(currentCell); }

                for (int i = 0; i < neigbours.Count; i++)
                {
                    if (closedSet.Contains(neigbours[i]) || (neigbours[i].CheckForTiles(blockingTiles, checkBlocking) && neigbours[i] != endCell))
                    {
                        continue;
                    }

                    bool adjacentCheck = true;
                    var neigbhourPos = neigbours[i].GridPos;

                    if (big && neigbhourPos != endPos)
                    {
                        var direction = (Dir)i;


                        if (neigbhourPos == endPos) GD.Print("XDDDD");
                        List<Vector2i> adjacent = new List<Vector2i>() { };

                        switch (direction)
                        {
                            case Dir.Up:
                                adjacent.Add(new Vector2i(neigbhourPos + Vector2i.Up));
                                adjacent.Add(new Vector2i(neigbhourPos + Vector2i.Up + Vector2i.Right));
                                break;
                            case Dir.Right:
                                adjacent.Add(new Vector2i(neigbhourPos + Vector2i.Right));
                                adjacent.Add(new Vector2i(neigbhourPos + Vector2i.Right + Vector2i.Up));
                                break;
                            case Dir.Down:
                                adjacent.Add(new Vector2i(neigbhourPos + Vector2i.Right));
                                break;
                            case Dir.Left:
                                adjacent.Add(new Vector2i(neigbhourPos + Vector2i.Up));
                                break;
                        }

                        foreach (var pos in adjacent)
                        {
                            if (CheckIfInRange(pos, gridSize) is false || grid[pos.x, pos.y] is null)
                            {
                                adjacentCheck = false;
                                break;
                            }


                            var adjacentNeigbour = grid[pos.x, pos.y];

                            if (closedSet.Contains(adjacentNeigbour) || (adjacentNeigbour.CheckForTiles(blockingTiles, checkBlocking) && adjacentNeigbour != endCell))
                            {
                                adjacentCheck = false;
                                break;
                            }
                            if (adjacentCheck is false) break;
                        }
                    }

                    if (adjacentCheck is false) continue;

                    int newCostToNeighbour = currentCell.GCost + GetDistance(currentCell, neigbours[i]);
                    if (newCostToNeighbour < neigbours[i].GCost || openSet.Contains(neigbours[i]) is false)
                    {
                        neigbours[i].GCost = newCostToNeighbour;
                        neigbours[i].HCost = GetDistance(neigbours[i], endCell);
                        neigbours[i].Parent = currentCell;

                        if (openSet.Contains(neigbours[i]) == false)
                        {
                            openSet.Add(neigbours[i]);
                        }
                    }
                }

            }
            return null;
        }

        List<PathFindingCell> GetNeigbours(PathFindingCell cell)
        {
            List<PathFindingCell> neigbours = new List<PathFindingCell>();

            var dirs = Enum.GetValues(typeof(Dir));
            foreach (Dir direction in dirs)
            {
                var neigbhourPos = cell.GridPos + directions[direction];
                if (CheckIfInRange(neigbhourPos, gridSize) && grid[neigbhourPos.x, neigbhourPos.y] != null)
                {
                    neigbours.Add(grid[neigbhourPos.x, neigbhourPos.y]);
                }
            }
            return neigbours;
        }

        List<PathFindingCell> GetNeigboursDiagonal(PathFindingCell cell) //TODO Going up doesn't work 
        {
            List<PathFindingCell> neigbours = new List<PathFindingCell>();

            var dirs = Enum.GetValues(typeof(DirDiagonal));
            foreach (DirDiagonal direction in dirs)
            {
                var neigbhourPos = cell.GridPos + directionsDiagonals[direction];
                if (CheckIfInRange(neigbhourPos, gridSize) && grid[neigbhourPos.x, neigbhourPos.y] != null)
                {
                    neigbours.Add(grid[neigbhourPos.x, neigbhourPos.y]);
                }
            }
            return neigbours;
        }

        List<PathFindingCell> RetracePath(PathFindingCell startNode, PathFindingCell endNode)
        {
            List<PathFindingCell> path = new List<PathFindingCell>();
            PathFindingCell curCell = endNode;

            while (curCell != startNode)
            {
                path.Add(curCell);
                curCell = curCell.Parent;
            }

            path.Reverse();
            return path;
        }

        int GetDistance(PathFindingCell cellA, PathFindingCell cellB)
        {
            int distantX = Mathf.Abs((int)(cellA.GridPos.x - cellB.GridPos.x));
            int distantY = Mathf.Abs((int)(cellA.GridPos.y - cellB.GridPos.y));

            if (distantX > distantY)
                return 14 * distantY + 10 * (distantX - distantY);
            else return 14 * distantX + 10 * (distantY - distantX);
        }


        public PathFinding(Vector2i _gridSize, Func<Vector2i, HashSet<TBlock>, bool> _checkBlocking)
        {
            gridSize = _gridSize;
            checkBlocking = _checkBlocking;
            // for example: Func<Vector2i, HashSet<Main.TileTypes>, bool> checkForBlocking = (pos, blocking) => { return blocking.Contains((Main.TileTypes)Main.mapTiles.GetCellv(pos.Vec2())); };

            grid = new PathFindingCell[gridSize.x, gridSize.y];

            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    grid[x, y] = new PathFindingCell(new Vector2i(x, y));
                }
            }

        }


        public class PathFindingCell
        {
            public Vector2i GridPos { get; set; }
            public PathFindingCell Parent { get; set; }

            public bool CheckForTiles(HashSet<TBlock> checkingFor, Func<Vector2i, HashSet<TBlock>, bool> checkBlocking)
            {
                if (checkingFor is null || checkingFor.Count < 1) return false;

                if (checkBlocking(GridPos, checkingFor)) { return true; }

                return false;
            }

            public int GCost { get; set; }
            public int HCost { get; set; }
            public int FCost
            {
                get { return GCost + HCost; }
            }

            public PathFindingCell(Vector2i gridPos)
            {
                this.GridPos = gridPos;
            }

        }


    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellGrid;
using System.Linq;


namespace PathFinding
{


    public class AStarPathFinding
    {
        //斜走
        const int DIGONAL_COST = 14;
        //直走
        const int STRAIGHT_COST = 10;

        protected GridContainer grid;

        public AStarPathFinding(GridContainer grid)
        {
            this.grid = grid;
        }

        public List<PathNode> FindPath(int startRow, int startCol, int endRow, int endCol)
        {

            foreach (var cache in grid.PathNodesCaches)
            {
                var cell = grid.GetCell(cache.Row, cache.Col);

                if (cell != null)
                {
                    cell.BackColor = Color.black;
                    cell.Text = cell.ToString();
                    cell.PathNode = null;
                }
            }

            grid.PathNodesCaches.Clear();

            var startNode = new PathNode(grid, startRow, startCol);

            startNode.GCost = 0;
            startNode.HCost = CalcDistanceCost(startRow, startCol, endRow, endCol);

            //带搜索的node集合
            //nodes wait for being searching
            HashSet<PathNode> openList = new HashSet<PathNode> { startNode };
            //被排除的node集合
            //nodes being excluded
            HashSet<PathNode> closeList = new HashSet<PathNode>();

            while (openList.Count > 0)
            {
                //找到Fcost最小的节点继续搜索
                //find lowerest Fcost node the use it for searching  
                PathNode currentNode = GetLowerCostNode(openList);

                //找到了目标
                //if found tagert return a path
                if (currentNode.Col == endCol && currentNode.Row == endRow)
                {
                    return TraceBackPath(currentNode);
                }

                openList.Remove(currentNode);
                closeList.Add(currentNode);


                var neighbours = GetNeighbourNodes(grid, currentNode, grid.Rows - 1, grid.Cols - 1);

                foreach (var node in neighbours)
                {
                    if (closeList.Contains(node))
                        continue;

                    if (!node.Walkable)
                    {
                        closeList.Add(node);
                        continue;
                    }

                    //从当前节点到这个节点的gcost
                    //calculate gcost from currentnode to  target neigbour node
                    int enterGost = currentNode.GCost + CalcDistanceCost(currentNode, node);

                    //如果一个节点是几个节点的的临近节点，则更新cost比较小的那个节点为这个节点的prevNode 这样cost最小的节点被链接起来
                    //在多次迭代后会产生一个cost最小的路径
                    //if a node is neighbour for multiple nodes , let this node's lowerset gcost neighbour to become prevNode of this node
                    //thus will generate a path consist of nodes that with lowerest gcost chainning together
                    if (enterGost < node.GCost)
                    {
                        node.PrevNode = currentNode;
                        node.GCost = enterGost;
                        node.HCost = CalcDistanceCost(node.Row, node.Col, endRow, endCol);
                        //如果没有被搜索过 就加入openlist搜索
                        if (!openList.Contains(node))
                        {
                            openList.Add(node);
                        }
                    }
                }

            }

            return null;
        }


        /// <summary>
        /// 找到相邻八个方向的node
        /// find nodes on octagnal directions
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<PathNode> GetNeighbourNodes(GridContainer grid, PathNode node, int maxRow, int maxCol)
        {
            List<PathNode> nodes = new List<PathNode>();

            bool exceedBottom = node.Row - 1 < 0;
            bool exceedTop = node.Row + 1 > maxRow;

            if (node.Col + 1 <= maxCol)
            {
                //正右方
                //right
                nodes.Add(GetOrCreateNode(grid, node.Row, node.Col + 1));
                //右下方
                //bottom-right
                if (!exceedBottom)
                {
                    nodes.Add(GetOrCreateNode(grid, node.Row - 1, node.Col + 1));
                }
                //右上方
                //top-right
                if (!exceedTop)
                {
                    nodes.Add(GetOrCreateNode(grid, node.Row + 1, node.Col + 1));
                }
            }

            if (node.Col - 1 >= 0)
            {
                //正左方
                //left
                nodes.Add(GetOrCreateNode(grid, node.Row, node.Col - 1));
                //左下
                //bottom-left
                if (!exceedBottom)
                {
                    nodes.Add(GetOrCreateNode(grid, node.Row - 1, node.Col - 1));
                }
                //左上
                //top-left
                if (!exceedTop)
                {

                    nodes.Add(GetOrCreateNode(grid, node.Row + 1, node.Col - 1));
                }
            }

            //正上方
            //top
            if (!exceedTop)
            {
                nodes.Add(GetOrCreateNode(grid, node.Row + 1, node.Col));
            }

            //正下方
            //bottom
            if (!exceedBottom)
            {
                nodes.Add(GetOrCreateNode(grid, node.Row - 1, node.Col));
            }

            return nodes;
        }


        public PathNode GetOrCreateNode(int row, int col, bool walkable)
        {
            return GetOrCreateNode(grid, row, col, walkable);
        }

        private PathNode GetOrCreateNode(GridContainer grid, int row, int col, bool walkable = true)
        {
            PathNode node = null;
            var cell = grid.CreateOrGetCell(row, col);

            if (cell.PathNode == null)
            {
                node = new PathNode(cell);
                node.Walkable = walkable;
            }
            else
            {
                //如果原来已经被标记为无法行走 ，则忽略
                //if node has been marked as unwalkable then ignore
                node = cell.PathNode;
            }

            if (node.Walkable)
            {
                grid.PathNodesCaches.Add(node);
            }
            else
            {
                grid.PathNodesCaches.Remove(node);
            }

            return node;
        }

        private List<PathNode> TraceBackPath(PathNode node)
        {

            List<PathNode> pathNodes = new List<PathNode> { node };

            while (node.PrevNode != null)
            {
                pathNodes.Add(node);

                node = node.PrevNode;
            }
            pathNodes.Add(node);

            return pathNodes;
        }

        private PathNode GetLowerCostNode(IEnumerable<PathNode> list)
        {
            PathNode result = list.FirstOrDefault();

            foreach (PathNode node in list)
            {
                if (node.FCost < result.FCost)
                    result = node;
            }

            return result;
        }

        private int CalcDistanceCost(PathNode a, PathNode b)
        {
            return CalcDistanceCost(a.Row, a.Col, b.Row, b.Col);
        }

        private int CalcDistanceCost(int sRow, int sCol, int eRow, int eCol)
        {
            int r = Mathf.Abs(sRow - eRow);
            int c = Mathf.Abs(sCol - eCol);
            int remainning = Mathf.Abs(r - c);

            //一种求HCost的算法
            // a algorithm get HCost (there was multiple ways)
            return DIGONAL_COST * Mathf.Min(r, c) + STRAIGHT_COST * remainning;
        }
    }
}

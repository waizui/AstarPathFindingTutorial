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

        public List<PathNode> FindPath(GridContainer grid, int startRow, int startCol, int endRow, int endCol)
        {
            var startNode = new PathNode(startRow, startCol);

            startNode.GCost = 0;
            startNode.HCost = CalcDistanceCost(startRow, startCol, endRow, endCol);

            HashSet<PathNode> openList = new HashSet<PathNode> { startNode };

            HashSet<PathNode> closeList = new HashSet<PathNode>();


            while (openList.Count > 0)
            {
                //找到Fcost最小的节点继续搜索
                PathNode currentNode = GetLowerCostNode(openList);

                //找到了目标
                if (currentNode.Col == endCol && currentNode.Row == endRow)
                {
                    return TraceBackPath(currentNode);
                }

                openList.Remove(currentNode);
                closeList.Add(currentNode);


                var neighbours = GetNeighbourNodes(currentNode, grid.Rows - 1, grid.Cols - 1);

                foreach (var node in neighbours)
                {
                    if (closeList.Contains(node))
                        continue;

                    //从当前节点到这个节点的gcost
                    int enterGost = currentNode.GCost + CalcDistanceCost(currentNode, node);

                    //如果一个节点是几个节点的的临近节点，则更新cost比较小的那个节点为这个节点的prevNode 这样cost最小的节点被链接起来
                    //在多次迭代后会产生一个cost最小的路径
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
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<PathNode> GetNeighbourNodes(PathNode node, int maxRow, int maxCol)
        {
            List<PathNode> nodes = new List<PathNode>();

            bool exceedBottom = node.Row - 1 < 0;
            bool exceedTop = node.Row + 1 > maxRow;

            if (node.Col + 1 <= maxCol)
            {
                //正右方
                nodes.Add(new PathNode(node.Row, node.Col + 1));
                //右下方
                if (!exceedBottom)
                {
                    nodes.Add(new PathNode(node.Row - 1, node.Col + 1));
                }
                //右上方
                if (!exceedTop)
                {
                    nodes.Add(new PathNode(node.Row + 1, node.Col + 1));
                }
            }

            if (node.Col - 1 >= 0)
            {
                //正左方
                nodes.Add(new PathNode(node.Row, node.Col - 1));
                //左下
                if (!exceedBottom)
                {
                    nodes.Add(new PathNode(node.Row - 1, node.Col - 1));
                }
                //左上
                if (!exceedTop)
                {

                    nodes.Add(new PathNode(node.Row + 1, node.Col - 1));
                }
            }

            //正上方
            if (!exceedTop)
            {
                nodes.Add(new PathNode(node.Row + 1, node.Col));
            }

            //正下方
            if (!exceedBottom)
            {
                nodes.Add(new PathNode(node.Row - 1, node.Col));
            }

            return nodes;
        }


        private List<PathNode> TraceBackPath(PathNode node)
        {

            List<PathNode> pathNodes = new List<PathNode> { node };

            while (node.PrevNode != null)
            {
                pathNodes.Add(node);

                node = node.PrevNode;
            }
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
            return DIGONAL_COST * Mathf.Min(r, c) + STRAIGHT_COST * remainning;
        }
    }
}

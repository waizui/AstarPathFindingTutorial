
# this repository is an implementation of 2D a\* path-finding algorithm.
#一个A\*算法实现。

<br/>演示stage2 仅打开PathFingTest这个gameobject，演示stage3 仅打开"PaithFidingCoroutine"；
<br/>only active "PathFingTest" gameobjec for demonstrate stage2,and only active "PaithFidingCoroutine" for stage3(sorry for miss spelling) 
![image](https://raw.githubusercontent.com/waizui/AstarPathFindingTutorial/master/GitResources/detail.png)

<br/>

## 阶段一 实现基本完成，图中是在一个表格系统中测试结果。
## stage 1 testing result on a grid system. 

![image](https://raw.githubusercontent.com/waizui/AstarPathFindingTutorial/master/GitResources/stage1.jpg)

<br/>

## 阶段二 标记不可行走区域
## stage 2 unwalkable area 

![image](https://raw.githubusercontent.com/waizui/AstarPathFindingTutorial/master/GitResources/stage2.gif)

![image](https://raw.githubusercontent.com/waizui/AstarPathFindingTutorial/master/GitResources/stage2.jpg)

<br/>

## 阶段三 逐步展示算法运算过程
## stage3 break algoritm into small steps

<br/>在选择目标点后，可以看到寻路过程，蓝色代表被加入到openList的区域，红色代表放入closeList的区域。
<br/>after select target , a procedure of path find will be animated, nodes be added to openlist will marked blue,nodes added to closeList will marked red.
![image](https://raw.githubusercontent.com/waizui/AstarPathFindingTutorial/master/GitResources/Stage3.gif)

<br/>
参考(reference):https://www.youtube.com/watch?v=alU04hvz6L4&list=PLoMxtzg_kS3h2mOwmKnyoJCGbyrWfJXms&index=4

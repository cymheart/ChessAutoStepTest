# ChessAutoStepTest
一个国际象棋自动游戏的测试题（简单AI）


1.所有棋子随机散布在棋盘中，黑白双方依次行动

2.每回合随机选择一枚棋子移动，棋子的移动规则需符合国际象棋的移动规则

3.在棋子的可移动范围内，优先选择消灭对方棋子，否则随机其中一个位置移动

4.100个回合后，场中棋子多者获胜，输出结果。0：和棋；1：黑方胜；2：白方胜

5.使用面向对象的设计思路

6.使用C#语言开发

7.要求结构清晰，代码精炼，可读性强

架构参考

Piece：选择位置

Board：判断相应位置对应的棋子

Player：选择棋子

GameManager：游戏开始，回合控制

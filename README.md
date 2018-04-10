# Tennis
Solutions for Tennis-Kata (see http://codingdojo.org/kata/Tennis/)

![UML Diagram](http://yuml.me/adcf849c.png)

Game is a container of two different Players playing Tennis.
Its WinBall method remembers when a Player wins a ball.
The Game's Winner property is null until the Game is finished,
i.e. one of the Players wins the game. 
Then the winner is appearing in the Winner property.

A Player represents a player playing Tennis.
His Name is for only referencing him in the program's messages.
His Score contains his points according to his actual results.

Score is an enumerate type for representing points could be gained by the Players
Regarding to the Tennis scoring rules the players could have only special values of points
controlled by a specific "Tennis-arithmetic". 

Tennis points could be incremented, decremented (when a Player loose his Advance). 
They could be compared for equality and ordered by their magnitude.


public record Coordinate(int xPos, int yPos);

public static class Day9 {
    public static void Solve() 
    {
        CalcTrail(1);
        CalcTrail(9);
    }

    public static void CalcTrail (int knot) {
    {
        int start = 10000;
        Coordinate head = new Coordinate(start, start);
        Coordinate[] tails = new Coordinate[knot].Select(c => new Coordinate(start, start)).ToArray();
        HashSet<Coordinate> trail = new HashSet<Coordinate>();

        foreach (string line in System.IO.File.ReadLines(@"day9-input.txt"))
        {
            string[] move = line.Split(' ');

            int moveY = 0;
            int moveX = 0;
            int step = int.Parse(move[1]);
                        
            for (int i = 0; i < step; i++)
            { 
                switch(char.Parse(move[0]))
                {
                    case 'U':
                        moveY = 1;
                        break;
                    case 'D':
                        moveY = -1;
                        break;
                    case 'R':
                        moveX = 1;
                        break;
                    case 'L':
                        moveX = -1;
                        break;

                }

                head = new Coordinate(head.xPos + moveX, head.yPos + moveY);

                for (int j = 0; j < knot; j++)
                {
                    Coordinate target = j == 0 ? head : tails[j - 1];
                    tails[j] = Follow(target, tails[j]);
                }
                trail.Add(tails[knot - 1]);
            }
        }

        trail.Add(tails[knot - 1]);
        int unique = trail.Count();

        Console.WriteLine("Unique coordinates with " + knot + " knots: " + unique);
    }
    }

    public static Coordinate Follow (Coordinate head, Coordinate tail)
    {
        int newX = tail.xPos;
        int newY = tail.yPos;

        if (Math.Abs(head.xPos - tail.xPos) > 1) {
            newX = head.xPos > tail.xPos ? newX + 1 : newX - 1;
            if (newY != head.yPos){
                newY = head.yPos > tail.yPos ? newY + 1 : newY - 1;
            }
        }
        else if (Math.Abs(head.yPos - tail.yPos) > 1) {
            newY = head.yPos > tail.yPos ? newY + 1 : newY - 1;
            if (newX != head.xPos) {
                newX = head.xPos > tail.xPos ? newX + 1 : newX - 1;
            }
        }

        return new Coordinate(newX, newY);
    }

    public static void printCoordinate (Coordinate cord, string name)
    {
        Console.WriteLine(name + ": xPos: " + cord.xPos + " yPos: " + cord.yPos);
    }
}


public static class Day2 {
    public static void Solve1(){
        int score = 0;
        foreach (string line in System.IO.File.ReadLines(@"day2-input.txt"))
        {  
         int elf = line[0] - 'A';
         int me = line[2] - 'X';
         score += me + 1;

         if (elf == me) {
             score += 3;
         } else if ((elf + 1) % 3 == me ){
             score += 6;
         }
        }
        Console.WriteLine("Part1: " + score);
    }

    public static void Solve2(){
        int score = 0;
        foreach (string line in System.IO.File.ReadLines(@"day2-input.txt"))
        {  
            int elf = line[0] - 'A';
            char me = line[2];

            switch(me)
            {
                case 'X':
                    score += elf == 0 ? 3 : elf;
                    break;

                case 'Y':
                    score += elf + 1;
                    score += 3;
                    break;

                case 'Z':
                    score += 6;
                    score += ((elf + 1) % 3) + 1;
                    break;
            }
        }
        Console.WriteLine("Part2: " + score);
    }
}
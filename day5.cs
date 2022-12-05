
public static class Day5 {
    public static string Reverse( string s ){
    char[] charArray = s.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
    }

    public static void Solve1(){
        String[] lines = System.IO.File.ReadAllLines(@"day5-input.txt");
        String[] stacks =  new String[9];
        
        List<String> table = lines.ToList().GetRange(0,8);
        table.Reverse();
        int offset = 1;
        for (int i = 0; i < 9; i++) {
            foreach (String line in table)
            {
                if(!Char.IsWhiteSpace(line[offset])) {
                    stacks[i] = stacks[i] + line[offset];
                }
            }
            offset += 4;
        }
        String[] stacks2 = (string[]) stacks.Clone();

        List<String> instructions = lines.ToList().GetRange(10, lines.Length - 10);
        foreach(String ins in instructions) {
            String[] move = ins.Split(' ');
            int toStackInd = int.Parse(move[5]) - 1;
            int fromStackInd = int.Parse(move[3]) - 1;
            int nr = int.Parse(move[1]);
            String moveString = stacks[fromStackInd].Substring(stacks[fromStackInd].Length - nr);
            stacks[toStackInd] = stacks[toStackInd] + Reverse(moveString);
            stacks[fromStackInd] = stacks[fromStackInd][..^nr];

            String moveString2 = stacks2[fromStackInd].Substring(stacks2[fromStackInd].Length - nr);
            stacks2[toStackInd] = stacks2[toStackInd] + moveString2;
            stacks2[fromStackInd] = stacks2[fromStackInd][..^nr];
        }
        String top = "";
        String top2 = "";

          for (int i = 0; i < 9; i++) {
            top = top + stacks[i][stacks[i].Length - 1];
            top2 = top2 + stacks2[i][stacks2[i].Length - 1];
        }
        Console.WriteLine(top + " , " + top2);
    }
}
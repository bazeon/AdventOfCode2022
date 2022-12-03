


public  static class Day3 {
        private static char CommonChar(String str1, String str2) {
        char found = 'a';
        foreach (char c in str1)
        {
            if(str2.Contains(c)) {
                found = c;
                break;
            }
        }
        return found;
    }

    private static String CommonChars(String str1, String str2) {
        String found = ""; 
        foreach (char c in str1)
        {
            if(str2.Contains(c)) {
                found = found + c;
            }
        }
        return found;
    }
    public static void Solve1() {
        int prio = 0;
        foreach (string line in System.IO.File.ReadLines(@"day3-input.txt"))
        {
            String rucksack1 = line.Substring(0, line.Length/2);
            String rucksack2 = line.Substring(line.Length/2);
            char common = CommonChar(rucksack1, rucksack2);
            if (Char.IsLower(common)) {
                prio +=  common - 'a' + 1; 
            } else {
                prio += common - 'A' + 27;
            }
            //Console.WriteLine(line + " " + rucksack1 + " " + rucksack2 + " " + common + " " + prio);
        }
        Console.WriteLine("Sum of priorities: " + prio);
    }

    public static void Solve2() {
        int prio = 0;
        String [] lines = System.IO.File.ReadAllLines(@"day3-input.txt");
        List<String> lineList = lines.ToList();

        while (lineList.Any()) {
            String common = CommonChars(lineList[0], lineList[1]);
            char badge = CommonChar(common, lineList[2]);
            lineList.RemoveRange(0, 3);

            if (Char.IsLower(badge)) {
                prio +=  badge - 'a' + 1; 
            } else {
                prio += badge - 'A' + 27;
            }
            //Console.WriteLine(common + " " + badge + " " + prio);
        }
        Console.WriteLine("Sum of badges: " + prio);
    }
}

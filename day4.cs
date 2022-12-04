public static class Day4 {
    public static void Solve () {
        int contain = 0;
        int overlap = 0;
        foreach (string line in System.IO.File.ReadLines(@"day4-input.txt"))
        {
            String[] elfs = line.Split(',');
            int [] elf0 = Array.ConvertAll(elfs[0].Split('-'), s => int.Parse(s));
            int [] elf1 =  Array.ConvertAll(elfs[1].Split('-'), s => int.Parse(s));

            if ((elf0[1] < elf1[0] || elf0[0] > elf1[1])) {
                continue;
            }

            if((elf0[0] <= elf1[0] && 
                elf0[1] >= elf1[1]) ||
               (elf0[0] >= elf1[0] &&
                elf0[1] <= elf1[1])){
                contain += 1;
            }
            overlap += 1;
        }
        Console.WriteLine("Part 1: Sections fully contained: " + contain);
        Console.WriteLine("Part 2: Section overlap: " + overlap);
    }
}
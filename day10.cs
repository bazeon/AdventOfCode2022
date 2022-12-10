

public static class Day10 {

    public static void Solve() {
        
        int regValue = 1;
        List<int> regHistory = new List<int>();

        foreach (string line in System.IO.File.ReadLines(@"day10-input.txt"))
        {
            if(line == "noop"){
                regHistory.Add(regValue);
            }
            else {
                regHistory.AddRange(new List <int> {regValue, regValue});
                regValue += int.Parse(line.Split()[1]);;
            }
        }

        int[] interestingCycles = {20, 60, 100, 140, 180, 220};

        int sum = 0;
        foreach (int value in interestingCycles)
        {
            sum += regHistory[value - 1] * value;   
        }

        Console.WriteLine("Part 1: " + sum);

        Console.WriteLine("Part 2:");

        int i = 0;
        foreach (int value in regHistory)
        {
            WriteCRT(value, i % 40);
            i++;

            if( i % 40 == 0) {
                Console.WriteLine("");
            }
        }
    }

    private static void WriteCRT(int register, int index){
        char  c = '.';

        if (Math.Abs(register - index) <= 1) {
            c = '#';
        }

        Console.Write(c);
    }
}
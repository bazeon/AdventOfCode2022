

public record Thrown(long value, int monkey);

public class Monkey {
    
    public int divisibleBy;
    int trueMonkey;
    int falseMonkey;
    Func<long, long> Operation;
    public List<long> items;
    public long inspectedItems;
    public int commonDivider;


    public Monkey (long[] startItems,  Func<long, long> op, int test, int ifTrue, int ifFalse)
    {
        items = new List<long>();
        items.AddRange(startItems);
        Operation = op;
        divisibleBy = test;
        trueMonkey = ifTrue;
        falseMonkey = ifFalse;
        inspectedItems = 0;
        commonDivider = 0;
    }

    public List<Thrown> Turn () 
    {
        List<Thrown> thrownItems = new List<Thrown>();
        for(int i = 0; i < items.Count(); i++)
        {
            long value = items[i]; 
            value = Operation(value);
            if(commonDivider == 0) {
                value = (long) value / 3;
            }
            else {
                value %= commonDivider;
            }
            int m = (value % divisibleBy == 0) ? trueMonkey : falseMonkey;
            thrownItems.Add(new Thrown(value, m));
            inspectedItems++;

        }

        items = new List<long>();

        return thrownItems;
    }
}


public static class Day11 {

    public static void Solve()
    {
        Console.WriteLine("Part 1: " + SimulateMonkeys(1));
        Console.WriteLine("Part 2: " + SimulateMonkeys(2));
    }

    private static long SimulateMonkeys(int part) 
    {
      List<string> input = System.IO.File.ReadAllLines(@"day11-input.txt").ToList();

        input = input.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

        List<Monkey> monkeys = new List<Monkey>();
        int commonDivider = 1;
        while (input.Count() != 0)
        {
            List<string> instructions = input.GetRange(0, 6);
            input.RemoveRange(0, 6);
            Monkey m = ParseInstr(instructions.ToArray());
            monkeys.Add(m);
            commonDivider *= m.divisibleBy;
        }

        if(part == 2) {
            foreach (Monkey m in monkeys)
            {
                m.commonDivider = commonDivider;
            }
        }

        int rounds = part == 1 ? 20 : 10000;

        for (int i = 0; i < rounds; i++)
        {
            monkeys = Round(monkeys);
        }

        List<long> inspected = new List<long>();
        foreach (Monkey monkey in monkeys)
        {
            inspected.Add(monkey.inspectedItems);
        }
        inspected.Sort();
        inspected.Reverse();
        return inspected[0] * inspected[1];
    }

    private static List<Monkey> Round (List<Monkey> monkeys)
    {
        foreach (Monkey monkey in monkeys)
        {
            List<Thrown> thrown = monkey.Turn();
            foreach (Thrown t in thrown)
            {
                monkeys[t.monkey].items.Add(t.value);
            }
        }
        return monkeys;
    }

    private static Monkey ParseInstr(string[] instructions)
    {
        long[] items = instructions[1].Trim().Split(" ")[2..].Select(x => long.Parse(x.Trim(','))).ToArray();
        string value = instructions[2].Split(" ").Last();

        Func<long, long> op;
        if(value == "old") {
            op = (x) => { return x * x; };

        }
        else if (instructions[2].Split()[^2] == "+") {
            op = (x) => { return x + int.Parse(value); };
        }
        else {
            op = (x) => { return x * int.Parse(value); };
        }

        int test = int.Parse(instructions[3].Split()[^1]);
        int ifTrue = int.Parse(instructions[4].Split()[^1]);
        int ifFalse = int.Parse(instructions[5].Split()[^1]);

        return new Monkey(items, op, test, ifTrue, ifFalse);
    }
}
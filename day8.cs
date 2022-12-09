

public static class Day8 {

    public static bool IsVisible (int[] trees, int index)
    {
        bool hidden = false;
        for (int i = index + 1; i < trees.Length; i++)
        {
            if (trees[index] <= trees[i]) {
               hidden = true;
               break;
            }
        }
        if(!hidden)
            return true;

        for (int j = 0; j < index; j++)
        {
            if(trees[index] <= trees[j])
                return false; 
        }
        return true;
    }

    public static int GetScenic (int[] trees, int index) {
        int view1 = 0;
        for (int i = index + 1; i < trees.Length; i++)
        {
            view1 += 1;
            if (trees[index] <= trees[i]) {
               break;
            }
        }
        
        int view2 = 0;
        for (int j = index - 1; j >= 0 ; j--)
        {
            view2 += 1;
            if(trees[index] <= trees[j]) {
                break;
            }
            
        }

        return view1 * view2;
    }

    public static void Solve() {
        String [] input = System.IO.File.ReadAllLines(@"day8-input.txt");
        int len = input.Length;
        List<int []> gridRows = new List<int[]>();
        List<int []> gridCols = new List<int[]>();

        foreach (string line in input) {        
            int[] row = Array.ConvertAll(line.ToCharArray(), c => (int)Char.GetNumericValue(c)); 
            gridRows.Add(row);
        }

        for (int i = 0; i < len; i++)
        {
            int[] col = new int[len];
            for (int j = 0; j < len; j++)
            {
                col[j] = gridRows[j][i]; 
            }
            gridCols.Add(col);
        }

        int visible = 0;
        int scenicScore = 0;
        for(int row = 0; row < len; row++)
        {
            for(int col = 0; col < len; col++)
            {   
                if (col == 0 || col == len - 1 ||
                    row == 0 || row == len - 1) 
                {
                    visible += 1;
                    continue;
                }
                
                if (IsVisible(gridRows[row], col) || IsVisible(gridCols[col], row))
                { 
                    visible += 1;
                }
                
                int sc = GetScenic(gridRows[row], col) * GetScenic(gridCols[col], row);
                if (scenicScore < sc) {
                    scenicScore = sc;
                }
            }
        }

        Console.WriteLine("Part 1: " + visible);
        Console.WriteLine("Part 2: " + scenicScore);

    }
}
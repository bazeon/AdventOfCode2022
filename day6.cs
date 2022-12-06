public static class Day6 {
    public static void Solve() {
        String input = System.IO.File.ReadAllText(@"day6-input.txt");
        Console.WriteLine("Part 1: " + findMarker(input, 4));
        Console.WriteLine("Part 2: " + findMarker(input, 14));
    }

    private static int findMarker(String input, int markerLen) {
        int foundMarker = 0; 
        for(int i = markerLen - 1; i < input.Length; i++)
        {
            String potentialMarker = input.Substring(i - markerLen + 1, markerLen);
            potentialMarker = String.Join("", potentialMarker.Distinct());
            if (potentialMarker.Length == markerLen) {
                foundMarker = i + 1;
                break;
            }
        }
        return foundMarker;
    }  
}


public abstract class SystemObject {
    public string? name;
    public abstract int GetSize();
    public abstract void Print(int spaces = 0);
}

public class File : SystemObject {
    public int size;
    public File (string fileName, int fileSize) {
        name=fileName;
        size=fileSize;
    }
    public override int GetSize () {
        return size;
    }
    public override void Print(int spaces = 0)
    {
        string indent = new string(Enumerable.Repeat(' ', spaces).ToArray());
        Console.WriteLine( indent + name + " " + size);
    }
}

public class Directory : SystemObject {
    List<SystemObject> children;
    public Directory (string dirName) {
        name = dirName;
        children = new List<SystemObject>();
    }
    public override int GetSize () {
        int size = 0;
        foreach (SystemObject child in children)
        {
            size += child.GetSize();
        }
        return size;
    }
    public List<int> GetSizeList() {
        List<int> sizes = new List<int>();
        int size = GetSize();
        if(size < 100000) {
            sizes.Add(size);
        }
        foreach (Directory child in GetDirChildren())
        {
            sizes.AddRange(child.GetSizeList());
        }

        return sizes;
    }
    public override void Print(int spaces = 0)
    {
        string indent = new string(Enumerable.Repeat(' ', spaces).ToArray());
        Console.WriteLine(indent + name + ":");

        foreach (SystemObject child in children)
        {
            child.Print(spaces + 2);
        }
    }
    public SystemObject GetChild(string name) {
        int i = 0;
        foreach (SystemObject child in children)
        {
            if (child.name == name) {
                break;
            }
            i++;
        }
        return children[i];
    }
    public List<Directory> GetDirChildren() {
        List<Directory> dirs = new List<Directory>();
        foreach (SystemObject child in children)
        {
            if(child is Directory){
                dirs.Add((Directory)child);
            }
        }
        return dirs;
    }
    public void addFile(string name, int size) {
        File newFile = new File(name, size);
        children.Add(newFile); 
    }
    public void addDirectory(string name) {
        Directory newDir = new Directory(name);
        children.Add(newDir); 
    }
    public int FindClosest (int lowest) {
        int size = GetSize();
        int closest = (size > lowest) ? size : Int32.MaxValue;
        foreach (Directory child in GetDirChildren())
        {
            int childClosest = child.FindClosest(lowest);
            closest = (closest < childClosest) ? closest : childClosest;
        }
        return closest;
    }
}



public static class Day7 {

    public static Directory GetDirectory (List<string> breadCrumb, Directory root) {
        Directory current = root;
        foreach (string dir in breadCrumb)
        {
            current = (Directory) current.GetChild(dir);
        }

        return current;
    }
    public static void Solve() 
    {
        List<string> breadCrumb = new List<string>();
        Directory root = new Directory("/");
        Directory current = root;
        foreach (string line in System.IO.File.ReadLines(@"day7-input.txt"))
        {
            if(line.StartsWith("$ cd")) {
                string dir = line.Split(" ")[2];
                
                if (dir == "/") {
                    continue;
                }

                else if (dir == "..") {
                    if(breadCrumb.Count != 0) {
                    breadCrumb.RemoveAt(breadCrumb.Count - 1);
                    }
                    current = GetDirectory(breadCrumb, root);
                }

                else {
                    breadCrumb.Add(dir);
                    current = (Directory) current.GetChild(dir);
                }
                
            }
            else if(line.StartsWith("dir ")) {
                current.addDirectory(line.Split(" ")[1]);
            }
            else if(Char.IsDigit(line[0])) {
                string [] l = line.Split();
                current.addFile(l[1], int.Parse(l[0]));
            }
        }
        Console.WriteLine("Part 1: " + root.GetSizeList().Sum());

        int toFree = 30000000 - (70000000 - root.GetSize());
        Console.WriteLine("Part 2: " + root.FindClosest(toFree));

    }
}
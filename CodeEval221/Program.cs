using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = args.Length > 0 ? args[0] : "../../input.txt";
        File.ReadAllLines(input)
            .Select(line =>
            {
                var tree = new Dictionary<char, HashSet<char>>();
                var relations = line.Split('|');
                foreach (var relation in relations)
                {
                    var boss = relation.Trim()[0];
                    var worker = relation.Trim()[1];
                    HashSet<char> workers = null;
                    tree.TryGetValue(boss, out workers);
                    if (workers == null)
                        workers = new HashSet<char>();
                    workers.Add(worker);
                    tree[boss] = workers;
                }
                return PrintTree(tree, FindRoot(tree));
            })
            .ToList()
            .ForEach(check => Console.WriteLine(check));
    }

    private static char FindRoot(Dictionary<char, HashSet<char>>  tree)
    {
        var children = tree.SelectMany(elem => elem.Value);
        return tree.Keys.Single(elem => !children.Contains(elem));
    }

    private static string PrintTree(Dictionary<char, HashSet<char>> tree, char root)
    {
        HashSet<char> children;
        if (!tree.TryGetValue(root, out children))
        {
            children = new HashSet<char>();
        }

        if (children.Any())
        {
            var pre = root + " [";
            var post = "]";

            var inside = children
                    .OrderBy(s => s)
                    .Aggregate(
                        string.Empty, 
                        (acc, child) => 
                            string.IsNullOrEmpty(acc) ? 
                                PrintTree(tree, child) 
                                : acc + ", " + PrintTree(tree,child));
            return pre + inside + post;
        }
        else return root.ToString();
    }
}
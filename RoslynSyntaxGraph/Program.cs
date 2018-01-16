using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoslynSyntaxGraph
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			//const string targetSolution = @"C:\src\djo\tests\ConsoleTestbed\ConsoleTestbed.sln";
			//const string targetSolution = @"C:\src\nv\ClubRunner\ClubRunner.sln";
			const string targetSolution = @"C:\src\nv\Umbrella\Umbrella.sln";

			var wrapper = new GraphBuilder(targetSolution);
			var graph = wrapper.BuildGraph().ToArray();
			//var multiParents = graph.Where(n => n.Parents.Count() > 1).ToArray();
			//var multiOccurrence = graph.Where(n => n.Parents.FirstOrDefault().Occurrences > 1).ToArray();
			;
			var viz = GraphVisualizer.ToGraphViz(graph);

			Console.WriteLine("");
			Console.WriteLine(viz);

			try
			{

				Clipboard.SetText(viz);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Clipboard failed: {e}");
			}
			Console.ReadLine();
			;
		}

	}
}

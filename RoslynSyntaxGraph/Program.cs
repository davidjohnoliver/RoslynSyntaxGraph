using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphVizWrapper;

namespace RoslynSyntaxGraph
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var today = DateTimeOffset.Now.ToString("yyyyMMdd");

			const string targetSolution = @"C:\src\djo\tests\ConsoleTestbed\ConsoleTestbed.sln";
			//const string targetSolution = @"C:\src\nv\ClubRunner\ClubRunner.sln";
			//const string targetSolution = @"C:\src\nv\Umbrella\Umbrella.sln";

			var wrapper = new GraphBuilder(targetSolution);
			var graph = wrapper.BuildGraph().ToArray();
			//var multiParents = graph.Where(n => n.Parents.Count() > 1).ToArray();
			//var multiOccurrence = graph.Where(n => n.Parents.FirstOrDefault().Occurrences > 1).ToArray();
			;
			var viz = GraphVisualizer.ToGraphViz(graph);

			Console.WriteLine("");
			Console.WriteLine(viz);

			var graphName = targetSolution.Split('\\').Last();
			const string baseOutputPath = @"C:\Users\david.oliver\Documents\Work\Internal\RoslynSyntaxGraph";
			var outputPath = Path.Combine(baseOutputPath, today);
			const string graphVizPath = @"C:\GraphViz2.38\bin\";
			var outputter = new GraphVisualizer(viz, graphVizPath, outputPath, graphName);
			
			outputter.OutputToImageFile(Enums.RenderingEngine.Dot);
			//foreach (var engine in Enum.GetValues(typeof(Enums.RenderingEngine)).Cast<Enums.RenderingEngine>())
			//{
			//	outputter.OutputToImageFile(engine);
			//}

			try
			{
				Clipboard.SetText(viz);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Clipboard failed: {e}");
			}

			Console.WriteLine();
			Console.WriteLine("Done");
			Console.ReadLine();
			;
		}

	}
}

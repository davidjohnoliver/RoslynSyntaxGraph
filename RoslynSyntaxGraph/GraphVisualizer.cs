using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;

namespace RoslynSyntaxGraph
{
	public class GraphVisualizer
	{
		public GraphVisualizer(string vizGraph, string graphVizPath, string defaultOutputFolder = "", string graphName = "")
		{
			VizGraph = vizGraph;
			GraphVizPath = graphVizPath;
			DefaultOutputFolder = defaultOutputFolder;
			GraphName = graphName;
		}

		public string VizGraph { get; }
		public string GraphVizPath { get; }
		public string DefaultOutputFolder { get; }
		public string GraphName { get; }

		public void OutputToImageFile(Enums.RenderingEngine renderingEngine, string filePath = null, Enums.GraphReturnType format = Enums.GraphReturnType.Jpg)
		{
			filePath = filePath ?? GetFilePath(renderingEngine, format);
			Console.WriteLine($"Generating {format} with rendering engine {renderingEngine}...");
			var generator = GetGraphGeneration();
			generator.RenderingEngine = renderingEngine;

			var img = generator.GenerateGraph(VizGraph, format);

			Console.WriteLine($"Saving {format} to {filePath} ...");

			if (!string.IsNullOrEmpty(DefaultOutputFolder))
			{
				Directory.CreateDirectory(DefaultOutputFolder);
			}

			File.WriteAllBytes(filePath, img);
		}

		private string GetFilePath(Enums.RenderingEngine renderingEngine, Enums.GraphReturnType format)
		{
			// TODO: this only works for some of the formats

			return Path.Combine(DefaultOutputFolder,
				GraphName + "_" +
				renderingEngine.ToString().ToLowerInvariant() + "." +
				format.ToString().ToLowerInvariant());
		}

		private GraphGeneration GetGraphGeneration()
		{
			var startProcessQuery = new GetStartProcessQuery();
			var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
			GraphGeneration generation = new GraphGeneration(startProcessQuery, getProcessStartInfoQuery, new RegisterLayoutPluginCommand(getProcessStartInfoQuery, startProcessQuery));
			generation.GraphvizPath = GraphVizPath;

			return generation;
		}

		/// <summary>
		/// Convert graph to GraphViz string.
		/// </summary>
		public static string ToGraphViz(IEnumerable<GraphNode> graph)
		{
			var sb = new StringBuilder();
			sb.AppendLine("digraph G {");

			foreach (var node in graph)
			{
				foreach (var tuple in node.Parents)
				{
					sb.AppendLine($"	\"{tuple.Parent.Name}\" -> \"{node.Node.Name}\"");
				}
			}

			sb.AppendLine("};");

			return sb.ToString();
		}
	}
}

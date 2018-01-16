using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynSyntaxGraph
{
	public static class GraphVisualizer
	{
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynSyntaxGraph
{
	public class GraphBuilder
	{
		private readonly string _solutionFilePath;

		public GraphBuilder(string solutionFilePath)
		{
			this._solutionFilePath = solutionFilePath;
		}

		public IEnumerable<GraphNode> BuildGraph()
		{
			var wrapper = new CompilationWrapper(_solutionFilePath);

			var graphNodes = new Dictionary<Type, GraphNode>();

			foreach (var node in wrapper.GetSyntaxNodes())
			{
				GraphNode graphNode;
				var nodeType = node.GetType();
				if (!graphNodes.TryGetValue(nodeType, out graphNode))
				{
					graphNodes[nodeType] = graphNode = new GraphNode(nodeType);
				}

				graphNode.RegisterParent(node.Parent?.GetType() ?? NullParentType);
			}

			graphNodes[NullParentType] = new GraphNode(NullParentType);

			return graphNodes.Values;
		}

		public static Type NullParentType { get; } = typeof(NoParentType);
	}
}

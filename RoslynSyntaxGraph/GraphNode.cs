using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynSyntaxGraph
{
	public class GraphNode
	{
		private readonly Dictionary<Type, int> _parents = new Dictionary<Type, int>();
		public GraphNode(Type node)
		{
			Node = node;
		}

		public Type Node { get; }

		public IEnumerable<(Type Parent, int Occurrences)> Parents => _parents.Select(kvp => (kvp.Key, kvp.Value));
		
		public void RegisterParent(Type parent)
		{
			if (_parents.TryGetValue(parent, out int value))
			{
				_parents[parent] = value + 1;
			}
			else
			{
				_parents[parent] = 1;
			}
		}
	}
}

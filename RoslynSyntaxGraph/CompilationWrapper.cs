using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace RoslynSyntaxGraph
{
	public class CompilationWrapper
	{
		private readonly string _solutionFilePath;
		private readonly bool _includeTrivia;

		public CompilationWrapper(string solutionFilePath, bool includeTrivia = false)
		{
			this._solutionFilePath = solutionFilePath;
			this._includeTrivia = includeTrivia;
		}

		public IEnumerable<SyntaxNode> GetSyntaxNodes()
		{
			Console.WriteLine($"Opening solution {_solutionFilePath}");
			var solution = MSBuildWorkspace.Create().OpenSolutionAsync(_solutionFilePath).Result;

			foreach (var project in solution.Projects)
			{
				Console.WriteLine($"Processing project {project.Name}");

				var compilation = project.GetCompilationAsync().Result;

				foreach (var tree in compilation.SyntaxTrees)
				{
					var root = tree.GetRoot();

					foreach (var node in root.DescendantNodesAndSelf(descendIntoTrivia: _includeTrivia))
					{
						yield return node;
					}
				}
			}
		}
	}
}

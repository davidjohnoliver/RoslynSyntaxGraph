using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynSyntaxGraph
{
	class Program
	{
		static void Main(string[] args)
		{
			//const string targetSolution = @"C:\src\djo\tests\ConsoleTestbed\ConsoleTestbed.sln";
			const string targetSolution = @"C:\src\nv\ClubRunner\ClubRunner.sln";

			var wrapper = new GraphBuilder(targetSolution);
			var nodes = wrapper.BuildGraph().ToArray();
			;
		}
	}
}

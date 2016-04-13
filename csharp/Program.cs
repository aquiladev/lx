using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lx
{
	class Program
	{
		static void Main(string[] args)
		{
			string input = File.ReadAllText("data.txt", Encoding.Default);

			var result = ReplaceNonLetter(input)
				.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
				.GroupBy(x => x.ToLower())
				.Select(x => new KeyValuePair<string, int>(x.Key, x.Count()))
				.OrderByDescending(x => x.Value)
				.ThenBy(x => x.Key);

			using (var file = new StreamWriter("results.txt"))
			{
				foreach (var item in result)
				{
					file.WriteLine(item.Key + " " + item.Value);
				}
			}
		}

		static string ReplaceNonLetter(string input)
		{
			var list = new char[input.Length];
			for (int i = 0; i < input.Length; i++)
			{
				if (char.IsLetter(input[i])
					|| (char.IsWhiteSpace(input[i])
						&& input[i] != '\r'
						&& input[i] != '\n'))
				{
					list[i] = input[i];
				}
				else
				{
					list[i] = ' ';
				}
			}

			return new string(list);
		}
	}
}

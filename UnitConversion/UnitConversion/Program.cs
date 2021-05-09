using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnitConversion
{
	public class Program
	{
		public static void Main()
		{
			var values = new List<String>() { "1000 mm", "20,2 cm", "3,35 m", "50 mm", "50,2 mm" };
			foreach (var val in values.AsQueryable().OrderBy(v => GetAbsoluteValue(v)))
			{
				Console.WriteLine(val);
			}
		}

		private static object GetAbsoluteValue(string v)
		{
			{
				var powers = new Dictionary<string, int>() { { "mm", 0 }, { "cm", 1 }, { "dm", 2 }, { "m", 3 } };
				var numbersPattern = @"([\d]*,?[\d]*)";
				var unitPattern = String.Format("({0})", String.Join("|", powers.Keys));
				var number = Regex.Match(v, numbersPattern);
				var unit = Regex.Match(v, unitPattern);

				var result = 0.0;

				if (number.Success)
					result = Double.Parse(number.Value);

				if (number.Success && unit.Success && powers.ContainsKey(unit.Value))
					result = Double.Parse(number.Value) * Math.Pow(10, powers[unit.Value]);

				return result;
			}
		}
	}
}

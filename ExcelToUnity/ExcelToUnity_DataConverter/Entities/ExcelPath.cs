using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class ExcelPath : IComparable<ExcelPath>
{
	public string path { get; set; }
	public bool exportIds { get; set; }
	public bool exportConstants { get; set; }
	public ExcelPath() { }

	public ExcelPath(string pPath)
	{
		path = pPath;
		exportIds = false;
		exportConstants = false;
	}

	public int CompareTo(ExcelPath other)
	{
		return path.CompareTo(other.path);
	}
}


[Serializable]
public class GoogleSheetsPath : IComparable<GoogleSheetsPath>
{
	public string name { get; set; }
	public string id { get; set; }
	public List<Sheet> sheets { get; set; } = new List<Sheet>();

	[Serializable]
	public class Sheet
	{
		public string name { get; set; }
		public bool selected { get; set; }
	}

	public void AddSheet(string name)
	{
		for (int i = 0; i < sheets.Count; i++)
			if (sheets[i].name == name)
				return;
		sheets.Add(new Sheet { name = name, selected = true });
	}

	public void RemoveSheet(string name)
	{
		for (int i = 0; i < sheets.Count; i++)
			if (sheets[i].name == name)
			{
				sheets.RemoveAt(i);
				break;
			}
	}

	public int CompareTo(GoogleSheetsPath other)
	{
		return name.CompareTo(other.name);
	}
}
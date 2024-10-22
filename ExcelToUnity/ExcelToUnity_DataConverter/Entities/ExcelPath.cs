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
public class GoogleSpreadSheetPath
{
    public string key;
    public List<SpreadSheet> spreadSheets = new List<SpreadSheet>();

	[Serializable]
	public class SpreadSheet
	{
		public string name;
		public bool selected;
	}
}
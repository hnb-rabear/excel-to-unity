using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SpreadSheetConfig : IComparable<SpreadSheetConfig>
{
    public string path { get; set; }
    public bool exportIds { get; set; }
    public bool exportConstants { get; set; }
	public SpreadSheetConfig() { }

	public SpreadSheetConfig(string pPath)
    {
        path = pPath;
        exportIds = false;
        exportConstants = false;
    }

    public int CompareTo(SpreadSheetConfig other)
    {
        return path.CompareTo(other.path);
    }
}
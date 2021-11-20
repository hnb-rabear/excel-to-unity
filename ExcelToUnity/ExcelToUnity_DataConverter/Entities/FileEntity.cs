using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class FileEntity : IComparable<FileEntity>
{
    public string path { get; set; }
    public bool exportIds { get; set; }
    public bool exportConstants { get; set; }

    public FileEntity(string pPath)
    {
        path = pPath;
        exportIds = false;
        exportConstants = false;
    }

    public int CompareTo(FileEntity other)
    {
        return path.CompareTo(other.path);
    }
}
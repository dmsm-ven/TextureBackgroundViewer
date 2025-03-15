using System.IO;

namespace TextureBackgroundViewer;

public class TextureInfo
{
    private readonly FileInfo fi;
    private readonly int number;

    public TextureInfo(FileInfo fi)
    {
        this.fi = fi;
        this.number = number;
    }

    public string FullName => fi.FullName;
    public string ShortName => fi.Name;
}
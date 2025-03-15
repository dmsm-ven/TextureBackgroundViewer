using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TextureBackgroundViewer.ViewModels;

public partial class DesignTime_MainWindowViewModel : MainWindowViewModel
{
    public DesignTime_MainWindowViewModel() : base()
    {
        string resourceFolder = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), @"Assets\DesignTimeTextures\");

        var textureFiles = Directory
            .GetFiles(resourceFolder, "*.*", SearchOption.TopDirectoryOnly)
            .ToArray();

        Debug.WriteLine($"WORKING FOLDER: {resourceFolder}, COUNT: {textureFiles.Length}");

        int designTimeLoadItems = 4;
        int i = 0;
        foreach (var file in textureFiles)
        {
            var item = new TextureInfo(file, this);
            if (i++ < designTimeLoadItems)
            {
                item.IsRefreshed = true;
            }
            TexturesCollection.Add(item);
        }


        if (textureFiles.Any())
        {
            this.SetTexture(TexturesCollection[0]);
        }
    }
}

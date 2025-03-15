using System.IO;
using System.Windows;

namespace TextureBackgroundViewer.ViewModels;

public partial class DesignTime_MainWindowViewModel : MainWindowViewModel
{
    public DesignTime_MainWindowViewModel() : base()
    {
        string resourceFolder = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), @"Assets\DesignTimeTextures\");

        LoadResourceFromWorkingFolder(resourceFolder).ContinueWith(t =>
        {
            Application.Current.Dispatcher.InvokeAsync(() => SetTexture(TexturesCollection[0]));
        });
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using TextureBackgroundViewer.ViewModels;

namespace TextureBackgroundViewer;

public partial class TextureInfo : ObservableObject
{
    private readonly FileInfo fi;
    private readonly MainWindowViewModel parentViewModel;

    public TextureInfo(FileInfo fi, MainWindowViewModel parentViewModel)
    {
        this.fi = fi;
        this.parentViewModel = parentViewModel;
    }

    public string FullName => fi.FullName;
    public string ShortName => fi.Name;

    [RelayCommand]
    public void SetThisTexture()
    {
        parentViewModel.SetTexture(this);
    }
}
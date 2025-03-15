using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TextureBackgroundViewer.ViewModels;

public partial class SizeSelectorItem : ObservableObject
{
    private readonly MainWindowViewModel parentViewModel;

    private readonly int width;
    private readonly int height;
    private readonly bool isOriginal;

    public string SizeString => $"{width}x{height}{(isOriginal ? "(orig)" : "")}";


    public SizeSelectorItem(MainWindowViewModel parentViewModel, int width, int height, bool isOriginal)
    {
        this.parentViewModel = parentViewModel;
        this.width = width;
        this.height = height;
        this.isOriginal = isOriginal;
    }

    [RelayCommand]
    public void SetSize()
    {
        parentViewModel.SetViewportSize(width, height);
    }
}

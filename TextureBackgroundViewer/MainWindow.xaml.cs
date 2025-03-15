using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TextureBackgroundViewer;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<TextureInfo> TexturesCollection { get; } = new();

    public string TexturesFolder { get; } = @"D:\Programming\Assets\Textures";

    public string Title
    {
        get => $"Textures Viewer | {SelectedTexturePath ?? "<Не выбрано>"} | Папка: {TexturesFolder ?? "<Не указано>"}";
    }

    private string selectedTexturePath;
    public string SelectedTexturePath
    {
        get => selectedTexturePath;
        set
        {
            selectedTexturePath = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTexturePath)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
        }
    }

    private Color selectedColor = Colors.White;
    public Color SelectedColor
    {
        get => selectedColor;
        set
        {
            selectedColor = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedColor)));
            SetGridLayerBackground();
        }
    }

    private double opacitySliderValue = 0.9;
    public double OpacitySliderValue
    {
        get => opacitySliderValue;
        set
        {
            opacitySliderValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OpacitySliderValue)));
            SetGridLayerBackground();
        }
    }

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var textureFiles = Directory.GetFiles(TexturesFolder, "*.*", SearchOption.AllDirectories)
            .Take(100)
            .Select(file => new FileInfo(file))
            .OrderByDescending(file => file.CreationTime)
            .ToArray();

        foreach (var file in textureFiles)
        {
            TexturesCollection.Add(new TextureInfo(file));
        }
    }

    private void btnSetTexture_Click(object sender, RoutedEventArgs e)
    {
        SelectedTexturePath = ((sender as Button).Tag as TextureInfo).FullName;
        var img = new System.Windows.Media.Imaging.BitmapImage(new Uri(SelectedTexturePath, UriKind.Relative));
        window.Background = new ImageBrush(img);

    }

    private void rectWhite_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SelectedColor = Colors.White;
    }

    private void rectBlack_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SelectedColor = Colors.Black;
    }

    private void SetGridLayerBackground()
    {
        byte opacityChannel = (byte)(byte.MaxValue - (byte.MaxValue * OpacitySliderValue));
        var gridBrush = Color.FromArgb(opacityChannel, SelectedColor.R, SelectedColor.G, SelectedColor.B);
        this.mainGrid.Background = new SolidColorBrush(gridBrush);

    }
}

using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using TextureBackgroundViewer.ViewModels;

namespace TextureBackgroundViewer;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly string settingsFileName = "settings.data";

    public App()
    {

    }
    protected override void OnStartup(StartupEventArgs e)
    {
        LoadSettings(this, e);

        var vm = new MainWindowViewModel();
        var window = new MainWindow();
        window.DataContext = vm;
        window.ShowDialog();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        SaveSettings(this, e);
        base.OnExit(e);
    }

    private void LoadSettings(object sender, StartupEventArgs e)
    {
        // Restore application-scope property from isolated storage
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
        try
        {
            if (storage.FileExists(settingsFileName))
            {
                using (IsolatedStorageFileStream stream = storage.OpenFile(settingsFileName, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new(stream))
                {
                    // Restore each application-scope property individually
                    while (!reader.EndOfStream)
                    {
                        string[] keyValue = reader.ReadLine().Split(new char[] { ',' });
                        Properties[keyValue[0]] = keyValue[1];
                    }
                }
            }
        }
        catch (DirectoryNotFoundException ex)
        {
            // Path the file didn't exist
        }
        catch (IsolatedStorageException ex)
        {
            // Storage was removed or doesn't exist
            // -or-
            // If using .NET 6+ the inner exception contains the real cause
        }
    }

    private void SaveSettings(object sender, ExitEventArgs e)
    {

        // Persist application-scope property to isolated storage
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
        using (IsolatedStorageFileStream stream = storage.OpenFile(settingsFileName, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new(stream))
        {
            // Persist each application-scope property individually
            foreach (string key in Properties.Keys)
                writer.WriteLine("{0},{1}", key, Properties[key]);
        }
    }
}

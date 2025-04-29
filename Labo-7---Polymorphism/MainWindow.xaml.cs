using Labo_7___Polymorphism.Data;
using Labo_7___Polymorphism.Entities;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace Labo_7___Polymorphism;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
   private Store<Machine> _store = new Store<Machine>();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        string path = "machines.csv";

        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "CSV Bestand|*.csv";
        if (ofd.ShowDialog() == true)
        {
            using (StreamReader sr = new StreamReader(ofd.FileName))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string lineLength = sr.ReadLine();
                    string[] line = lineLength.Split(',');
                    Machine machine = null;
                    switch (line[0])
                    {
                        case "L":
                            string nameLaser = line[1];
                            int workSpaceWidthLaser = int.Parse(line[2]);
                            int workSpaceLengthLaser = int.Parse(line[3]);
                            double costPerMinuteLaser = double.Parse(line[4]);
                            double accuracy = double.Parse(line[5]);
                            machine = 
                            new LaserCutter(nameLaser, 
                            workSpaceLengthLaser, 
                            workSpaceWidthLaser, 
                            costPerMinuteLaser, 
                            accuracy);
                            break;
                        case "R":
                            string nameRouter = line[1];
                            int workSpaceWidthRouter = int.Parse(line[2]);
                            int workSpaceLengthRouter = int.Parse(line[3]);
                            double costPerMinuteRouter = double.Parse(line[4]);
                            machine =
                                new Router(nameRouter,
                                workSpaceLengthRouter,
                                workSpaceWidthRouter,
                                costPerMinuteRouter);
                            break;
                        case "G":
                            string nameGeneral = line[1];
                            machine = new General(nameGeneral);
                            break;
                        default:
                            break;
                    }
                    if (machine is not null)
                    {
                        _store.AddItem(machine);
                        itemsListBox.Items.Add(machine);
                    }
                }
            }

            clearButton.IsEnabled = true;
            sortButton.IsEnabled = true;
            filterButton.IsEnabled = true;
        }
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        if (itemsListBox.SelectedIndex != -1)
        {
          

            if (selectedMachine != null)
            {
                _store.RemoveItem(selectedMachine);
                UpdateListBox();
            }
        }
        
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        itemsListBox.Items.Clear();
        UpdateListBox();
    }

    private void UseButton_Click(object sender, RoutedEventArgs e)
    {
        if (itemsListBox.SelectedIndex != -1)
        { 
            if (selectedMachine != null)
            {
                if (int.TryParse(inputTextBox.Text, out int minutes) && minutes > 0)
                {
                    selectedMachine.Use(minutes);
                        UpdateListBox();
                }
                else
                {
                    MessageBox.Show("Please use the input box above to input how many minutes :)");
                }
                    
            }
        }
    }

    private void SortButton_Click(object sender, RoutedEventArgs e)
    {
        _store.SortItems((x, y) => string.Compare(x.Name, y.Name));//Pay attention to the parenthesis here, you almost had it before it was explained. 
        UpdateListBox();
    }

    private void FilterButton_Click(object sender, RoutedEventArgs e)
    {
        string filter = inputTextBox.Text;
        if (!string.IsNullOrEmpty(filter))
        {
            itemsListBox.Items.Clear();
            foreach (var item in _store.FilterItems(m => m.Name.Contains(filter))) //this is a predicate
            {
                itemsListBox.Items.Add(item);
            }
        }
    }
    public Machine selectedMachine => itemsListBox.SelectedItem as Machine; 
    private void itemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (itemsListBox.SelectedIndex != -1)
        {
          useButton.IsEnabled = !selectedMachine.OutOfUse;
            removeButton.IsEnabled = true;
        }
        else
        {
            useButton.IsEnabled = true;
            removeButton.IsEnabled = true;
        }
            
        
    }

    private void UpdateListBox()
    {
        itemsListBox.Items.Clear();
        foreach (Machine machine in _store.GetAllItems())
        {
            itemsListBox.Items.Add(machine);
        }
        removeButton.IsEnabled= false;
        useButton.IsEnabled= false;  
    }
}
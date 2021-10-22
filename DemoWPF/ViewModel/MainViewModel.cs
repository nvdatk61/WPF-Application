using DemoWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
  public class MainViewModel : BaseViewModel
  {
    public ICommand AddCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand SearchComamnd { get; set; }
    private ObservableCollection<item> _List;
    public ObservableCollection<item> List
    {
      get
      {
        return _List;
      }
      set
      {
        _List = value;
        OnPropertyChanged();
      }
    }
    private item _SelectedItem;
    public item SelectedItem
    {
      get
      {
        return _SelectedItem;
      }
      set
      {
        _SelectedItem = value;
        OnPropertyChanged();
        if (SelectedItem != null)
        {
          Content = SelectedItem.Content;
          Name = SelectedItem.Name;
          create = SelectedItem.Created.ToString();
          update = SelectedItem.Updated.ToString();
          if (Content != null)
          {
            numchar = Content.Count();
          }
          else
          {
            numchar = 0;
          }
          if (Content != null)
          {
            string[] str = Content.Split(' ');
            numword = str.Length;
          }
          else
          {
            numword = 0;
          }
        }
      }
    }
    private string _content;
    public string Content
    {
      get
      {
        return _content;
      }
      set
      {
        _content = value;
        OnPropertyChanged();
      }
    }
    private string _name;
    public string Name
    {
      get
      {
        return _name;
      }
      set
      {
        _name = value;
        OnPropertyChanged();
      }
    }

    private string _create;
    public string create
    {
      get
      {
        return _create;
      }
      set
      {
        _create = value;
        OnPropertyChanged();
      }
    }
    private string _update;
    public string update
    {
      get
      {
        return _update;
      }
      set
      {
        _update = value;
        OnPropertyChanged();
      }
    }

    private int _numword;
    public int numword
    {
      get
      {
        return _numword;
      }
      set
      {
        _numword = value;
        OnPropertyChanged();
      }
    }
    private int _numchar;
    public int numchar
    {
      get
      {
        return _numchar;
      }
      set
      {
        _numchar = value;
        OnPropertyChanged();
      }
    }

    private string _DisplayContent;
    public string DisplayContent { get => _DisplayContent; set { _DisplayContent = value; OnPropertyChanged(); } }

    private string _DisplayName;
    public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(_DisplayName); OnPropertyChanged(Name); } }

    public MainViewModel()
    {
      List = new ObservableCollection<item>(DataProvider.Ins.DB.items);
      AddCommand = new RelayCommand<object>((p) =>
      {
        return true;

      }, (p) =>
      {
        var num = DataProvider.Ins.DB.items.ToList().Count()+1;
        var unit = new item() { Name = "Item "+num, Content = null, Created= DateTime.Now, Updated=DateTime.Now };

        DataProvider.Ins.DB.items.Add(unit);
        DataProvider.Ins.DB.SaveChanges();
        MessageBox.Show("Add Success Item " + num);
        List.Add(unit);
      });
      SaveCommand = new RelayCommand<object>((p) =>
      {
        if (string.IsNullOrEmpty(Content) || SelectedItem == null)
          return false;

        return true;

      }, (p) =>
      {
        var unit = DataProvider.Ins.DB.items.Where(x => x.ID == SelectedItem.ID).SingleOrDefault();
        unit.Content = Content;
        unit.Updated = DateTime.Now;
        DataProvider.Ins.DB.SaveChanges();

        SelectedItem.Content = Content;
        MessageBox.Show("Save Success");
      });
      DeleteCommand = new RelayCommand<object>((p) =>
      {
        if (SelectedItem == null)
          return false;
        return true;

      }, (p) =>
      {
        var unit = DataProvider.Ins.DB.items.Where(x => x.ID == SelectedItem.ID).SingleOrDefault();
        DataProvider.Ins.DB.items.Remove(unit);
        DataProvider.Ins.DB.SaveChanges();

        List.Remove(unit);
      });
      SearchComamnd = new RelayCommand<object>((p) =>
      {
        return true;

      }, (p) =>
      {
        string name = DisplayName;
        if (DisplayName != null && DisplayName != "")
        {
          var unit = DataProvider.Ins.DB.items.Where(x => x.Name == DisplayName).SingleOrDefault();
          List = new ObservableCollection<item>(DataProvider.Ins.DB.items.Where(x => x.Name == DisplayName));
        }
        else
        {
          List = new ObservableCollection<item>(DataProvider.Ins.DB.items);
        }
      });
    }
  }
}
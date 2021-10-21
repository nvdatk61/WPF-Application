using DemoWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
  public class MainViewModel : BaseViewModel
  {
    public ICommand AddEvent { get; set; }
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
    private string _content;
    public string Content
    {
      get
      {
        return _content;
      }
      set
      {
        this._content = value;
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
        if (_SelectedItem != null)
        {
          Content = SelectedItem.Content;
          create = SelectedItem.Created.ToString();
        }
      }
    }
    public MainViewModel()
    {
      List = new ObservableCollection<item>(DataProvider.Ins.DB.items);
    }
  }
}
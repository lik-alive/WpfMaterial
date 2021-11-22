using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMaterial.Preview
{
    /// <summary>
    /// Interaction logic for DataGridStylePrev.xaml
    /// </summary>
    public partial class DataGridStylePrev : Window
    {
        public class TaskInfo
        {
            public String ID { get; private set; }
            public String Start { get; private set; }
            public String Finish { get; private set; }

            public TaskInfo(String id, String start, String finish)
            {
                this.ID = id;
                this.Start = start;
                this.Finish = finish;
            }
        }

        public DataGridStylePrev()
        {
            Tasks = new ObservableCollection<TaskInfo>();
            Tasks.Add(new TaskInfo("1", "1", "1"));
            Tasks.Add(new TaskInfo("2", "2", "2"));
            Tasks.Add(new TaskInfo("3", "3", "3"));
            Tasks.Add(new TaskInfo("4", "4", "4"));
            Tasks.Add(new TaskInfo("5", "5", "5"));
            InitializeComponent();
            
        }

        public ObservableCollection<TaskInfo> Tasks
        {
            get;
            set;
        }
    }
}

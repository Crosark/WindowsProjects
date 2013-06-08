using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AsynchronousTasks.Resources;
using System.Threading.Tasks;

namespace AsynchronousTasks
{
    public partial class MainPage : PhoneApplicationPage
    {
        List<Action> todoList = new List<Action>();
        List<StackPanel> UIElements = new List<StackPanel>();

        public MainPage() {
            InitializeComponent();

            // Setup dummy tasks
            todoList.Add(() => boringTask());
            todoList.Add(() => boringTask());
            todoList.Add(() => boringTask());
            todoList.Add(() => boringTask());

            // Setup UI Elements
            UIElements.Add(Task1Panel);
            UIElements.Add(Task2Panel);
            UIElements.Add(Task3Panel);
            UIElements.Add(Task4Panel);
        }

        private void boringTask() {
            for (int i = 0; i < 1000000000; i++) ;
            Console.WriteLine("Executing Task...");
        }

        public async void performAllTasks() {
            for (int k = 0; k < todoList.Count; k++) {
                
                // pre-task stuff
                Image bullet = UIElements[k].Children[0] as Image;
                Image chk = UIElements[k].Children[1] as Image;
                bullet.Visibility = System.Windows.Visibility.Visible;
                chk.Visibility = System.Windows.Visibility.Collapsed;
                
                // perform the task, wait for it to get over
                Action taskTodo = todoList[k];
                await Task.Run(()=>taskTodo());

                // post-task stuff
                bullet.Visibility = System.Windows.Visibility.Collapsed;
                chk.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e) {
            performAllTasks();
        }
    }
}
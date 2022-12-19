using Note.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Note
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NoteAddingPage), typeof(NoteAddingPage));
            Routing.RegisterRoute(nameof(TaskAddingPage), typeof(TaskAddingPage));
            Routing.RegisterRoute(nameof(NotificationAddingPage), typeof(NotificationAddingPage));
        }
    }
}

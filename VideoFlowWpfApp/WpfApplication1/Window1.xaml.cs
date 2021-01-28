using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b!=null)
            {
                MediaElement m = b.Content as MediaElement;
                if (m != null)
                {
                    if (b.Tag == null)
                    {
                        m.Play();
                    }
                    else
                    {
                        Boolean isPlaying = (Boolean)b.Tag;
                        isPlaying ^= true;
                        if (isPlaying)
                        {
                            m.Play();
                        }
                        else
                        {
                            m.Pause();
                        }
                        b.Tag = (Boolean)isPlaying;
                    }
                }
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Position = new TimeSpan(0, 0, 0, 0, 0);
            base.Tag = false;
        }
    }
}

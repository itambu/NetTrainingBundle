using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace WpfApplication1
{
    class MyButton : Button
    {
        private MediaElement media;

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            Media = newContent as MediaElement;
        }

        public MediaElement Media
        {
            get { return media; }
            set 
            { 
                media = value;
                value.LoadedBehavior = MediaState.Manual;
                value.MediaEnded += MediaEndedHandler;
            }
        }

        
        //public static readonly DependencyProperty MediaProperty;

        //static MyButton()
        //{
        //    FrameworkPropertyMetadata fpmd = new FrameworkPropertyMetadata(
        //        new MediaElement()
        //        );
        //    MediaProperty = DependencyProperty.Register(
        //        "Media",
        //        typeof(MediaElement),
        //        typeof(MyButton), fpmd);
        //}

        public MyButton()
        {
            Click += ClickHandler;
        }

        protected void MediaEndedHandler(object sender, EventArgs e)
        {
            this.IsPlaying = false;
            Media.Position = new TimeSpan(0, 0, 0, 0, 0);
        }

        protected void ClickHandler(object sender, EventArgs e)
        {
            this.IsPlaying ^= true; 
        }


        //public MediaElement Media
        //{
        //    get { return (MediaElement)GetValue(MediaProperty); }
        //    set 
        //    {
        //        if (value != null)
        //        {
        //            this.AddChild(value);
        //            value.LoadedBehavior = MediaState.Manual;
        //            value.MediaEnded += MediaEndedHandler;
        //        }
        //        SetValue(MediaProperty, value);
        //    }
        //}

        bool isPlaying =false;
        public bool IsPlaying
        {
            get
            {
                return this.isPlaying;
            }
            set
            {
                if (value)
                {
                    Media.Play();
                }
                else
                {
                    Media.Pause();
                }
                this.isPlaying = value;
            }
        }
    }
}

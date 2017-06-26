using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TexasHoldemClient.BusinessLayer.Models;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace TexasHoldemClient.PL.UserControls
{
    /// <summary>
    /// Interaction logic for CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        public Card Card
        {
            get { return (Card)GetValue(CardProperty); }
            set {
                LinkedList<CardType> adapterList = new LinkedList<CardType>();
                adapterList.AddFirst(CardType.Club);
                adapterList.AddLast(CardType.Spade);
                adapterList.AddLast(CardType.Heart);
                adapterList.AddLast(CardType.Diamond);
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Resorces/"
                    + ((int)value.number
                    + LinkedListExt.IndexOf(adapterList, value.type) + 1)
                    + ".png";

                model.SetImageData(File.ReadAllBytes(path));
                SetValue(CardProperty, value); DataContext = value; }
        }

      

        public static DependencyProperty CardProperty = DependencyProperty.Register("Card", typeof(Card), typeof(Card));


        MainModel model = new MainModel();
        public CardControl()
        {
            DataContext = Card; 
            InitializeComponent();

            
            PhotoSpace.DataContext = model;


        }

     
    }
    public static class LinkedListExt
    {
        public static int IndexOf<T>(this LinkedList<T> list, T item)
        {
            var count = 0;
            for (var node = list.First; node != null; node = node.Next, count++)
            {
                if (item.Equals(node.Value))
                    return count;
            }
            return -1;
        }
    }

    class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetImageData(byte[] data)
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = new MemoryStream(data);
            source.EndInit();

            // use public setter
            ImageSource = source;
        }

        ImageSource imageSource;
        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

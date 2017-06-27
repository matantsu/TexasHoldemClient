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

namespace TexasHoldemClient.PL.UserControls
{
    /// <summary>
    /// Interaction logic for ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {
        IEnumerable<ChatMessage> chat;
        public ChatControl()
        {
            InitializeComponent();
            DataContext = chat;
            
        }

        internal void setChat(IEnumerable<ChatMessage> chat)
        {
            this.chat = chat;
        }
    }
}

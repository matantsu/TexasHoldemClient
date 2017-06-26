using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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
using TexasHoldemClient.BusinessLayer;

namespace TexasHoldemClient.PL.UserControls
{

    /// <summary>
    /// Interaction logic for TextBoxExtention.xaml
    /// </summary>
    public partial class TextBoxExtention : UserControl
    {
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string Text
        {
            get {  return TextBlock_Title.Text; }
            set { TextBlock_Title.Text = value; }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string UserInput
        {
            get { return TextBox_Content.Text; }
            set { TextBox_Content.Text = value; }
        }


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string ErrorMsg
        {
            get { return TextBlock_Error.Text; }
            set { TextBlock_Error.Text = value; }
        }

        
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Func<string, bool> IsValid { get; set; }

        public TextBoxExtention()
        {
            InitializeComponent();
            ErrorMsg = "⚠ empty field";
            IsValid = DefualtValidation;
        }

        public bool ValidateInput()
        {
            if (IsValid(UserInput))
            {
                HideError();
                return true;
            }
            else
            {
                ShowError();
                return false;
            }
                
        }

        public virtual bool DefualtValidation(string text)
        {
            return !String.IsNullOrWhiteSpace(text);
        }
        
        private void TextBox_Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateInput();
        }

        public void ShowError()
        {
            TextBlock_Error.Visibility = Visibility.Visible;
        }

        public void HideError()
        {
            TextBlock_Error.Visibility = Visibility.Collapsed;
        }

      
        private void TextBox_Content_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBlock_Title.Foreground = Brushes.Black;
        }

        private void TextBox_Content_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBlock_Title.Foreground = Brushes.Blue;
        }

        private void TextBox_Content_Error(object sender, ValidationErrorEventArgs e)
        {

        }
    }
}

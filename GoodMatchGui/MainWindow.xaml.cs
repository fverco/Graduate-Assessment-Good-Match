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
using MatchUpLibrary;

namespace GoodMatchGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name1 = TextBoxName1.Text, name2 = TextBoxName2.Text;

            if (name1 != "" && name2 != "")
            {
                MatchResult? result = PlayerMatcher.MatchPlayers(name1, name2);

                if (result != null)
                {
                    TextBoxResults.Text = result.Value.resultMessage;
                }
                else
                {
                    MessageBox.Show("One or both names contain non - alphabetic characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please provide a Name 1 and a Name 2.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

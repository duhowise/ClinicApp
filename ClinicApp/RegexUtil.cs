using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ClinicApp
{
    public static class RegexUtil
    {
        private static void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
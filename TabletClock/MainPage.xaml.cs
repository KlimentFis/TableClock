using System;
using Xamarin.Forms;

namespace TabletClock
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            PopulateGridWithColors();
        }

        private void PopulateGridWithColors()
        {
            var random = new Random();

            for (int row = 0; row < 21; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    // Создание нового лейбла с случайным цветом фона
                    var color = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
                    var label = new Label
                    {
                        BackgroundColor = color,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };

                    // Установка строки и столбца для лейбла
                    Grid.SetRow(label, row);
                    Grid.SetColumn(label, column);

                    // Добавление лейбла в сетку
                    MainGrid.Children.Add(label);
                }
            }
        }
    }
}

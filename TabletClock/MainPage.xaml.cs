using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TabletClock
{
    public partial class MainPage : ContentPage
    {
        private readonly Dictionary<char, List<Tuple<int, int>>> charPatterns = new Dictionary<char, List<Tuple<int, int>>>
        {
            { '0', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(0,1), Tuple.Create(0,2), Tuple.Create(0,3), Tuple.Create(0,4), Tuple.Create(1,0), Tuple.Create(1,4), Tuple.Create(2,0), Tuple.Create(2,1), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { '1', new List<Tuple<int, int>> { Tuple.Create(2,0), Tuple.Create(2,1), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { '2', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(0,2), Tuple.Create(0,3), Tuple.Create(0,4), Tuple.Create(1,0), Tuple.Create(1,2), Tuple.Create(1,4), Tuple.Create(2,0), Tuple.Create(2,1), Tuple.Create(2,2), Tuple.Create(2,4) } },
            { '3', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(0,2), Tuple.Create(0,4), Tuple.Create(1,0), Tuple.Create(1,2), Tuple.Create(1,4), Tuple.Create(2,0), Tuple.Create(2,1), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { '4', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(0,1), Tuple.Create(0,2), Tuple.Create(1,2), Tuple.Create(2,0), Tuple.Create(2,1), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { '5', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(0,1), Tuple.Create(0,2), Tuple.Create(0,4), Tuple.Create(1,0), Tuple.Create(1,2), Tuple.Create(1,4), Tuple.Create(2,0), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { '6', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(0,1), Tuple.Create(0,2), Tuple.Create(0,3), Tuple.Create(0,4), Tuple.Create(1,0), Tuple.Create(1,2), Tuple.Create(1,4), Tuple.Create(2,0), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { '7', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(1,0), Tuple.Create(2,0), Tuple.Create(2,1), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { '8', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(0,1), Tuple.Create(0,2), Tuple.Create(0,3), Tuple.Create(0,4), Tuple.Create(1,0), Tuple.Create(1,2), Tuple.Create(1,4), Tuple.Create(2,0), Tuple.Create(2,1), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { '9', new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(0,1), Tuple.Create(0,2), Tuple.Create(0,4), Tuple.Create(1,0), Tuple.Create(1,2), Tuple.Create(1,4), Tuple.Create(2,0), Tuple.Create(2,1), Tuple.Create(2,2), Tuple.Create(2,3), Tuple.Create(2,4) } },
            { ':', new List<Tuple<int, int>> { Tuple.Create(1,1), Tuple.Create(1,3) } }
        };

        public MainPage()
        {
            InitializeComponent();
            PopulateGridWithTime();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                PopulateGridWithTime();
                return true; // True = Repeat again, False = Stop the timer
            });
        }

        private void PopulateGridWithTime()
        {
            MainGrid.Children.Clear();

            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            int elementCount = currentTime.Length;

            int startRow = (21 - (elementCount * 5 + (elementCount - 1))) / 2; // Центрирование по вертикали
            int startCol = (9 - 3) / 2; // Центрирование по горизонтали

            for (int i = 0; i < elementCount; i++)
            {
                char currentChar = currentTime[i];
                DrawChar(currentChar, startRow + i * 6, startCol); // Смещение по вертикали
            }
        }

        private void DrawChar(char character, int startRow, int startCol)
        {
            if (!charPatterns.ContainsKey(character)) return;

            var pattern = charPatterns[character];

            foreach (var point in pattern)
            {
                int x = startRow + point.Item1;
                int y = startCol + point.Item2;

                if (x >= 0 && x < 21 && y >= 0 && y < 9)
                {
                    var boxView = new BoxView
                    {
                        Color = GetRainbowColor(x, y),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };

                    Grid.SetRow(boxView, x);
                    Grid.SetColumn(boxView, y);
                    MainGrid.Children.Add(boxView);
                }
            }
        }

        private Color GetRainbowColor(int row, int col)
        {
            return Color.FromRgb(
                (int)(255.0 / 21 * row), // Используем исходные координаты
                (int)(255.0 / 9 * col),
                (int)(255.0 / (21 * 9) * (row * col)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CaptchaGenerationLibrary
{
    public class CaptchaGeneration
    {
        public string captcha;

        /// <summary>
        /// Стиль текствого блока
        /// </summary>
        /// <param name="textBlock">Объект текстового блока</param>
        /// <returns>Стиль текста</returns>
        private TextBlock TBlockStyle(TextBlock textBlock, Random random)
        {
            int style = random.Next(1, 3);
            switch (style)
            {
                case 1:
                    textBlock.FontStyle = FontStyles.Oblique;
                    break;
                case 2:
                    textBlock.FontStyle = FontStyles.Normal;
                    break;
                case 3:
                    textBlock.FontStyle = FontStyles.Italic;
                    break;
                default:
                    break;
            }
            return textBlock;
        }
        /// <summary>
        /// Создаем уникальные свойства для «Текстовый блок»
        /// </summary>
        /// <param name="textBlock">Объект текстового блока</param>
        /// <param name="algorithm">Алгоритм</param>
        /// <returns>Текстовый блок с уникальными свойствами</returns>
        public TextBlock TBlock(TextBlock textBlock, char algorithm, Random random, Brush brush)
        {
            textBlock.Text += algorithm;
            // Размер текста
            textBlock.FontSize = random.Next(9, 18);
            // Отступы
            textBlock.Margin = new Thickness(4, 4, 4, 4);
            // Поворот капчи
            textBlock.RenderTransformOrigin = new Point(0.5, 0.5);
            SkewTransform skewTransform = new SkewTransform(random.Next(0, 35), 0);
            // Размер капчи
            textBlock.RenderTransform = skewTransform;
            RotateTransform rotateTransform = new RotateTransform(random.Next(-35, 35));
            textBlock.RenderTransform = rotateTransform;
            // Цвет капчи
            textBlock.Foreground = brush;
            // Рандомная выборка толщины текста
            int weights = random.Next(1, 7);
            switch (weights)
            {
                case 1:
                    textBlock.FontWeight = FontWeights.UltraLight;
                    TBlockStyle(textBlock, random);
                    break;
                case 2:
                    textBlock.FontWeight = FontWeights.Thin;
                    TBlockStyle(textBlock, random);
                    break;
                case 3:
                    textBlock.FontWeight = FontWeights.Light;
                    TBlockStyle(textBlock, random);
                    break;
                case 4:
                    textBlock.FontWeight = FontWeights.Thin;
                    TBlockStyle(textBlock, random);
                    break;
                case 5:
                    textBlock.FontWeight = FontWeights.UltraBlack;
                    TBlockStyle(textBlock, random);
                    break;
                case 6:
                    textBlock.FontWeight = FontWeights.Black;
                    TBlockStyle(textBlock, random);
                    break;
                case 7:
                    textBlock.FontWeight = FontWeights.DemiBold;
                    TBlockStyle(textBlock, random);
                    break;
            }
            return textBlock;
        }

        /// <summary>
        /// Создаем капчу
        /// </summary>
        /// <param name="panel">Панель для привязки</param>
        public void Generation(StackPanel panel)
        {
            // Объект рандома
            Random rand = new Random();
            // Цветовая палитра | Область капчи
            Color randomColor = Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
            // Задаем заливку области капчи
            panel.Background = new SolidColorBrush(randomColor);
            // Входные данные
            string capitalAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string normalAlphabet = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";
            string symbols = "!#$%&()*+./:;=>?@[\\]^`{|}~'\\";
            // Длина капчи
            int length = rand.Next(8, 16);
            for (int i = 0; i < length; i++)
            {
                // Цветовая палитра | Текста капчи
                Color randomColor2 = Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
                // Заливка цвета
                Brush fillingText = new SolidColorBrush(randomColor2);
                // Создаем объект текстового блока
                TextBlock textBlock = new TextBlock();
                int index_1 = rand.Next(capitalAlphabet.Length);
                int index_2 = rand.Next(normalAlphabet.Length);
                int index_3 = rand.Next(numbers.Length);
                int index_4 = rand.Next(symbols.Length);
                int state = rand.Next(4);
                // Создание уникальной комбинации
                switch (state)
                {
                    case 0:
                        TBlock(textBlock, capitalAlphabet[index_1], rand, fillingText);
                        panel.Children.Add(textBlock);
                        captcha += capitalAlphabet[index_1];
                        break;
                    case 1:
                        TBlock(textBlock, normalAlphabet[index_2], rand, fillingText);
                        panel.Children.Add(textBlock);
                        captcha += normalAlphabet[index_2];
                        break;
                    case 2:

                        TBlock(textBlock, numbers[index_3], rand, fillingText);
                        panel.Children.Add(textBlock);
                        captcha += numbers[index_3];
                        break;
                    case 3:

                        TBlock(textBlock, symbols[index_4], rand, fillingText);
                        panel.Children.Add(textBlock);
                        captcha += symbols[index_4];
                        break;
                }
            }
        }
    }
}

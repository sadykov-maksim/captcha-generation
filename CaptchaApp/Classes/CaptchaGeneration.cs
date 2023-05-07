using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CaptchaApp.Classes
{
    public class CaptchaGeneration
    {
        public string captcha;
        public bool skip = false;
        /// <summary>
        /// Стиль текствого блока
        /// </summary>
        /// <param name="textBlock">Объект текстового блока</param>
        /// <returns>Стиль текста</returns>
        private TextBlock TBlockStyle(TextBlock textBlock,  Random random)
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
            textBlock.FontSize = random.Next(9, 18);
            textBlock.Margin = new Thickness(4, 4, 4, 4);
            // Поворот капчи
            textBlock.RenderTransformOrigin = new Point(0.5, 0.5); // Центральная точка поворота
            SkewTransform skewTransform = new SkewTransform(random.Next(0, 35), 0);
            textBlock.RenderTransform = skewTransform;
            RotateTransform rotateTransform = new RotateTransform(random.Next(-35, 35));
            textBlock.RenderTransform = rotateTransform;
            //
            textBlock.Foreground = brush;
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
        public void Generation(StackPanel panel) {
            Random rand = new Random();
            Color randomColor = Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
            panel.Background = new SolidColorBrush(randomColor);

            string capitalAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string normalAlphabet = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";
            string symbols = "!#$%&()*+./:;=>?@[\\]^`{|}~'\\";
            int length = rand.Next(8, 16);
            for (int i = 0; i < length; i++)
            {
                Color randomColor2 = Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
                Brush fillingText = new SolidColorBrush(randomColor2);
                TextBlock textBlock = new TextBlock();
                int index = rand.Next(capitalAlphabet.Length);
                int index2 = rand.Next(normalAlphabet.Length);
                int index3 = rand.Next(numbers.Length);
                int index4 = rand.Next(symbols.Length);
                int state = rand.Next(4);
                switch (state)
                {
                    case 0:
                        TBlock(textBlock, capitalAlphabet[index], rand, fillingText);
                        panel.Children.Add(textBlock);
                        captcha += capitalAlphabet[index];
                        break;
                    case 1:
                        TBlock(textBlock, normalAlphabet[index2], rand, fillingText);
                        panel.Children.Add(textBlock);
                        captcha += normalAlphabet[index2];
                        break;
                    case 2:
                      
                        TBlock(textBlock, numbers[index3], rand, fillingText);
                        panel.Children.Add(textBlock);
                        captcha += numbers[index3];
                        break;
                    case 3:
                       
                        TBlock(textBlock, symbols[index4], rand, fillingText);
                        panel.Children.Add(textBlock);
                        captcha += symbols[index4];
                        break;
                }
            }
        }
    }
}

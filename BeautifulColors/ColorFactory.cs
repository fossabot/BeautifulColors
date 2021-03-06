﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BeautifulColors
{
    public class ColorFactory
    {
        Random random;
        public ColorFactory(int? randomSeed = null)
        {
            if (randomSeed.HasValue)
            {
                random = new Random(randomSeed.Value);
            }
            else
            {
                random = new Random(DateTime.UtcNow.TimeOfDay.Milliseconds);
            }
        }

        public IEnumerable<Color> Random(int count = 1, NamedColors? hue = null)
        {
            var randomColors = new List<Color>(count);
            if (hue == null)
            {
                var randomValues = new byte[3];
                for (int i = 0; i < count; i++)
                {
                    random.NextBytes(randomValues);
                    randomColors.Add(Color.FromRgb(randomValues[0], randomValues[1], randomValues[2]));
                }
            }
            else
            {
                var seed = Color.FromNamedColor(hue.Value);
                for (int i = 0; i < count; i++)
                {
                    randomColors.Add(Color.FromHSV(seed.HSV.Item1, random.NextDouble(), random.NextDouble()));
                }
            }
            return randomColors;
        }

        public IEnumerable<Color> RandomBeautiful(int count = 1, NamedColors? hue = null)
        {
            var randomColors = new List<Color>(count);
            if (hue == null)
            {
                var randomValues = new byte[3];
                for (int i = 0; i < count; i++)
                {
                    randomColors.Add(Color.FromHSV(random.Next(0, 360), random.NextDouble() * .5 + .5, random.NextDouble() * .7 + .3));
                }
            }
            else
            {
                var seed = Color.FromNamedColor(hue.Value);
                for (int i = 0; i < count; i++)
                {
                    randomColors.Add(Color.FromHSV(seed.HSV.Item1 - 5 + random.Next(0, 10), random.NextDouble() * .5 + .5, random.NextDouble() * .4 + .6));
                }
            }
            return randomColors;
        }

        public IEnumerable<NamedColors> RandomNamed(int count = 1)
        {
            var namedColors = new List<NamedColors>(count);
            var values = Enum.GetValues(typeof(NamedColors));
            for (int i = 0; i < count; i++)
            {
                namedColors.Add((NamedColors)values.GetValue(random.Next(values.Length)));
            }
            return namedColors;
        }
    }
}

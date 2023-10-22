using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ejercicio 1(Bear Color)
            IFilter flg = (IFilter) new FilterGreyscale();
            IFilter fln = (IFilter) new FilterNegative();

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");

            PipeSerial pis = new PipeSerial(flg, new PipeSerial(fln, new PipeNull()));
            IPicture pi1 = pis.Send(picture);

            PipeSerial pis2 = (PipeSerial) pis.Next;
            IPicture pi2 = pis2.Send(pi1);

            PipeNull pn = (PipeNull) pis2.Next;
            IPicture pi3 = pn.Send(pi2);

            provider.SavePicture(pi3, @"beer2.jpg");

            // Ejercicio 2(Save Photos)
            string baseFolder = "./trans/";

            provider.SavePicture(pi1, baseFolder + @"beer_after_filter_negative.jpg");
            provider.SavePicture(picture, baseFolder + @"beer_base_picture.jpg");
            provider.SavePicture(pi2, baseFolder + @"beer_after_filter_greyscale.jpg");
        }
    }
}

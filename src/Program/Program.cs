using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            //(Bear Color) 1
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

            //(Save Photos) 2
            string baseFolder = "./trans/";

            provider.SavePicture(pi1, baseFolder + @"beer_after_filter_negative.jpg");
            provider.SavePicture(picture, baseFolder + @"beer_base_picture.jpg");
            provider.SavePicture(pi2, baseFolder + @"beer_after_filter_greyscale.jpg");

            //(Twitter) 3
            FilterTwitter fit = new FilterTwitter(@"resParcial1.jpg");
            PipeSerial ps = new PipeSerial(fit, serial2);
            PipeSerial ps1 = new PipeSerial(resParcial1, ps);
        }
    }
}

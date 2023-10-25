using System;
using System.Drawing;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y la guarda.
    /// </remarks>
    public class FilterTwitter : IFilter
    {
        public string path;
        public string text;

        public FilterTwitter(string path, string text)
        {
            this.path = path;
            this.text = text;
        }

        /// Procesa la imagen pasada por parametro y guarda la imagen.
        /// </summary>
        /// <param name="image">La imagen que se va a guardar.</param>
        public IPicture Filter (IPicture picture)
        {
            IPicture result = picture.Clone();
            var twitter = new TwitterImage();
            Console.WriteLine(twitter.PublishToTwitter(this.text, this.path));
            return result;
        }
    }
}
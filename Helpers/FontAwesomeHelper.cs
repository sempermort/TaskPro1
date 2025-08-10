using Mapsui.Styles;
using SkiaSharp;
using System.Reflection;
namespace TaskPro1.Helpers
{
    public class FontAwesomeHelper
    {
        public static async Task<int> CreateFontAwesomePin(string iconCode, string fontPath, SKColor color, int size)
        {
            // Create a bitmap canvas
            var bitmap = new SKBitmap(size, size);

            using var canvas = new SKCanvas(bitmap);

            // Fill background (optional, remove if no background needed)
            canvas.Clear(SKColors.Transparent);

            // var fontData = File.ReadAllBytes(fontPath);
       
            // using var skData = SKData.CreateCopy(fontData);
            var typeface =await LoadFontAwesomeTypeface(fontPath);

            if (typeface == null)
            {
                throw new Exception("FontAwesome font not found.");
            }
            var skFont = new SKFont(typeface, size * 0.5f);
            // Prepare the paint with font and color
            var paint = new SKPaint
            {
                IsAntialias = true,
                Color = color
            };

            var textBounds = new SKRect();
            var iconCodeBytes = System.Text.Encoding.UTF8.GetBytes(iconCode);
            skFont.MeasureText(iconCode, out textBounds, paint);
            var textHeight = textBounds.Height;

            // Calculate the center positions
            var x = size / 2f; // Center horizontally
            var y = (size / 2f) + (textHeight / 1f); // Center vertically

            canvas.DrawText(iconCode, x, y,skFont,paint);


            // Convert bitmap to Mapsui's BitmapId
            return BitmapRegistry.Instance.Register(bitmap);

        }

        public static async Task<SKTypeface> LoadFontAwesomeTypeface(string path)
        {
            // Get the font path from resources
            var assembly = Assembly.GetExecutingAssembly();
            await using var  targetFile =await FileSystem.OpenAppPackageFileAsync(path);
           
            if (targetFile == null)
                throw new Exception("FontAwesome font file not found in resources.");

            // Load the typeface from the font file stream
            return SKTypeface.FromStream(targetFile);
        }
    }
}
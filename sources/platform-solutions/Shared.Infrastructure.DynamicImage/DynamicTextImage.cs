using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace Shared.Infrastructure.DynamicImage
{
	public sealed class DynamicTextImage
	{
		private string _backgroundHexColor = null;
		private readonly Func<Font> _fontInstanceCreator = null;
		private string _foregroundHexColor = null;
		private readonly int _height;
		private readonly int _width;

		public DynamicTextImage(int width, int height, string backgroundHexColor, string foregroundHexColor, string fontFamilyName = null)
		{
			if (width <= 0)
			{
				throw new ArgumentException(nameof(DynamicTextImage), nameof(width));
			}

			if (height <= 0)
			{
				throw new ArgumentException(nameof(DynamicTextImage), nameof(height));
			}

			_width = width;
			_height = height;

			_backgroundHexColor = backgroundHexColor ?? throw new ArgumentNullException(nameof(backgroundHexColor), nameof(DynamicTextImage));
			_foregroundHexColor = foregroundHexColor ?? throw new ArgumentNullException(nameof(foregroundHexColor), nameof(DynamicTextImage));
			_fontInstanceCreator = (() => new Font(fontFamilyName ?? "GenericSansSerif", 15, FontStyle.Regular, GraphicsUnit.Pixel));
		}

		public MemoryStream Build(string text)
		{
			text = text ?? "?";

			var stream = new MemoryStream();

			var backgroundColor = ColorTranslator.FromHtml($"#{_backgroundHexColor.Trim('#')}");
			var foreGroundColor = ColorTranslator.FromHtml($"#{_foregroundHexColor.Trim('#')}");

			using (var baseFont = _fontInstanceCreator())
			{
				using (var bitmap = new Bitmap(_width, _height, PixelFormat.Format24bppRgb))
				{
					using (var graphics = Graphics.FromImage(bitmap))
					{
						var textSize = graphics.MeasureString(text, baseFont);
						var fontScale = Math.Max(textSize.Width / bitmap.Width, textSize.Height / bitmap.Height);

						using (var scaledFont = new Font(baseFont.FontFamily, baseFont.SizeInPoints / fontScale, baseFont.Style, baseFont.Unit))
						{
							graphics.Clear(backgroundColor);
							graphics.SmoothingMode = SmoothingMode.AntiAlias;
							graphics.CompositingQuality = CompositingQuality.HighQuality;
							graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
							graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

							StringFormat strinngFormat = new StringFormat(StringFormat.GenericTypographic)
							{
								Alignment = StringAlignment.Center,
								LineAlignment = StringAlignment.Center,
								FormatFlags = StringFormatFlags.MeasureTrailingSpaces
							};

							textSize = graphics.MeasureString(text, scaledFont);

							graphics.DrawString(
								text,
								scaledFont,
								new SolidBrush(foreGroundColor),
								new RectangleF(
									0,
									0,
									bitmap.Width,
									bitmap.Height
								),
								strinngFormat
							);

							bitmap.Save(stream, ImageFormat.Png);

							stream.Position = 0;
						}
					}
				}
			}

			return stream;
		}
	}
}

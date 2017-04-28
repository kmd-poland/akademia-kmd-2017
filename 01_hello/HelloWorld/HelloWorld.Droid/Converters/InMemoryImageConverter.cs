using System;
using System.Globalization;
using System.IO;
using Android.Graphics;
using MvvmCross.Platform.Converters;

namespace Converters
{
	public class InMemoryImageConverter : MvxValueConverter<byte[], Bitmap>
	{
		protected override Bitmap Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || value.Length == 0) { return null; }
			return BitmapFactory.DecodeByteArray(value, 0, value.Length);
		}

		protected override byte[] ConvertBack(Bitmap value, Type targetType, object parameter, CultureInfo culture)
		{
			var stream = new MemoryStream();
			value.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
			return stream.ToArray();
		}
	}
}

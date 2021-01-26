using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TM.Infrastructure.TencentCloud.Api;

namespace TM.Core.Helpers
{
    public class PictureHelper
	{
		private readonly static PixelFormat[] pixelFormatArray = {
											PixelFormat.Format1bppIndexed
											,PixelFormat.Format4bppIndexed
											,PixelFormat.Format8bppIndexed
											,PixelFormat.Undefined
											,PixelFormat.DontCare
											,PixelFormat.Format16bppArgb1555
											,PixelFormat.Format16bppGrayScale
										};

		private static bool IsIndexedPixelFormat(System.Drawing.Imaging.PixelFormat imagePixelFormat)
		{
			foreach (PixelFormat pf in pixelFormatArray)
			{
				if (imagePixelFormat == pf)
					return true;
			}
			return false;
		}

		private static void CreateGraphics(Image data, out Image image, out Graphics graphics)
		{
			var tempImage = data;
			if (IsIndexedPixelFormat(tempImage.PixelFormat))
			{
				var bmp = new Bitmap(tempImage.Width, tempImage.Height, PixelFormat.Format32bppArgb);
				using (tempImage)
				{
					graphics = Graphics.FromImage(bmp);
					graphics.DrawImage(tempImage, 0, 0);
				}
				image = bmp;
			}
			else
			{
				image = tempImage;
				graphics = Graphics.FromImage(tempImage);
			}
		}

		/// <summary>
		/// 下载图片
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private static Image GetUrlImage(string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			using (WebResponse response = request.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					return Image.FromStream(response.GetResponseStream());
				}
			}
		}


		/// <summary>
		/// 获取文字水印位置  
		/// </summary>
		/// <param name="pos">
		///         1左上，2中上，3右上
		///         4左中，5中，  6右中
		///         7左下，8中下，9右下
		/// </param>
		/// <returns></returns>
		private static StringFormat GetStringFormat(int pos)
		{
			StringFormat format = new StringFormat();
			switch (pos)
			{
				case 1: format.Alignment = StringAlignment.Near; format.LineAlignment = StringAlignment.Near; break;
				case 2: format.Alignment = StringAlignment.Center; format.LineAlignment = StringAlignment.Near; break;
				case 3: format.Alignment = StringAlignment.Far; format.LineAlignment = StringAlignment.Near; break;
				case 4: format.Alignment = StringAlignment.Near; format.LineAlignment = StringAlignment.Center; break;
				case 6: format.Alignment = StringAlignment.Far; format.LineAlignment = StringAlignment.Center; break;
				case 7: format.Alignment = StringAlignment.Near; format.LineAlignment = StringAlignment.Far; break;
				case 8: format.Alignment = StringAlignment.Center; format.LineAlignment = StringAlignment.Far; break;
				case 9: format.Alignment = StringAlignment.Far; format.LineAlignment = StringAlignment.Far; break;
				default: format.Alignment = StringAlignment.Center; format.LineAlignment = StringAlignment.Center; break;
			}
			return format;
		}

		/// <summary>
		/// 给图片加入文字水印,且设置水印透明度
		/// </summary>
		/// <param name="imgUrl"></param>
		/// <param name="saleText">售价或者活动价  文本</param>
		///  <param name="saleValue">售价或者活动价  值</param>
		/// <param name="originalText">原价</param>
		/// <returns></returns>
		public static byte[] DrawWaterTextAsync(string imgUrl, string saleText, string saleValue, string originalText)
		{
			var image = GetUrlImage(imgUrl);
			if (image == null)
				return null;

			int padding = 0;
			int height = 60;

			CreateGraphics(image, out var srcImage, out var graphics);
			MakeThumNail(ref srcImage, out graphics, 500, 400, "Cut");
			using (srcImage)
			{
				using (graphics)
				{
					//设置高质量查值法
					graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					//设置高质量，低速度呈现平滑程度
					graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

					var with = (int)(srcImage.Width * 0.5);


					//---------------------------原价块-----------------------------------

					var format = GetStringFormat(4);

					var originalValidRect = new Rectangle(with, srcImage.Height - height, srcImage.Width - with, height);

					//原价背景颜色
					var originalBackGroupBrush = new SolidBrush(Color.FromArgb(255, 242, 108));
					graphics.FillRectangle(originalBackGroupBrush, originalValidRect);

					//原价
					originalValidRect = new Rectangle(with + 40, srcImage.Height - height, srcImage.Width - with, height);
					var originalTextBrush = new SolidBrush(Color.FromArgb(135, 135, 135));
					var font = new Font("微软雅黑", (height / 2) - 8, FontStyle.Regular, GraphicsUnit.Pixel);//统一尺寸
					graphics.DrawString(originalText, font, originalTextBrush, originalValidRect, format);

					//原价横线
					//var lineWith = (int)(srcImage.Width * 0.75);
					//var lineHeght = srcImage.Height - height / 2;
					//graphics.DrawLine(new Pen(Color.Black, 2), lineWith - originalText.Length * height / 6, lineHeght, lineWith + originalText.Length * height / 6, lineHeght);


					//---------------------------售价块-----------------------------------

					var saleValidRect = new Rectangle(padding, srcImage.Height - height, with / 2, height);

					//售价背景颜色
					format = GetStringFormat(6);
					var saleBackGroupBrush = new SolidBrush(Color.FromArgb(255, 90, 107));
					graphics.FillRectangle(saleBackGroupBrush, saleValidRect);

					//售价文字
					var salesTextBrush = new SolidBrush(Color.White);
					font = new Font("微软雅黑", height / 2 - 8, FontStyle.Regular, GraphicsUnit.Pixel);//统一尺寸
					saleValidRect = new Rectangle(padding, srcImage.Height - height, with / 2 - 30, height);
					graphics.DrawString(saleText, font, salesTextBrush, saleValidRect, format);

					//售价
					saleValidRect = new Rectangle(with / 2 - 31, srcImage.Height - height, with / 2 + 31, height);

					format = GetStringFormat(4);
					saleBackGroupBrush = new SolidBrush(Color.FromArgb(255, 90, 107));
					graphics.FillRectangle(saleBackGroupBrush, saleValidRect);

					salesTextBrush = new SolidBrush(Color.White);
					font = new Font("微软雅黑", height / 2 + 5, FontStyle.Regular, GraphicsUnit.Pixel);//统一尺寸
					graphics.DrawString(saleValue, font, salesTextBrush, saleValidRect, format);

					//srcImage.Save("D:\\Desktop\\water1.jpg", ImageFormat.Jpeg);

					MemoryStream ms = new MemoryStream();
					srcImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
					return ms.ToArray();
				}
			}
		}

		/// <summary>
		/// 缩放图像
		/// </summary>
		/// <param name="url"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		public static async Task<byte[]> MakeThumNail(string url, int width, int height, string model)
		{
			var bytes = await CloudInfiniteApi.GetImage(url);
			var memStream = new MemoryStream(bytes);
			var img = Image.FromStream(memStream);

			MakeThumNail(ref img, out var graphics, width, height, model);

			MemoryStream ms = new MemoryStream();
			img.Save(ms, ImageFormat.Jpeg);
			return ms.ToArray();
		}

		/// <summary>
		/// 缩放图像
		/// </summary>
		/// <param name="originalImage">原始图片</param>
		/// <param name="graphics">原始画板</param>
		/// <param name="width">缩放图的宽</param>
		/// <param name="height">缩放图的高</param>
		/// <param name="model">缩放模式</param>
		private static void MakeThumNail(ref Image originalImage, out Graphics graphics,int width, int height, string model)
		{
			int thumWidth = width;      //缩略图的宽度
			int thumHeight = height;    //缩略图的高度

			int x = 0;
			int y = 0;

			int originalWidth = originalImage.Width;    //原始图片的宽度
			int originalHeight = originalImage.Height;  //原始图片的高度

			switch (model)
			{
				case "HW":      //指定高宽缩放,可能变形
					break;
				case "W":       //指定宽度,高度按照比例缩放
					thumHeight = originalImage.Height * width / originalImage.Width;
					break;
				case "H":       //指定高度,宽度按照等比例缩放
					thumWidth = originalImage.Width * height / originalImage.Height;
					break;
				case "Cut":
					if ((double)originalImage.Width / (double)originalImage.Height > (double)thumWidth / (double)thumHeight)
					{
						originalHeight = originalImage.Height;
						originalWidth = originalImage.Height * thumWidth / thumHeight;
						y = 0;
						x = (originalImage.Width - originalWidth) / 2;
					}
					else
					{
						originalWidth = originalImage.Width;
						originalHeight = originalWidth * height / thumWidth;
						x = 0;
						y = (originalImage.Height - originalHeight) / 2;
					}
					break;
				default:
					break;
			}

			//新建一个bmp图片
			System.Drawing.Image bitmap = new System.Drawing.Bitmap(thumWidth, thumHeight);

			//新建一个画板
			System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);

			//设置高质量查值法
			graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

			//设置高质量，低速度呈现平滑程度
			graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			//清空画布并以透明背景色填充
			graphic.Clear(System.Drawing.Color.Transparent);

			//在指定位置并且按指定大小绘制原图片的指定部分
			graphic.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight), new System.Drawing.Rectangle(x, y, originalWidth, originalHeight), System.Drawing.GraphicsUnit.Pixel);

			//bitmap.Save("D:\\Desktop\\water.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
			originalImage.Dispose();

			originalImage = bitmap;
			graphics = graphic;
		}
	}
}

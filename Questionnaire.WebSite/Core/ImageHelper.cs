using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Sobits.Story.Logic.Repo;
using Sobits.Story.Logic.BLL;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace Questionnaire.WebSite.Core
{
    public class ImageHelper
    {
        #region public methods

        ///// <summary>
        ///// Generate thumbnail image
        ///// </summary>
        ///// <param name="imageStream"></param>
        ///// <param name="previewFormatId"></param>
        ///// <returns></returns>
        //public static Byte[] GetThumbnailImage(Stream imageStream, Int32 previewFormatId)
        //{
        //    var formatRepo = RepoFactory.Instance.GetRepo<IImagePreviewFormatRepo>();
        //    var format = formatRepo.Get(previewFormatId);

        //    Int32 imageWidth = format.ImageWidth;
        //    Int32 imageHeight = format.ImageHeight;

        //    Image image = System.Drawing.Image.FromStream(imageStream);

        //    Int32 widthTh = imageWidth;
        //    Int32 heightTh = imageHeight;

        //    Bitmap bp = new Bitmap(widthTh, heightTh);
        //    Graphics g = Graphics.FromImage(bp);
        //    g.SmoothingMode = SmoothingMode.HighQuality;
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //    Rectangle rect = new Rectangle(0, 0, widthTh, heightTh);
        //    g.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

        //    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        //    ImageCodecInfo codec = null;
        //    for (int i = 0; i < codecs.Length; i++)
        //    {
        //        if (codecs[i].MimeType.Equals("image/jpeg"))
        //        {
        //            codec = codecs[i];

        //            break;
        //        }
        //    }

        //    EncoderParameters ep = new EncoderParameters();
        //    ep.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
        //    Bitmap bpResult = bp.Clone(new Rectangle(0, 0, widthTh, heightTh), bp.PixelFormat);

        //    Byte[] data;
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        bpResult.Save(stream, ImageFormat.Jpeg);
        //        stream.Position = 0;
        //        data = new Byte[stream.Length];
        //        stream.Read(data, 0, Convert.ToInt32(stream.Length));
        //        stream.Close();
        //    }

        //    bpResult.Dispose();
        //    bp.Dispose();
        //    g.Dispose();

        //    return data;
        //}

        #endregion public methods
    }
}
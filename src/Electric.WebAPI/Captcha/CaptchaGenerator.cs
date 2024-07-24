using SkiaSharp;

namespace Electric.WebAPI.Captcha
{
    /// <summary>
    /// 验证码生成
    /// </summary>
    public class CaptchaGenerator
    {
        /// <summary>
        ///  随机生成验证码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CreateValidateCode(int len)
        {
            // 可选字符集  
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // 创建一个新的随机数生成器  
            Random random = new Random();

            // 生成验证码  
            string code = new string(Enumerable.Repeat(chars, len)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }

        /// <summary>
        /// 生成验证码图片，并返回字节流
        /// </summary>
        /// <param name="code"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Byte[] GenerateCode(string code, int width, int height)
        {
            // 创建一个SkiaSharp画布  
            using (var surface = SKSurface.Create(new SKImageInfo(width, height)))
            {
                var canvas = surface.Canvas;

                // 清除画布  
                canvas.Clear(SKColors.White);

                // 使用SkiaSharp绘制验证码文本  
                using (var textPaint = new SKPaint())
                {
                    textPaint.Color = SKColors.Black;
                    textPaint.IsAntialias = true;
                    textPaint.TextSize = height * 0.8f; // 设置文本大小  
                    textPaint.StrokeWidth = 3;

                    var textBounds = new SKRect();
                    textPaint.MeasureText(code, ref textBounds);
                    var xText = (width - textBounds.Width) / 2;
                    var yText = (height - textBounds.Height) / 2 - textBounds.Top;

                    canvas.DrawText(code, xText, yText, textPaint);
                }

                // 绘制干扰线  
                using (var linePaint = new SKPaint())
                {
                    // 半透明黑色  
                    linePaint.Color = new SKColor(0, 0, 0, 128);
                    linePaint.StrokeWidth = 1;
                    linePaint.IsAntialias = true;

                    var random = new Random();
                    for (int i = 0; i < 5; i++) // 绘制5条干扰线  
                    {
                        float x1 = 0;
                        float y1 = random.Next(height);
                        float x2 = width;
                        float y2 = random.Next(height);
                        canvas.DrawLine(x1, y1, x2, y2, linePaint);
                    }
                }

                // 保存图像到文件  
                using (var image = surface.Snapshot())
                using (var data = image.Encode())
                {
                    return data.ToArray();
                }
            }
        }
    }
}

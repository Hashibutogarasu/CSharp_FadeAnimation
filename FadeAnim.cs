using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Twitter_CSharp
{
    static class FadeAnim
    {
        public async static Task Fade_out(this PictureBox control,int time = 10)
        {
            for(float t = 1F; t < 10F; t = t - 0.1F)
            {
                await control.setOpacity(t);
                await Task.Delay(time);
            }
        }

        public static Task setOpacity(this PictureBox control,float opacity)
        {
            //using System.Drawing;

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(control.Width, control.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            //画像を読み込む
            Image img = control.Image;

            //ColorMatrixオブジェクトの作成
            System.Drawing.Imaging.ColorMatrix cm =
                new System.Drawing.Imaging.ColorMatrix();
            //ColorMatrixの行列の値を変更して、アルファ値が0.5に変更されるようにする
            cm.Matrix00 = 1;
            cm.Matrix11 = 1;
            cm.Matrix22 = 1;
            cm.Matrix33 = opacity;
            cm.Matrix44 = 1;

            //ImageAttributesオブジェクトの作成
            System.Drawing.Imaging.ImageAttributes ia =
                new System.Drawing.Imaging.ImageAttributes();
            //ColorMatrixを設定する
            ia.SetColorMatrix(cm);

            //ImageAttributesを使用して画像を描画
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            //リソースを解放する
            img.Dispose();
            g.Dispose();

            //PictureBox1に表示する
            control.Image = canvas;

            return Task.CompletedTask;
        }
    }
}

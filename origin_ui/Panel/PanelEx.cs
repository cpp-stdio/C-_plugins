using System;
using System.Drawing;
using System.Windows.Forms;

namespace everywhere.origin_ui
{
    class PanelEx : Panel
    {
        public PanelEx()
        {
            
        }

        /// <summary>
        /// 枠線。
        /// </summary>
        private Color borderColor = Color.Black;

        /// <summary>
        /// 枠線
        /// </summary>
		public Color BorderColor
        {
            get { return this.borderColor; }
            set
            {
                this.borderColor = value;
            }
        }

        /// <summary>
        /// OnPaintイベント
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnPaint(PaintEventArgs e)
        {

            int right = this.ClientRectangle.Right - 1;
            int bottom = this.ClientRectangle.Bottom - 1;

            Pen pen = new Pen(this.borderColor);

            // 四角を描画
            Graphics g = this.CreateGraphics();
            g.DrawLine(pen, 0, 0, right, 0); // 上辺
            g.DrawLine(pen, 0, 0, 0, bottom); // 左辺
            g.DrawLine(pen, right, 0, right, bottom); // 右辺
            g.DrawLine(pen, 0, bottom, right, bottom); // 下辺
        }

        /// <summary>
        /// OnSizeChangedイベント
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            // 描画をクリア
            this.Refresh();

            base.OnSizeChanged(e);
        }

    }
}

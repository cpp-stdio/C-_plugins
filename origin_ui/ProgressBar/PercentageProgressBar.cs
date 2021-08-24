using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

// https://dobon.net/vb/dotnet/control/pbshowtext.html

namespace everywhere.origin_ui
{
    class PercentageProgressBar : ProgressBar
    {
        private int WM_PAINT = 0x000F;

        private int _percent = 0;
        public int Percent
        {
            get { return this._percent; }
        }

        [SecurityPermission(SecurityAction.Demand,Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                //表示する文字列を決定する
                double percent = (double)(this.Value - this.Minimum) / (double)(this.Maximum - this.Minimum);
                string displayText = string.Format("{0}%", percent * 100.0);
                
                //判定用変数に結果を代入
                this._percent = (int)(percent * 100.0);

                //追加で入力がある場合（あまり知られていないが、ProgressBarにもTextがある）
                if (Text != "")
                {
                    displayText += " : " + Text;
                }

                //文字列を描画する
                TextFormatFlags tff = TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.SingleLine;
                Graphics g = this.CreateGraphics();
                TextRenderer.DrawText(g, displayText, this.Font,
                    this.ClientRectangle, SystemColors.ControlText, tff);
                g.Dispose();
            }
        }
    }
}

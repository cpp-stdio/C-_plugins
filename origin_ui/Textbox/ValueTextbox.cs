using System.Windows.Forms;

namespace everywhere.origin_ui
{
    class ValueTextbox : PlaceholderTextBox
    {
        protected ErrorProvider error = new ErrorProvider();

        public ValueTextbox()
        {
            //エラーメッセージを整える
            error.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            KeyPress += Value_KeyPress;
            Leave += Value_Focus;
            LostFocus += Value_Focus;
        }

        private void Value_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            error.Clear();
            if (Text != "")
            {
                if (e.KeyChar == '-')
                {
                    error.SetError(this, "マイナスは先頭のみ有効");
                    e.Handled = true;
                }
            }
        }

        private void Value_Focus(object sender, System.EventArgs e)
        {
            if (Text == "") return;

            try
            {
                decimal value = decimal.Parse(Text);
                Text = value.ToString();
            }
            catch
            {
                error.SetError(this, "数値を入力してください");
                Text = "";
            }
        }
    }
}

using System.Windows.Forms;

namespace everywhere.origin_ui
{
    class IntegerTextbox : ValueTextbox
    {
        public IntegerTextbox()
        {
            KeyPress += Integer_KeyPress;
            //エラーメッセージを整える
            error.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
        }


        private void Integer_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '-')
            {
                //押されたキーが 0～9でない場合は、イベントをキャンセルする
                error.SetError(this, "半角数字のみ");
                e.Handled = true;
            }
        }

        private void Range(Label label)
        {
            var text = label.Text;
            decimal integer = decimal.Parse(text);
        }
    }
}

using System.Windows.Forms;

namespace everywhere.origin_ui
{
    class DoubleTextbox : ValueTextbox
    {
        public DoubleTextbox()
        {
            KeyPress += Double_KeyPress;
        }

        private void Double_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '.' && e.KeyChar != '\b' && e.KeyChar != '-')
            {
                //押されたキーが 0～9でない場合は、イベントをキャンセルする
                error.SetError(this, "半角数字と小数点のみ");
                e.Handled = true;
            }
        }

        private void Range(Label label)
        {
            decimal value = decimal.Parse(label.Text);

        }
    }
}

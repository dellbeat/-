using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace 教务系统是小妖精吗
{
    public partial class ClassForm : Form
    {
        string[,] b = new string[11, 6];
        public ClassForm(string year,string trem,string[,] data)
        {
            InitializeComponent();
            this.Text += "(" + year + trem + "课表)";
            Array.Copy(data,b,b.Length);
            CallSet();
        }

        private void SetClassValue()
        {
            int x, y;

            Regex Classname = new Regex("(?<=:).+",RegexOptions.Multiline);

            for (x = 0; x < 11; x++) 
            {
                for (y = 0; y < 6; y++) 
                {
                    if(b[x,y]!=null)
                    {
                        ((TextBox)(this.Controls.Find("textBox" + Convert.ToString(1 + y + x * 6), false)[0])).Text = Classname.Match(b[x, y]).Value;
                    }
                }
            }
        }

        private Task SetClassTask()
        {
            return Task.Run(() =>
            {
                SetClassValue();
            });
        }

        private async void CallSet()
        {
            await SetClassTask();
        }

        private void PrintClassInfo(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }
    }
}

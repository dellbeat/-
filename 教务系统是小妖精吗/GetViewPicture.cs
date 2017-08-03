using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.IO;

namespace 教务系统是小妖精吗
{
    public class GetViewPicture
    {
        Bitmap pic
        {
            get;
            set;
        }

        string status
        {
            get;
            set;
        }

        public GetViewPicture(CookieContainer cookies)
        {
            Uri uri = new Uri("http://ojjx.wzu.edu.cn/CheckCode.aspx");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.CookieContainer = cookies;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            status = Convert.ToString(response.StatusCode);
            Stream resStream = response.GetResponseStream();//得到验证码数据流
            Bitmap sourcebm = new Bitmap(resStream);//初始化Bitmap图片
            response.Close();
            pic = sourcebm;
        }

        public Bitmap GetPic()
        {
            return pic;
        }

        public string GetStatus()
        {
            return status;
        }
    }
}

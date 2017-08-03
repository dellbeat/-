using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Collections.Specialized;
using System.Web;
using System.Threading;

namespace 教务系统是小妖精吗
{
    public partial class LoginForm : Form
    {
        #region 功能模块节点声明
        struct mode
        {
            public string linkname;//如xskbcx（学生课表查询）
            public string modecode;//功能模块代码
            public string name;//对应中文菜单名称
        }
        CancellationTokenSource _cts = new CancellationTokenSource();
        List<mode> ModeList = new List<mode>();
        #endregion

        public LoginForm()
        {
            InitializeComponent();
            ModeList.Add(SetModeNode("xskbcx", "N121603", "学生个人课表(重要)"));
            CallRequestCode();
        }

        struct ClassNode
        {
            public string ClassName;
            public string ClassTime;
            public string Teacher;
            public string Location;
        }

        #region 初始化所需信息（如姓名、主页链接、帐号、页面数据等）
        CookieContainer cookie;
        string MainUrl = "http://ojjx.wzu.edu.cn/default2.aspx";
        string UserName = "";//登陆的用户姓名
        string UserAccount = "";//用户帐号（即学号）
        string PageCode = "";//页面数据
        string[,] ClassTable = new string[11, 6];//课程表
        List<string> lists = new List<string>();//年份选择
        ClassForm classform = null;//初始化窗体
        bool IsRedirect = false;
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if(StuAccount.Text==""||StuPassword.Text==""||ViewCode.Text=="")
            {
                MessageBox.Show("有未输入的内容，请检查后再试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string name = StuAccount.Text;
            string psw = StuPassword.Text;
            UserAccount = name;

            LoginParameter login = new LoginParameter();
            login.loginname = name;
            login.password = psw;
            login.secertcode = ViewCode.Text;
            
            
            CallLogin(login);
            
        }

        private async void ViewPic_Click(object sender, EventArgs e)
        {
            await RequestTask();
            //MessageBox.Show(Convert.ToString(ViewPic.Image.Height) + "," + Convert.ToString(ViewPic.Image.Width));//27,72
        }

        private async void TestView_Click(object sender, EventArgs e)
        {
            ClassTableGoal goal = new ClassTableGoal();
            goal.Year = Year.Text;
            goal.Trem = Time.Text;
            await GetTableAsync(goal);
            await GetTableTask();
            MessageBox.Show(ClassTable[0, 0]);
        }

        #region 请求验证码
        private void RequestSecertCode()
        {
            LoginCookies getcookie = new LoginCookies();
            if(getcookie.Getstatus()!="OK")
            {
                MessageBox.Show("无法请求Cookies，请稍候重试！");
                return;
            }
            cookie = getcookie.getcookies();
            GetViewPicture getpic = new GetViewPicture(cookie);
            if(getpic.GetStatus()!="OK")
            {
                MessageBox.Show("验证码图片加载失败，请检查网络后再试！");
                return;
            }
            ViewPic.Image = getpic.GetPic();          
        }
        #endregion

        #region 将验证码请求异步化，防止界面假死
        private Task RequestTask()
        {
            return Task.Run(() =>
            {
                RequestSecertCode();
            });
        }

        private async void CallRequestCode()
        {
            await RequestTask();
        }
        #endregion

        #region 将获取课表数据异步化，防止界面假死
        private Task<string> GetTableAsync(ClassTableGoal goal)
        {
            return Task.Run<string>(() =>
            {
                return GetClassTable(goal);
            });
        }
        #endregion

        #region 将课程表数据匹配异步化，防止界面假死
        private Task GetTableTask()
        {
            return Task.Run(() =>
            {
                   GetTalbeRegex();
            });
        }
        #endregion

        #region 获取并返回课表数据
        private string GetClassTable(ClassTableGoal classtable)
        {
            #region 创建变量并初始化正则表达式
            string postvalue = "";
            PageCode = Send_Get(SetUrl(ModeList[0]));
            string url = SetUrl(ModeList[0]);
            Regex input = new Regex("input type=\"hidden\" name=\".*?\" value=\".*?\"");
            Regex inputname = new Regex("name=\".*?\"");
            Regex inputvalue = new Regex("value=\".*?\"");
            Regex SelectYear = new Regex("<option selected=\"selected\" value=\"\\d+-\\d+\">");
            Regex SelectTerm = new Regex("<option selected=\"selected\" value=\"\\d\">");
            #endregion

            #region 检查请求的学期是否为当前学期 如是当前学期则直接返回数据
            Match YearMatch = SelectYear.Match(PageCode);
            Match TermMatch = SelectTerm.Match(PageCode);
            String Year = YearMatch.ToString();
            String Term = TermMatch.ToString();
            if(Year.IndexOf(classtable.Year)!=-1&&Term.IndexOf(classtable.Trem)!=-1)
            {
                return PageCode;
            }
            #endregion

            #region 构造发送的字符串
            MatchCollection inputmatch = input.Matches(PageCode);
            postvalue = inputname.Match(inputmatch[0].Value).ToString().Replace("name=\"", "").Replace("\"", "") + "=xnd&" + inputname.Match(inputmatch[1].Value).ToString().Replace("name=\"", "").Replace("\"", "") + "=&" + inputname.Match(inputmatch[2].Value).ToString().Replace("name=\"", "").Replace("\"", "") + "=" + inputvalue.Match(inputmatch[2].Value).ToString().Replace("name=\"", "").Replace("\"", "").Replace("+", "%2b") + "&xnd=";
            postvalue += classtable.Year + "-" + Convert.ToString(Convert.ToInt32(classtable.Year) + 1) + "&xqd=" + classtable.Trem;
            postvalue = postvalue.Replace("value=", "");
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postvalue);
            #endregion

            #region 构造参数结构
            SendPostLoginParameter send = new SendPostLoginParameter();
            send.Bytesarray = bytes;
            send.PostUrl = url;
            send.ReferUrl = url;
            send.NeedRedirect = false;
            #endregion

            PageCode = Send_Post(send);

            return PageCode;
        }
        #endregion

        #region GetClassTable函数参数结构
        struct ClassTableGoal
        {
            public string Year;
            public string Trem;
        }
        #endregion

        #region Login函数参数结构
        /// <summary>
        /// Login函数参数结构
        /// </summary>
        struct LoginParameter
        {
            public string loginname;
            public string password;
            public string secertcode;
        }
        #endregion

        #region Send_Post函数参数结构
        /// <summary>
        /// Send_Post函数传参结构
        /// </summary>
        struct SendPostLoginParameter
        {
            public string PostUrl;
            public string ReferUrl;
            public byte[] Bytesarray;
            public bool NeedRedirect;
        }
        #endregion

        #region 登陆主页函数
        /// <summary>
        /// 登陆主页函数
        /// </summary>
        /// <param name="ParaLogin"></param>
        /// <returns></returns>
        private string Login(LoginParameter ParaLogin)
        {
            #region 变量声明
            Regex reg = new Regex("<input type=\"hidden\" name=\"__VIEWSTATE\" value=\"(.*?)\" />");
            string postUrl = MainUrl;//请求地址
            string poststring = "";//构造POST请求字符串
            string code = ParaLogin.secertcode;//验证码
            string statecode;//网页内的VIEWSTATE
            #endregion

            #region 登陆主页获取网页内容
            HttpWebRequest GetStateCode = (HttpWebRequest)HttpWebRequest.Create(postUrl);//连接主页
            WebResponse result = GetStateCode.GetResponse();//获取响应
            Stream receviceStream = result.GetResponseStream();//创建IO流
            StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("gb2312"));//创建流读取器
            string strHTML = readerOfStream.ReadToEnd();//获取网页内容
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();//关闭流
            #endregion

            #region 匹配ViewState
            Match whatstate = reg.Match(strHTML);//创建匹配VIEWSTATE的正则表达式
            statecode = whatstate.Groups[1].Value;//匹配结果
            statecode = HttpUtility.UrlEncode(statecode);//URL编码
            poststring = "__VIEWSTATE=" + statecode + "&txtUserName=" + ParaLogin.loginname + "&Textbox1=&TextBox2=" + ParaLogin.password + "&txtSecretCode=" + code + "&RadioButtonList1=%D1%A7%C9%FA" + "&Button1=&lbLanguage=&hidPdrs=&hidsc=";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(poststring);
            #endregion

            #region 构造参数结构
            SendPostLoginParameter sendpost = new SendPostLoginParameter();
            sendpost.PostUrl = postUrl;
            sendpost.ReferUrl = MainUrl;
            sendpost.Bytesarray = bytes;
            sendpost.NeedRedirect = true;
            #endregion

            return Send_Post(sendpost);
        }
        #endregion

        #region 将登陆过程异步化，防止界面假死
        private Task<string> LoginAsync(LoginParameter ParaLogin)
        {
            return Task.Run<string>(() =>
                {
                    return Login(ParaLogin);
                });
        }
        private async void CallLogin(LoginParameter ParaLogin)
        { 
            SetTextboxValue(await LoginAsync(ParaLogin));
        }
        private void SetTextboxValue(string value)
        {
            PageCode = value;
            if (Judge_LoginSuccess(PageCode) == true)
            {
                UserName = Get_UserName(PageCode);
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                StuAccount.Visible = false;
                StuPassword.Visible = false;
                ViewCode.Visible = false;
                ViewPic.Visible = false;
                LoginIn.Visible = false;
                ResetTextBox.Visible = false;
                LogOut.Visible = true;
                ClassTableQuery.Enabled = true; 
                MessageBox.Show("你好，" + UserName + "同学！", "欢迎", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if(PageCode.IndexOf("验证码不正确")!=-1)
                {
                    MessageBox.Show("验证码不正确，请检查后重新输入", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(PageCode.IndexOf("密码错误")!=-1)
                {
                    MessageBox.Show("密码或用户名输入错误，请检查后重新输入", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(PageCode.IndexOf("用户名不存在")!=-1)
                {
                    MessageBox.Show("用户名不存在或未按照要求参加教学活动，请检查用户名是否输入错误或与教务处联系", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("未知错误，请检查网络后重试或联系作者反馈！", "登录失败",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                CallRequestCode();
                return;
            }
            CallInit();
        }
        #endregion

        #region 判断登陆状态
        /// <summary>
        /// 判断登陆状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool Judge_LoginSuccess(string input)
        {
            if(input.IndexOf("验证码")!=-1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 登陆后在主页获取登陆用户姓名
        /// <summary>
        /// 登陆后在主页获取登陆用户姓名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string Get_UserName(string input)
        {
            string output = "";

            Regex hello = new Regex("<span id=\"xhxm\">[\u4e00-\u9fa5]+</span></em>");//匹配包含姓名的字符串
            Regex names = new Regex("[\u4e00-\u9fa5]+");//匹配字符串内的中文
            Match match = hello.Match(input);//第一次匹配
            Match matchname = names.Match(match.ToString());//第二次匹配
            output = matchname.ToString().Replace("同学", "");//去除“同学”字样

            return output;
        }
        #endregion

        #region 正则匹配课表数据
        /// <summary>
        /// 正则匹配课表数据
        /// </summary>
        /// <returns></returns>
        private void GetTalbeRegex()
        {
            Regex Table = new Regex("<table.+?</table>",RegexOptions.Singleline);//获取课程表表格
            Regex Class = new Regex("align=\"Center\" rowspan=\"\\d\".+?</td>", RegexOptions.Singleline);//获取格内课程信息
            Regex td = new Regex("align=\"Center\" rowspan.+?>", RegexOptions.Singleline);//课程信息TD头
            Regex Info = new Regex(".+?<.+?>", RegexOptions.Singleline);//获取单行信息
            Regex InfoLabel = new Regex("<.+?>", RegexOptions.Singleline);//单行信息标签
            Regex TimeInfo = new Regex("\\d{4}.\\d{2}.\\d{2}.+?<.+?>.[^<]+", RegexOptions.Singleline);//去除额外时间信息
            Regex huge = new Regex("{.+?}", RegexOptions.Singleline);//获取周数
            Regex Location = new Regex("[\u4e00-\u9fa5]-[^<]+",RegexOptions.Singleline);
            List<ClassNode> Classes = new List<ClassNode>();//课程数据节点

            Match tablematch = Table.Match(PageCode);//获取表格数据
            string tabledata = tablematch.Value;
            MatchCollection ClassesMatch = Class.Matches(tabledata);//匹配课程数据

            for(int i=0;i<ClassesMatch.Count;i++)
            {
                string output = ClassesMatch[i].Value;
                string Time = TimeInfo.Match(output).Value;
                string Locations = "";
                output = output.Replace(td.Match(output).Value, "");//去除头部
                if(Time.Length!=0)
                {
                    output = output.Replace(Time, "");//去除时间
                }
                if(Location.IsMatch(output))
                {
                    Locations = Location.Match(output).ToString();
                }
                else
                {
                    Locations = "课表内无教室信息";
                }
                MatchCollection classinfo = Info.Matches(output);//带br或td的ClassInfo
                string[] classstring = new string[classinfo.Count];
                int index = 0;
                for(int j=0;j<classinfo.Count;j++)
                {
                    string infos=classinfo[j].ToString().Replace(InfoLabel.Match(classinfo[j].Value).Value, "");//去除标签，获得信息
                    if(infos!="")
                    {
                        classstring[index++] = infos;
                    }
                    else
                    {
                        j++;
                    }
                }
                ClassNode node = new ClassNode();
                node.ClassName = classstring[0];
                node.ClassTime = classstring[1];
                node.Teacher = classstring[2];
                //node.Location = classstring[3];
                node.Location = Locations;
                if(classstring.Length>4)
                {
                    for (int hugei = 4; hugei < classstring.Length; hugei++)
                    {
                        try
                        {
                            if (huge.IsMatch(classstring[hugei]))
                            {
                                node.ClassTime += " " + huge.Match(classstring[hugei]).Value;
                                break;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                Classes.Add(node);
            }

            //string outputs="";
            Array.Clear(ClassTable, 0, 6 * 11);//初始化课程表
            for(int i=0;i<Classes.Count;i++)
            {
                string input = "课程名称:" + Classes[i].ClassName + "\r\n" + "课程时间:" + Classes[i].ClassTime + "\r\n" + "课程教师:" + Classes[i].Teacher + "\r\n" + "课程地点:" + Classes[i].Location + "\r\n";
                SetClassToArray(Classes[i].ClassTime, input);
                //outputs += "Name:" + Classes[i].ClassName + " Time:" + Classes[i].ClassTime + " Teacher:" + Classes[i].Teacher + " Location:" + Classes[i].Location + "\r\n";
            }

            //return outputs;
            return;

        }
        #endregion

        #region 将课表数据写入数组
        private void SetClassToArray(string TimeInfo,string input)
        {
            Regex nums = new Regex("\\d,\\d{1,2},\\d{1,2}");
            Regex num = new Regex("\\d,\\d");

            int x=0, y=0;

            switch (TimeInfo[1])
            {
                case '一':
                    {
                        x = 1;
                        break;
                    }
                case '二':
                    {
                        x = 2;
                        break;
                    }
                case '三':
                    {
                        x = 3;
                        break;
                    }
                case '四':
                    {
                        x = 4;
                        break;
                    }
                case '五':
                    {
                        x = 5;
                        break;
                    }
                case '六':
                    {
                        x = 6;
                        break;
                    }
            }

            if(nums.IsMatch(TimeInfo))
            {
                string a = nums.Match(TimeInfo).Value;
                y = Convert.ToInt32(Convert.ToString(a[0]));
                ClassTable[y - 1, x - 1] = input;
                y++;
                ClassTable[y - 1, x - 1] = input;
                y++;
                ClassTable[y - 1, x - 1] = input;
            }
            else
            {
                string a = num.Match(TimeInfo).Value;
                y = Convert.ToInt32(Convert.ToString(a[0]));
                ClassTable[y - 1, x - 1] = input;
                y++;
                ClassTable[y - 1, x - 1] = input;
            }

        }
        #endregion

        #region 登陆后初始化课程表查询范围
        private void ClassYearInit()
        {
            lists.Clear();
            Regex SelectForm = new Regex("<select name=\"xnd\".+?</select>",RegexOptions.Singleline);
            Regex Time = new Regex("\\d{4}-\\d{4}", RegexOptions.Singleline);
            string GetData = "";
            string url=SetUrl(ModeList[0]);
            GetData=Send_Get(url);
            GetData = SelectForm.Match(GetData).Value;
            MatchCollection TimeCollect = Time.Matches(GetData);
            for(int i=0;i<TimeCollect.Count;i+=2)
            {
                lists.Add(TimeCollect[i].Value+"学年");
            }
            lists.Sort();
            Action<string> InitTermItems = (x) => { TermYear.Items.Add(x); };
            for(int i=0;i<lists.Count;i++)
            {
                TermYear.BeginInvoke(InitTermItems, lists[i]);
            }
        }
        #endregion

        #region 异步化更新范围函数，防止界面假死
        private Task InitClassTask()
        {
            return Task.Run(() =>
                {
                    ClassYearInit();
                });
        }

        private async void CallInit()
        {
            await InitClassTask();
        }
        #endregion

        /// <summary>
        /// 发送Get请求并返回页面内容（通常用在从主页一次可到达的链接）
        /// </summary>
        /// <param name="urlstring"></param>
        /// <returns></returns>
        private string Send_Get(string urlstring)
        {
            string posturl = urlstring;
            string output = "";

            #region 构造请求头
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(posturl);
            request.Method = "GET";
            SetHeaderValue(request.Headers, "Host", "ojjx.wzu.edu.cn");
            SetHeaderValue(request.Headers, "Connection", "keep-alive");
            request.Headers["Upgrade-Insecure-Requests"] = "1";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.Headers["DNT"] = "1";
            request.Referer = "http://ojjx.wzu.edu.cn/xs_main.aspx?xh="+UserAccount;
            request.Headers["Accept-Encoding"] = "gzip, deflate";
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
            request.CookieContainer = cookie;
            #endregion

            #region 返回并接收数据
            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            StreamReader ReaderOfStream = new StreamReader(res.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312"));
            output = ReaderOfStream.ReadToEnd();
            #endregion

            return output;
        }

        /// <summary>
        /// 指定载入网址、来源网址和要post的字符串发送POST请求
        /// </summary>
        /// <param name="posturl"></param>
        /// <param name="ReferUrl"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private string Send_Post(SendPostLoginParameter SendPost)
        {
            string output = "";

            #region 构造请求头
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(SendPost.PostUrl);

            httpRequest.Method = "POST";
            SetHeaderValue(httpRequest.Headers, "Host", "ojjx.wzu.edu.cn");
            SetHeaderValue(httpRequest.Headers, "Connection", "keep-alive");
            httpRequest.Headers["Cache-Control"] = "max-age=0";
            httpRequest.Headers["Origin"] = "http://ojjx.wzu.edu.cn";
            httpRequest.Headers["Upgrade-Insecure-Requests"] = "1";
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            httpRequest.Headers["DNT"] = "1";
            httpRequest.Referer = SendPost.ReferUrl;
            httpRequest.Headers["Accept-Encoding"] = "gzip, deflate";
            httpRequest.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
            if(SendPost.NeedRedirect==false)
            {
                httpRequest.AllowAutoRedirect = false;
            }
            else
            {
                httpRequest.AllowAutoRedirect = true;
            }
            httpRequest.CookieContainer = cookie;
            #endregion

            #region 创建IO流写入POST内容
            Stream stream = httpRequest.GetRequestStream();
            stream.Write(SendPost.Bytesarray, 0, SendPost.Bytesarray.Length);
            stream.Close();
            #endregion

            #region 返回并接收数据
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            if (Convert.ToString(httpResponse.StatusCode) == "Found")
            {
                IsRedirect = true;
            }
            else
            {
                IsRedirect = false;
            }
            StreamReader ReadersOfStream = new StreamReader(httpResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312"));
            output = ReadersOfStream.ReadToEnd();
            #endregion

            return output;
        }

        /// <summary>
        /// 为报头某些属性设置值（如HOST、Connection等）
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        /// <summary>
        /// 传入功能序号和要写入的bytearray进行post操作
        /// </summary>
        /// <param name="index"></param>
        /// <param name="bytearray"></param>
        /// <returns></returns>
        private string PostModeLink(int index,byte[] bytearray)
        {
            string Url = SetUrl(ModeList[index]);
            SendPostLoginParameter sendpost = new SendPostLoginParameter();
            sendpost.PostUrl = Url;
            sendpost.ReferUrl = Url;
            sendpost.Bytesarray = bytearray;
            string output = Send_Post(sendpost);

            return output;
        }

        /// <summary>
        /// 设置节点的值并返回节点
        /// </summary>
        /// <param name="link"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private mode SetModeNode(string link,string code,string name)
        {
            mode node;
            node.linkname = link;
            node.modecode = code;
            node.name = name;

            return node;
        }

        /// <summary>
        /// 设置功能模块链接并返回
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string SetUrl(mode node)
        {
            string url = "http://ojjx.wzu.edu.cn/";
            string username = HttpUtility.UrlEncode(UserName,System.Text.Encoding.GetEncoding("gb2312"));
            url += node.linkname + ".aspx?xh=" + UserAccount + "&xm=" + username + "&gnmkdm=" + node.modecode;
            //url = HttpUtility.UrlEncode(url);

            return url;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString() + ":"+DateTime.Now.Millisecond;
        }

        private void ResetTextBox_Click(object sender, EventArgs e)
        {
            StuAccount.Text = "";
            StuPassword.Text = "";
            ViewCode.Text = "";
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            cookie = null;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            StuAccount.Visible = true;
            StuPassword.Visible = true;
            ViewCode.Visible = true;
            ViewPic.Visible = true;
            LoginIn.Visible = true;
            ResetTextBox.Visible = true;
            LogOut.Visible = false;
            ResetTextBox_Click(sender, e);
            TermIndex.Text = null;
            TermYear.Text = null;
            TermYear.Items.Clear();
            ClassTableQuery.Enabled = false;
            MessageBox.Show("已退出登陆，欢迎下次使用，再见！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void Query_Click(object sender, EventArgs e)
        {
            if(TermIndex.Text==""||TermYear.Text=="")
            {
                MessageBox.Show("有未选择部分，请选择后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string Year = "";
            string TermDate = "";
            Regex YearRegex = new Regex("\\d{4}");
            Year = YearRegex.Match(TermYear.Text).Value;
            if(TermIndex.Text.IndexOf('1')!=-1)
            {
                TermDate = "1";
            }
            else
            {
                TermDate = "2";
            }
            ClassTableGoal classgoal = new ClassTableGoal();
            classgoal.Year = Year;
            classgoal.Trem = TermDate;
            await GetTableAsync(classgoal);
            await GetTableTask();
            Regex times = new Regex("\\d{4}-\\d{4}", RegexOptions.Singleline);
            string timesstring = times.Match(TermYear.Text).Value;


            if(PageCode.IndexOf("<option selected=\"selected\" value=\""+timesstring)==-1)
            {
                MessageBox.Show("当前学期暂无课程，请检查查询时间后再试！", "无课程提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if(classform==null||classform.IsDisposed)
            {
                classform = new ClassForm(TermYear.Text, TermIndex.Text, ClassTable);
                classform.Show();
            }
            else
            {
                classform.Activate();
            }
        }
    }

     
}

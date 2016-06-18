using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CnBlogs.UWP.Constants;
using CNBlogs.UWP.Models;

namespace CnBlogs.UWP.Services
{
    /// <summary>
    /// 博客相关服务
    /// </summary>
    internal static class BlogService
    {
        /// <summary>
        /// 分页获取首页文章列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static async Task<List<CnBlog>> LoadHomeArticleAsync(int index, int size)
        {
            try
            {
                string url = string.Format(BlogsConstant.PagingLoadHomeArticle, index, size);
                string xml = await HttpService.SendGetRequest(url);
                #region
                //<?xml version="1.0" encoding="utf-8"?>
//< feed xmlns = "http://www.w3.org/2005/Atom" >
//     < title type = "text" > 博客园 </ title >
//      < id > uuid:07a7723c - 4143 - 49a5 - 86e8 - 3ed9f58adb2f; id = 22090 </ id >           
//                  < updated > 2016 - 02 - 11T06: 14:50Z </ updated >                      
//                          < link href = "http://www.cnblogs.com/" />
//                           < entry >
//                               < id > 5186375 </ id >
//                               < title type = "text" > js 默认的参数、可变的参数、变量作用域 </ title >
//                                    < summary type = "text" > 可以通过对象来实现可变的参数在函数代码中，使用特殊对象，开发者无需明确指出参数名，就能访问它们。是一个数组对象，可以通过下标来实别参数的位置，通过来获参数的个数。代码实例：我们通过来改变函数的默认参数 </ summary >
//                                         < published > 2016 - 02 - 11T12: 00:00 + 08:00 </ published >                                        
//                                                   < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
//                                                           < author >
//                                                               < name > Ziksang </ name >
//                                                               < uri > http://www.cnblogs.com/Ziksang/</uri>
//            < avatar > http://pic.cnblogs.com/face/890922/20160207234530.png</avatar>
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/Ziksang/p/5186375.html" />
//           < blogapp > Ziksang </ blogapp >
//           < diggs > 0 </ diggs >
//           < views > 41 </ views >
//           < comments > 1 </ comments >
//       </ entry >
//       < entry >
   
//           < id > 5186362 </ id >
   
//           < title type = "text" > How ADB works</ title >
        
//                < summary type = "text" > yyyyy </ summary >
         
//                 < published > 2016 - 02 - 11T11: 37:00 + 08:00 </ published >
                   
//                           < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                           
//                                   < author >
                           
//                                       < name > iFantasticMe </ name >
                           
//                                       < uri > http://www.cnblogs.com/ifantastic/</uri>
//            < avatar > http://pic.cnblogs.com/face/477475/20130610000523.png</avatar>
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/ifantastic/p/5186362.html" />
   
//           < blogapp > ifantastic </ blogapp >
   
//           < diggs > 0 </ diggs >
   
//           < views > 25 </ views >
   
//           < comments > 0 </ comments >
   
//       </ entry >
   
//       < entry >
   
//           < id > 5186332 </ id >
   
//           < title type = "text" > 程序员狂想曲 </ title >
    
//            < summary type = "text" >【前序】博客园首页对博文的规定有：原创精品、排版整齐、有足够的篇幅、与程序员相关、能够让读者从中学到知识的基本要求。我心想，除非不同时空，否则这绝对是我原创的；段落分明排版自然问题不大；一千三百来字的文章远超八百字的高考作文篇幅的要求；出于程序员手里的，难道这还不跟程序员相关吗；能给读者带来情感波动 </ summary >
        
//                < published > 2016 - 02 - 11T10: 27:00 + 08:00 </ published >
                  
//                          < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                          
//                                  < author >
                          
//                                      < name > wc的一些事一些情 </ name >
                          
//                                      < uri > http://www.cnblogs.com/wcd144140/</uri>
//            < avatar />
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/wcd144140/p/5186332.html" />
   
//           < blogapp > wcd144140 </ blogapp >
   
//           < diggs > 0 </ diggs >
   
//           < views > 88 </ views >
   
//           < comments > 1 </ comments >
   
//       </ entry >
   
//       < entry >
   
//           < id > 5186322 </ id >
   
//           < title type = "text" > 6.python模块（导入，内置，自定义，开源）</ title >
        
//                < summary type = "text" > 一、模块模块简介模块是一个包含所有你定义的函数和变量的文件，其后缀名是y。模块可以被别的程序引入，以使用该模块中的函数等功能。这也是使用y标准库的方法。类似于函数式编程和面向过程编程，函数式编程则完成一个功能，其他代码用来调用即可，提供了代码的重用性和代码间的耦合。而对于一个 </ summary >
             
//                     < published > 2016 - 02 - 11T10: 09:00 + 08:00 </ published >
                       
//                               < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                               
//                                       < author >
                               
//                                           < name > 刘耀 </ name >
                               
//                                           < uri > http://www.cnblogs.com/liu-yao/</uri>
//            < avatar />
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/liu-yao/p/5186322.html" />
   
//           < blogapp > liu - yao </ blogapp >
   
//           < diggs > 1 </ diggs >
   
//           < views > 48 </ views >
   
//           < comments > 0 </ comments >
   
//       </ entry >
   
//       < entry >
   
//           < id > 5186214 </ id >
   
//           < title type = "text" > UITaleView的基础使用及数据展示操作 </ title >
    
//            < summary type = "text" > 表视图，是实用的数据展示的基础控件，是继承于，所以也可以滚动。但不同于，只可以上下滚动，而不能左右滚动。因为是数据展示，必然少不了数据的存在，嗯，使用文件来获取想要的数据。通过模型来获取。说到这 </ summary >
         
//                 < published > 2016 - 02 - 10T23: 34:00 + 08:00 </ published >
                   
//                           < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                           
//                                   < author >
                           
//                                       < name > 揍揍揍揍揍揍揍小屁孩 </ name >
                           
//                                       < uri > http://www.cnblogs.com/xueyao/</uri>
//            < avatar > http://pic.cnblogs.com/face/890522/20160129164157.png</avatar>
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/xueyao/p/5186214.html" />
   
//           < blogapp > xueyao </ blogapp >
   
//           < diggs > 0 </ diggs >
   
//           < views > 47 </ views >
   
//           < comments > 0 </ comments >
   
//       </ entry >
   
//       < entry >
   
//           < id > 5186207 </ id >
   
//           < title type = "text" > 慎重管理SQL Server服务的登录（启动）账户和密码 </ title >
        
//                < summary type = "text" > 今天是大年初三，先跟大家拜个年，祝大家新年快乐。今天处理了一个y问题——辅助副本因为磁盘空间不足一直显示【未同步——可疑】，在日志中可以看到数据库处于挂起状态，与主副本失去同步。原以为只需把辅助副本的磁盘做个清理，腾出一点空间，然后重启服务就好了（重启让数据库从挂起 </ summary >
             
//                     < published > 2016 - 02 - 10T23: 24:00 + 08:00 </ published >
                       
//                               < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                               
//                                       < author >
                               
//                                           < name > i6first </ name >
                               
//                                           < uri > http://www.cnblogs.com/i6first/</uri>
//            < avatar />
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/i6first/p/5186207.html" />
   
//           < blogapp > i6first </ blogapp >
   
//           < diggs > 0 </ diggs >
   
//           < views > 128 </ views >
   
//           < comments > 0 </ comments >
   
//       </ entry >
   
//       < entry >
   
//           < id > 5186200 </ id >
   
//           < title type = "text" > HTTP与HTTPS握手的那些事 </ title >
    
//            < summary type = "text" > 今天我总结了什么是三次握手，还有握手的过程以及为什么是安全的。前提在讲述这两个握手时候，有一些东西需要提前说明。与区别？协议是传输层协议，主要解决数据如何在网络中传输，而是应用层协议，主要解决如何包装数据。使用 </ summary >
         
//                 < published > 2016 - 02 - 10T23: 17:00 + 08:00 </ published >
                   
//                           < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                           
//                                   < author >
                           
//                                       < name > 海角在眼前 </ name >
                           
//                                       < uri > http://www.cnblogs.com/lovesong/</uri>
//            < avatar > http://pic.cnblogs.com/face/555379/20130831110432.png</avatar>
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/lovesong/p/5186200.html" />
   
//           < blogapp > lovesong </ blogapp >
   
//           < diggs > 0 </ diggs >
   
//           < views > 137 </ views >
   
//           < comments > 0 </ comments >
   
//       </ entry >
   
//       < entry >
   
//           < id > 5180801 </ id >
   
//           < title type = "text" > 主进程被杀死时，如何保证子进程同时退出，而不变为孤儿进程（一）</ title >
        
//                < summary type = "text" > 在y中，由于全局解释器锁的存在，使得y中的多线程并不能大大提高程序的运行效率（这里单指密集型），那么在处理密集型计算时，多用多进程模型来处理，而y标准库中提供了库来支持多进程模型的编程。中提供 </ summary >
             
//                     < published > 2016 - 02 - 10T22: 49:00 + 08:00 </ published >
                       
//                               < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                               
//                                       < author >
                               
//                                           < name > Tour大咸鱼 </ name >
                               
//                                           < uri > http://www.cnblogs.com/Tour/</uri>
//            < avatar > http://pic.cnblogs.com/face/u323775.jpg?id=14174243</avatar>
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/Tour/p/5180801.html" />
   
//           < blogapp > Tour </ blogapp >
   
//           < diggs > 1 </ diggs >
   
//           < views > 104 </ views >
   
//           < comments > 0 </ comments >
   
//       </ entry >
   
//       < entry >
   
//           < id > 5186149 </ id >
   
//           < title type = "text" > struts2学习笔记--总结获取servletAPI的几种方式 </ title >
        
//                < summary type = "text" > 的放弃了等使得在业务层上更加独立在有时候使用进行开发的时候不可避免的要在中使用那么如何在中获取并使用呢通过 </ summary >
         
//                 < published > 2016 - 02 - 10T21: 53:00 + 08:00 </ published >
                   
//                           < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                           
//                                   < author >
                           
//                                       < name > 醉眼识朦胧 </ name >
                           
//                                       < uri > http://www.cnblogs.com/fingerboy/</uri>
//            < avatar > http://pic.cnblogs.com/face/870109/20151230155918.png</avatar>
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/fingerboy/p/5186149.html" />
   
//           < blogapp > fingerboy </ blogapp >
   
//           < diggs > 0 </ diggs >
   
//           < views > 47 </ views >
   
//           < comments > 0 </ comments >
   
//       </ entry >
   
//       < entry >
   
//           < id > 5186118 </ id >
   
//           < title type = "text" > Qt5.5.0使用mysql编写小软件源码讲解-- - 顾客信息登记表 </ title >
        
//                < summary type = "text" > 使用y编写小软件源码讲解顾客信息登记表一个个人觉得比较简单小巧的软件。下面就如何编写如何发布打包来介绍一下吧！先下载y的库文件链接：yz把两个文件放入 </ summary >
             
//                     < published > 2016 - 02 - 10T21: 11:00 + 08:00 </ published >
                       
//                               < updated > 2016 - 02 - 11T06: 14:50Z </ updated >
                               
//                                       < author >
                               
//                                           < name > 小波Linux </ name >
                               
//                                           < uri > http://www.cnblogs.com/xiaobo-Linux/</uri>
//            < avatar > http://pic.cnblogs.com/face/782772/20150708142739.png</avatar>
//        </ author >
//        < link rel = "alternate" href = "http://www.cnblogs.com/xiaobo-Linux/p/5186118.html" />
   
//           < blogapp > xiaobo - Linux </ blogapp >
   
//           < diggs > 1 </ diggs >
   
//           < views > 116 </ views >
   
//           < comments > 1 </ comments >
   
//       </ entry >
//   </ feed >

                #endregion
                if (xml != null)
                {
                    XElement xElement = XElement.Parse(xml);
                    return xElement.Elements("entry")
                        .Select(entry => new CnBlog(entry))
                        .ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定博客正文
        /// </summary>
        /// <param name="postId">博客id</param>
        /// <returns></returns>
        public async static Task<string> LoadArticleContentAsync(string postId)
        {
            try
            {
                string url = string.Format(BlogsConstant.LoadArticleContent, postId);
                string xml = await HttpService.SendGetRequest(url);
                #region 返回内容
                //<? xml version = "1.0" encoding = "utf-8" ?>
                //   < string > &lt; p & gt; &amp; nbsp; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &amp; nbsp; &amp; nbsp; 【前序】博客园首页对博文的规定有：原创精品、排版整齐、有足够的篇幅、与程序员相关、能够让读者从中学到知识的基本要求。我心想，除非不同时空，否则这绝对是我原创的；段落分明排版自然问题不大；一千三百来字的文章远超八百字的高考作文篇幅的要求；出于程序员手里的，难道这还不跟程序员相关吗；能给读者带来情感波动，也算是学习了吧，哪怕让读者心想原来还有这样的SB也算让读者意识到原来自己站在那么多的SB头上。所以这篇博文有理由能在博客园首页多活一分钟。&lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &amp; nbsp; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &amp; nbsp; &amp; nbsp; 醉酒三分醒，茶醉十分清。今晚我独自一人傻傻地蹲在月球边上静静地看着蓝白色的地球，为什么地球是蓝白色的？反正我现在蹲着的月球和蓝白色的地球都是专家的说的，管他是不是真的，反正对我来说还是有点儿利用价值，毕竟还可以让我在这做点文章字眼。&lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &amp; nbsp; &amp; nbsp; 专家说我现在盯看着的这个地球有好几十亿年岁了。是吗？我也不知道，我只知道在这个圆圆的空间里活过好多代人了，我为什么会说是圆圆的？也是专家说的。那么这么多代人过去了，这个空间发生了什么变化？从我这个角度看下去，好像也并没有多大变化。我为什么会说看下去，地球一定在月球之下吗？我也不知道。算了吧，这样下去，在这里看个十天八天也什么两样，别浪费时间了，下去看看吧。我为什么又说下去，奇怪！&lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &amp; nbsp; &amp; nbsp; 下来了，有什么好看？看了二十多年了，都不知道自己在看什么。很多个为什么在心里头也没人可以帮我解答，所以我刚才才傻兮兮的跑到月球去溜达溜达。现在我所有的想象都不是我自己的，都是别人给予的，就像我所说的专家，包括我现在所写的都是专家所影响的，所以这个地球只是专家的地球。为了摆脱专家的束缚，我只能在黑暗中依靠笔记本的这点光线自行摸索。&lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &amp; nbsp; &amp; nbsp; 今天是2016年2月11日凌晨1点27分51秒，中国的正月初四，也就是年初四。这时间对我来说有什么意义？我也不知道，我只知道我跟随着传统，现在待在岳父母家拜年。什么是传统？我也不知道。因为从小我就接受着尊重传统的训导，所以我大概知道我接下来的几个正月初四可能都是这样度过。这几天毫无倦意的鞭炮声简直震耳欲聋，我为什么要用震耳欲聋，以前不是这种感觉的，为什么他们要放鞭炮？我也不知道。一年下来，就这么几天可以让人清净一下，为什么还要让人绞尽脑汁和亲戚相互寒暄，莫名其妙。我为什么只有这几天才可以清净？其他时间我不可以清净吗？我真是莫名其妙。春节为什么还要发红包？因为孩子高兴？如果我自己觉得不高兴，是不是可以不发？为什么？为什么会有数字红包？专家说是科技发展的必然结果？为什么要发展？什么叫发展？发展对我有什么好处？是不是一定要发展我们才能生存？是不是不发展人类就要灭亡？灭亡了又会怎么样？还有达尔文进化论是什么？这不是达尔文自己提出自己的想法吗？就算达尔文的观点是正确的，我们知道了又怎么样？难道可以长寿一点吗？长寿一定好吗？为什么乔布斯会说死亡是生命最好的发明。他说是最好，就一定是最好的吗？莫名其妙。&lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &amp; nbsp; &amp; nbsp; 为什么专家会影响着我的生活？为什么我会觉得传统很束缚？我为什么不可以创新？我为什么要用创新这个词？尼玛的专家腔调。人生在世，能不能让自己过得顺心点儿。为什么我会说人生在世？好像这不是专家说的，好像这是现实，终于找到一个不是专家说的了，真实倍感实在。人生在世，不就为了找点实在感吗？为什么那么多人喜欢待在泡沫虚幻当中，难道只有我一个人这么想的？天空那么地蓝，为什么不走出屋子抬头看看且非得对着桌面的蓝天白云？风那么地清爽，为什么不让自己暴露在野外非得让死气沉沉的空调冷冰冰地吹着？阳光那么地温暖，为什么不面对面地与太阳坦诚相对非得让自己闷骚在二氧化碳堆里。&lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &lt;/ span & gt; &lt;/ p & gt; &#xD;
                //&lt; p & gt; &lt; span style = "font-size: 16px;" & gt; &amp; nbsp; &amp; nbsp; &amp; nbsp; 为什么我会那么多为什么？爸妈会说，你长大了就懂了，难道我现在还没长大？老师会说，你以后会懂的，我什么时候才能到以后去？谁能解答？&lt;/ span & gt; &lt;/ p & gt;</ string >

                #endregion
                if (xml != null)
                {
                    XElement xElement = XElement.Parse(xml);
  
                    return xElement.Value;
                    //return "<style>body{font-family:微软雅黑;font-size=14px}</style>" + doc.ChildNodes[1].InnerText;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据博主blog_app获取博客列表
        /// </summary>
        /// <param name="blogApp"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async static Task<List<CnBlog>> GetBlogsByUserAsync(string blogApp,int pageIndex,int pageSize)
        {
            try
            {
                string url = string.Format(BlogsConstant.PagingLoadPersonalBlogsArticleList, blogApp, pageIndex, pageSize);
                string xml = await HttpService.SendGetRequest(url);
                #region 
   //             <? xml version = "1.0" encoding = "utf-8" ?>
   //< feed xmlns = "http://www.w3.org/2005/Atom" >
   //    < title type = "text" > 博客园_杰哥很忙 </ title >
   //     < id > uuid:07a7723c - 4143 - 49a5 - 86e8 - 3ed9f58adb2f; id = 22160 </ id >
   //                 < updated > 2016 - 02 - 04T21: 04:15 + 08:00 </ updated >
   //                           < logo > http://pic.cnblogs.com/face/sample_face.gif</logo>
   // < author >
   //     < name > 杰哥很忙 </ name >
   //     < uri > http://www.cnblogs.com/Jack-Blog/</uri>
   // </ author >
   // < postcount > 25 </ postcount >
   // < entry >
   //     < id > 5182310 </ id >
   //     < title type = "text" > 实现更简单的异步操作 </ title >
   //      < summary type = "text" > 前言 在.net4.0以后异步操作,并行计算变得异常简单,但是由于公司项目开发基于.net3.5所以无法用到4.0的并行计算以及Task等异步编程。因此,为了以后更方便的进行异步方式的开发,我封装实现了异步编程框架,通过BeginInvoke、EndInvoke的方式实现异步编程。 框架结构 整个框</ summary >
   //           < published > 2016 - 02 - 04T20: 45:00 + 08:00 </ published >
   //                     < updated > 2016 - 02 - 11T06: 17:53Z </ updated >
                        
   //                             < author >
                        
   //                                 < name > 杰哥很忙 </ name >
                        
   //                                 < uri > http://www.cnblogs.com/Jack-Blog/</uri>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/Jack-Blog/p/5182310.html" />
   
   //        < diggs > 0 </ diggs >
   
   //        < views > 316 </ views >
   
   //        < comments > 0 </ comments >
   
   //    </ entry >
   
   //    < entry >
   
   //        < id > 5079433 </ id >
   
   //        < title type = "text" > 一步一步学习SignalR进行实时通信_9_托管在非Web应用程序 </ title >
    
   //         < summary type = "text" > (&amp; lt;/ span & amp; lt; span class=&amp;quot;pln&amp;quot; address&amp;lt;/span &amp;lt;span class=&amp;quot;pun&amp;quot; );&amp;lt;/span &amp;lt;/code &amp;lt;/li &amp;lt;li class=&amp;quot;L2&amp;quot; &amp;lt;code &amp;lt;span class=&amp;quot;pln&amp;quot; &amp;lt;/span &amp;lt;span class=&amp;quot;pun&amp;quot; }&amp;lt;/span...</summary>
   //     <published>2015-12-27T02:16:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //     <author>
   //         <name>杰哥很忙</name>
   //         <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href="http://www.cnblogs.com/Jack-Blog/p/5079433.html"/>
   //     <diggs>0</diggs>
   //     <views>475</views>
   //     <comments>6</comments>
   // </entry>
   // <entry>
   //     <id>4979507</id>
   //     <title type = "text" > 一步一步学习SignalR进行实时通信_8_案例2 </ title >
   //     < summary type="text">==&amp;quot;&amp;quot;&amp;lt;/span &amp;lt;/code &amp;lt;/li &amp;lt;li class=&amp;quot;L8&amp;quot; &amp;lt;code &amp;lt;span class=&amp;quot;pln&amp;quot; &amp;lt;/span &amp;lt;span class=&amp;quot;pun&amp;quot; );&amp;lt;/span &amp;lt;/code &amp;lt;/li &amp;lt;li class=&amp;quot;L9&amp;quot; &amp;lt;code &amp;lt;span class=&amp;quot;pln&amp;quot; &amp;lt;/s...</summary>
   //     <published>2015-11-20T00:12:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //     <author>
   //         <name>杰哥很忙</name>
   //         <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href="http://www.cnblogs.com/Jack-Blog/p/4979507.html"/>
   //     <diggs>0</diggs>
   //     <views>383</views>
   //     <comments>0</comments>
   // </entry>
   // <entry>
   //     <id>4970488</id>
   //     <title type = "text" > 一步一步学习SignalR进行实时通信_7_非代理 </ title >
   //     < summary type="text">&amp;lt;!DOCTYPE html &amp;lt;html &amp;lt;head &amp;lt;meta charset = &amp; quot;utf 8&amp;quot; &amp;lt;title 一步一步学习SignalR进行实时通信\_7_非代理&amp;lt;/title &amp;lt;/head &amp;lt;body &amp;lt;div id = &amp; quot;wmd preview&amp;quot; class=&amp;quot;wmd preview&amp;quot; &amp;lt;code...</summary>
   //     <published>2015-11-16T23:59:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //     <author>
   //         <name>杰哥很忙</name>
   //         <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href="http://www.cnblogs.com/Jack-Blog/p/4970488.html"/>
   //     <diggs>0</diggs>
   //     <views>375</views>
   //     <comments>2</comments>
   // </entry>
   // <entry>
   //     <id>4858555</id>
   //     <title type = "text" > 一步一步学习SignalR进行实时通信_6_案例 </ title >
   //     < summary type="text">&amp;quot;&amp;quot;&amp;lt;/span &amp;lt;span class=&amp;quot;pun&amp;quot; );&amp;lt;/span &amp;lt;/code &amp;lt;/li &amp;lt;li class=&amp;quot;L3&amp;quot; &amp;lt;code &amp;lt;span class=&amp;quot;pln&amp;quot; $panel&amp;lt;/span &amp;lt;span class=&amp;quot;pun&amp;quot; .&amp;lt;/span &amp;lt;span class=&amp;quot;pln&amp;quot; scroll...</summary>
   //     <published>2015-10-07T13:03:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //     <author>
   //         <name>杰哥很忙</name>
   //         <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href="http://www.cnblogs.com/Jack-Blog/p/4858555.html"/>
   //     <diggs>0</diggs>
   //     <views>430</views>
   //     <comments>6</comments>
   // </entry>
   // <entry>
   //     <id>4779779</id>
   //     <title type = "text" > 一步一步学习SignalR进行实时通信_5_Hub </ title >
   //     < summary type="text">一步一步学习SignalR进行实时通信\_5_HubSignalR一步一步学习SignalR进行实时通信_5_Hub前言Hub命名规则Hub封装好的常用方法Hub常用方法解释保持状态前后台交互结束语参考文献前言上一讲,我们简单的介绍了下Hub的配置以及实现方法,这一将我希望把更多的细节梳理清楚,不至...</summary>
   //     <published>2015-09-03T00:27:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //     <author>
   //         <name>杰哥很忙</name>
   //         <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href="http://www.cnblogs.com/Jack-Blog/p/4779779.html"/>
   //     <diggs>0</diggs>
   //     <views>601</views>
   //     <comments>0</comments>
   // </entry>
   // <entry>
   //     <id>4765244</id>
   //     <title type = "text" > 一步一步学习SignalR进行实时通信_4_Hub </ title >
   //     < summary type="text">一步一步学习SignalR进行实时通信\_4_HubSignalR一步一步学习SignalR进行实时通信_4_Hub前言创建Hub配置Hub创建Hubs服务详细代码代码解析效果展示结束语参考文献前言之前我们介绍了SignalR有2级抽象,前2篇文章我们讲的是较底层PersistentConnecti...</summary>
   //     <published>2015-08-28T01:43:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //     <author>
   //         <name>杰哥很忙</name>
   //         <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href="http://www.cnblogs.com/Jack-Blog/p/4765244.html"/>
   //     <diggs>0</diggs>
   //     <views>633</views>
   //     <comments>10</comments>
   // </entry>
   // <entry>
   //     <id>4735199</id>
   //     <title type = "text" > 一步一步学习SignalR进行实时通信_3_通过CORS解决跨域 </ title >
   //     < summary type="text">一步一步学习SignalR进行实时通信\_3_通过CORS解决跨域SignalR一步一步学习SignalR进行实时通信_3_通过CORS解决跨域前言关于start()的补充跨域解决方案JSONPCORSCORS跨域演示结束语参考文献前言这周工作比较忙,一直没有时间学习SignalR,大致希望一周能写...</summary>
   //     <published>2015-08-16T22:40:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //     <author>
   //         <name>杰哥很忙</name>
   //         <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href="http://www.cnblogs.com/Jack-Blog/p/4735199.html"/>
   //     <diggs>0</diggs>
   //     <views>705</views>
   //     <comments>2</comments>
   // </entry>
   // <entry>
   //     <id>4719344</id>
   //     <title type = "text" > 一步一步学习SignalR进行实时通信_2_Persistent Connections</title>
   //     <summary type = "text" > 一步一步学习SignalR进行实时通信\_2_Persistent ConnectionsSignalR一步一步学习SignalR进行实时通信_2_Persistent Connections前言安装Persistent Connections映射并配置持久连接结束语参考文献前言上一篇文章简单的介绍...</summary>
   //         <published>2015-08-10T21:45:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //         <author>
   //         <name>杰哥很忙</name>
   //             <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href= "http://www.cnblogs.com/Jack-Blog/p/4719344.html" />
    
   //         < diggs > 0 </ diggs >
    
   //         < views > 642 </ views >
    
   //         < comments > 10 </ comments >
    
   //     </ entry >
    
   //     < entry >
    
   //         < id > 4717706 </ id >
    
   //         < title type= "text" > 一步一步学习SignalR进行实时通信_1_简单介绍 </ title >
    
   //         < summary type= "text" > 一步一步学习SignalR进行实时通信\_1_简单介绍SignalR一步一步学习SignalR进行实时通信_1_简单介绍前言SignalR介绍支持的平台相关说明OWIN结束语参考文献前言本来前几个月想写一系列的关于SignalR的文章, 但是由于在做项目, 时间非常的紧急, 花了1天的时间大致了解了下Si...</summary>
   //     <published>2015-08-10T13:02:00+08:00</published>
   //     <updated>2016-02-11T06:17:53Z</updated>
   //         <author>
   //         <name>杰哥很忙</name>
   //             <uri>http://www.cnblogs.com/Jack-Blog/</uri>
   //     </author>
   //     <link rel = "alternate" href= "http://www.cnblogs.com/Jack-Blog/p/4717706.html" />
    
   //         < diggs > 0 </ diggs >
    
   //         < views > 578 </ views >
    
   //         < comments > 0 </ comments >
    
   //     </ entry >
   // </ feed >
    
                #endregion
                if (xml != null)
                {
                    XElement xElement = XElement.Parse(xml);
                    return xElement.Elements("entry")
                        .Select(entry => new CnBlog(entry))
                        .ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据博客id获取评论
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async static Task<List<CnBlogComment>> GetBlogCommentsAsync(string postId,int pageIndex,int pageSize)
        {
            try
            {
                string url = string.Format(BlogsConstant.LoadArticleComment, postId, pageIndex, pageSize);
                string xml = await HttpService.SendGetRequest(url);
                #region 
   //             <? xml version = "1.0" encoding = "utf-8" ?>
   //< feed xmlns = "http://www.w3.org/2005/Atom" >
   
   //    < title type = "text" > 博客园文章评论 </ title >
    
   //     < id > uuid:07a7723c - 4143 - 49a5 - 86e8 - 3ed9f58adb2f; id = 22399 </ id >
                
   //                 < updated > 2016 - 02 - 11T06: 27:22Z </ updated >
                        
   //                         < entry >
                        
   //                             < id > 3335408 </ id >
                        
   //                             < title type = "text" />
                         
   //                              < published > 2015 - 12 - 27T08: 57:59 + 08:00 </ published >
                                   
   //                                        < updated > 2016 - 02 - 11T06: 27:22Z </ updated >
                                           
   //                                                < author >
                                           
   //                                                    < name > jiulang </ name >
                                           
   //                                                    < uri > http://home.cnblogs.com/u/160192/</uri>
   //     </ author >
   //     < content type = "text" > 除了.net客户端，安卓等平台的客户端有吗，或者容易自己开发吗 </ content >
     
   //      </ entry >
     
   //      < entry >
     
   //          < id > 3335437 </ id >
     
   //          < title type = "text" />
      
   //           < published > 2015 - 12 - 27T11: 05:09 + 08:00 </ published >
                
   //                     < updated > 2016 - 02 - 11T06: 27:22Z </ updated >
                        
   //                             < author >
                        
   //                                 < name > 杰哥很忙 </ name >
                        
   //                                 < uri > http://home.cnblogs.com/u/580757/</uri>
   //     </ author >
   //     < content type = "text" > &lt; a href = "#3335408" title = "查看所回复的评论" onclick = 'commentManager.renderComments(0,50,3335408);' & gt;@&lt;/ a & gt; jiulang & lt; br / &gt; 好奇怪，用安卓的手机客户端2次回复都没用 </ content >
                        
   //                         </ entry >
                        
   //                         < entry >
                        
   //                             < id > 3335440 </ id >
                        
   //                             < title type = "text" />
                         
   //                              < published > 2015 - 12 - 27T11: 08:12 + 08:00 </ published >
                                   
   //                                        < updated > 2016 - 02 - 11T06: 27:22Z </ updated >
                                           
   //                                                < author >
                                           
   //                                                    < name > 杰哥很忙 </ name >
                                           
   //                                                    < uri > http://home.cnblogs.com/u/580757/</uri>
   //     </ author >
   //     < content type = "text" > &lt; a href = "#3335408" title = "查看所回复的评论" onclick = 'commentManager.renderComments(0,50,3335408);' & gt;@&lt;/ a & gt; jiulang & lt; br / &gt; Signalr不支持安卓客户端，可以的话用web客户端吧，如果自己重写，signalr是开源项目，去看下.net和javascript客户端源码，应该还是能写出来的 </ content >
                        
   //                         </ entry >
                        
   //                         < entry >
                        
   //                             < id > 3335506 </ id >
                        
   //                             < title type = "text" />
                         
   //                              < published > 2015 - 12 - 27T14: 06:59 + 08:00 </ published >
                                   
   //                                        < updated > 2016 - 02 - 11T06: 27:22Z </ updated >
                                           
   //                                                < author >
                                           
   //                                                    < name > jiulang </ name >
                                           
   //                                                    < uri > http://home.cnblogs.com/u/160192/</uri>
   //     </ author >
   //     < content type = "text" > &lt; a href = "#3335440" title = "查看所回复的评论" onclick = 'commentManager.renderComments(0,50,3335440);' & gt;@&lt;/ a & gt; 杰哥很忙 & lt; br / &gt; 个人觉得Signalr把webSocket，foreverFrame，serverSentEvents，longPolling都抽象封装了显得复杂而没必要：&lt; br / &gt; 1、多少会造成协议不透明，像比如自己开发其它平台的客户端显得不容易。&lt; br / &gt; 2、这么多协议的目的是为了web上也能双工而已，我觉得foreverFrame，serverSentEvents，longPolling模拟双工都是鸡肋，不如在不支持webSocket的浏览器上用flash来帮助浏览器实现webSocket的功能来得简单直接。</ content >
                                
   //                                 </ entry >
                                
   //                                 < entry >
                                
   //                                     < id > 3335511 </ id >
                                
   //                                     < title type = "text" />
                                 
   //                                      < published > 2015 - 12 - 27T14: 16:43 + 08:00 </ published >
                                           
   //                                                < updated > 2016 - 02 - 11T06: 27:22Z </ updated >
                                                   
   //                                                        < author >
                                                   
   //                                                            < name > jiulang </ name >
                                                   
   //                                                            < uri > http://home.cnblogs.com/u/160192/</uri>
   //     </ author >
   //     < content type = "text" > &lt; a href = "#3335440" title = "查看所回复的评论" onclick = 'commentManager.renderComments(0,50,3335440);' & gt;@&lt;/ a & gt; 杰哥很忙 & lt; br / &gt; 还有一个，比如我有一台单片机的硬件，想作Signalr的客户端，管理员在Web就能看到这个硬件目前的实时情况，这个想单独使用Signalr做不到吧。为了能与web通讯而在单片机开发Signalr的客户端不现实，再架个服务做Signalr与单片机硬件的中间桥梁，显得Signalr的牛刀却连只鸡也杀不了。</ content >
                        
   //                         </ entry >
                        
   //                         < entry >
                        
   //                             < id > 3335555 </ id >
                        
   //                             < title type = "text" />
                         
   //                              < published > 2015 - 12 - 27T16: 01:28 + 08:00 </ published >
                                   
   //                                        < updated > 2016 - 02 - 11T06: 27:22Z </ updated >
                                           
   //                                                < author >
                                           
   //                                                    < name > 杰哥很忙 </ name >
                                           
   //                                                    < uri > http://home.cnblogs.com/u/580757/</uri>
   //     </ author >
   //     < content type = "text" > &lt; a href = "#3335511" title = "查看所回复的评论" onclick = 'commentManager.renderComments(0,50,3335511);' & gt;@&lt;/ a & gt; jiulang & lt; br / &gt; signalr的作用并不是让开发者知道底层是怎么通信的,而是一个通用的解决方案,再者,你想在非web及非.net环境作为客户端,这本来就不是官方推荐支持的,选择一项技术首先要了解所需要的作用范围,合适才是最好。</ content >
                        
   //                         </ entry >
   //                     </ feed >

                #endregion
                if (xml == null) return null;
                XElement xElement = XElement.Parse(xml);
                return xElement.Elements("entry")
                    .Select(entry => new CnBlogComment(entry))
                    .ToList();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取48小时阅读排行榜
        /// </summary>
        /// <param name="itemCount"></param>
        /// <returns></returns>
        public async static Task<List<CnBlog>> Get48TopViewsAysnc(int itemCount)
        {
            try
            {
                string url = string.Format(BlogsConstant.FortyEightHoursTopViewPosts, itemCount);
                string xml = await HttpService.SendGetRequest(url);
                #region
   //             <? xml version = "1.0" encoding = "utf-8" ?>
   //< feed xmlns = "http://www.w3.org/2005/Atom" >
   
   //    < title type = "text" > 博客园_48小时阅读排行 </ title >
    
   //     < id > uuid:07a7723c - 4143 - 49a5 - 86e8 - 3ed9f58adb2f; id = 22430 </ id >
                
   //                 < updated > 2016 - 02 - 11T06: 28:17Z </ updated >
                        
   //                         < entry >
                        
   //                             < id > 5184851 </ id >
                        
   //                             < title type = "text" > 2015，平凡之路 </ title >
                             
   //                                  < summary type = "text" > 2015年过去了，躺在床上拖着略微疲惫的身体，点开了QQ空间，突然发觉每年都会写点什么，不过去年是写在自己的技术博客上，今年还是老样子，写在QQ空间里面，顺便记录一下自己的点滴 2015年，感觉自己当初毅然决然的花时间搞技术，搞IT是对的，在今年越花时间在技术上，收获越多，甚至有点稍微打乱了原先的生 </ summary >
                                  
   //                                       < published > 2016 - 02 - 09T22: 08:00 + 08:00 </ published >
                                            
   //                                                 < updated > 2016 - 02 - 11T06: 28:17Z </ updated >
                                                    
   //                                                         < author >
                                                    
   //                                                             < name > 爱吃猫的鱼 </ name >
                                                    
   //                                                             < uri > http://www.cnblogs.com/codefish/</uri>
   //         < avatar > http://pic.cnblogs.com/face/u396331.jpg?id=27225144</avatar>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/codefish/p/5184851.html" />
   
   //        < diggs > 4 </ diggs >
   
   //        < views > 507 </ views >
   
   //        < comments > 6 </ comments >
   
   //    </ entry >
   
   //    < entry >
   
   //        < id > 5185543 </ id >
   
   //        < title type = "text" > 前端源码安全 </ title >
    
   //         < summary type = "text" > 今天思考下前端源码安全的东西（不是前端安全，只是针对于源码部分）。在我看来，源码安全有两点，一是防止抄袭，二是防止被攻破。实际上讲，前端的代码大多是没有什么可抄袭性，安全更是形同虚设的（任何前端输入都是不能相信的）。但如果还是想防止源码被查看，HTML、CSS并不能做什么，最终都会用露出来（最简单用 </ summary >
         
   //              < published > 2016 - 02 - 09T14: 42:00 + 08:00 </ published >
                   
   //                        < updated > 2016 - 02 - 11T06: 28:17Z </ updated >
                           
   //                                < author >
                           
   //                                    < name > 海角在眼前 </ name >
                           
   //                                    < uri > http://www.cnblogs.com/lovesong/</uri>
   //         < avatar > http://pic.cnblogs.com/face/555379/20130831110432.png</avatar>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/lovesong/p/5185543.html" />
   
   //        < diggs > 1 </ diggs >
   
   //        < views > 473 </ views >
   
   //        < comments > 2 </ comments >
   
   //    </ entry >
   
   //    < entry >
   
   //        < id > 5185906 </ id >
   
   //        < title type = "text" > 看过年人流高峰，浅聊并发之战[架构篇] </ title >
        
   //             < summary type = "text" > 引语：人多是好事！人多好赚钱。不过这对于技术人员来说，却也不是一个小问题，我对这种问题一直是抱以一颗敬畏之心的。这更多的是一个架构问题，作为一个开发我也就这点见识了！看着微信、支付宝等等大公司发着几个亿的红包的，我急红了眼，不是因为我错过了几个亿（实际上我基本一点都没抢到），而是羡慕他们技术上的牛掰 </ summary >
             
   //                  < published > 2016 - 02 - 10T12: 18:00 + 08:00 </ published >
                       
   //                            < updated > 2016 - 02 - 11T06: 28:17Z </ updated >
                               
   //                                    < author >
                               
   //                                        < name > 等你归去来 </ name >
                               
   //                                        < uri > http://www.cnblogs.com/yougewe/</uri>
   //         < avatar > http://pic.cnblogs.com/face/830731/20160103113200.png</avatar>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/yougewe/p/5185906.html" />
   
   //        < diggs > 1 </ diggs >
   
   //        < views > 424 </ views >
   
   //        < comments > 0 </ comments >
   
   //    </ entry >
   
   //    < entry >
   
   //        < id > 5185536 </ id >
   
   //        < title type = "text" > asp.net mvc 利用过滤器进行网站Meta设置</ title >
        
   //             < summary type = "text" > 过去几年都是用asp.net webform进行开发东西，最近听说过时了，同时webform会产生ViewState（虽然我已经不用ruanat = server的控件好久了 :)），对企业应用无所谓，但对于互联网应用就不太友好了，这几天学习了一下asp.net mvc，自己做了个网站玩玩（asp.ne </ summary >
               
   //                    < published > 2016 - 02 - 09T14: 30:00 + 08:00 </ published >
                         
   //                              < updated > 2016 - 02 - 11T06: 28:17Z </ updated >
                                 
   //                                      < author >
                                 
   //                                          < name > MHL </ name >
                                 
   //                                          < uri > http://www.cnblogs.com/mmmhhhlll/</uri>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/mmmhhhlll/p/5185536.html" />
   
   //        < diggs > 2 </ diggs >
   
   //        < views > 282 </ views >
   
   //        < comments > 4 </ comments >
   
   //    </ entry >
   //</ feed >

                #endregion
                if (xml == null) return null;
                XElement xElement = XElement.Parse(xml);
                return xElement.Elements("entry")
                    .Select(entry => new CnBlog(entry))
                    .ToList();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 十天推荐榜
        /// </summary>
        /// <param name="itemCount"></param>
        /// <returns></returns>
        public async static Task<List<CnBlog>> GetTenDaysTopDiggPostsAysnc(int itemCount)
        {
            try
            {
                string url = string.Format(BlogsConstant.TenDaysTopDiggPosts, itemCount);
                string xml = await HttpService.SendGetRequest(url);
                #region
   //             <? xml version = "1.0" encoding = "utf-8" ?>
   //< feed xmlns = "http://www.w3.org/2005/Atom" >
   
   //    < title type = "text" > 博客园_10天内推荐排行 </ title >
    
   //     < id > uuid:07a7723c - 4143 - 49a5 - 86e8 - 3ed9f58adb2f; id = 22468 </ id >
                
   //                 < updated > 2016 - 02 - 11T06: 29:26Z </ updated >
                        
   //                         < entry >
                        
   //                             < id > 5180939 </ id >
                        
   //                             < title type = "text" > 玩转Asp.net MVC 的八个扩展点</ title >
                             
   //                                  < summary type = "text" > MVC模型以低耦合、可重用、可维护性高等众多优点已逐渐代替了WebForm模型。能够灵活使用MVC提供的扩展点可以达到事半功倍的效果，另一方面Asp.net MVC优秀的设计和高质量的代码也值得我们去阅读和学习。 本文将介绍Asp.net MVC中常用的八个扩展点并举例说明。 一、ActionRes </ summary >
                                  
   //                                       < published > 2016 - 02 - 04T21: 02:00 + 08:00 </ published >
                                            
   //                                                 < updated > 2016 - 02 - 11T06: 29:26Z </ updated >
                                                    
   //                                                         < author >
                                                    
   //                                                             < name > richiezhang </ name >
                                                    
   //                                                             < uri > http://www.cnblogs.com/richieyang/</uri>
   //         < avatar > http://pic.cnblogs.com/face/600216/20140118232824.png</avatar>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/richieyang/p/5180939.html" />
   
   //        < diggs > 69 </ diggs >
   
   //        < views > 2309 </ views >
   
   //        < comments > 35 </ comments >
   
   //    </ entry >
   
   //    < entry >
   
   //        < id > 5173488 </ id >
   
   //        < title type = "text" > 博客园—Android客户端 </ title >
        
   //             < summary type = "text" > 如果有一个博客园客户端支持：点赞、支持、反对、评论、@、收藏等等等等，那么博客园的新闻、博文评论区是否能更加活跃？园友能否更加积极？进步能否更加快速？博客园能否更加精彩？一起来看看吧。 笔者业余开发的博客园Android客户端版本首次在博客园公布，希望广大园友多多支持，极速省流稳定，当然阅读界面由于 </ summary >
             
   //                  < published > 2016 - 02 - 02T09: 56:00 + 08:00 </ published >
                       
   //                            < updated > 2016 - 02 - 11T06: 29:26Z </ updated >
                               
   //                                    < author >
                               
   //                                        < name > 黄海彬 </ name >
                               
   //                                        < uri > http://www.cnblogs.com/huanghaibin/</uri>
   //         < avatar > http://pic.cnblogs.com/face/813041/20160202103401.png</avatar>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/huanghaibin/p/5173488.html" />
   
   //        < diggs > 54 </ diggs >
   
   //        < views > 2599 </ views >
   
   //        < comments > 42 </ comments >
   
   //    </ entry >
   
   //    < entry >
   
   //        < id > 5172045 </ id >
   
   //        < title type = "text" > 游戏服务端究竟解决了什么问题？</ title >
        
   //             < summary type = "text" > 既然是游戏服务端程序员，那博客里至少还是得有一篇跟游戏服务端有关的文章，今天文章主题就关于游戏服务端。</ summary >
             
   //                  < published > 2016 - 02 - 01T16: 55:00 + 08:00 </ published >
                       
   //                            < updated > 2016 - 02 - 11T06: 29:26Z </ updated >
                               
   //                                    < author >
                               
   //                                        < name > fingerpass </ name >
                               
   //                                        < uri > http://www.cnblogs.com/fingerpass/</uri>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/fingerpass/p/game-server-programming-paradigm.html" />
   
   //        < diggs > 54 </ diggs >
   
   //        < views > 4678 </ views >
   
   //        < comments > 54 </ comments >
   
   //    </ entry >
   
   //    < entry >
   
   //        < id > 5178852 </ id >
   
   //        < title type = "text" > 起点——2015年终总结 </ title >
        
   //             < summary type = "text" > 按照园子里的规矩，又到了一年一度年终总结的时候了，回望2015年，感觉真是短暂的很，稍不留神，时间便跳到年末了。回顾一下这一年的经历，做一个记录供今后来回忆，同时也展望一下2016的美好计划。2015年，如果要给自己找一个关键字的话，我觉得是「起点」，我的方方面面都在2015年找到了一个起点。 工作 </ summary >
             
   //                  < published > 2016 - 02 - 03T01: 33:00 + 08:00 </ published >
                       
   //                            < updated > 2016 - 02 - 11T06: 29:26Z </ updated >
                               
   //                                    < author >
                               
   //                                        < name > 吕大豹 </ name >
                               
   //                                        < uri > http://www.cnblogs.com/lvdabao/</uri>
   //         < avatar > http://pic.cnblogs.com/face/520134/20150225124958.png</avatar>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/lvdabao/p/5178852.html" />
   
   //        < diggs > 49 </ diggs >
   
   //        < views > 2569 </ views >
   
   //        < comments > 34 </ comments >
   
   //    </ entry >
   
   //    < entry >
   
   //        < id > 5179506 </ id >
   
   //        < title type = "text" > ASP.NET Identity系列教程（目录）</ title >
        
   //             < summary type = "text" > 最近看到不少介绍微软ASP.NET Identity技术的文章，但感觉都不够完整深入，本人又恰好曾在Adam Freeman所著的《Pro ASP.NET MVC Platform》一书中看到过有关ASP.NET Identity的完整介绍，为此特将有关章节翻译出来，希望需要了解此项技术的园友能从中...</ summary >
             
   //                  < published > 2016 - 02 - 03T13: 34:00 + 08:00 </ published >
                       
   //                            < updated > 2016 - 02 - 11T06: 29:26Z </ updated >
                               
   //                                    < author >
                               
   //                                        < name > r01cn </ name >
                               
   //                                        < uri > http://www.cnblogs.com/r01cn/</uri>
   //         < avatar > http://pic.cnblogs.com/face/u85296.jpg?id=20181922</avatar>
   //     </ author >
   //     < link rel = "alternate" href = "http://www.cnblogs.com/r01cn/p/5179506.html" />
   
   //        < diggs > 36 </ diggs >
   
   //        < views > 1356 </ views >
   
   //        < comments > 13 </ comments >
   
   //    </ entry >
   //</ feed >

                #endregion
                if (xml == null) return null;
                XElement xElement = XElement.Parse(xml);
                return xElement.Elements("entry")
                    .Select(entry => new CnBlog(entry))
                    .ToList();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取推荐博主
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async static Task<List<CnBloger>> GetRecommendBlogersAsync(int pageIndex,int pageSize)
        {
            try
            {
                string url = string.Format(BlogsConstant.PagingLoadRecommendBloggers, pageIndex, pageSize);
                string xml = await HttpService.SendGetRequest(url);
                #region
//                <? xml version = "1.0" encoding = "utf-8" ?>
//   < feed xmlns = "http://www.w3.org/2005/Atom" >
   
//       < title type = "text" > 博客园_推荐博客 </ title >
    
//        < id > uuid:07a7723c - 4143 - 49a5 - 86e8 - 3ed9f58adb2f; id = 22500 </ id >
                
//                    < updated > 2016 - 02 - 11T06: 30:15Z </ updated >
                        
//                            < entry >
                        
//                                < id > http://www.cnblogs.com/fish-li/</id>
//        < title type = "text" > Fish Li </ title >
     
//             < updated > 2013 - 11 - 18T14: 31:32 + 08:00 </ updated >
               
//                       < link rel = "alternate" href = "http://www.cnblogs.com/fish-li/" />
                  
//                          < blogapp > fish - li </ blogapp >
                  
//                          < avatar > http://pic.cnblogs.com/face/u281816.png?id=28134852</avatar>
//        < postcount > 60 </ postcount >
//    </ entry >
//    < entry >
//        < id > http://www.cnblogs.com/artech/</id>
//        < title type = "text" > Artech </ title >
 
//         < updated > 2015 - 10 - 20T17: 04:39 + 08:00 </ updated >
           
//                   < link rel = "alternate" href = "http://www.cnblogs.com/artech/" />
              
//                      < blogapp > artech </ blogapp >
              
//                      < avatar > http://pic.cnblogs.com/face/u19327.jpg</avatar>
//        < postcount > 556 </ postcount >
//    </ entry >
//    < entry >
//        < id > http://www.cnblogs.com/TomXu/</id>
//        < title type = "text" > 汤姆大叔 </ title >
 
//         < updated > 2015 - 09 - 11T11: 21:47 + 08:00 </ updated >
           
//                   < link rel = "alternate" href = "http://www.cnblogs.com/TomXu/" />
              
//                      < blogapp > TomXu </ blogapp >
              
//                      < avatar > http://pic.cnblogs.com/face/u349491.jpg?id=02230504</avatar>
//        < postcount > 161 </ postcount >
//    </ entry >
//</ feed >

                #endregion
                if (xml == null) return null;
                XElement xElement = XElement.Parse(xml);
                return xElement.Elements("entry")
                    .Select(entry => new CnBloger(entry))
                    .ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}

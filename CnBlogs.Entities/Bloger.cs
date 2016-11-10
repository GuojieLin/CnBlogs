using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{
    //<entry>
    //  <id>http://www.cnblogs.com/fish-li/</id>
    //  <title type = "text" > Fish Li</title>
    //  <updated>2013-11-18T14:31:32+08:00</updated>
    //  <link rel = "alternate" href="http://www.cnblogs.com/fish-li/" />
    //  <blogapp>fish-li</blogapp>
    //  <avatar>http://pic.cnblogs.com/face/u281816.png?id=28134852</avatar>
    //  <postcount>60</postcount>
    //</entry>
    class Bloger
    {
        public int Uri { get; set; }
        public string Name { get; set; }
        public string BlogApp { get; set; }
        public string BlogerHome { get; set; }
        public string Updated { get; set; }
        public string Avator { get; set; }
        public string PostCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace recsc
{
    public enum SycleTime {毎週,一回のみ,毎日};
    public enum Channels {NHK総合=1,Eテレ,tvk,日本テレビ,テレビ朝日,TBS,テレビ東京,フジテレビジョン,TOKYO_MX,
        BS日テレ = 141,
        ＷＯＷＯＷプライム = 191,
        ＷＯＷＯＷライブ = 192,
        ＷＯＷＯＷシネマ = 193,
        スターチャンネル１ = 200,
        スターチャンネル２ = 201,
        スターチャンネル３ = 202,
        ＢＳ１１イレブン = 211,
        ＢＳスカパー = 241,
        ＡＴＸ = 333,
        ディスカバリー = 340
    }


    static class EnumeExt
    {
        // Gender に対する拡張メソッドの定義
        public static string DisplayName(this SycleTime st)
        {
            string[] names = { "毎週", "一回のみ", "毎日" };
            return names[(int)st];
        }
        //public static string DisplayName(this Channels ch)
        //{
        //    string[] names = { "不定","NHK総合", "NHKEテレテレ東京", "tvk", "日本テレビ", "テレビ朝日", "TBS", "テレビ東京","フジテレビジョン","TOKYO_MX" };
        //    return names[(int)ch];
        //}
        public static SycleTime ToSycleTime(string str)
        {
            switch (str)
            {
                case "毎週":
                    return SycleTime.毎週;
                case "一回のみ":
                    return SycleTime.一回のみ;
                case "毎日":
                    return SycleTime.毎日;
                default:
                    return SycleTime.毎週;
            }
        }
        //public static Channels ToChannels(string str)
        //{
        //    switch (str)
        //    {
        //        case "NHK総合":
        //            return Channels.NHK総合;
        //        case "Eテレ":
        //            return Channels.Eテレ;
        //        case "tvk":
        //            return Channels.tvk;
        //        case "日本テレビ":
        //            return Channels.日本テレビ;
        //        case "テレビ朝日":
        //            return Channels.テレビ朝日;
        //        case "TBS":
        //            return Channels.TBS;
        //        case "テレビ東京":
        //            return Channels.テレビ東京;
        //        case "フジテレビジョン":
        //            return Channels.フジテレビジョン;
        //        case "TOKYO_MX":
        //            return Channels.TOKYO_MX;
        //        default:
        //            return Channels.TOKYO_MX;
        //    }
        //}

    }

    public class Schedule : IComparable<Schedule>
    {
        public string chName { get; set; }
        public Channels channel { get; set; }
        public DayOfWeek recWeek { get; set; }
        public DateTime recTime { get; set; }
        public SycleTime sycleTime { get; set; } //= SycleTime.毎週;      
        public TimeSpan recSpan { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public bool startFlag = true;  
        
        [System.Xml.Serialization.XmlIgnore]
        public Process ptv { get; set; }

        public string recSpanStr
        {
            get { return recSpan.ToString(); }
            set { recSpan = TimeSpan.Parse(value); }
        }

        public Schedule()
        {
            chName = "ななし";
            recTime = DateTime.Now.AddHours(1);
            recWeek = recTime.DayOfWeek;
            channel = Channels.TOKYO_MX;
        }

        public Schedule(string name,Channels ch,DateTime time,DayOfWeek week)
        {  
            chName = name;
            recTime = time;
            recWeek = week;
            channel = ch;
            recSpan = TimeSpan.FromMinutes(30);        
        }

        public Schedule(string name, Channels ch, DateTime time, DayOfWeek week,TimeSpan sp)
        {
            chName = name;
            recTime = time;
            recWeek = week;
            channel = ch;
            recSpan = sp;
        }

        public string ToArgOption(string chsr ="/rch ")
        {
            string name = chName +"_"+ DateTime.Now.ToString("yyMMdd-HHmmss");
            return " /rec " + chsr + (int)channel + "/mute /min" +
                    //" /recfile \"D:\\Users\\" + Environment.UserName + "\\Videos\\" + name +
                    " /recfile \"C:\\Users\\ATARASHIUNAGI\\Videos\\" + name +
                    ".ts\" ";

            //return "/rch " + (int)channel + " /rec " +
            //    "/recduration " + recSpan.Seconds.ToString() +
            //    " /recfile \"D:\\Users\\"+Environment.UserName+"\\Videos\\" + chName +
            //    ".ts\" ";
        }

        public int CompareTo(Schedule sc)
        {
            return recTime.CompareTo(sc.recTime);
        }

        public override String ToString()
        {
            return chName + ":" + recTime.ToString();
        }
    }
}

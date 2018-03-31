using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace recsc
{
    public class Settings
    {
        //private static Settings settingInstance = new Settings();

        public string tvtestPath;
        public string tvtestBsPath;

        public List<Schedule> scList;

        private Settings()
        {
            //ReadSettings();
        }


        //public static Settings GetInstance()
        //{
        //    return settingInstance;
        //}

        public void WriteSettings()
        {
            //XML処理 
            string filename = "config.xml";
            XmlSerializer serializzer = new XmlSerializer(typeof(Settings));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                filename,false, new System.Text.UTF8Encoding(false));
            serializzer.Serialize(sw, this);
            sw.Close();
        }

        public static Settings ReadSettings()
        {
            Settings set;
            //＜XMLファイルから読み込む＞
            //XmlSerializerオブジェクトの作成
            string fileName = "config.xml";
            XmlSerializer serializer2 =new XmlSerializer(typeof(Settings));
            //ファイルを開く
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName, new System.Text.UTF8Encoding(false));
            //XMLファイルから読み込み、逆シリアル化する
            set =(Settings)serializer2.Deserialize(sr);
            //閉じる
            sr.Close();
            return set;
        }

        public void SetSettings(string tvpath,List<Schedule> sc)
        {
            tvtestPath = tvpath;
            scList = sc;
        }
        public void SetSettings(string tvpath, string bspath, List<Schedule> sc)
        {
            tvtestPath = tvpath;
            tvtestBsPath = bspath;
            scList = sc;
        }
    }
}

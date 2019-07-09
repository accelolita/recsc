using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recsc
{
    public class ScheduleList :List<Schedule>
    {
        /// <summary>
        /// 来週の録画時刻をセットする
        /// </summary>
        public void Next()
        {
            DateTime now = DateTime.Now;
            foreach (var item in this)
            {
                while (now.CompareTo(item.recTime)==1)
                {
                    if (item.sycleTime==SycleTime.毎週)
                    {
                        item.recTime=item.recTime.AddDays(7);
                    }
                    else if (item.sycleTime==SycleTime.毎日)
                    {
                        item.recTime = item.recTime.AddDays(1);
                    }
                }
            }
        }
    }
}

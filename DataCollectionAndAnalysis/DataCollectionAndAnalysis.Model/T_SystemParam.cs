using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace DataCollectionAndAnalysis.Model
{
    /// <summary>
    /// 系统参数
    /// </summary>
    [Table("T_SystemParam")]
    public class T_SystemParam:BasePoco
    {
        /// <summary>
        /// 固有频率
        /// </summary>
        public double natural_frequency { get; set; }
        /// <summary>
        /// 当前主轴转速
        /// </summary>
        public double current_spindle_speeds { get; set; }
        /// <summary>
        /// 最高主轴转速
        /// </summary>
        public double max_spindle_speeds { get; set; }
        /// <summary>
        /// 当前进给率
        /// </summary>
        public double current_feedrates { get; set; }
    }
}

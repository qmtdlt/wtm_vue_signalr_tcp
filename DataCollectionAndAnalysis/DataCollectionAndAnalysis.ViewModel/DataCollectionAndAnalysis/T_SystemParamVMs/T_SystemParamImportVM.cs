using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DataCollectionAndAnalysis.Model;


namespace DataCollectionAndAnalysis.ViewModel.DataCollectionAndAnalysis.T_SystemParamVMs
{
    public partial class T_SystemParamTemplateVM : BaseTemplateVM
    {
        public ExcelPropety natural_frequency_Excel = ExcelPropety.CreateProperty<T_SystemParam>(x => x.natural_frequency);
        public ExcelPropety current_spindle_speeds_Excel = ExcelPropety.CreateProperty<T_SystemParam>(x => x.current_spindle_speeds);
        public ExcelPropety max_spindle_speeds_Excel = ExcelPropety.CreateProperty<T_SystemParam>(x => x.max_spindle_speeds);
        public ExcelPropety current_feedrates_Excel = ExcelPropety.CreateProperty<T_SystemParam>(x => x.current_feedrates);

	    protected override void InitVM()
        {
        }

    }

    public class T_SystemParamImportVM : BaseImportVM<T_SystemParamTemplateVM, T_SystemParam>
    {

    }

}

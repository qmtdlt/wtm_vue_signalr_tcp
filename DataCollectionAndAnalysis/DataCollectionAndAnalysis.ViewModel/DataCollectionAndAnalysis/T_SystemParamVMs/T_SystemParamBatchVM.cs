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
    public partial class T_SystemParamBatchVM : BaseBatchVM<T_SystemParam, T_SystemParam_BatchEdit>
    {
        public T_SystemParamBatchVM()
        {
            ListVM = new T_SystemParamListVM();
            LinkedVM = new T_SystemParam_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class T_SystemParam_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}

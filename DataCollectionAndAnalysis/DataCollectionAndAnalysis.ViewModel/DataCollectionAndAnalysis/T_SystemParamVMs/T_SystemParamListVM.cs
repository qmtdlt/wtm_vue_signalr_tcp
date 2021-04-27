using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DataCollectionAndAnalysis.Model;


namespace DataCollectionAndAnalysis.ViewModel.DataCollectionAndAnalysis.T_SystemParamVMs
{
    public partial class T_SystemParamListVM : BasePagedListVM<T_SystemParam_View, T_SystemParamSearcher>
    {

        protected override IEnumerable<IGridColumn<T_SystemParam_View>> InitGridHeader()
        {
            return new List<GridColumn<T_SystemParam_View>>{
                this.MakeGridHeader(x => x.natural_frequency),
                this.MakeGridHeader(x => x.current_spindle_speeds),
                this.MakeGridHeader(x => x.max_spindle_speeds),
                this.MakeGridHeader(x => x.current_feedrates),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<T_SystemParam_View> GetSearchQuery()
        {
            var query = DC.Set<T_SystemParam>()
                .Select(x => new T_SystemParam_View
                {
				    ID = x.ID,
                    natural_frequency = x.natural_frequency,
                    current_spindle_speeds = x.current_spindle_speeds,
                    max_spindle_speeds = x.max_spindle_speeds,
                    current_feedrates = x.current_feedrates,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class T_SystemParam_View : T_SystemParam{

    }
}

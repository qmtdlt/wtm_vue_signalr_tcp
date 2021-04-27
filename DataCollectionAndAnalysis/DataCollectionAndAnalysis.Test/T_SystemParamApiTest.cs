using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using DataCollectionAndAnalysis.Controllers;
using DataCollectionAndAnalysis.ViewModel.DataCollectionAndAnalysis.T_SystemParamVMs;
using DataCollectionAndAnalysis.Model;
using DataCollectionAndAnalysis.DataAccess;


namespace DataCollectionAndAnalysis.Test
{
    [TestClass]
    public class T_SystemParamApiTest
    {
        private T_SystemParamController _controller;
        private string _seed;

        public T_SystemParamApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<T_SystemParamController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new T_SystemParamSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            T_SystemParamVM vm = _controller.Wtm.CreateVM<T_SystemParamVM>();
            T_SystemParam v = new T_SystemParam();
            
            v.natural_frequency = 70;
            v.current_spindle_speeds = 1;
            v.max_spindle_speeds = 12;
            v.current_feedrates = 49;
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<T_SystemParam>().Find(v.ID);
                
                Assert.AreEqual(data.natural_frequency, 70);
                Assert.AreEqual(data.current_spindle_speeds, 1);
                Assert.AreEqual(data.max_spindle_speeds, 12);
                Assert.AreEqual(data.current_feedrates, 49);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            T_SystemParam v = new T_SystemParam();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.natural_frequency = 70;
                v.current_spindle_speeds = 1;
                v.max_spindle_speeds = 12;
                v.current_feedrates = 49;
                context.Set<T_SystemParam>().Add(v);
                context.SaveChanges();
            }

            T_SystemParamVM vm = _controller.Wtm.CreateVM<T_SystemParamVM>();
            var oldID = v.ID;
            v = new T_SystemParam();
            v.ID = oldID;
       		
            v.natural_frequency = 62;
            v.current_spindle_speeds = 16;
            v.max_spindle_speeds = 95;
            v.current_feedrates = 73;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.natural_frequency", "");
            vm.FC.Add("Entity.current_spindle_speeds", "");
            vm.FC.Add("Entity.max_spindle_speeds", "");
            vm.FC.Add("Entity.current_feedrates", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<T_SystemParam>().Find(v.ID);
 				
                Assert.AreEqual(data.natural_frequency, 62);
                Assert.AreEqual(data.current_spindle_speeds, 16);
                Assert.AreEqual(data.max_spindle_speeds, 95);
                Assert.AreEqual(data.current_feedrates, 73);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            T_SystemParam v = new T_SystemParam();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.natural_frequency = 70;
                v.current_spindle_speeds = 1;
                v.max_spindle_speeds = 12;
                v.current_feedrates = 49;
                context.Set<T_SystemParam>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            T_SystemParam v1 = new T_SystemParam();
            T_SystemParam v2 = new T_SystemParam();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.natural_frequency = 70;
                v1.current_spindle_speeds = 1;
                v1.max_spindle_speeds = 12;
                v1.current_feedrates = 49;
                v2.natural_frequency = 62;
                v2.current_spindle_speeds = 16;
                v2.max_spindle_speeds = 95;
                v2.current_feedrates = 73;
                context.Set<T_SystemParam>().Add(v1);
                context.Set<T_SystemParam>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<T_SystemParam>().Find(v1.ID);
                var data2 = context.Set<T_SystemParam>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}

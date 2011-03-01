using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

/**
 * 本文件代码可以实现ui界面的异步任务
 * 使用示例（使用时完全拷贝下面代码即可）：
 * 
 AsyTaskManager.getInstance().AddTask(new AsyTask(
                delegate()
                {
                    //耗时线程代码，
                    //return objA;//最后返回这个对象处理的结果
                },
                delegate(object objA)
                {
                    //再次回到ui线程，这里的objA也就是上面返回的objA
                }));
 
 */

namespace ITJZ.CTFY.UIAsynTask
{
    public delegate object AsyTaskReasonDelegate();
    public delegate void AsyTaskResultDelegate(object o);
    public class AsyTask
    {
        public AsyTask(AsyTaskReasonDelegate fromMethod, AsyTaskResultDelegate toMethod)
        {
            this.fromMethod = fromMethod;
            this.toMethod = toMethod;
        }

        public AsyTaskReasonDelegate fromMethod;
        public AsyTaskResultDelegate toMethod;
        public object toObj;
    }

    public class AsyTaskManager
    {

        private static Hashtable allAsyTaskManager = new Hashtable();
        private AsyTaskManager()
        {
            Timer timer = new Timer(
                new TimerCallback(delegate(object o) { AutoTimerMethod(); }),
                new AutoResetEvent(false),
                5,
                20);
        }
        /// <summary>
        /// 获取AsyTaskManager示例，此类将自动根据线程生成相应的实例。 即每个线程最多对应一个AsyTaskManager
        /// </summary>
        /// <returns></returns>
        public static AsyTaskManager getInstance()
        {
            int threadHashCode = Thread.CurrentThread.GetHashCode();
            if (null == allAsyTaskManager[threadHashCode])
            {
                allAsyTaskManager[threadHashCode] = new AsyTaskManager(); 
            }
            return allAsyTaskManager[threadHashCode] as AsyTaskManager;
        }

        /// <summary>
        /// 添加异步任务
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void AddTask(AsyTask asytask)
        {
            asytask.fromMethod.BeginInvoke(new AsyncCallback(delegate(IAsyncResult ir)
            {
                lock (oktask)
                {
                    oktask.Push(asytask);
                }
                asytask.toObj = ((AsyTaskReasonDelegate)ir.AsyncState).EndInvoke(ir);
            }), asytask.fromMethod);
        }

        private Stack<AsyTask> oktask = new Stack<AsyTask>();

        /// <summary>
        /// UI线程定时检查任务
        /// </summary>
        private void AutoTimerMethod()
        {
            lock (oktask)
            {
                //在主线程中执行完已经完成的任务
                while (oktask.Count > 0)
                {
                    AsyTask task = oktask.Pop();
                    task.toMethod(task.toObj);
                }
            }
        }
    }
}


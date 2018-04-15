namespace AutoScalping.Threading
{
    public delegate void TaskWithParamsEventHandler(object param);
    public delegate void TaskWithoutParamEventHandler();

    public class ThreadProxy
    {
        private TaskWithParamsEventHandler task;
        private TaskWithoutParamEventHandler nonTask;
        private object objParams;

        public ThreadProxy(TaskWithParamsEventHandler task,object param)
        {
            this.task = task; 
            this.objParams = param;
        }

        public ThreadProxy(TaskWithoutParamEventHandler task)
        {
            this.nonTask = task;
        }

        internal void DoWork()
        {
            if (task != null)
                task.Invoke(objParams);
            else
                nonTask.Invoke();
        }
    }
}

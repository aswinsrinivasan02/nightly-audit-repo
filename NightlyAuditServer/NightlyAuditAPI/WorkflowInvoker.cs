using BallyTech.Infrastructure.Utilities;
using BallyTech.Infrastructure.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightlyAuditAPI
{
    public class WorkflowInvoker
    {
        private class Executor<TInput, TOutput>
        {
            private readonly IWorkflowExecutor executor;
            private TInput input;
            private TOutput output;

            public Executor(IWorkflowExecutor executor)
            {
                this.executor = executor;
            }

            public void SetInput(TInput input)
            {
                this.input = input;
            }

            public void Run()
            {
                this.output = executor.Run<TInput, TOutput>(this.input);
            }

            public TOutput GetOutput()
            {
                return this.output;
            }
        }

        public static TReturn Invoke<TParam, TReturn>(System.String workflow,
                                                      IWorkflowService workflowService,
                                                      IUtilityProvider utility,
                                                      TParam param)
        {
            var executor = new Executor<TParam, TReturn>(workflowService.GetExecutor(workflow));
            executor.SetInput(param);
            var safeblock = utility.GetSafeBlockProvider().CreateResult("Service.Data");
            return safeblock.Invoke(executor.Run, null, null, executor.GetOutput);
        }
    }
}

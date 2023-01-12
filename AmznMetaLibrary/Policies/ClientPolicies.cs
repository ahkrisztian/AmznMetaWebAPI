using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Policies
{
    public class ClientPolicies
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }

        public ClientPolicies()
        {
            ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode).WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(3));
        }

    }
}

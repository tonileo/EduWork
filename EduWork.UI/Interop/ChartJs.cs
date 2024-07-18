using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace EduWork.UI.Interop
{
    public static class ChartJs
    {
        //private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        //public IJSRuntime JsRuntime { get; }

        //public ChartJs(IJSRuntime jsRuntime)
        //{
        //    moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
        //    "import", "./js/interopFunctions.js").AsTask());

        //    JsRuntime = jsRuntime;
        //}

        public static ValueTask<object> CreateChartAsync(IJSRuntime jsRuntime, string canvasId, string chartType, object chartData)
        {
            //var module = await moduleTask.Value;
            return jsRuntime.InvokeAsync<object>("createChart", canvasId, chartType, chartData);
        }
    }

}
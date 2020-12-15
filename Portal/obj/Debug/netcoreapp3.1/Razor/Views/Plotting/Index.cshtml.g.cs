#pragma checksum "C:\workspace\Portal\Portal\Views\Plotting\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b4aa7a795f1653c2b395cc0003362804ad9f8c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Plotting_Index), @"mvc.1.0.view", @"/Views/Plotting/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\workspace\Portal\Portal\Views\_ViewImports.cshtml"
using Portal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\workspace\Portal\Portal\Views\_ViewImports.cshtml"
using Portal.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b4aa7a795f1653c2b395cc0003362804ad9f8c2", @"/Views/Plotting/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea73c18953461b2da0883cbece56eb399a5ce162", @"/Views/_ViewImports.cshtml")]
    public class Views_Plotting_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\workspace\Portal\Portal\Views\Plotting\Index.cshtml"
  
    ViewData["Title"] = "Plotting";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Index</h1>\r\n\r\n<script src=\"https://cdn.plot.ly/plotly-latest.min.js\"></script>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b4aa7a795f1653c2b395cc0003362804ad9f8c23307", async() => {
                WriteLiteral("\r\n    <div id=\"scatter\"></div>\r\n    <div id=\"bar\"></div>\r\n    <div id=\"time\"></div>\r\n    <div id=\"pie\"></div>\r\n    <div id=\"gauge\"></div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<script>
    function getrandom(num, mul) {
        var value = [];
        for (i = 0; i <= num; i++) {
            var rand = Math.random() * mul;
            value.push(rand);
        }
        return value;
    }


    var data = [
        {
            opacity: 0.4,
            type: 'scatter3d',
            x: getrandom(50, -75),
            y: getrandom(50, -75),
            z: getrandom(50, -75),
        },
        {
            opacity: 0.5,
            type: 'scatter3d',
            x: getrandom(50, -75),
            y: getrandom(50, 75),
            z: getrandom(50, 75),
        },
        {
            opacity: 0.5,
            type: 'scatter3d',
            x: getrandom(50, 100),
            y: getrandom(50, 100),
            z: getrandom(50, 100),
        }
    ];
    var layout = {
        scene: {
            aspectmode: ""manual"",
            aspectratio: {
                x: 1, y: 0.7, z: 1,
            },
            xaxis: {
                ntick");
            WriteLiteral(@"s: 9,
                range: [-200, 100],
            },
            yaxis: {
                nticks: 7,
                range: [-100, 100],
            },
            zaxis: {
                nticks: 10,
                range: [-150, 100],
            }
        },
    };
    Plotly.newPlot('scatter', data, layout);
    var lp = ['HH-LE-123', 'HH-LE-124', 'HH-LE-125', 'HH-LE-126', 'HH-LE-127', 'HH-LE-128', 'HH-LE-129', 'HH-LE-130', 'HH-LE-131', 'HH-LE-132', 'HH-LE-133', 'HH-LE-134', 'HH-LE-135', 'HH-LE-136', 'HH-LE-137', 'HH-LE-138', 'HH-LE-139', 'HH-LE-140', 'HH-LE-141', 'HH-LE-142', 'HH-LE-143', 'HH-LE-144', 'HH-LE-145', 'HH-LE-146', 'HH-LE-147', 'HH-LE-148', 'HH-LE-149', 'HH-LE-150', 'HH-LE-151', 'HH-LE-152'];

    var dates = [new Date('01-01-2018'), new Date('01-01-2018'), new Date('01-01-2018'), new Date('01-01-2018'), new Date('01-01-2018'), new Date('01-03-2018'), new Date('01-03-2018'), new Date('01-03-2018'), new Date('01-03-2018'), new Date('01-03-2018'), new Date('01-07-2018'), new");
            WriteLiteral(@" Date('01-07-2018'), new Date('01-07-2018'), new Date('01-07-2018'), new Date('01-08-2018'), new Date('01-08-2018'), new Date('01-08-2018'), new Date('01-08-2018'), new Date('01-08-2018'), new Date('01-08-2018'), new Date('01-10-2018'), new Date('01-10-2018'), new Date('01-10-2018'), new Date('01-10-2018'), new Date('01-10-2018'), new Date('01-10-2018'), new Date('01-10-2018'), new Date('01-10-2018'), new Date('01-12-2018'), new Date('01-12-2018')];

    var dates_unique = [new Date('01-01-2018'), new Date('01-03-2018'), new Date('01-07-2018'), new Date('01-08-2018'), new Date('01-10-2018'), new Date('01-12-2018')];
    var dates_unique_text = ['01.01.2018', '03.01.2018', '07.01.2018', '08.01.2018', '10.01.2018', '12.01.2018',];
    var startDate = new Date(""2018-01-01"");
    var endDate = new Date(""2018-01-31"");

    var getDateArray = function (start, end) {
        var arr = new Array();
        var dt = new Date(start);
        while (dt <= end) {
            arr.push(new Date(dt));
          ");
            WriteLiteral(@"  dt.setDate(dt.getDate() + 1);
        }
        return arr;
    }
    var dates_complete = getDateArray(startDate, endDate);

    var v1 = [60, 2, 57, 75, 94, 12, 49, 37, 38, 46, 59, 50, 14, 3, 68, 1, 56, 96, 83, 54, 4, 70, 77, 26, 81, 74, 27, 35, 10, 67];
    var v2 = [89, 8, 80, 48, 36, 98, 32, 74, 29, 56, 5, 5, 51, 6, 55, 2, 60, 9, 57, 1, 99, 48, 52, 94, 86, 40, 53, 89, 83, 60];
    var v3 = [71, 19, 10, 83, 82, 62, 12, 55, 9, 55, 31, 26, 68, 29, 16, 5, 43, 40, 56, 56, 40, 72, 90, 60, 47, 46, 15, 20, 45, 81];

    var v1_by_date = [288, 182, 126, 358, 394, 77];
    var v1_by_date_with_null = [288, null, 182, null, null, null, 126, 358, null, 394, null, 77, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null];

    var v1_by_date_with_zeroes = [288, 0, 182, 0, 0, 0, 126, 358, 0, 394, 0, 77, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
    // BAR PLOT

    var series_bar_1 = {
        type: 'bar',
        x: lp,
  ");
            WriteLiteral(@"      y: v1,
    };
    var series_bar_2 = {
        type: 'bar',
        x: lp,
        y: v2,
    };
    var series_bar_3 = {
        type: 'bar',
        x: lp,
        y: v3,
    };

    var data_bar = [series_bar_1, series_bar_2, series_bar_3];

    var layout_bar = {
        displayLogo: false,
    }

    Plotly.newPlot('bar', data_bar, layout_bar, {
        displaylogo: false,
        responsive: true
    });

    // BAR PLOT WITH TIMESERIES

    var series_time_1 = {
        mode: ""lines"",
        x: dates_complete,
        y: v1_by_date_with_zeroes,
    }

    var data_time = [series_time_1];

    var layout_time = {
        title: 'Time Series with Rangeslider',
        xaxis: {
            autorange: true,
            range: ['2018-01-01', '2018-01-31'],
            rangeslider: { range: ['2018-01-01', '2018-01-31'] },
            type: 'date'
        },
        yaxis: {
            autorange: true,
            range: [0, 400],
            type: 'linear'");
            WriteLiteral(@"
        },
        responsive: true
    };

    Plotly.newPlot('time', data_time, layout_time, { displaylogo: false });

    var data_pie = [{
        values: v1_by_date,
        labels: dates_unique_text,
        type: 'pie'
    }];

    Plotly.newPlot('pie', data_pie, {}, { displaylogo: false });


    // Enter a speed between 0 and 180
    var level = 101;

    // Trig to calc meter point
    var degrees = 180 - level,
        radius = 0.5;
    var radians = degrees * Math.PI / 180;
    var x = radius * Math.cos(radians);
    var y = radius * Math.sin(radians);

    // Path: may have to change to create a better triangle
    var mainPath = 'M -.0 -0.025 L .0 0.025 L ',
        pathX = String(x),
        space = ' ',
        pathY = String(y),
        pathEnd = ' Z';
    var path = mainPath.concat(pathX, space, pathY, pathEnd);

    var data = [
        {
            type: 'scatter',
            x: [0], y: [0],
            marker: { size: 28, color: '850000' },
      ");
            WriteLiteral(@"      showlegend: false,
            name: 'Value 1',
            text: level,
            hoverinfo: 'text+name'
        },
        {
            values: [50 / 6, 50 / 6, 50 / 6, 50 / 6, 50 / 6, 50 / 6, 50],
            rotation: 90,
            text: ['251-300', '201-250', '151-200', '101-150',
                '51-100', '0-50', ''],
            textinfo: 'text',
            textposition: 'inside',
            marker: {
                colors: ['rgba(14, 127, 0, .5)', 'rgba(110, 154, 22, .5)',
                    'rgba(170, 202, 42, .5)', 'rgba(202, 209, 95, .5)',
                    'rgba(210, 206, 145, .5)', 'rgba(232, 226, 202, .5)',
                    'rgba(255, 255, 255, 0)']
            },
            labels: ['251-300', '201-250', '151-200', '101-150', '51-100', '0-50', ''],
            hoverinfo: 'label',
            hole: 0.5,
            type: 'pie',
            showlegend: false
        }
    ];

    var layout = {
        shapes: [{
            type: 'path',
        ");
            WriteLiteral(@"    path: path,
            fillcolor: '850000',
            line: {
                color: '850000'
            }
        }],
        title: 'Gauge Speed 0-100',
        xaxis: {
            zeroline: false, showticklabels: false,
            showgrid: false, range: [-1, 1]
        },
        yaxis: {
            zeroline: false, showticklabels: false,
            showgrid: false, range: [-1, 1]
        }
    };

    Plotly.newPlot('gauge', data, layout, { displaylogo: false });
</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

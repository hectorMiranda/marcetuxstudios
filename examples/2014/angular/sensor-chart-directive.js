// A directive that bridges Angular data to a D3 time-series chart.
// The directive owns the SVG; Angular owns the data and the update cycle.
angular.module('sensorDash')
  .directive('sensorChart', function () {
    return {
      restrict: 'E',
      scope: { readings: '=' },
      link: function (scope, element) {
        var margin = { top: 10, right: 20, bottom: 30, left: 50 };
        var width  = 600 - margin.left - margin.right;
        var height = 200 - margin.top  - margin.bottom;

        var svg = d3.select(element[0]).append('svg')
          .attr('width',  width  + margin.left + margin.right)
          .attr('height', height + margin.top  + margin.bottom)
          .append('g').attr('transform', 'translate(' + margin.left + ',' + margin.top + ')');

        var x = d3.time.scale().range([0, width]);
        var y = d3.scale.linear().range([height, 0]);
        var line = d3.svg.line()
          .x(function (d) { return x(new Date(d.recorded_at)); })
          .y(function (d) { return y(d.value); });

        var path = svg.append('path').attr('class', 'line');

        scope.$watch('readings', function (data) {
          if (!data || !data.length) return;
          x.domain(d3.extent(data, function (d) { return new Date(d.recorded_at); }));
          y.domain(d3.extent(data, function (d) { return d.value; }));
          path.datum(data).attr('d', line);
        });
      }
    };
  });

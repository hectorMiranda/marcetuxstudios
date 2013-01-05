// A tiny AngularJS controller + service. Note the array-annotation DI so it
// survives minification (the sharp edge from the post).
angular.module('dash', [])
  .factory('Bandwidth', ['$http', function ($http) {
    return {
      forCustomer: function (id) {
        return $http.get('/api/bandwidth/' + id).then(function (r) { return r.data; });
      }
    };
  }])
  .controller('DashCtrl', ['$scope', 'Bandwidth', function ($scope, Bandwidth) {
    $scope.rows = [];
    $scope.total = 0;
    $scope.load = function (id) {
      Bandwidth.forCustomer(id).then(function (rows) {
        $scope.rows = rows;
        $scope.total = rows.reduce(function (a, r) { return a + r.gb; }, 0);
      });
    };
    $scope.load(4821);
  }]);

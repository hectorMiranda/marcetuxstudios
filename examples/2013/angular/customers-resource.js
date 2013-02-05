// Wrap a REST resource once; controllers ask the service for data and never
// touch a URL or a verb. Note the array-style DI so it survives minification.
angular.module('dash')
  .factory('Customer', ['$resource', function ($resource) {
    return $resource('/api/v1/customers/:id', { id: '@id' }, {
      // extra action beyond the get/query/save/delete you get for free
      bandwidth: { method: 'GET', url: '/api/v1/customers/:id/bandwidth', isArray: true }
    });
  }])
  .controller('CustomerCtrl', ['$scope', 'Customer', function ($scope, Customer) {
    $scope.customers = Customer.query();              // GET /api/v1/customers
    $scope.open = function (id) {
      $scope.current = Customer.get({ id: id });      // GET /api/v1/customers/:id
      $scope.usage   = Customer.bandwidth({ id: id });
    };
  }]);

// Jasmine + ngMock spec for the Customer service.
// Verifies the $resource calls without hitting the real API.
describe('Customer service', function () {
  var Customer, $httpBackend;

  beforeEach(angular.mock.module('dash'));
  beforeEach(inject(function (_Customer_, _$httpBackend_) {
    Customer     = _Customer_;
    $httpBackend = _$httpBackend_;
  }));

  afterEach(function () {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('GETs a single customer by id', function () {
    $httpBackend.expectGET('/api/v1/customers/42').respond(200, { id: 42, name: 'Acme' });
    var result;
    Customer.get({ id: 42 }, function (c) { result = c; });
    $httpBackend.flush();
    expect(result.name).toBe('Acme');
  });

  it('queries the customer list', function () {
    $httpBackend.expectGET('/api/v1/customers').respond(200, [{ id: 1 }, { id: 2 }]);
    var list = Customer.query();
    $httpBackend.flush();
    expect(list.length).toBe(2);
  });
});

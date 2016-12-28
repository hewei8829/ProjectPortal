//appModule.service('ApiCall', ['$http', function ($http) {
//    var result;

//    var config = { headers: { 'Content-Type': 'text/json' } };
//    // This is used for calling get methods from web api
//    this.GetApiCall = function (url, queryString) {
//        result = $http.get(url + '?' + queryString, config).success(function (data, status) {
//            result = (data);
//        }).error(function () {
//            alert("Something went wrong");
//        });
//        return result;
//    };

//    // This is used for calling post methods from web api with passing some data to the web api controller
//    this.PostApiCall = function (URL, obj) {
//        result = $http.post(URL, obj, config).success(function (data, status) {
//            result = (data);
//        }).error(function () {
//            alert("Something went wrong");
//        });
//        return result;
//    };
//}]);
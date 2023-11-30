var app = angular.module('NgheNhacTrucTuyen', []);
app.controller("UserCtrl", function ($scope, $http) {
     //---------Initialization-------------------------
     $scope.listTopNewAlbum;
     $scope.listTopHotAlbum;

     $scope.listTopHotNhac;
     $scope.listNhacLeft = [];
     $scope.listNhacCenter = [];
     $scope.listNhacRight = [];

     $scope.listNgheSi;

     //-----------Function---------------------------------
     
     //-- Album
     $scope.TopNewAlbum = function(){
          $http({
               method: "POST",
               url: current_url + '/api-user/Album/topnew-album?top=' + 6,
               headers: {
                    'Content-Type': undefined
               }
          }).then(
               function(response){
                    $scope.listTopNewAlbum = response.data;
               }
          )
     }

     $scope.TopHotAlbum = function(){
          $http({
               method: "POST",
               url: current_url + '/api-user/Album/tophot-album?top=' + 6,
               headers: {
                    'Content-Type': undefined
               }
          }).then(
               function(response){
                    $scope.listTopHotAlbum = response.data;
               }
          )
     }

     //-- Nhac
     $scope.TopHotNhac = function(){
          $http({
               method: "GET",
               url: current_url + '/api-user/Nhac/tophot-nhac?top=' + 5,
               headers: {
                    'Content-Type': undefined
               }
          }).then(
               function(response){
                    $scope.listTopHotNhac = response.data.data;
               }
          )
     }

     $scope.LoadNhac = function (size, index, datas) {
          return $http({
              method: 'POST',
              data: { pageIndex: index, pageSize: size },
              url: current_url + '/api-user/nhac/search-nhac',
          }).then(function (response) {
              datas.length = 0;
              Array.prototype.push.apply(datas, response.data.data); // Thêm dữ liệu mới vào mảng datas
              return datas;
          });
     };

     //-- NgheSi
     $scope.LoadNgheSi= function () {
          $http({
              method: 'GET',
              url: current_url + '/api-user/NgheSi/getall-nghesi'
          }).then(function (response) {  
              $scope.listNgheSi = response.data;  
          });
     }

     //---------------------Load-----------------
     $scope.TopNewAlbum();
     $scope.TopHotAlbum();
     $scope.TopHotNhac();
     $scope.LoadNgheSi();
     $scope.LoadNhac(10, 2, $scope.listNhacLeft)
     .then(function () {
         return $scope.LoadNhac(10, 3, $scope.listNhacCenter);
     })
     .then(function () {
         return $scope.LoadNhac(10, 4, $scope.listNhacRight);
     });
})

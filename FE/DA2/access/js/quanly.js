var app = angular.module('NgheNhacTrucTuyen', []);
app.controller("AdminCtrl", function ($scope, $http) {
    //---------Initialization-------------------------
    $scope.img;
    $scope.audio;
    $scope.lyrics;

    $scope.listNhac;
    $scope.listNhacByID;
    $scope.listIndex;
    $scope.listNhacByIDNgheSi;

    $scope.listNgheSi;
    $scope.listNgheSiSearch;
    $scope.NgheSiByID;
    $scope.listIndexNS;

    $scope.listTheLoai;
    $scope.listTheLoaiSearch;
    $scope.TheLoaiByID;
    $scope.listIndexTL;

    $scope.listAlbum;
    $scope.AlbumByID;
    $scope.listNhacTrongAlbum;
    $scope.listNhacKhongCoTrongAlbum;
    $scope.listNhacCapNhat = [];
    $scope.listIndexA;


    //-----------Function---------------------------------
    $scope.UploadFileIMG = function(file) {
        var formData = new FormData();
        formData.append('file', file);

        $http({
            method: "POST",
            url: current_url + '/api-admin/Tool/upload_IMG-tool',
            data: formData,
            headers: {
                'Content-Type': undefined
            }
        })
        .then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else {
                    $scope.img = data.fullPath;
                }
            }
        );
    }

    $scope.UploadFileAUDIO = function(file) {
        var formData = new FormData();
        formData.append('file', file);

        $http({
            method: "POST",
            url: current_url + '/api-admin/Tool/upload_AUDIO-tool',
            data: formData,
            headers: {
                'Content-Type': undefined
            }
        })
        .then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else {
                    $scope.audio = data.fullPath;
                }
            }
        );
    }

    $scope.UploadFileTEXT = function(file) {
        var formData = new FormData();
        formData.append('file', file);
        $http({
            method: "POST",
            url: current_url + '/api-admin/Tool/upload_TEXT-tool',
            data: formData,
            headers: {
                'Content-Type': undefined
            }
        })
        .then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else {
                    $scope.lyrics = data.fullPath;
                }
            }
        );
    }

    $scope.ResetData = function(){
        $scope.img = null;
        $scope.audio = null;
        $scope.lyrics = null;
    }

    //NgheSi
    $scope.LoadNgheSi= function () {
        $http({
            method: 'GET',
            url: current_url + '/api-admin/nghesi/getall-nghesi',
        }).then(function (response) {  
            $scope.listNgheSi = response.data;  
        });
    }

    $scope.AddNgheSi = function(){
        var item = {};
        item.tenNgheSi = $('#tenNgheSi').value;
        item.moTa = $('#moTa').value;
        item.anhDaiDien = $scope.img;
        item.list_jsonchitietnhactheonghesi = [];
        item.list_jsonchitietalbumtheonghesi = [];

        $http({
            method: 'POST',
            url: current_url + '/api-admin/nghesi/create-nghesi',
            data: item,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else if(data.message == 'Nghệ sĩ đã tồn tại') {
                    alert('Nghệ sĩ đã tồn tại');
                }
                else{
                    alert('Thêm thành công');
                }
            },
            function() {
                alert('Có lỗi');
                console.log(item);
            }
        );
    }

    $scope.LoadSearchNgheSi = function(size, index){
        return new Promise(function (resolve, reject) {
            $http({
                method: 'POST',
                data: { pageIndex: index, pageSize: size },
                url: current_url + '/api-admin/nghesi/search-nghesi',
            }).then(function (response) {
                $scope.listNgheSiSearch = response.data.data;
                const pageCount = Math.ceil(response.data.totalItems / $('#pageSizeNS').value);
                $scope.listIndexNS = Array.from({ length: pageCount}, (_, index) => index + 1);
                resolve(); 
            }, function (error) {
                reject(error);
            });
        });
    }

    $scope.GetByIDNgheSi = function(id) {
        return new Promise(function (resolve, reject) {
        $http({
            method: 'GET',
            url: current_url + '/api-admin/nghesi/getbyid-nghesi?idNgheSi=' + id,
            headers: {
                'Content-Type': undefined
            }
        }).then(
            function(response) {
                $scope.NgheSiByID = response.data.data;
                resolve(); 
            },
            function(error) {
                console.error('Error:', error);
                reject(error);
            }
        );
        });
    };

    $scope.LoadNS = function(size,index){
        $scope.LoadSearchNgheSi(size, index).then(function () {
            changeaudio($scope);
            changeimg($scope);
            changetxt($scope);
        });
    };

    $scope.LoadDetailNgheSi = function(id){
        $scope.GetByIDNgheSi(id).then(function () {
            $scope.LoadNS(10,1)
        });
    };

    $scope.UpdateNgheSi = function () {
        var item = {};
        item.idNgheSi = $('#textarea').getAttribute('data-idnghesi');
        item.tenNgheSi = $('#tenNgheSiUpdate').value;
        item.soLuongBaiHat = $('#soLuongBaiHat').value;
        item.moTa = $('#textarea').value;

        if ($scope.img == null) {
            item.anhDaiDien = $('#img').src;
        } else {
            item.anhDaiDien = $scope.img;
        }
        
        item.list_jsonchitietnhactheonghesi = [];
        item.list_jsonchitietalbumtheonghesi = [];
    
        $http({
            method: 'POST',
            url: current_url + '/api-admin/nghesi/update-nghesi',
            data: item,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(
            function (response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else {
                    alert('Cập nhật thành công');
                    $scope.LoadNS(10,1);
                    closeFormUpdateMusic();
                    console.log(item);
                }
            },
            function () {
                alert('Có lỗi');
            }
        );
    };

    //TheLoai
    $scope.LoadTheLoai= function () {
        $http({
            method: 'GET',
            url: current_url + '/api-admin/theloai/getall-theloai',
        }).then(function (response) {  
            $scope.listTheLoai = response.data;  
        });
    } 

    $scope.AddTheLoai = function(){
        var item = {};
        item.tenTheLoai = $('#tenTheLoai').value;
        item.moTa = $('#moTa').value;
        item.anhDaiDien = $scope.img;
        item.list_jsonchitietnhactheotheloai = [];

        $http({
            method: 'POST',
            url: current_url + '/api-admin/theloai/create-theloai',
            data: item,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else if(data.message == 'Thể loại đã tồn tại') {
                    alert('Thể loại đã tồn tại');
                }
                else{
                    alert('Thêm thành công');
                }
            },
            function() {
                alert('Có lỗi');
                console.log(item);
            }
        );
    }

    $scope.LoadSearchTheLoai = function(size, index){
        return new Promise(function (resolve, reject) {
            $http({
                method: 'POST',
                data: { pageIndex: index, pageSize: size },
                url: current_url + '/api-admin/theloai/search-theloai',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('Token')
                }
            }).then(function (response) {
                $scope.listTheLoaiSearch = response.data.data;
                const pageCount = Math.ceil(response.data.totalItems / $('#pageSizeTL').value);
                $scope.listIndexTL = Array.from({ length: pageCount}, (_, index) => index + 1);
                resolve(); 
            }, function (error) {
                reject(error);
            });
        });
    }

    $scope.GetByIDTheLoai = function(id) {
        return new Promise(function (resolve, reject) {
        $http({
            method: 'GET',
            url: current_url + '/api-admin/theloai/getbyid-theloai?idTheLoai=' + id,
            headers: {
                'Content-Type': undefined,
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function(response) {
                $scope.TheLoaiByID = response.data;
                resolve(); 
            },
            function(error) {
                console.error('Error:', error);
                reject(error);
            }
        );
        });
    };

    $scope.LoadTL = function(size,index){
        $scope.LoadSearchTheLoai(size, index).then(function () {
            changeaudio($scope);
            changeimg($scope);
            changetxt($scope);
        });
    };

    $scope.LoadDetailTheLoai = function(id){
        $scope.GetByIDTheLoai(id).then(function () {
            $scope.LoadTL(10,1)
        });
    };

    $scope.UpdateTheLoai = function () {
        var item = {};
        item.idTheLoai = $('#textarea').getAttribute('data-idtheloai');
        item.tenTheLoai = $('#tenTheLoaiUpdate').value;
        item.soLuongBaiHat = $('#soLuongBaiHat').value;
        item.moTa = $('#textarea').value;

        if ($scope.img == null) {
            item.anhDaiDien = $('#img').src;
        } else {
            item.anhDaiDien = $scope.img;
        }
        
        item.list_jsonchitietnhactheotheloai = [];
    
        $http({
            method: 'POST',
            url: current_url + '/api-admin/theloai/update-theloai',
            data: item,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function (response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else {
                    alert('Cập nhật thành công');
                    closeFormUpdateMusic();
                }
            },
            function () {
                alert('Có lỗi');
            }
        );
    };

    //Nhac
    $scope.LoadNhac = function (size, index) {
        return new Promise(function (resolve, reject) {
            $http({
                method: 'POST',
                data: { pageIndex: index, pageSize: size },
                url: current_url + '/api-admin/nhac/search-nhac',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('Token')
                }
            }).then(function (response) {
                $scope.listNhac = response.data.data;
                const pageCount = Math.ceil(response.data.totalItems / $('#pageSizeN').value);
                $scope.listIndex = Array.from({ length: pageCount}, (_, index) => index + 1);
                resolve();
            }, function (error) {
                reject(error);
            });
        });
    }
    
    $scope.AddNhac= function (){
        var item = {};
        item.tenNhac = $('#tenNhac').value;
        item.idTheLoai = $('#theLoai').value;
        item.idNgheSi = $('#ngheSi').value;
        item.audio = $scope.audio;
        item.img = $scope.img;
        item.lyrics = $scope.lyrics;
        item.list_jsonchitietnhactheonghesi = [];
        item.list_jsonchitietnhactheotheloai = [];

        $http({
            method: 'POST',
            url: current_url + '/api-admin/nhac/create-nhac',
            data: item,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else if(data.message == 'Nhạc đã tồn tài') {
                    alert('Nhạc đã tồn tài');
                }
                else{
                    alert('Thêm thành công');
                }
            },
            function() {
                alert('Có lỗi');
            }
        );
    }

    $scope.UpdateNhac = function () {
        var item = {};
        item.idNhac = $('#spans').getAttribute('data-idnhac');
        item.tenNhac = $('#tenNhacUpdate').value;
        item.idTheLoai = $('#theLoaiUpdate').value;
        item.idNgheSi = $('#ngheSiUpdate').value;
        
        if ($scope.audio == null) {
            item.audio = $('#audio').src;
        } else {
            item.audio = $scope.audio;
        }
    
        if ($scope.img == null) {
            item.img = $('#img').src;
        } else {
            item.img = $scope.img;
        }
    
        if ($scope.lyrics == null) {
            item.lyrics = $('#spans').getAttribute('data-content');
        } else {
            item.lyrics = $scope.lyrics;
        }
        
    
        item.list_jsonchitietnhactheonghesi = [];
        item.list_jsonchitietnhactheotheloai = [];
    
        $http({
            method: 'POST',
            url: current_url + '/api-admin/nhac/update-nhac',
            data: item,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function (response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else {
                    alert('Cập nhật thành công');
                    closeFormUpdateMusic();
                    $scope.LoadN($('#pageSizeN').value,$('#pageIndexN').value)
                }
            },
            function () {
                alert('Có lỗi');
            }
        );
    };

    $scope.GetByIDNhac = function(id) {
        return new Promise(function (resolve, reject) {
        $http({
            method: 'GET',
            url: current_url + '/api-admin/Nhac/getbyid-nhac?idNhac=' + id,
            headers: {
                'Content-Type': undefined,
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function(response) {
                $scope.listNhacByID = response.data.data;
                resolve(); 
            },
            function(error) {
                console.error('Error:', error);
                reject(error);
            }
        );
        });
    };

    $scope.LoadN = function(size,index){
        $scope.LoadNhac(size, index).then(function () {
            changeaudio($scope);
            changeimg($scope);
            changetxt($scope);
        });
    };

    $scope.LoadDetail = function(id){
        $scope.GetByIDNhac(id).then(function () {
            handleSelectChangeIndexN();
        });
    };

    $scope.LoadNhacByIDNgheSi = function(id){
        return new Promise(function (resolve, reject) {
            $http({
                method: 'GET',
                url: current_url + '/api-admin/Nhac/getnhacbyidnghesi-nhac?idNgheSi=' + id,
                headers: {
                    'Content-Type': undefined,
                    'Authorization': 'Bearer ' + localStorage.getItem('Token')
                }
            }).then(
                function(response) {
                    $scope.listNhacByIDNgheSi = response.data.data;
                    resolve(); 
                },
                function(error) {
                    console.error('Error:', error);
                    reject(error);
                }
            );
        });
    }

    $scope.DeleteNhacByID = function(id) {
        $http({
            method: 'DELETE',
            url: current_url + '/api-admin/Nhac/delete-nhac?idNhac=' + id,
            headers: {
                'Content-Type': undefined,
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function (response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                } else {
                    alert('Xóa thành công');
                    $scope.listNhac = $scope.listNhac.filter(function(item) {
                        return item.idNhac !== id;
                    });
                    closeForDeleteMusic();
                }
            },
            function (error) {
                console.error('Có lỗi', error);
                alert('Có lỗi');
            }
        );
    };
    
    //Album
    $scope.LoadAlbum = function(size,index){
        return new Promise(function (resolve, reject) {
            $http({
                method: 'POST',
                data: { pageIndex: index, pageSize: size },
                url: current_url + '/api-admin/album/search-album',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('Token')
                }
            }).then(function (response) {
                $scope.listAlbum = response.data.data;
                const pageCount = Math.ceil(response.data.totalItems / $('#pageSizeA').value);
                $scope.listIndexA = Array.from({ length: pageCount}, (_, index) => index + 1);
                resolve();
            }, function (error) {
                reject(error);
            });
        });
    }

    $scope.LoadA = function(size,index){
        $scope.LoadAlbum(size, index).then(function () {
            changeaudio($scope);
            changeimg($scope);
            changetxt($scope);
        });
    };

    $scope.LoadAlbumByID = function(id){
        return new Promise(function (resolve, reject) {
            $http({
                method: 'GET',
                url: current_url + '/api-admin/Album/getbyid-album?idAlbum=' + id,
                headers: {
                    'Content-Type': undefined,
                    'Authorization': 'Bearer ' + localStorage.getItem('Token')
                }
            }).then(
                function(response) {
                    $scope.AlbumByID = response.data.data;
                    $scope.listNhacTrongAlbum = response.data.data[0].list_jsonnhactrongalbum;
                    $scope.listNhacKhongCoTrongAlbum = response.data.data[0].list_jsonnhactheonghesikhongcotrongalbum;
                    resolve(); 
                },
                function(error) {
                    console.error('Error:', error);
                    reject(error);
                }
            );
        });
    }

    $scope.LoadDetailAlbum = function(id){
        $scope.LoadAlbumByID(id).then(function () {
            checkboxmodal();
            changeaudio($scope);
            changeimg($scope);
            changetxt($scope);
        });
    };

    $scope.AddAlbum= function (){
        var item = {};
        item.tenAlbum = $('#tenAlbum').value;
        item.moTa = $('#moTa').value;
        item.idNgheSi = $('#ngheSi').value;
        item.anhDaiDien = $scope.img;

        var checkedValues = [];
        var checkboxes = document.getElementsByClassName('checkbox');
        
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked && checkboxes[i].value != null) {
                var idNhac = Number(checkboxes[i].value);

                if (!isNaN(idNhac) && isFinite(idNhac)){
                    var chiTietAlbum = {
                        idChiTietAlbum: 0,
                        idAlbum: 0,
                        idNhac: idNhac,
                        status: 0
                    };
                    checkedValues.push(chiTietAlbum);
                }
            }
        }
        
        item.list_json_chitietalbum = checkedValues;
        
        
        item.list_jsonnhactrongalbum = [];
        item.list_jsonnhactheonghesikhongcotrongalbum = []

        $http({
            method: 'POST',
            url: current_url + '/api-admin/Album/create-album',
            data: item,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                }
                else{
                    alert('Thêm thành công');
                }
            },
            function() {
                alert('Có lỗi');
            }
        );
    }

    $scope.AddNhacVaoAlbum = function(idNhac,tenNhac,idTheLoai,img){
        $scope.listNhacKhongCoTrongAlbum = $scope.listNhacKhongCoTrongAlbum.filter(function(item) {
            return item.idNhac !== idNhac;
        });
        var chiTietAlbumCapNhat = {
            idChiTietAlbum: 0,
            idAlbum: 0,
            idNhac: idNhac,
            status: 1
        };
        var chiTietAlbum = {
            idNhac: idNhac,
            tenNhac: tenNhac,
            img: img,
            idTheLoai: idTheLoai,
        }
        $scope.listNhacCapNhat.push(chiTietAlbumCapNhat);
        $scope.listNhacTrongAlbum.push(chiTietAlbum);
    }

    $scope.DeleteNhacKhoiAlbum = function(idNhac, idAlbum, tenNhac,img,idTheLoai){
        $scope.listNhacTrongAlbum = $scope.listNhacTrongAlbum.filter(function(item) {
            return item.idNhac !== idNhac;
        });
        var chiTietAlbumCapNhat = {
            idChiTietAlbum: 0,
            idAlbum: idAlbum,
            idNhac: idNhac,
            status: 2
        }
        var chiTietAlbum = {
            idNhac: idNhac,
            tenNhac: tenNhac,
            img: img,
            idTheLoai: idTheLoai,
        }
        $scope.listNhacCapNhat.push(chiTietAlbumCapNhat);
        $scope.listNhacKhongCoTrongAlbum.push(chiTietAlbum);
    }

    $scope.UpdateAlbum= function (){
        var item = {};
        item.idAlbum = $('#moTaUpdate').getAttribute('data-idAlbum');
        item.tenAlbum = $('#tenAlbumUpdate').value;
        item.moTa = $('#moTaUpdate').value;
        item.idNgheSi = $('#idNgheSi').getAttribute('data-idNgheSi');
        
        if ($scope.img == null) {
            item.anhDaiDien = $('#img').src;
        } else {
            item.anhDaiDien = $scope.img;
        } 
        item.list_json_chitietalbum = $scope.listNhacCapNhat;
        
        item.list_jsonnhactrongalbum = [];
        item.list_jsonnhactheonghesikhongcotrongalbum = []

        $http({
            method: 'POST',
            url: current_url + '/api-admin/Album/update-album',
            data: item,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('Token')
            }
        }).then(
            function(response) {
                var data = response.data;
                if (data != null && data.message != null && data.message != 'undefined') {
                    alert(data.message);
                }
                else{
                    alert('Cập nhật thành công');
                }
            },
            function() {
                alert('Có lỗi');
                console.log(item);
            }
        );
    }

    //---------------------Load-----------------
    $scope.LoadTheLoai();
    $scope.LoadNgheSi();
    $scope.LoadN(10,1);

    $scope.LoadNS(10,1);

    $scope.LoadTL(10,1);

    $scope.LoadA(10,1);

});


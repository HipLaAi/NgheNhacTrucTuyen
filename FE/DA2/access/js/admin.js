const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);

function showtime(){
    var allrank = ['Chủ Nhật','Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7'];
    var datetime = new Date();
    var ranks = allrank[datetime.getDay()];
    var days = String(datetime.getDate()).padStart(2, '0');
    var months = String((datetime.getMonth() + 1)).padStart(2, '0');
    var years = datetime.getFullYear();
    var hrs = String(datetime.getHours()).padStart(2, '0');
    var mins = String(datetime.getMinutes()).padStart(2, '0');
    var sec = String(datetime.getSeconds()).padStart(2, '0');
    $('#h3').innerHTML =   
    `
            ${ranks}, ${days}/${months}/${years} - ${hrs} giờ ${mins} phút ${sec} giây
    `;
}

function logout(){
    window.location.href = "dangnhap.html";
    localStorage.clear();
}

function checkbox() {
    $('th .checkbox').addEventListener('change', function () {
      $$('td .checkbox').forEach(function (checkbox) {
        checkbox.checked = $('th .checkbox').checked;
      });
    });
}


function openFormDeleteDataMusic(){
  $('#modal_deletedata').style.opacity = 1;
  $('#modal_deletedata').style.pointerEvents  = 'auto';
  $('#modal_deletedata .container').style.transform  = 'translateY(0)';
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
    $scope.ResetData();
  });
}

function closeFormDeleteDataMusic(){
  $('#modal_deletedata').style.opacity = 0;
  $('#modal_deletedata').style.pointerEvents  = 'none';
  $('#modal_deletedata .container').style.transform  = 'translateY(-100%)';
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
    $scope.ResetData();
  });
}

function openFormAddMusic(){
  $('#modal_create').style.opacity = 1;
  $('#modal_create').style.pointerEvents  = 'auto';
  $('#modal_create .container').style.transform  = 'translateY(0)';
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
    $scope.ResetData();
  });
}

function closeFormAddMusic(){
  $('#modal_create').style.opacity = 0;
  $('#modal_create').style.pointerEvents  = 'none';
  $('#modal_create .container').style.transform  = 'translateY(-100%)';
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
    $scope.ResetData();
  });
}

function openFormUpdateMusic(){
  $('#modal_update').style.opacity = 1;
  $('#modal_update').style.pointerEvents  = 'auto';
  $('#modal_update .container').style.transform  = 'translateY(0)';
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
    $scope.ResetData();
  });
}

function closeFormUpdateMusic(){
  $('#modal_update').style.opacity = 0;
  $('#modal_update').style.pointerEvents  = 'none';
  $('#modal_update .container').style.transform  = 'translateY(-100%)';
}

function openFormDeleteMusic(){
  $('#modal_delete').style.opacity = 1;
  $('#modal_delete').style.pointerEvents  = 'auto';
  $('#modal_delete .container').style.transform  = 'translateY(0)';
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
    $scope.ResetData();
  });
}

function closeForDeleteMusic(){
  $('#modal_delete').style.opacity = 0;
  $('#modal_delete').style.pointerEvents  = 'none';
  $('#modal_delete .container').style.transform  = 'translateY(-100%)';
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
    $scope.ResetData();
  });
}


function openFormModalAddMusic(){
  $('#modal_addmussic').style.opacity = 1;
  $('#modal_addmussic').style.pointerEvents  = 'auto';
  $('#modal_addmussic .container').style.transform  = 'translateY(0)';
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
    $scope.ResetData();
  });
}

function closeFormModalAddMusic(){
  $('#modal_addmussic').style.opacity = 0;
  $('#modal_addmussic').style.pointerEvents  = 'none';
  $('#modal_addmussic .container').style.transform  = 'translateY(-100%)';
}


function handleSelectChangeIndexN(){
  var selectedSizeValue = $('#pageSizeN').value;
  var selectedIndexValue = $('#pageIndexN').value;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadN(selectedSizeValue,selectedIndexValue);
  });
}

function handleSelectChangeSizeN(){
  var selectedSizeValue = $('#pageSizeN').value;
  var selectedIndexValue = 1;
  $('#pageIndexN').value = 1;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadN(selectedSizeValue,selectedIndexValue);
  });
}

function handleSelectChangeIndexTL(){
  var selectedSizeValue = $('#pageSizeTL').value;
  var selectedIndexValue = $('#pageIndexTL').value;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadTL(selectedSizeValue,selectedIndexValue);
  });
}

function handleSelectChangeSizeTL(){
  var selectedSizeValue = $('#pageSizeTL').value;
  var selectedIndexValue = 1;
  $('#pageIndexTL').value = 1;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadTL(selectedSizeValue,selectedIndexValue);
  });
}

function handleSelectChangeIndexNS(){
  var selectedSizeValue = $('#pageSizeNS').value;
  var selectedIndexValue = $('#pageIndexNS').value;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadNS(selectedSizeValue,selectedIndexValue);
  });
}

function handleSelectChangeSizeNS(){
  var selectedSizeValue = $('#pageSizeNS').value;
  var selectedIndexValue = 1;
  $('#pageIndexNS').value = 1;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadNS(selectedSizeValue,selectedIndexValue);
  });
}

function handleSelectChangeIndexA(){
  var selectedSizeValue = $('#pageSizeA').value;
  var selectedIndexValue = $('#pageIndexA').value;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadA(selectedSizeValue,selectedIndexValue);
  });
}

function handleSelectChangeSizeA(){
  var selectedSizeValue = $('#pageSizeA').value;
  var selectedIndexValue = 1;
  $('#pageIndexA').value = 1;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadA(selectedSizeValue,selectedIndexValue);
  });
}

var currentIndex = 0;
function sortTable(columnIndex) {
  currentIndex += 1;
  var table, rows, switching, i, x, y, shouldSwitch;
  table = $("#mytable");
  switching = true;
  console.log(currentIndex);

  if(currentIndex % 2 == 0){
    if (typeof handleSelectChangeIndexA === 'function') {
      handleSelectChangeIndexA();
    }
    if (typeof handleSelectChangeIndexN === 'function') {
      handleSelectChangeIndexN();
    }
    if (typeof handleSelectChangeIndexTL === 'function') {
      handleSelectChangeIndexTL();
    }
    if (typeof handleSelectChangeIndexNS === 'function') {
      handleSelectChangeIndexNS();
    }   
  }
  else{
    while (switching) {
        switching = false;
        load = true;
        rows = table.rows;

        for (i = 1; i < rows.length - 1; i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("td")[columnIndex];
            y = rows[i + 1].getElementsByTagName("td")[columnIndex];

            if (isNaN(x.innerHTML)) {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            } else {
                if (Number(x.innerHTML) > Number(y.innerHTML)) {
                    shouldSwitch = true;
                    break;
                }
            }
        }

        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
  }
}

function handleSelectChangeTenNgheSi(){
  var idNgheSi = $('#ngheSi').value;
  angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
      $scope.LoadNhacByIDNgheSi(idNgheSi).then(function(){
        checkboxmodal();
      })
  })
}

function checkboxmodal(){
  $('#table_create th .checkbox').addEventListener('change', function () {
    $$('#table_create td .checkbox').forEach(function (checkbox) {
      checkbox.checked = $('#table_create th .checkbox').checked;
    });
  });
}

//---------------------------------------
function changeimg($scope){
  $$('td .file_img').forEach(function(input, index){
    input.addEventListener('change', function(event){
      const imgElement = $$('td .img')[index];
      const fileInput = event.target;

      if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();
        reader.onload = function(e) {
          imgElement.src = e.target.result;  
        };
        reader.readAsDataURL(fileInput.files[0]);

        // angular.element(document.querySelector('[ng-app]')).scope().$apply(function($scope) {
        //   $scope.UploadFileIMG(input.files[0]);
        // });

      } 
      else {
        imgElement.src = '';
      }
      $scope.UploadFileIMG(fileInput.files[0]);
    })
  })
}

function changeaudio($scope){
  $$('td .file_audio').forEach(function(input, index){
    input.addEventListener('change', function(event){
      const audioElement = $$('td .audio')[index];
      const fileInput = event.target;

      if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();
        reader.onload = function(e) {
          audioElement.src = e.target.result;
        };
        reader.readAsDataURL(fileInput.files[0]);
      } 
      else {
        audioElement.src = '';
      }
      $scope.UploadFileAUDIO(fileInput.files[0]);
    })
  })
}

function changetxt($scope) {
  $$('td .file_txt').forEach(function(input) {
    input.addEventListener('change', function(event) {
      const txtElement = input.closest('td').querySelector('span');
      const fileInput = event.target;
      
      if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();
        reader.onload = function(e) {
          const content = e.target.result;
          txtElement.innerHTML = `${fileInput.files[0].name}`;
          // $('#span').innerHTML = `${content}`;
        };
        reader.readAsText(fileInput.files[0]);
      } else {
        txtElement.innerHTML = ``;
        // $('#span').innerHTML = ``;
      }
      $scope.UploadFileTEXT(fileInput.files[0]);
    })
  })
}

document.addEventListener('DOMContentLoaded', function() {
    setInterval(showtime, 1000);
    checkbox();
});

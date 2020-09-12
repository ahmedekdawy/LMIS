function news(data) {
    this.NewsID = ko.observable(data.NewsID);
    this.NewsTitle = ko.observable(data.NewsTitle);
    this.NewsDescription = ko.observable(data.NewsDescription);
    this.NewsDate = ko.observable(moment.utc(data.NewsDate).local().format("YYYY/MM/DD"));
    this.NewsExpiryDate = ko.observable(moment.utc(data.NewsExpiryDate).local().format("YYYY/MM/DD"));
    this.NewsBannerPath = ko.observable(data.NewsBannerPath);
    this.NewsIconPath = ko.observable(data.NewsIconPath);
    this.NewsVideoPath = ko.observable(data.NewsVideoPath);
    this.PostDate = ko.observable(moment.utc(data.PostDate).local().format("YYYY/MM/DD"));
    this.newshref = ko.observable('NewsItem.aspx?newsId=' + data.NewsID);
    this.IsInformal = ko.observable(data.IsInformal);
    this.IsAchievement = ko.observable(data.IsAchievement);
    this.NewsLangauage = ko.observable(data.NewsLangauage);


}

function ViewModel() {

    var self = this;
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var validExtensionsVideo = ".mp4,.flv,.avi,.wmv,.3gp,.mpg,.mpeg,.asf,.AVI";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var maxFileSizeVideo = 100 * 1024 * 1024; // 1 MBytes
    self.AcceptedFilesBanners = validExtensions;
    self.AcceptedFilesIcon = validExtensions;
    self.AcceptedFilesVideos = validExtensionsVideo;
    self.NewsID = ko.observable();
    self.NewsTitle = ko.observable();
    self.NewsDescription = ko.observable();
    self.NewsDate = ko.observable();
    self.NewsExpiryDate = ko.observable();
    self.NewsBannerPath = ko.observable();
    self.NewsIconPath = ko.observable();
    self.NewsVideoPath = ko.observable();
    self.PostDate = ko.observable();
    self.newshref = ko.observable();
    self.IsInformal = ko.observable();
    self.newsList = ko.observableArray();
    self.FileBanner = ko.observable();
    self.FileIcon = ko.observable();
    self.FileVideo = ko.observable();
    self.NewsLangauage = ko.observable();
    self.IsAchievement = ko.observable();
    
    //Initialize VM
    self.UpdateNews = function (news, indx) {
      //  FCKeditorAPI.ready(function() {
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKNewsTitle').SetHTML(news.NewsTitle());
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKNewsDescription').SetHTML(news.NewsDescription()); 
      //  });
     
        $(".NewsDate").val(news.NewsDate());
        $(".NewsExpiryDate").val(news.NewsExpiryDate());
        
        if (news.IsInformal()) {
            
            $(".IsInformal").prop("checked", true);
         
            $(".IsAchievement").prop("checked",false);
           
           
        } else {
           
            $(".IsAchievement").prop("checked", true);
           
            $(".IsInformal").prop("checked",false);
         
         
            
        }
        $(".Banners").prop("href", lmis.x.downloadURL + 'News/Banners/' + news.NewsBannerPath());
        $(".Icons").prop("href", lmis.x.downloadURL + 'News/Icons/' + news.NewsIconPath());
        $(".Videos").prop("href", lmis.x.downloadURL + 'News/Videos/' + news.NewsVideoPath());
        $("#hdfNewsID").val(news.NewsID());
        $("#hdfRowId").val(indx);
        
        $(".Banners").prop("style", false);
        $(".Icons").prop("style", false);
        if (news.NewsVideoPath() != undefined) {
            $(".Videos").prop("style", false);
        } 
         showpopup('Edit');
    }
    self.DeleteNews = function (news, indx) {

        if (confirm($("#X_ConfirmContinue").html())) {
            
            var dto = { newsId: news.NewsID(), deleteReason: "" };
            return lmis.ajax("../BackEnd/News.aspx/Delete", dto, 0, "",
                function(data) {
                    console.log(data.d);
                    if (data.d > 0) {
                        self.newsList.remove(news);
                        $('#grdNews').dataTable().fnDeleteRow(indx, null, true);
                        lmis.notification.success();
                    }

                });

        } else {
            return false;
        }


    }
    self.ValidateBanner = function (item, e) {

        var selectedFile = e.target.files[0];

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensions)) {
                    self.FileBanner(selectedFile);
                    $("#txtNewsBanner").val(selectedFile.name);
                } else self.ClearBannerFile();
            } else self.ClearBannerFile();
        } else self.ClearBannerFile();

    }
    self.ValidateIcon = function (item, e) {

        var selectedFile = e.target.files[0];

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensions)) {
                    self.FileIcon(selectedFile);
                    $("#txtNewsIcon").val(selectedFile.name);
                } else self.ClearIconFile();
            } else self.ClearIconFile();
        } else self.ClearIconFile();

    }
    self.ValidateVideo = function (item, e) {

        var selectedFile = e.target.files[0];

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSizeVideo) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensionsVideo)) {
                    self.FileVideo(selectedFile);
                    $("#txtNewsVideo").val(selectedFile.name);
                } else self.ClearVideoFile();
            } else self.ClearVideoFile();
        } else self.ClearVideoFile();

    }
    self.ClearBannerFile = function () {

        self.FileBanner(null);
        self.NewsBannerPath(null);
        $("#txtNewsBanner").val("");
        lmis.fileInput.clear($("#hdnNewsBannerBrowser"));

    }
    self.ClearIconFile = function () {

        self.FileIcon(null);
        self.NewsIconPath(null);
        $("#txtIconBanner").val("");
        lmis.fileInput.clear($("#hdnNewsIconBrowser"));

    }
    self.ClearVideoFile = function () {

        self.FileVideo(null);
        self.NewsVideoPath(null);
        $("#txtVideoBanner").val("");
        lmis.fileInput.clear($("#hdnNewsVideoBrowser"));

    }
    self.UploadBanner = function ( ) {

        if (!self.FileBanner()) {
            self.ClearBannerFile();
            return null;
        };
      
        return lmis.ajaxUpload("/api/upload/imageWithPath?path=" + '/News/Banners', self.FileBanner(), 0, "show",
            function (data) {
                self.NewsBannerPath(data);
              
                if ($("#txtNewsVideo").val() == '') {
                    console.log('No video');
                    self.onSuccess();
                } else {
                    console.log('has video');
                      self.UploadVideo();
                }
                
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearBannerFile();   //Validation Error
               
            });

    }
    self.UploadIcon = function () {
        if ($(".txtNewsIcon").val() != '') {
     
        if (!self.FileIcon()) {
            self.ClearIconFile();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/imageWithPath?path=" + '/News/Icons', self.FileIcon(), 0, "show",
            function (data) {
                self.NewsIconPath(data);
                self.UploadBanner();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearIconFile();   //Validation Error
             
            });
        }
        else if ($(".txtNewsBanner").val() != '' && $(".txtNewsIcon").val() == '') {
            self.UploadBanner();
        }
        else if ($("#txtNewsVideo").val() != '' && $(".txtNewsBanner").val() == '' && $(".txtNewsIcon").val() == '') {
            self.UploadVideo();
        } else {
            self.onSuccess();
        }
    }

    self.UploadVideo = function () {

        if (!self.FileVideo()) {
            self.ClearVideoFile();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/Video?path=" + '/News/Videos', self.FileVideo(), 0, "show",
            function (data) {
                self.NewsVideoPath(data);
                self.onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearVideoFile();   //Validation Error
              
            });

    }
    self.INSERT = function () {
        if (new Date($(".NewsDate").val()) > new Date($(".NewsExpiryDate").val())) {
            return noty({
                type: "error",
                text: "Error: News Date must be less than Expire Date",
                layout: "center", closeWith: ["click", "backdrop"],
                modal: true, killer: true
            });
        }
        var FCKNewsTitle = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKNewsTitle');
        var FCKNewsDescription = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKNewsDescription');
        
        if (FCKNewsTitle == '') {FCKNewsTitle.focus(); return;}
        if (FCKNewsDescription == '') { FCKNewsDescription.focus(); return; }
        if ($(".NewsDate").val().toString() == '') {
            $(".NewsDate").focus(); return;
        }
        if ($(".NewsExpiryDate").val().toString() == '') {
            $(".NewsExpiryDate").focus(); return;
        }
        if ($("#hdfNewsID").val() == '0') {
            if (!self.FileBanner()) {
                self.ClearBannerFile();
                $(".txtNewsBanner").focus();
                return;

            };
            if (!self.FileIcon()) {
                self.ClearIconFile();
                $(".txtNewsIcon").focus();
                return;
            };

        }

        self.UploadIcon();
        

    }
    
    self.onSuccess = function() {
        $("#txtNewsBanner").val(self.NewsBannerPath());
        $("#txtNewsIcon").val(self.NewsIconPath());
        $("#txtNewsVideo").val(self.NewsVideoPath());
        var FCKNewsTitle = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKNewsTitle');
        var FCKNewsDescription = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKNewsDescription');
        var NewsVm = {
            NewsID: $("#hdfNewsID").val(),
            NewsTitle: FCKNewsTitle.GetXHTML(true),
            NewsDescription: FCKNewsDescription.GetXHTML(true),
            NewsDate: $(".NewsDate").val().toString(),
            NewsExpiryDate: $(".NewsExpiryDate").val().toString(),
            NewsBannerPath: self.NewsBannerPath(),
            NewsIconPath: self.NewsIconPath(),
            NewsVideoPath: self.NewsVideoPath(),
            NewsLangauage: $(".ddlLanguage").val().toString(),
            IsInformal: $(".IsInformal").prop("checked") ? true : false,
            IsAchievement: $(".IsAchievement").prop("checked") ? true : false
        };
        var dto = { News: NewsVm };
        lmis.ajax("../BackEnd/News.aspx/Insert", dto, 0, "show,close", function (data) {
            lmis.notification.success();
            console.log(data.d.Data);
            if ($("#hdfNewsID").val() != '0') {
               
                var index = parseInt($("#hdfRowId").val());

                self.newsList()[index].NewsTitle(NewsVm.NewsTitle);
                self.newsList()[index].NewsDescription(NewsVm.NewsDescription);
                self.newsList()[index].NewsDate(NewsVm.NewsDate);
                self.newsList()[index].NewsExpiryDate(NewsVm.NewsExpiryDate);
                self.newsList()[index].NewsLangauage(NewsVm.NewsLangauage);
                self.newsList()[index].IsInformal(NewsVm.IsInformal);
                self.newsList()[index].IsAchievement(NewsVm.IsAchievement);
                self.newsList()[index].NewsBannerPath(NewsVm.NewsBannerPath);
                self.newsList()[index].NewsIconPath(NewsVm.NewsIconPath);
                self.newsList()[index].NewsVideoPath(NewsVm.NewsVideoPath);
              
            } else {
                NewsVm.NewsID = data.d.Data;
                self.newsList.push(new news(NewsVm));
            }
          
            clearControls();
        });
    }
    self.onError = function () {}
    self.clearControlls= function() {

        $(".pop").hide();
    }
    self.GetNews = function () {

        return lmis.ajax("../BackEnd/News.aspx/GetNews", null, 0, "",
            function (data) {
                var ds = $.map(data.d, function (item) { return new news(item) });
                self.newsList(ds);
             //   $('#grdNews').dataTable().clear();
                $('#grdNews').dataTable();

            });

    }
    self.GetNews();
}
$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
   

});


function showpopupInsert() {
    showpopup('Add');
    $("#hdfNewsID").val(0);
    clearControls();
}

function showpopup(_title) {
  
    var d = $(".pop");
    d.dialog({
        title: _title,
        position: 'fixed',
        top:20,

        width: 1000
    });
    return false;
}
function clearControls() {
    $("#txtNewsBanner").val('');
    $("#txtNewsIcon").val('');
    $("#txtNewsVideo").val('');
    $(".Banners").hide();
    $(".Icons").hide();
    $(".Videos").hide();
    $(".NewsDate").val('');
    $(".NewsExpiryDate").val('');
    FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKNewsTitle').SetHTML('');
    FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKNewsDescription').SetHTML('');
}
function news(data) {
    this.NewsID = ko.observable(data.NewsID);
    this.NewsTitle = ko.observable(data.NewsTitle);
    this.NewsDescription = ko.observable(data.NewsDescription);
    this.NewsDate = ko.observable(lmis.format.dateToString(data.NewsDate));
    this.NewsExpiryDate = ko.observable(lmis.format.dateToString(data.NewsExpiryDate));
    this.NewsBannerPath = ko.observable('../Uploads/News/Banners/' + data.NewsBannerPath);
    this.NewsIconPath = ko.observable('../Uploads/News/Icons/' + data.NewsIconPath);
    this.NewsVideoPath = ko.observable('../Uploads/News/Videos/' + data.NewsVideoPath + '?autoplay=0');
    this.PostDate = ko.observable(lmis.format.dateToString(data.PostDate));
    this.newshref = ko.observable('../NewsDetails?newsId=' + data.NewsID);


}

function ViewModel() {

    var self = this;
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
    self.newsList = ko.observableArray();

    //Initialize VM
    self.GetNews = function () {

        return lmis.ajax("../FrontEnd/PastAchievements.aspx/GetNews", null, 0, "",
            function (data) {
                var ds = $.map(data.d, function (item) { return new news(item) });
                self.newsList(ds);


            });

    }
    self.GetNews();
}
$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
});
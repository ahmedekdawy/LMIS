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
    this.newshref = ko.observable('/NewsDetails?newsId=' + data.NewsID);


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
    self.PartnersList = ko.observableArray();
    //Initialize VM
    self.GetNews = function () {

        return lmis.ajax( "../FrontEnd/home.aspx/GetNews", null, 0, "",
            function (data) {
                var ds = $.map(data.d, function (item) { return new news(item) });
                self.newsList(ds);


            });

    }
    self.GetPartners = function ()
    {
        return lmis.ajax("../FrontEnd/home.aspx/GetPartners", null, 0, "",
         function (data) {
             self.PartnersList(data.d);

         });
    }
    self.GetIndicator = function () {
        return lmis.ajax("../FrontEnd/home.aspx/GetIndicator", null, 0, "",
         function (data) {

             $(".AvailableVacancies").html(data.d.AvailableVacancies);
             $(".UnEployeedJobSeekers").html(data.d.UnEployeedJobSeekers);
             $(".JobSeekers").html(data.d.JobSeekers);
             $(".JobsApplied").html(data.d.JobsApplied);
             $(".JobsOffers").html(data.d.JobsOffers);

         });
    }
    self.GetIndicator();
    self.GetNews();
    self.GetPartners();
}
$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
});